

![header](https://capsule-render.vercel.app/api?type=transparent&height=300&section=header&text=White%20Vision👁️‍🗨️&fontSize=90&fontColor=00BFFF)  
## 🎬시연 영상🎬  

[![Video Label](http://img.youtube.com/vi/kCf2NHandRo/0.jpg)](https://youtu.be/kCf2NHandRo?si=iHz43jF7gY15bN5G)  
▲ 클릭하여 영상으로 이동

<br>  

## 📄프로젝트 소개📄
- **기간** : 2024.09.23 ~ 2024.12.10 (약 3달)
- **소개** : XR 환경에서 Vision AI를 활용한 시각 보조 서비스이다. 시각장애인, 저시력자처럼 제한적인 시야를 가진 사회적 약자를 위해 컴퓨터 비전 기술을 활용하여 도보를 밝혀주고, 위험 요소를 안내하고, 보다 명확하게 길을 인지할 수 있도록 도와준다.
- **배경** : 잔존시력을 가진 대다수(88%) 시각장애인은 보행에 불편함과 어려움을 겪고 있다. 점자블록, 음향신호기 등 시각장애인용 인프라가 있지만, 파손되거나 잘못 설치되는 등 제 기능을 수행하지 못하는 경우가 많다. 누구나 안전한 보행을 할 수 있도록 혼합현실(MR) 기술과 컴퓨터 비전 기술을 활용하여 시각 보조 앱을 개발하고자 하였다.
- **개발 목표**
  - 도로와 도보를 색상 대비를 통해 명확히 구분할 수 있도록 할 것
  - 길거리의 장애물들을 사전에 인지할 수 있도록 할 것
  - 실시간 영상처리를 활용하여 **보행하면서** 사용 가능할 정도의 낮은 지연속도와, 높은 주사율(Frame Rate) 보일 것
  - 서버 통신이 필요없는 OnDeive AI를 활용하여 언제 어디서든 XR 기기만으로 사용 가능할 것 
  - 누구나 사용 가능한 직관적인 사용 방법일 것
  - Segmentation, Detection, Edge Coloring 등 기능들을 원하는 대로 조합하여 사용이 가능할 것
- **기대 효과** : 잔존시력을 가진 시각장애인, 저시력자, 노인에게 보행을 돕는 정보를 제공하여 **안전한 독립 보행에 기여**하고자 한다. 
<br>  

## 👥프로젝트 참여자👥
- **임채윤(팀장) - XR & AI Application**
  - 프로젝트 일정 관리, 최종 발표 PPT 제작
  - Unity 활용한 XR 환경 구축, Hand Tracking 가상 손목 메뉴 구현, Passthrough Layer 활용한 Edge Coloring 기능 구현
  - C# 환경에서 Segmentation, Detection 모델을 활용한 시각 보조 기능 구현
- 서동주 - AI Model 
  - 최종 발표 PPT 제작, 시연 영상 편집
  - PyTorch 환경에서 Semantic Segmentation 모델 개발 및 검증
> 임채윤 (Chaeyun Lim : GitHub Page => https://github.com/ChaeDoll)

> 서동주 (Dongju Seo : GitHub Page => https://github.com/seodj01)
 
<br>  

## ⚙사용 기술 (개발 언어 및 도구)⚙
<img src="https://img.shields.io/badge/Unity-222324?style=for-the-badge&logo=Unity&logoColor=white"/> <img src="https://img.shields.io/badge/Meta XR SDK-0467DF?style=for-the-badge&logo=meta&logoColor=white"/> <img src="https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white"/>
- Language : C#, Python
- Platform : Unity Editor 2022.3.35f, Jupyter Notebook
- Library : Meta XR All-In-One SDk, Unity Sentis 1.3.0-pre, PyTorch
- Tools/IDE : GitHub, Visual Studio, Visual Code, Python IDE
  
<br>

## 🧭Develop Flow🧭
![image](https://github.com/user-attachments/assets/69f805a1-0d40-46ef-8539-d2e018f82cb7)

## 서비스 실행 과정
![image](https://github.com/user-attachments/assets/b5aa4b58-6a44-49a7-9987-91dcce0c5535)
1. 앱이 실행되면, 좌측 손목에 있는 가상 UI 전원 버튼을 눌러서 켠다.
2. 전원을 켜면 MediaProjection에 의해 1024x1024 크기의 카메라 데이터를 실시간으로 가져온다.
3. Segmentation, Detection, Edge Coloring 중 원하는 기능들을 누른다.
4. 버튼을 누르면, 해당 AI Model을 관리하는 스크립트가 담긴 Object를 활성화한다.
5. 모델 관리자가 활성화되면 Model을 불러오고, 카메라 데이터를 모델의 Input Tensor에 맞게 전처리한다.
6. 값을 추론하여 Output Tensor를 가져오고, 각 용도에 맞게 활용한다.
> **Segmentation**  
> 출력 화면의 픽셀마다 클래스 별 확률 값을 비교하여 가장 큰 값을 가진 클래스를 선택하여 클래스에 맞는 색상을 Class Map에 저장하고, Class Map의 색상을 한 번에 출력 화면에 반영하여 결과를 보여준다.  
> **Detection**  
> 출력으로 나온 left_x, left_y, right_x, right_y, label, confidence를 바탕으로 장애물의 position과 width, height를 구한다. 이후 class index에 맞게 이름을 매칭하고 최소 확률값(threshold)보다 높은 값이 나온 장애물에 **경고 이미지**를 띄운다.  
> **Edge Coloring**  
> 예외적으로 AI 모델을 사용하지 않고, Passthrough Layer에서 제공하는 Edge Color 기능을 관리하여 사용한다. 버튼을 누를 때마다 윤곽선의 색상이 변경된다.  
7. 동시에 사용하고 싶은 기능들을 모두 활성화하여 함께 사용하며 보행한다.
 
<br>

## ✅개발 구현 체크리스트✅
**0단계 : 기본 설정**
- [x] 앱 시작 시 MR 환경에서 자연스럽게 나타나는 텍스트 UI 구현 

<br>

## 🖼️구현 사항🖼️
### **도보/도로 색상 구분**
- Semantic Segmentation 기술 활용

<img src="https://github.com/user-attachments/assets/f7ee7656-8622-4804-84b4-1363f3eec54f" width="50%"/>  
<br>

### **위험요소 감지 및 경고**
- Object Detection 기술 활용
  
<img src="https://github.com/user-attachments/assets/369306d9-a4e0-4300-a1b0-df7ad78267ac" width="50%"/>   
<br>

### **윤곽선 강조**
- Edge Coloring 기술 활용
  
<img src="https://github.com/user-attachments/assets/369306d9-a4e0-4300-a1b0-df7ad78267ac" width="50%"/>   
<br>

## 💡추후 업데이트 아이디어💡
<details>
  <summary>클릭하여 펼쳐보기</summary>
  <ui>
    <li>Semantic Segmentation 모델 성능 향상</li>
    <li>Segmentation 데이터 증강 활용하여 학습 (Rotate, Crop, Zoom, Shear, Brightness 등 조절)</li>
    <li>CycleGAN 등 생성형 이미지 활용하여 계절 변화 대응하는 데이터 생성 및 학습</li>
    <li>Object Detection 모델 성능 향상 (Latency, Frame 등 문제 해결)</li>
  </ui>
</details>

<br>

## 🍪개발 현황🍪
- v0.0.1 : White Vision - 1차 개발 완료 ( 2024.12.05 )

### Reference
- *Thanks for trev3d - MediaProjection API* - https://github.com/trev3d/QuestDisplayAccessDemo
- *Boundaryless Setting* - https://developers.meta.com/horizon/documentation/unity/unity-boundaryless/
