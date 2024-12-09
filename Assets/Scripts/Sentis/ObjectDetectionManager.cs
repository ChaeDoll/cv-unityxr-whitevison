using Anaglyph.DisplayCapture;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Sentis;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Sentis.Model;

public class ObjectDetectionManager : MonoBehaviour
{
    public RawImage displayPannel;

    [Header("Init Setting")]
    [SerializeField] private ModelAsset modelAsset;
    [SerializeField] private TextAsset labelAssets;
    [SerializeField] private Sprite borderSprite; 
    [SerializeField] private Texture2D borderTexture;
    [SerializeField] private Font font;
    [SerializeField] private float updateInterval = 1f;

    private float lastUpdateTime = 0;
    private DisplayCaptureManager displayCaptureManager;
    private Model model;
    private IWorker engine;
    private string[] labels;
    private RenderTexture targetRT;
    private const int imageWidth = 640;
    private const int imageHeight = 640;

    private bool isProcessing = false; // 작업상태 확인
    List<GameObject> boxPool = new List<GameObject>();

    [SerializeField] private GameObject DetectDisplay;
    [SerializeField] private GameObject SegmentDisplay;

    public struct BoundingBox
    {
        public float centerX;
        public float centerY;
        public float width;
        public float height;
        public string label;
        public float confidence;
    }

    private void Awake()
    {
        labels = labelAssets.text.Split('\n');
        targetRT = new RenderTexture(imageWidth, imageHeight, 0);
        displayCaptureManager = DisplayCaptureManager.Instance;
    }

    private void OnEnable()
    {
        model = ModelLoader.Load(modelAsset);
        engine = WorkerFactory.CreateWorker(BackendType.GPUCompute, model);
        displayCaptureManager.onNewFrame.AddListener(ObjectDetection);
    }

    private void OnDisable()
    {
        ClearAnnotations();

        model = null;
        engine?.Dispose();
        displayCaptureManager.onNewFrame?.RemoveListener(ObjectDetection);
    }

    public void ObjectDetection()
    {
        if (Time.time - lastUpdateTime < updateInterval || isProcessing) return;
        isProcessing = true;
        lastUpdateTime = Time.time;

        ClearAnnotations();

        DetectDisplay.SetActive(false);
        SegmentDisplay.SetActive(false);

        Graphics.Blit(displayCaptureManager.ScreenCaptureTexture, targetRT);
        using var inputTensor = TextureConverter.ToTensor(targetRT, imageWidth, imageHeight, 3);

        DetectDisplay.SetActive(true);
        SegmentDisplay.SetActive(true);

        engine.Execute(inputTensor);
        var outputTensor = engine.PeekOutput() as TensorFloat;
        outputTensor.CompleteOperationsAndDownload();

        float displayWidth = displayPannel.rectTransform.rect.width;
        float displayHeight = displayPannel.rectTransform.rect.height;
        float scaleX = displayWidth / imageWidth;
        float scaleY = displayHeight / imageHeight;
        int foundBoxes = outputTensor.shape[0];

        for (int n = 0; n < foundBoxes; n++)
        {
            if ((int)outputTensor[n, 5] < 6)
            {
                var box = new BoundingBox
                {
                    centerX = ((outputTensor[n, 1] + outputTensor[n, 3]) * scaleX - displayWidth) / 2,
                    centerY = ((outputTensor[n, 2] + outputTensor[n, 4]) * scaleY - displayHeight) / 2,
                    width = (outputTensor[n, 3] - outputTensor[n, 1]) * scaleX,
                    height = (outputTensor[n, 4] - outputTensor[n, 2]) * scaleY,
                    label = labels[(int)outputTensor[n, 5]],
                    confidence = Mathf.FloorToInt(outputTensor[n, 6] * 100 + 0.5f)
                };
                DrawBox(box, n);
            }
        }
        outputTensor.Dispose();
        isProcessing = false;
    }

    public void DrawBox(BoundingBox box, int id)
    {
        //Create the bounding box graphic or get from pool
        GameObject panel;
        if (id < boxPool.Count)
        {
            panel = boxPool[id];
            panel.SetActive(true);
        }
        else
        {
            panel = CreateNewBox(new Color(1f, 1f, 1f, 0.5f));
        }
        //Set box position
        panel.transform.localPosition = new Vector3(box.centerX, -box.centerY);

        //Set box size
        RectTransform rt = panel.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(box.width, box.height);

        //Set label text
        var label = panel.GetComponentInChildren<Text>();
        label.text = box.label + " (" + box.confidence + "%)";
    }

    public GameObject CreateNewBox(Color color)
    {
        //Create the box and set image

        var panel = new GameObject("ObjectBox");
        panel.AddComponent<CanvasRenderer>();
        Image img = panel.AddComponent<Image>();
        img.color = color;
        img.sprite = borderSprite;
        //img.type = Image.Type.Sliced;
        img.type = Image.Type.Filled;
        img.preserveAspect = true;
        panel.transform.SetParent(displayPannel.transform, false);

        //Create the label

        var text = new GameObject("ObjectLabel");
        text.AddComponent<CanvasRenderer>();
        text.transform.SetParent(panel.transform, false);
        Text txt = text.AddComponent<Text>();
        txt.font = font;
        txt.color = color;
        txt.fontSize = 40;
        txt.horizontalOverflow = HorizontalWrapMode.Overflow;

        RectTransform rt2 = text.GetComponent<RectTransform>();
        rt2.offsetMin = new Vector2(20, rt2.offsetMin.y);
        rt2.offsetMax = new Vector2(0, rt2.offsetMax.y);
        rt2.offsetMin = new Vector2(rt2.offsetMin.x, 0);
        rt2.offsetMax = new Vector2(rt2.offsetMax.x, 30);
        rt2.anchorMin = new Vector2(0, 0);
        rt2.anchorMax = new Vector2(1, 1);

        boxPool.Add(panel);
        return panel;
    }

    public void ClearAnnotations() 
    {
        foreach (var box in boxPool)
        {
            box.SetActive(false);
        }
    }
}
