using Anaglyph.DisplayCapture;
using OVRSimpleJSON;
using System;
using System.IO;
using TMPro;
using Unity.Sentis;
using Unity.Sentis.Layers;
using UnityEngine;
using UnityEngine.UI;

public class SegmentationManagerTest : MonoBehaviour
{
    public TMP_Text debugText;
    public RawImage displayPannel;
    public RawImage originalImage;

    [Header("Initial Setting")]
    [SerializeField] private ModelAsset modelAsset;
    [SerializeField] private float updateInterval;
    [SerializeField] private float segmentThreshold;
    public string imagePath; // 로컬 이미지 경로

    private Model model;
    private IWorker engine;
    private RenderTexture targetRT;

    private Color[] classColors = new Color[]
    {
        new Color(0f, 0f, 0f, 0f),       // Class 0 - 투명색
        new Color(255f / 255f, 255f / 255f, 0f / 255f, 0.5f),   // Class 1 - 주황 (도보)
        new Color(255f / 255f, 255f / 255f, 0f / 255f, 0.5f),   // Class 2 - 노랑 (점자블록)
        new Color(255f / 255f, 0f / 255f, 0f / 255f, 0.5f),      // Class 3 - 남색 (도로) 
        new Color(255f / 255f, 0f / 255f, 0f / 255f, 0.5f),      // Class 4 - 회색 (골목길)
        new Color(135f / 255f, 206f / 255f, 235f / 255f, 0.5f), // Class 5 - 하늘색 (횡단보도)
        new Color(255f / 255f, 0f / 255f, 0f / 255f, 0.5f),   // Class 6 - 보라색 (자전거도로)
        new Color(255f / 255f, 0f / 255f, 0f / 255f, 0.5f)      // Class 7 - 빨간색 (위험구역)
    };

    private const int imageWidth = 256;
    private const int imageHeight = 256;

    void Awake()
    {
        targetRT = new RenderTexture(imageWidth, imageHeight, 0);
    }

    private void Start()
    {
        Model myModel = new Model();
    }

    private void OnEnable()
    {
        displayPannel.enabled = true;

        model = ModelLoader.Load(modelAsset);
        engine = WorkerFactory.CreateWorker(BackendType.GPUCompute, model);

        TestSegmentationWithLocalImage();
    }

    private void OnDisable()
    {
        displayPannel.enabled = false;

        model = null;
        engine?.Dispose();
    }

    private void TestSegmentationWithLocalImage()
    {
        // 1. 로컬 이미지 불러오기
        Texture2D inputImage = LoadImageFromPath(Path.Combine(Application.dataPath, "Scripts/test2.jpg"));

        if (inputImage == null)
        {
            Debug.LogError($"Failed to load image from path: {imagePath}");
            return;
        }

        originalImage.texture = inputImage;

        // 2. 이미지를 RenderTexture로 복사
        Graphics.Blit(inputImage, targetRT);


        // 3. RenderTexture를 Tensor로 변환
        using TensorFloat inputTensor = TextureConverter.ToTensor(targetRT, imageWidth, imageHeight, 3);

        inputTensor.CompleteOperationsAndDownload();
        NormalizeTensor(inputTensor, imageHeight, imageWidth);

        // 4. 모델 실행
        engine.Execute(inputTensor);
        var outputTensor = engine.PeekOutput() as TensorFloat;
        outputTensor.CompleteOperationsAndDownload();

        //print(string.Join(", ", outputTensor.ToReadOnlyArray().Take(100)));

        // 5. Softmax 적용
        ApplySoftmax(outputTensor, 8, imageHeight, imageWidth);
        

        // 6. 텐서에서 클래스 맵 생성
        int[,] classMap = GenerateClassMap(outputTensor, imageWidth, imageHeight);

        // 7. 클래스 맵에서 Texture2D 생성
        Texture2D segmentationTexture = GenerateSegmentationTexture(classMap, imageWidth, imageHeight);

        // 8. 결과를 RawImage에 출력
        displayPannel.texture = segmentationTexture;

        outputTensor.Dispose();
    }

    private Texture2D LoadImageFromPath(string path)
    {
        if (!File.Exists(path))
        {
            Debug.LogError($"Image file does not exist at path: {path}");
            return null;
        }

        byte[] fileData = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        if (!texture.LoadImage(fileData))
        {
            Debug.LogError($"Failed to load image data from file: {path}");
            return null;
        }

        // 크기 조정
        Texture2D resizedTexture = new Texture2D(imageWidth, imageHeight, TextureFormat.RGBA32, false);
        Graphics.ConvertTexture(texture, resizedTexture);
        return resizedTexture;
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

    private void NormalizeTensor(TensorFloat inputTensor, int height, int width)
    {
        // ImageNet 평균 및 표준편차 값 (RGB 순서)
        float[] mean = { 0.485f, 0.456f, 0.406f };
        float[] std = { 0.229f, 0.224f, 0.225f };

        for (int c = 0; c < 3; c++) // RGB 채널
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
                float[] probabilities = mSoftmax(logits);

                for (int c=0; c < numClasses ; c++)
                {
                    int index = (c * height + y) * width + x;
                    inputTensor[index] = probabilities[c];
                }
            }
        }
    }

    private float[] mSoftmax(float[] logits)
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
