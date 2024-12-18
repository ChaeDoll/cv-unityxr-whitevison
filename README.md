

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
- Device : Meta Quest 3
-	Dataset : 인도 보행 영상 (AI Hub)
-	AI Model : DeeplabV3 (Semantic Segmentation), YOLOv7-tiny (Object Detection)
<details>
  <summary>상세 기술</summary>
  ①	Hand Tracking: 손동작 감지 기술<br>
  ②	Mixed Reality: Passthrough Layer를 활용한 혼합현실 구현<br>  
  ③	MediaProjection: 카메라 정보 강제 접근 및 활용을 위한 API<br>  
  ④	Boundaryless: 외부 앱 실행을 위한 경계 설정<br>  
  ⑤	Semantic Segmentation: 도로 및 도보 구분을 위한 이미지 분리 기법<br>  
  ⑥	Object Detection: 장애물 감지 및 경고를 위한 객체 탐지 기법<br>  
  ⑦	Edge Coloring: 윤곽선 강조를 위한 Passthrough Edge Renderer 색상 변경<br>  
  ⑧	Multi-Layer Overlay: 두 개의 AI모델 추론 결과를 종합하여 혼합현실에 반영<br>  
  ⑨	Curved Display: 몰입감과 시야각을 넓이기 위해 평면 디스플레이가 아닌, 곡면 디스플레이에 결과를 투영<br>  
  ⑩	Data Preprocessing: 학습을 위한 데이터 라벨링 및 데이터 전처리<br>   
  ⑪	Fine Tuning: 도로 및 도보를 구분하기 위해 사전학습 모델에 새로운 데이터셋으로 추가 학습 진행<br>
</details>
  
<br>

## 서비스 실행 과정
![image](https://github.com/user-attachments/assets/d7c804e3-ba75-4513-bd37-be67393a991e)

1. 앱이 실행되면, 좌측 손목에 있는 가상 UI 전원 버튼을 눌러서 켠다.
2. 전원을 켜면 MediaProjection에 의해 1024x1024 크기의 카메라 데이터를 실시간으로 가져온다.
3. Segmentation, Detection, Edge Coloring 중 원하는 기능들을 누른다.
4. 버튼을 누르면, 해당 AI Model을 관리하는 스크립트가 담긴 Object를 활성화한다.
5. 모델 관리자가 활성화되면 Model을 불러오고, 카메라 데이터를 모델의 Input Tensor에 맞게 전처리한다.
6. 값을 추론하여 Output Tensor를 가져오고, 각 용도에 맞게 활용한다.
7. 동시에 사용하고 싶은 기능들을 모두 활성화하여 함께 사용하며 보행한다.

<br>

## 🧭서비스 작동 Flow🧭
![image](https://github.com/user-attachments/assets/679e4cc0-68ca-49e2-96d3-f91ed0ceeda5)

- **Segmentation**  
출력 화면의 픽셀마다 클래스 별 확률 값을 비교하여 가장 큰 값을 가진 클래스를 선택하여 클래스에 맞는 색상을 Class Map에 저장하고, Class Map의 색상을 한 번에 출력 화면에 반영하여 결과를 보여준다.  
- **Detection**  
출력으로 나온 left_x, left_y, right_x, right_y, label, confidence를 바탕으로 장애물의 position과 width, height를 구한다. 이후 class index에 맞게 이름을 매칭하고 최소 확률값(threshold)보다 높은 값이 나온 장애물에 **경고 이미지**를 띄운다.  
- **Edge Coloring**  
예외적으로 AI 모델을 사용하지 않고, Passthrough Layer에서 제공하는 Edge Color 기능을 관리하여 사용한다. 버튼을 누를 때마다 윤곽선의 색상이 변경된다.  

<br>

## 🖼️구현 사항🖼️
### **도보/도로 색상 분류**
<img src="https://github.com/user-attachments/assets/338be7f6-8f9b-4470-b46a-ace246a9b750" width="50%"/>  

- Semantic Segmentation 기술 활용
- MobileNetV3를 백본으로 하는 DeepLabV3 모델 채택
- AI Hub '인도보행 영상' 데이터셋에서 Surface Masking 데이터 활용
- 7개의 클래스(인도, 도로, 점자블록, 위험구역, 자전거도로, 골목길, 횡단보도)로 정수형 라벨링하여 학습
- Unity 환경에서 사용하기 위해 ONNX(Open Neural Network eXchange) 포맷으로 변환하여 사용
- 후처리를 통해 안전한 길(인도, 점자블록)과 위험한 길(도로, 위험구역, 자전거도로, 골목길), 횡단보도 세 부분으로 나누어 색상 출력

<br>

### **장애물 경고 안내**
<img src="https://github.com/user-attachments/assets/dc239ad1-11c3-405c-b033-557269af0636" width="50%"/>   

- Object Detection 기술 활용
- 경량화 한 모델인 YOLOv7-tiny 모델 채택
- COCO 데이터셋으로 학습되어 있는 사전학습 모델 활용
- 모델 경량화를 위해 FP16 => UINT8 로 양자화(Quantization)
- Unity 환경에서 사용하기 위해 ONNX(Open Neural Network eXchange) 포맷으로 변환하여 사용
- 후처리를 통해 결과 클래스 80개 중, 위험 요소(사람, 자전거, 자동차, 오토바이, 버스, 트럭) 부류만 별도 필터링하여 활용
- 출력 결과 Feature에 있는 좌측 x, y 좌표와 우측 x, y 좌표를 토대로 감지된 장애물의 위치와 크기를 계산
- 계산된 결과를 토대로 경고 안내 이미지를 화면에 출력

<br>

### **윤곽선 강조**
<img src="https://github.com/user-attachments/assets/0bc82aef-8d2e-434e-8c8f-a2b780f2a8eb" width="50%"/>   

- Meta XR All-In-One SDK 활용
- Passthrough Layer 컴포넌트에 있는 Edge Rendering / Edge Color 속성을 활용
- C# 스크립트를 작성하여 윤곽선의 활성화/비활성화와 색상 변경을 제어
- 빨간색, 초록색, 파란색, 노란색, 검은색 중 원하는 색상을 선택하여 표시 가능
- 변환 버튼을 누를 때마다 윤곽선 색상이 변경되도록 구현

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
