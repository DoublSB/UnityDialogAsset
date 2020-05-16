# D'Dialogue System
![ddialoguethumb](https://user-images.githubusercontent.com/39475849/79770026-cfd60000-8367-11ea-83b2-9dc6a504f7c8.png)

D'Dialogue System by DoublSB, MIT LICENCE.


## 설치
1)	에셋 스토어에서 D'Dialogue System을 Import합니다.
2)	Asseet/DialogAsset/Prefab 경로에 있는 ‘DialogAsset’ 프리팹을 찾습니다.

![image](https://user-images.githubusercontent.com/39475849/79880034-6f5bc700-842a-11ea-8064-b7972ae9c373.png)

3) 원하는 씬에 ‘DialogAsset’ 프리팹을 넣습니다.

![image](https://user-images.githubusercontent.com/39475849/79880094-80a4d380-842a-11ea-97da-446597761361.png)

4) ‘Character’ 프리팹을 DialogAsset/Characters 하위에 넣습니다.

![image](https://user-images.githubusercontent.com/39475849/79880127-8bf7ff00-842a-11ea-95b6-8450284539f7.png)

![image](https://user-images.githubusercontent.com/39475849/79880142-91554980-842a-11ea-8876-82a61894c83e.png)


## 기본 예시
1) 기본 감정 표현이 될 스프라이트를 골라 넣습니다.

![image](https://user-images.githubusercontent.com/39475849/79880180-9f0acf00-842a-11ea-8571-9c69fddd2daf.png)

2) 해당 게임 오브젝트의 이름을 캐릭터 이름으로 변경합니다.

![image](https://user-images.githubusercontent.com/39475849/79880198-a631dd00-842a-11ea-923b-8607de1d128e.png)

3) 기본 설정이 끝났습니다. DialogManager에 접근하여 'Show' 메서드를 호출하세요.

![image](https://user-images.githubusercontent.com/39475849/79880222-af22ae80-842a-11ea-8fdb-87351dfee14e.png)

- using Doublsb.Dialog를 선언하세요.
- 새 dialogData 객체를 만들고, 생성자 파라미터에 표시할 텍스트와 캐릭터 이름을 넣으세요.
- 생성한 dialogData 객체로 'Show' 메서드를 호출하세요.


## 명령어
텍스트를 수정하는 것만으로, 다양한 명령어를 사용할 수 있습니다.
![image](https://user-images.githubusercontent.com/39475849/79880315-ce214080-842a-11ea-92a2-79de7e4df0bb.png)

모든 명령어는 사선 문자( / )로 시작과 끝을 선언합니다.

### Speed
텍스트 속도를 조절할 수 있습니다.

|command|description|
|-----|------|
|/speed:up/|텍스트 표시 딜레이를 0.25초 감소시킵니다.|
|/speed:down/|텍스트 표시 딜레이를 0.25초 증가시킵니다.|
|/speed:init/|텍스트 표시 딜레이를 초기화합니다.|
|/speed:(float)/|텍스트 표시 딜레이를 float값으로 바꿉니다.|


### Size
텍스트 크기를 조절할 수 있습니다.

|command|description|
|-----|------|
|/size:up/|텍스트 크기를 10 늘립니다.|
|/size:down/|텍스트 크기를 10 줄입니다.|
|/size:init/|텍스트 크기를 초기화합니다.|
|/size:(int)/|텍스트 크기를 int값으로 바꿉니다.|



### Click
윈도우 클릭 전까지, 텍스트 출력을 일시 정지합니다.

|command|description|
|-----|------|
|/click/|윈도우 클릭 전까지, 텍스트 출력을 일시 정지합니다.|



### Close
상호작용 없이 윈도우를 강제로 닫습니다.

|command|description|
|-----|------|
|/close/|상호작용 없이 윈도우를 강제로 닫습니다.|


### Wait
지정한 시간동안 텍스트 출력을 멈춥니다.

|command|description|
|-----|------|
|/wait:(float)/|지정한 시간(float)동안 텍스트 출력을 멈춥니다.|


### Color
텍스트의 색을 변경합니다.

|command|description|
|-----|------|
|/color:(색 이름)/|지원되는 색의 이름으로 텍스트 색을 변경합니다. 유니티 공식 문서에서 'Supported Colors' 항목을 확인하세요. [link](https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/StyledText.html)|
|/color:(Hex 코드)/|Hex 코드로 색을 변경합니다. Ex) /color:#1fcbfc/|


### Emote
텍스트를 출력하는 동안 캐릭터의 스프라이트를 변경합니다.

|command|description|
|-----|------|
|/emote:(emotion 이름)/|emotion 이름으로 캐릭터의 스프라이트를 변경합니다.|

새로운 emotion를 캐릭터에 추가하고 싶다면, 아래의 단계를 따르세요.

- emotion을 추가할 캐릭터의 Inspector 창을 열어 주세요.

![image](https://user-images.githubusercontent.com/39475849/79881113-ca41ee00-842b-11ea-8cf0-9a8e85b17b0c.png)

- emotion의 이름을 적은 뒤, create 버튼을 누르세요.

![image](https://user-images.githubusercontent.com/39475849/79881156-d4fc8300-842b-11ea-999c-af9e5e00cb08.png)

- 새로 만든 emotion의 스프라이트를 변경하세요.

![image](https://user-images.githubusercontent.com/39475849/79881188-dded5480-842b-11ea-9bec-592b2d30cf35.png)

- emote 명령어를 사용해 추가한 emotion의 스프라이트를 표시할 수 있습니다.

![image](https://user-images.githubusercontent.com/39475849/79881249-ef366100-842b-11ea-9edc-9b6886efe095.png)


### Sound
텍스트를 표시하는 동안 사운드를 재생합니다.

|command|description|
|-----|------|
|/emote:(사운드 이름)/|사운드의 이름으로 재생합니다.|

원하는 사운드를 재생하려면, 캐릭터의 Call SE를 설정해야 합니다. 다음 단계를 따르세요.

- 사운드를 추가할 캐릭터의 Inspector 창을 열어 주세요.

![image](https://user-images.githubusercontent.com/39475849/79881361-0a08d580-842c-11ea-9575-1546b616847e.png)

- 원하는 사운드를 넣으면, 이제 해당 사운드의 이름으로 sound 명령어를 사용할 수 있습니다.

![image](https://user-images.githubusercontent.com/39475849/79881424-1e4cd280-842c-11ea-8fc4-0527d6e3dc05.png)


## Other Settings

### Chat SE 설정
![image](https://user-images.githubusercontent.com/39475849/79881468-302e7580-842c-11ea-9580-483f70002bca.png)

Chat SE를 추가할 경우, 해당 sound는 텍스트 표시 딜레이마다 재생됩니다.
이 기능은 텍스트 한 글자를 표시할 때마다 캐릭터들이 다양한 소리를 낼 수 있게 해 줍니다.
여러개의 Chat SE를 설정했다면, 그 중 하나를 랜덤으로 선택해 재생합니다.

### 콜백(Callback)
![image](https://user-images.githubusercontent.com/39475849/79881501-3cb2ce00-842c-11ea-92af-cc65d9579363.png)

윈도우가 사라질 때의 콜백을 설정할 수도 있습니다.
메세지가 모두 표시된 후, 윈도우를 클릭하면 윈도우는 사라집니다. 그 후, 콜백이 실행됩니다.

### 스킵 불가 설정
![image](https://user-images.githubusercontent.com/39475849/79881530-4805f980-842c-11ea-8b3f-be25b2774ae2.png)

'isSkipable'을 false로 설정했다면, 윈도우를 눌렀더라도 메세지를 스킵할 수 없게 됩니다.

### 기본 텍스트 크기 설정
![image](https://user-images.githubusercontent.com/39475849/79881566-53f1bb80-842c-11ea-80c7-12a9ef3f41ac.png)

DialogData.Format.DefaultSize를 변경하면 기본 텍스트 크기를 변경할 수 있습니다.

### 기본 텍스트 표시 딜레이 설정
![image](https://user-images.githubusercontent.com/39475849/79881606-610eaa80-842c-11ea-9603-3a5b413d5d9e.png)

Dialog Manager의 Inspector에서 기본 텍스트 표시 딜레이를 설정할 수 있습니다.
