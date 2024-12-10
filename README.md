

![header](https://capsule-render.vercel.app/api?type=transparent&height=300&section=header&text=White%20Vision&fontSize=90&fontColor=81F7F3)  
## 🎬시연 영상🎬  

[![Video Label](http://img.youtube.com/vi/iHz43jF7gY15bN5G/0.jpg)](https://youtu.be/kCf2NHandRo?si=iHz43jF7gY15bN5G)  
▲ 클릭하여 영상으로 이동

<br>  

## 📄프로젝트 소개📄
- **기간** : 2024.09.23 ~ 2024.12.10 (약 3달)
- **소개** : 시각장애인을 위한
- **배경** : 독립적 보행 필요
- **개발 목표**
  - 실시간 영상처리를 통해 보행 시 사용 가능하도록 구현
- **기대 효과** : 시각장애인들이 독립적으로...

<br>  

## 👥프로젝트 참여자👥
- *AI* : 서동주, **임채윤**, 
- *XR Develop* : **임채윤**
> 임채윤 (Chaeyun Lim : GitHub Page => https://github.com/ChaeDoll)
 
<br>  

## ⚙사용 기술 (개발 언어 및 도구)⚙
<img src="https://img.shields.io/badge/Unity-222324?style=for-the-badge&logo=Unity&logoColor=white"/> <img src="https://img.shields.io/badge/Meta XR SDK-0467DF?style=for-the-badge&logo=meta&logoColor=white"/> <img src="https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white"/> <img src="https://img.shields.io/badge/Notion-000000?style=for-the-badge&logo=notion&logoColor=white"/>
- Language : C#, Python
- Develop Platform : Unity (유니티), Jupyter Notebook, Python IDE
- Library : Meta XR All-In-One SDk, PyTorch
- Tools : GitHub, Visual Studio
  
<br>

## 🧭협업 Work Flow🧭
![image](https://github.com/user-attachments/assets/69f805a1-0d40-46ef-8539-d2e018f82cb7)

![image](https://github.com/user-attachments/assets/b5aa4b58-6a44-49a7-9987-91dcce0c5535)
**경험 과정**
1. 차를 마시기 전, 혼합현실(MR) 환경에서 차에 대한 설명 및 원재료 등의 정보가 담긴 티 카드를 통해 경험할 차에 대해 간단하게 이해한다.
2. 첫 번째 경험 - 소리에 집중할 수 있는 공간에 진입하여 차 이미지에 대한 소리에 집중하고, 마시게 될 차에 대해 소리를 들으며 상상한다.
3. 두 번째 경험 - 혼합현실(MR) 환경으로 돌아와서 눈 앞에 놓인 차를 마시며 향과 맛을 경험한다.
4. 세 번째 경험 - 가상공간(VR) 으로 이동하여 차가 전달하고자 하는 분위기를 시·청각적으로 경험하고 차의 원재료들을 만져보며 즐긴다.
5. 경험을 마친 후, 실물 티 카드 뒷면에 경험에 대한 후기를 남긴다.

<br>

## ✅개발 구현 체크리스트✅
**0단계 : 기본 설정**
- [x] 앱 시작 시 MR 환경에서 자연스럽게 나타나는 텍스트 UI 구현 

<br>

## 🖼️구현 사항🖼️
### **도보/도로 색상 구분**
- Sementic Segmentation 기술 활용

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
    <li>Sementic Segmentation 모델 성능 향상</li>
    <li>Segmentation 데이터 증강 활용하여 학습 (Rotate, Crop, Zoom, Shear, Brightness 등 조절)</li>
    <li>CycleGAN 등 생성형 이미지 활용하여 계절 변화 대응하는 데이터 생성 및 학습</li>
    <li>Object Detection 모델 성능 향상 (Latency, Frame 등 문제 해결)</li>
  </ui>
</details>

<br>

## 🍪개발 현황🍪
- v0.0.1 : White Vision - 1차 개발 완료 ( 2024.12.05 )

### Reference
*Thanks for trev3d/QuestDisplayAccessDemo - MediaProjection API*
