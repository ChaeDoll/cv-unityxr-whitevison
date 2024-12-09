using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUIOpacity : MonoBehaviour
{
    public CanvasGroup menuCanvasGroup;  // 메뉴의 CanvasGroup (투명도 조절)
    public Transform wristTransform;     // 손목(컨트롤러)의 Transform
    public Transform centerEyeAnchor;   // CenterEyeAnchor Transform
    public float maxAngle = 30f;        // 최대 허용 각도 (이 각도 이상일 때 Opacity는 0)
    public float opacityChangeSpeed = 5f; // Opacity 변화 속도

    void Update()
    {
        // 손목(컨트롤러)의 forward 방향
        Vector3 wristForward = wristTransform.up;

        // 손목에서 사용자(헤드셋) 방향 계산
        Vector3 toUserDirection = (centerEyeAnchor.position - wristTransform.position).normalized;

        // 손목 forward와 사용자 방향 사이의 각도 계산
        float angle = Vector3.Angle(wristForward, toUserDirection);

        // Opacity 계산 (각도가 작을수록 1에 가까움, maxAngle에서 0이 됨)
        float targetOpacity = Mathf.Clamp01(1 - (angle / maxAngle));

        // CanvasGroup의 Alpha를 선형 보간으로 설정
        menuCanvasGroup.alpha = Mathf.Lerp(menuCanvasGroup.alpha, targetOpacity, Time.deltaTime * opacityChangeSpeed);

        // Alpha 값이 0.5 이하일 때 Interaction 막기
        menuCanvasGroup.interactable = menuCanvasGroup.alpha > 0.5f;
    }
}
