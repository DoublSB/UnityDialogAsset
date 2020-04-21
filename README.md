# D'Dialogue System
![ddialoguethumb](https://user-images.githubusercontent.com/39475849/79770026-cfd60000-8367-11ea-83b2-9dc6a504f7c8.png)

D'Dialogue System by DoublSB, MIT LICENCE.


## Installation
1)	Import D’Dialogue System.
2)	Find the ‘DialogAsset’ prefab which in Asseet/DialogAsset/Prefab folder.

![image](https://user-images.githubusercontent.com/39475849/79880034-6f5bc700-842a-11ea-8064-b7972ae9c373.png)

3)	Move the ‘DialogAsset’ prefab to the scene which you want.

![image](https://user-images.githubusercontent.com/39475849/79880094-80a4d380-842a-11ea-97da-446597761361.png)

4)	Move the ‘Character’ prefab to the DialogAsset/Characters.

![image](https://user-images.githubusercontent.com/39475849/79880127-8bf7ff00-842a-11ea-95b6-8450284539f7.png)

![image](https://user-images.githubusercontent.com/39475849/79880142-91554980-842a-11ea-8876-82a61894c83e.png)


## Basic Example
1)	Set the sprite you want to display to Emotion -> Normal.

![image](https://user-images.githubusercontent.com/39475849/79880180-9f0acf00-842a-11ea-8571-9c69fddd2daf.png)

2)	Change gameobject name to your character name.

![image](https://user-images.githubusercontent.com/39475849/79880198-a631dd00-842a-11ea-923b-8607de1d128e.png)

3)	The basic setup is complete! Get DialogManager and call method ‘Show’!

![image](https://user-images.githubusercontent.com/39475849/79880222-af22ae80-842a-11ea-8fdb-87351dfee14e.png)

-	Using Doublsb.Dialog namespace.
-	Create new dialogData. Input two parameters, text, character name.
-	Call Show method with created DialogData.


## Commands
You can use various commands just by editing the text.
![image](https://user-images.githubusercontent.com/39475849/79880315-ce214080-842a-11ea-92a2-79de7e4df0bb.png)

All commands begin and end with slash character. ( / )

### Speed
Adjusting text speed.

|command|description|
|-----|------|
|/speed:up/|Reducing text delay by 0.25 seconds.|
|/speed:down/|Increasing text delay by 0.25 seconds.|
|/speed:init/|Changing text delay to initial value.|
|/speed:(float)/|Changing text delay to float value.|


### Size
Adjusting size within the text.

|command|description|
|-----|------|
|/size:up/|Increasing text size by 10.|
|/size:down/|Decreasing text size by 10.|
|/size:init/|Changing text size to initial value.|
|/size:(int)/|Changing text size to int value.|



### Click
Pause the printing until the window is clicked.

|command|description|
|-----|------|
|/click/|Pause the printing until the window is clicked.|



### Close
Forces the window to close without interaction.

|command|description|
|-----|------|
|/close/|Forces the window to close without interaction.|


### Wait
Pause the printing for amount of time.

|command|description|
|-----|------|
|/wait/|Pause the printing for amount of time.|


### Color
Change color within the text.

|command|description|
|-----|------|
|/color:(Color Name)/|Change color with supported color names. Check out ‘Supported colors’ in Unity’s official documentation. [link](https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/StyledText.html)|
|/color:(Hex Code)/|Change color with hex code. Ex) /color:#1fcbfc/|


### Emote
Change the character’s sprite while printing text.

|command|description|
|-----|------|
|/emote:(emote name)/|Change the character sprite with emote name.|

If you want to add new emotions to your character, follow the steps.

- Open the inspector of character which you want to add emotions.

![image](https://user-images.githubusercontent.com/39475849/79881113-ca41ee00-842b-11ea-8cf0-9a8e85b17b0c.png)

- Enter the emotion’s name and click create.

![image](https://user-images.githubusercontent.com/39475849/79881156-d4fc8300-842b-11ea-999c-af9e5e00cb08.png)

- Change new emotion’s sprite.

![image](https://user-images.githubusercontent.com/39475849/79881188-dded5480-842b-11ea-9bec-592b2d30cf35.png)

- Use emote command to display new emotion.

![image](https://user-images.githubusercontent.com/39475849/79881249-ef366100-842b-11ea-9edc-9b6886efe095.png)


### Sound
Play the sound while printing text.

|command|description|
|-----|------|
|/emote:(sound name)/|Play the sound with sound name.|

If you want to play sound, you need to set Call SE. Follow the steps.

- Open the inspector of character and add sound object.

![image](https://user-images.githubusercontent.com/39475849/79881361-0a08d580-842c-11ea-9575-1546b616847e.png)

- Enter the emotion’s name and click create.

![image](https://user-images.githubusercontent.com/39475849/79881156-d4fc8300-842b-11ea-999c-af9e5e00cb08.png)

- Change new emotion’s sprite.

![image](https://user-images.githubusercontent.com/39475849/79881188-dded5480-842b-11ea-9bec-592b2d30cf35.png)

- Use emote command to display new emotion.

![image](https://user-images.githubusercontent.com/39475849/79881249-ef366100-842b-11ea-9edc-9b6886efe095.png)
![image](https://user-images.githubusercontent.com/39475849/79881391-13923d80-842c-11ea-84dd-d632b77c0258.png)

- Now we can use sound command with sound object’s name.

![image](https://user-images.githubusercontent.com/39475849/79881424-1e4cd280-842c-11ea-8fc4-0527d6e3dc05.png)


## Other Settings

### Setting Chat SE
![image](https://user-images.githubusercontent.com/39475849/79881468-302e7580-842c-11ea-9580-483f70002bca.png)

If you add Chat SE, sound will be play for each delay.
This feature allows you to make different chat sounds for each character.
If you set multiple ‘Chat Se’, one of the sounds will be play randomly for each delay.

### Callback
![image](https://user-images.githubusercontent.com/39475849/79881501-3cb2ce00-842c-11ea-92af-cc65d9579363.png)

You can set callback when the window is hidden.
When the message is over and the window is clicked, the window is hidden. After that, the callback is invoked.

### Setting Skippable
![image](https://user-images.githubusercontent.com/39475849/79881530-4805f980-842c-11ea-8b3f-be25b2774ae2.png)

If you set ‘isSkippable’ to false, users cannot skip text even if you click on the window.

### Default text size
![image](https://user-images.githubusercontent.com/39475849/79881566-53f1bb80-842c-11ea-80c7-12a9ef3f41ac.png)

Set DialogData.Format.DefaultSize to change default text size.

### Default print delay time
![image](https://user-images.githubusercontent.com/39475849/79881606-610eaa80-842c-11ea-9603-3a5b413d5d9e.png)

Set default delay time at Dialog Manager inspector.
