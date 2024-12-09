using Anaglyph.DisplayCapture;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] DisplayCaptureManager PowerManager;
    [SerializeField] SegmentationManager SegmentationManager;
    [SerializeField] ObjectDetectionManager DetectionManager;
    [SerializeField] OVRPassthroughLayer EdgeColoringManager;

    [Header("Buttons")]
    [SerializeField] Image PowerButton;
    [SerializeField] Image SegmentButton;
    [SerializeField] Image DetectButton;
    [SerializeField] Image EdgeButton;

    private bool powerOn = false;
    private bool segmentOn = false;
    private bool detectOn = false;
    private int currentColorIndex = 0;
    Color[] colors =
    {
        new Color(1, 1, 1, 1),  // Off (흰색)
        new Color(1, 0, 0, 1), // 빨간색
        new Color(0, 1, 0, 1), // 초록색
        new Color(0, 0, 1, 1), // 파란색
        new Color(1, 1, 0, 1), // 노란색
        new Color(0, 0, 0, 1) // 검정색
    };

    private void Start()
    {
        PowerManager.StopScreenCapture();
        SegmentationManager.enabled = false;
        DetectionManager.enabled = false;
        EdgeColoringManager.edgeRenderingEnabled = true;
        EdgeColoringManager.edgeColor = new Color(0, 0, 0, 0);
        EdgeButton.color = Color.white;

        PowerButton.color = Color.gray;
        SegmentButton.color = Color.gray;
        DetectButton.color = Color.gray;
    }

    public void TogglePower()
    {
        if (!powerOn) // 켜져있지 않다면 킨다.
        {
            PowerManager.StartScreenCapture();
            PowerButton.color = Color.white;
            powerOn = true;
        }
        else // 켜져있으면 모든 기능을 끈다.
        {
            PowerManager.StopScreenCapture();
            PowerButton.color = Color.gray;
            powerOn = false;

            SegmentationManager.enabled = false;
            SegmentButton.color = Color.gray;
            segmentOn = false;

            DetectionManager.enabled = false;
            DetectButton.color = Color.gray;
            detectOn = false;

            EdgeColoringManager.edgeColor = new Color(0, 0, 0, 0);
            EdgeButton.color = Color.white;
        }
    }
    
    public void ToggleSegment()
    {
        if (!segmentOn) // 켜져있지 않다면 킨다.
        {
            SegmentationManager.enabled = true;
            SegmentButton.color = Color.white;
            segmentOn = true;
        }
        else
        {
            SegmentationManager.enabled = false;
            SegmentButton.color = Color.gray;
            segmentOn = false;
        }
    }

    public void ToggleDetect()
    {
        if (!detectOn) // 켜져있지 않다면 킨다.
        {
            DetectionManager.enabled = true;
            DetectButton.color = Color.white;
            detectOn = true;
        }
        else
        {
            DetectionManager.enabled = false;
            DetectButton.color = Color.gray;
            detectOn = false;
        }
    }

    public void ToggleEdge()
    {
        currentColorIndex = (currentColorIndex + 1) % colors.Length;
        if (currentColorIndex == 0)
        {
            EdgeColoringManager.edgeColor = new Color(0, 0, 0, 0);
            EdgeButton.color = Color.white;
        }
        else
        {
            EdgeColoringManager.edgeColor = colors[currentColorIndex];
            EdgeButton.color = colors[currentColorIndex];
        }
    }
}
