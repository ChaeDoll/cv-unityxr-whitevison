using Anaglyph.DisplayCapture;
using OVR.OpenVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ServerSolution : MonoBehaviour
{
    private Texture2D textureToSend; // 전송할 Texture2D

    public RawImage objectDetectRenderer; // YOLO 결과를 표시할 렌더러
    public RawImage segmentationRenderer; // DeepLabV3 결과를 표시할 렌더러

    int frameStack = 0;

    public void SendToServer()
    {
        if (frameStack < 30) 
        {
            frameStack++;
        } else
        {
            textureToSend = DisplayCaptureManager.Instance.ScreenCaptureTexture;
            StartCoroutine(UploadImage("http://192.168.0.78:5000/process", textureToSend));
            frameStack = 0;
        }
    }

    IEnumerator UploadImage(string url, Texture2D textureToSend)
    {
        // Texture2D를 바이트 배열로 변환 (PNG 형식)
        byte[] imageBytes = textureToSend.EncodeToPNG();

        // 요청 생성
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(imageBytes);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/octet-stream"); // 바이너리 데이터 전송

        // 요청 전송
        yield return request.SendWebRequest();

        // 응답 처리
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error: {request.error}");
        }
        else
        {
            // JSON 응답 처리
            string jsonResponse = request.downloadHandler.text;
            ProcessServerResponse(jsonResponse, textureToSend);
        }
    }

    void ProcessServerResponse(string jsonResponse, Texture2D textureToSend)
    {
        // JSON 파싱
        var responseData = JsonUtility.FromJson<ServerResponse>(jsonResponse);
            
        // YOLO 결과를 시각화
        if (responseData.yolo_results != null)
        {
            DisplayYoloResults(responseData.yolo_results, textureToSend);
        }

        // DeepLabV3 결과를 시각화
        if (responseData.deeplab_segmentation != null)
        {
            DisplaySegmentationResults(responseData.deeplab_segmentation, textureToSend);
        }
    }

    void DisplayYoloResults(List<YoloBox> yoloResults, Texture2D textureToSend)
    {
        Texture2D yoloTexture = Instantiate(textureToSend); // 원본 이미지 복사

        foreach (var box in yoloResults)
        {
            DrawBoundingBox(yoloTexture, box.x, box.y, box.width, box.height);
        }

        // YOLO 결과를 렌더러에 적용
        objectDetectRenderer.texture = yoloTexture;
    }

    void DisplaySegmentationResults(List<List<int>> segmentationData, Texture2D textureToSend)
    {
        int width = segmentationData[0].Count;
        int height = segmentationData.Count;

        Texture2D segmentationTexture = new Texture2D(width, height, TextureFormat.RGBA32, false);
        Color[] pixels = new Color[width * height];

        // 세그멘테이션 데이터를 색상 맵으로 변환
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int classId = segmentationData[y][x];
                pixels[y * width + x] = ClassIdToColor(classId);
            }
        }
        segmentationTexture.SetPixels(pixels);
        segmentationTexture.Apply();
        // 세그멘테이션 결과를 렌더러에 적용
        segmentationRenderer.texture = segmentationTexture;
    }

    void DrawBoundingBox(Texture2D texture, float x, float y, float width, float height)
    {
        Color color = Color.red; // 바운딩 박스 색상

        for (int i = 0; i < width; i++)
        {
            texture.SetPixel((int)x + i, (int)y, color); // 상단
            texture.SetPixel((int)x + i, (int)(y + height), color); // 하단
        }

        for (int i = 0; i < height; i++)
        {
            texture.SetPixel((int)x, (int)y + i, color); // 좌측
            texture.SetPixel((int)(x + width), (int)y + i, color); // 우측
        }

        texture.Apply();
    }

    Color ClassIdToColor(int classId)
    {
        // 클래스 ID를 색상으로 변환 (단순 예제)
        switch (classId)
        {
            case 0: return Color.red;
            case 1: return Color.green;
            case 2: return Color.blue;
            default: return Color.black;
        }
    }

    // JSON 응답에 맞는 클래스 정의
    [System.Serializable]
    public class ServerResponse
    {
        public List<YoloBox> yolo_results;
        public List<List<int>> deeplab_segmentation;
    }

    [System.Serializable]
    public class YoloBox
    {
        public float x;
        public float y;
        public float width;
        public float height;
        public int classId;
        public float confidence;
    }
}
