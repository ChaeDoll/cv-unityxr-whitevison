
Thanks for trev3d/QuestDisplayAccessDemo - MediaProjection API

![header](https://capsule-render.vercel.app/api?type=transparent&height=300&section=header&text=White%20Vision&fontSize=90&fontColor=81F7F3)  
## 🎬시연 영상🎬  

[![Video Label](http://img.youtube.com/vi/_yc9_xnwfqc/0.jpg)](https://youtu.be/_yc9_xnwfqc?si=QwZ2zHGG-j6zkwQO)  
▲ 클릭하여 영상으로 이동

<br>  

## 📄프로젝트 소개📄
- **기간** : 2024.11.18 ~ 2024.12.03 (약 3주)
- **소개** : 공감각 XR티룸을 통해 차를 시·청각적으로 느낄 수 있는 몰입형 가상-혼합현실 콘텐츠 90도씨
- **배경** : 브랜드 이미지(BI)로 구매를 결정하는 현재의 소비 트렌드 + XR 기술이 가지는 시·청각적 경험 = 기업이 건네고자 하는 이미지를 강렬히 각인시키는 효과
- **개발 목표**
  - XR 경험을 활용하여 '오설록'의 '달빛 걷기' 제품을 더욱 잘 이해할 수 있는 경험 제작하기
  - 몰입감 있는 경험을 통해 브랜드 이미지를 각인시킬 수 있는 경험 개발하기
  - 가상(VR)과 현실(MR) 사이의 전환을 최대한 자연스럽게 만들기
  - 사용자가 직접 가상 오브젝트와 인터렉션을 하며 몰입할 수 있는 경험을 제공하기
  - 이질감 없는 경험을 위해 최적화 기법 활용 및 공간 사운드 활용하기
- **기대 효과** : 차에 대한 정보를 소리, 향과 맛, 시각적으로 세 단계에 나누어 경험하여 마시는 차에 대해 더욱 깊게 이해할 수 있다.

<br>  

## 👥프로젝트 참여자👥
- *Research* : 최윤서, 강원빈
- *Design* : 김다훈, 박예지, 박단비, 조윤빈
- *Develop* : 강가은, 나우진, 임채윤
> 강가은 (Gaeun Kang : GitHub Page => https://github.com/Gaeun-Kang)

> 나우진 (Woojin Na : GitHub Page => https://github.com/JIN-U-N)

> 임채윤 (Chaeyun Lim : GitHub Page => https://github.com/ChaeDoll)
 
<br>  

## ⚙사용 기술 (개발 언어 및 도구)⚙
<img src="https://img.shields.io/badge/Unity-222324?style=for-the-badge&logo=Unity&logoColor=white"/> <img src="https://img.shields.io/badge/Meta XR SDK-0467DF?style=for-the-badge&logo=meta&logoColor=white"/> <img src="https://img.shields.io/badge/GitHub-181717?style=for-the-badge&logo=github&logoColor=white"/> <img src="https://img.shields.io/badge/Notion-000000?style=for-the-badge&logo=notion&logoColor=white"/>
- Language : C#
- Develop Platform : Unity (유니티) 2022.3.35f1-URP
- Library : Meta XR All-In-One SDk
- Tools : GitHub, Notion, Visual Studio
  
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
- [x] UI 상호작용 및 이벤트 구현 
- [x] 공간 음향을 활용하여 현실적인 사운드 구현 
- [x] 씬 이벤트에 따라 자연스럽게 나타나고 사라지는 설명 자막 구현  
- [x] 자연스러운 Phase 이동을 위한 끊김 없는 (Non-Latency) 경험 구현  

**1단계 : 티 카드를 통해 차 이해하기**
- [x] Stencil Shader를 적용한 가상 티 카드 구현
- [x] 티 카드 나타나기/사라지기 구현 
- [x] 티 카드를 손으로 상호작용 가능하도록 구현  
- [x] 티 카드를 머리에 가까이하여 ASMR Phase로 이동
      
**2단계 : 시음하게 될 차에 대해 상상하기**
- [x] Phase 시작과 끝에 서서히 화면이 어두워지고 서서히 밝아지기 구현 
- [x] 차의 이미지에 맞는 ASMR 사운드 제공 
- [x] ASMR 사운드 종료 시 다음 Phase로 자연스럽게 이동
- [ ] 차의 추상적 이미지를 눈으로 볼 수 있도록 색면추상 구현

**3단계 : 차 시음하기**
- [x] 고개 각도를 위로 올릴 때 발동하는 이벤트 구현 
- [x] 차를 시음하는 타이밍에 맞게 사운드 및 화면 어두워짐 구현 
- [x] 차 시음한 이후 티 카드 다시 등장
- [x] 티 카드를 뒤집어서 가상공간(VR)이 있는 Scene으로 이동 

**4단계 : 차의 향과 맛을 시·청각으로 구현한 공감각 공간 감상하기**
- [x] 혼합현실(MR)에서 가상공간(VR)으로 자연스럽게 변경하기
- [x] 공감각 공간과 함께 배경음악 Fade-In, 60초 뒤 다시 Fade-Out
- [x] 안전을 위해 바닥을 보면 서서히 현실이 보이도록 구현 
- [x] 별사탕(Star Candy) 오브젝트 인터랙션 및 이벤트 구현
- [x] 공감각 공간 최적화 기법 적용
- [x] 공감각 경험 이후 텍스트 UI와 함께 종료 안내
- [ ] 배(Pear) 오브젝트 인터랙션 구현

<br>

## 🖼️구현 사항🖼️
### **0단계 : 기본 설정**
- Lazy Follow를 활용하여 고개 방향에 따라 늦게 따라오는 UI
- 자연스럽게 나타나고 사라지는 텍스트 자막
- 버벅임 없는(Non-Latency) Phase 전환

<img src="https://github.com/user-attachments/assets/f7ee7656-8622-4804-84b4-1363f3eec54f" width="50%"/>  
<br>

### **1단계 : 티 카드를 통해 차 이해하기**
- Mathf.Lerp(선형 보간)으로 Scale값을 조정하여 커지며 나타나고 작아지며 사라짐
- Grab Interaction을 활용하여 잡고 움직일 수 있는 티 카드
- Stencil Shader을 활용하여 카드 내에 다른 차원 구현
- 티 카드와 사용자 머리를 OnTriggerEnter하여 다음 Phase로 자연스럽게 이동
  
<img src="https://github.com/user-attachments/assets/369306d9-a4e0-4300-a1b0-df7ad78267ac" width="50%"/>   
<br>

### **2단계 : 시음하게 될 차에 대해 상상하기**
- Passthrough Layer의 Brightness(밝기), Saturation(채도)를 조정하여 어두운 화면으로 Fade-In
- 싱글톤으로 구현한 BackgroundMusicMannager을 활용하여 사운드 재생
- 사운드가 종료되면서 어두운 화면이 Fade-Out 되며 다음 Phase로 자연스럽게 이동

<img src="https://github.com/user-attachments/assets/40e90d0c-e319-473e-8727-87654228b84c" width="50%"/>   
<br>

### **3단계 : 차 시음하기**
- Update문과 Math.Clamp 함수를 사용하여 사용자 머리(Camera)의 오일러 각도 x축을 토대로 현재 고개 각도를 계산
- 고개 각도에 따른 '차 시음하기' 이벤트 실행 - 화면을 어둡게하며 시음 멜로디 재생
- 티 카드의 뒷면을 바라보고, 사용자의 시야가 설정한 각도 내에 있으면 VR Scene으로 이동하는 이벤트 실행
- SceneLoadAsync를 활용하여 비동기로 끊김 없이 다른 Scene의 콘텐츠를 불러오며 이동
  
<img src="https://github.com/user-attachments/assets/5f1df0ab-c3ed-4788-8a56-cfb9a78b26c8" width="50%"/>  
<br>

### **4단계 : 차의 향과 맛을 시·청각으로 구현한 공감각 공간 감상하기**
- Overlay Passthrough를 띄운 뒤 맵을 활성화시켜 맵을 안 보이게 한 이후 Passthrough Opacity 값 낮추어 서서히 VR Phase로 전환
- 사용자 머리(Camera)의 오일러 각도 x축을 토대로 현재 고개 각도를 계산
- 고개 각도가 15도 보다 낮으면 서서히 현실세계가 보임
- Grab Interaction으로 별사탕을 잡을 수도 있고, 잡으면 빛과 함께 소리가 남
- Bake 조명, 그림자 표시 거리 줄이기, 안티앨리어싱, Level Of Detail, 스크립트 Update문 최소화 등 최적화

<img src="https://github.com/user-attachments/assets/7227cddc-0a30-40a2-bd04-14b66dd24397" width="40%"/>  
<img src="https://github.com/user-attachments/assets/9065b2bd-cb5a-466a-821b-486d1188cb84" width="40%"/>  

<br>

## 💡추후 업데이트 아이디어💡
<details>
  <summary>클릭하여 펼쳐보기</summary>
  <ui>
    <li>시작 시 "Play" 버튼을 사용자 앞에 따라다니게 만들고, 누르면 사용자 위치를 초기화하며 경험 시작하기</li>
    <li>ASMR Phase 색면추상까지 구현하기</li>
    <li>ASMR Phase 움직이는 공간음향 추가하기 (정면부터 우측 후방 좌측 정면처럼 360도 돌아도 됨)</li>
    <li>ASMR Phase 및 VR Phase 에서 남은 시간 자연스럽게 알려주기 (서서히 차오르는 Bar 또는 Pie 그래프 활용 가능)</li>
    <li>차 시음하기 Phase에서 차 마시는 각도 더 올리거나 (현재 위로 15도인데 생각보다 트리거가 잘 발동함) ASMR씬이 끝나기 전에 UI를 두어 시선 아래로 유도하기</li>
    <li>VR Phase에서 배(pear)도 Distance Grab으로 잡아서 상호작용하고 먹을 수 있도록 구현</li>
  </ui>
</details>

<br>

## 🍪개발 현황🍪
- v0.0.1 : 90도씨 - XREAL 리뎁디 1차 개발 완료 ( 2024.12.03 )
