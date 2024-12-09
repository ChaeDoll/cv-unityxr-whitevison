using Anaglyph.DisplayCapture;
using System.Linq;
using TMPro;
using Unity.Sentis;
using UnityEngine;
using UnityEngine.UI;

public class SegmentationManager : MonoBehaviour
{
    // public TMP_Text debugText;
    public RawImage displayPannel;
    public RawImage originImage;

    [Header("Initial Setting")]
    [SerializeField] private ModelAsset modelAsset;
    [SerializeField] private float updateInterval;
    [SerializeField] private float segmentThreshold;

    private float lastUpdateTime = 0f;
    private DisplayCaptureManager displayCaptureManager;
    private Model model;
    private IWorker engine;
    private RenderTexture targetRT;

    [SerializeField] private GameObject DetectDisplay;
    [SerializeField] private GameObject SegmentDisplay;

    private Color[] classColors = new Color[]
    {
        new Color(0f, 0f, 0f, 0f),       // Class 0 - 투명색
        new Color(255f / 255f, 255f / 255f, 0f / 255f, 0.4f),   // Class 1 - 노랑 (도보)
        new Color(255f / 255f, 255f / 255f, 0f / 255f, 0.4f),   // Class 2 - 노랑 (점자블록)
        new Color(255f / 255f, 0f / 255f, 0f / 255f, 0.4f),      // Class 3 - 빨간 (도로) 
        new Color(255f / 255f, 0f / 255f, 0f / 255f, 0.4f),      // Class 4 - 빨간 (골목길)
        new Color(135f / 255f, 206f / 255f, 235f / 255f, 0.4f), // Class 5 - 하늘색 (횡단보도)
        new Color(255f / 255f, 0f / 255f, 0f / 255f, 0.4f),   // Class 6 - 빨간색 (자전거도로)
        new Color(255f / 255f, 0f / 255f, 0f / 255f, 0.4f)      // Class 7 - 빨간색 (위험구역)
    };

    private const int imageWidth = 256;
    private const int imageHeight = 256;

    void Awake()
    {
        targetRT = new RenderTexture(imageWidth, imageHeight, 0);
        displayCaptureManager = DisplayCaptureManager.Instance;
    }

    private void OnEnable()
    {
        displayPannel.enabled = true;

        model = ModelLoader.Load(modelAsset);
        engine = WorkerFactory.CreateWorker(BackendType.GPUCompute, model);
        displayCaptureManager.onNewFrame.AddListener(ImageSegmentation);
    }

    private void OnDisable()
    {
        displayPannel.enabled = false;

        model = null;
        engine?.Dispose();
        displayCaptureManager.onNewFrame?.RemoveListener(ImageSegmentation);
    }


    public void ImageSegmentation()
    {
        if (Time.time - lastUpdateTime < updateInterval) return;
        lastUpdateTime = Time.time;

        //originImage.texture = displayCaptureManager.ScreenCaptureTexture;

        DetectDisplay.SetActive(false);
        SegmentDisplay.SetActive(false);

        Graphics.Blit(displayCaptureManager.ScreenCaptureTexture, targetRT);
        using TensorFloat inputTensor = TextureConverter.ToTensor(targetRT, imageWidth, imageHeight, 3);
        inputTensor.CompleteOperationsAndDownload();

        DetectDisplay.SetActive(true);
        SegmentDisplay.SetActive(true);

        NormalizeTensor(inputTensor, imageHeight, imageWidth);

        engine.Execute(inputTensor);
        var outputTensor = engine.PeekOutput() as TensorFloat;
        outputTensor.CompleteOperationsAndDownload();

        ApplySoftmax(outputTensor, 8, imageHeight, imageWidth);

        // 1. 텐서에서 클래스 맵 생성
        int[,] classMap = GenerateClassMap(outputTensor, imageWidth, imageHeight);
        // 2. 클래스 맵에서 Texture2D 생성
        Texture2D segmentationTexture = GenerateSegmentationTexture(classMap, imageWidth, imageHeight);
        // 3. RawImage에 텍스처 출력
        displayPannel.texture = segmentationTexture;

        outputTensor.Dispose();
    }

    private void NormalizeTensor(TensorFloat inputTensor, int height, int width)
    {
        // ImageNet 평균 및 표준편차 값 (RGB 순서)
        float[] mean = { 0.485f, 0.456f, 0.406f };
        float[] std = { 0.229f, 0.224f, 0.225f };

        for (int c = 0; c < mean.Length; c++) // RGB 채널
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float value = inputTensor[0, c, y, x];
                    value = (value - mean[c]) / std[c]; // 정규화
                    inputTensor[0, c, y, x] = value; // 정규화 값 다시 저장
                }
            }
        }
    }

    private int[,] GenerateClassMap(TensorFloat outputs, int width, int height)
    {
        float[] outputData = outputs.ToReadOnlyArray(); // Sentis 텐서를 float 배열로 변환
        int numClasses = outputs.shape[1]; // 클래스 수
        int[,] classMap = new int[height, width];
        // Loop through pixels
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int pixelIndex = y * width + x;
                float maxScore = float.MinValue;
                int maxClass = 0;

                // 클래스별 점수 비교 (argmax)
                for (int c = 0; c < numClasses; c++)
                {
                    float score = outputData[c * width * height + pixelIndex];
                    if (score > maxScore)
                    {
                        maxScore = score;
                        maxClass = c;
                    }
                }
                if (maxScore > segmentThreshold) 
                { classMap[y, x] = maxClass; }
                else
                { classMap[y, x] = 0; }
            }
        }
        return classMap;
    }

    // 클래스 맵에서 Texture2D 생성
    private Texture2D GenerateSegmentationTexture(int[,] classMap, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        Color[] pixels = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = y * width + x;
                int classId = classMap[y, x];
                pixels[index] = classColors[classId];
            }
        }

        texture.SetPixels(pixels);
        texture.Apply();
        return texture;
    }

    private void ApplySoftmax(TensorFloat inputTensor, int numClasses, int height, int width)
    {
        float[] logits = new float[numClasses];
        for (int y=0; y < height; y++)
        {
            for (int x=0; x < width; x++)
            {
                // 1. 각 픽셀의 Logits 값 가져오기
                for (int c=0; c < numClasses; c++)
                {
                    int index = (c * height + y) * width + x;
                    logits[c] = inputTensor[index];
                }

                // 2. Softmax 계산
                float[] probabilities = Softmax(logits);

                for (int c=0; c < numClasses ; c++)
                {
                    int index = (c * height + y) * width + x;
                    inputTensor[index] = probabilities[c];
                }
            }
        }
    }

    private float[] Softmax(float[] logits)
    {
        float maxLogit = Mathf.Max(logits);
        float[] exps = new float[logits.Length];
        float sumExps = 0f;
        for (int i = 0; i < logits.Length; i++)
        {
            exps[i] = Mathf.Exp(logits[i] - maxLogit);
            sumExps += exps[i];
        }
        for (int i = 0; i < logits.Length; i++)
        {
            exps[i] /= sumExps;
        }
        return exps;
    }
}
