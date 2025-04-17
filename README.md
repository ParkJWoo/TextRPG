# TextRPG 개인 과제 README 

## 1. 개요

-  C# 콘솔 창을 통해 출력되는 텍스트 메시지와 상황에 맞게 선택지를 선택하여 게임을 진행한다.

- 해당 게임 내에서는 메인 화면,상태 창 화면, 인벤토리 화면, 상점 화면, 전투 화면, 휴식 화면, 저장하기와 불러오기로 구성되었다.

**게임 시퀀스 소개**
-  게임 실행 → 2초 정도의 딜레이 후, 저장된 플레이어의 정보 불러오기 → 메인 화면 진입 → 각 선택지에 따라 화면을 다르게 출력 → 메인 화면에서 "0. 나가기"를 눌러 게임 종료

## 2. 화면 별 구성 요소 설명 


### 1) 메인 화면 구성

**코드: 1. 게임 메인 씬 폴더 → TextRPG_StartScene.cs**

![image](https://github.com/user-attachments/assets/92b51e26-51df-4f4c-b8da-edf3f5b7f7fd)

**해당 화면에서 플레이어가 고른 선택지에 따라 화면이 전환된다.**
- 상태 확인: 플레이어의 현재 능력치 정보를 확인하는 화면으로 이동
- 인벤토리 확인: 플레이어가 현재 보유하고 있는 아이템의 리스트 확인 및 보유 중인 아이템을 장착할 수 있는 화면으로 이동
- 상점 가기: 플레이어가 보유한 골드를 사용해 아이템을 구매 및 판매할 수 있는 화면으로 이동
- 던전 입장: 플레이어가 선택한 난이도에 따라 진행되는 전투가 이뤄지는 화면으로 이동
- 휴식하기: 플레이어가 보유한 골드를 사용해 체력을 회복하는 화면으로 이동
- 저장하기: 현재 플레이어의 정보를 저장하는 기능
- 불러오기: 플레이어가 마지막으로 저장한 정보를 불러오는 기능
- 나가기: 누를 시 게임 종료

---

### 2) 플레이어 상태 정보 확인 화면 구성

**코드: 2. 플레이어 정보 씬 폴더 → TextRPG_StatInfoScene**

![image](https://github.com/user-attachments/assets/1b2806eb-5096-49dd-bf8a-a767a1c07ae7)

**현재 플레이어의 능력치 정보를 출력한다.**
- 플레이어 정보: TextRPG_Player 클래스에 들어있는 능력치 값들을 받아와서 출력한다.
- 능력치 정보: 레벨, 이름, 직업, 공격력, 방어력, 체력, 보유 골드 정보

**플레이어의 인벤토리에 아이템의 여부에 따라 화면을 다르게 구성했다.**
- 인벤토리에 아이템이 없는 경우: 공격력과 방어력, 체력 란에 (+ 아이템 장착 시 올라간 수치값) 표시 없음
- 인벤토리에 아이템이 있는 경우: 공격력과 방어력, 체력 란에 (+ 아이템 장착 시 올라간 수치값) 표시 

---

### 3) 인벤토리 화면 구성

**코드: 3. 인벤토리 씬 폴더 → TextRPG_InventoryScene.cs → InventoryScene 메서드**

#### 3-1) 인벤토리 화면
![image](https://github.com/user-attachments/assets/3efbfc19-e15a-4700-a2ba-cfe381276e4a)

![image](https://github.com/user-attachments/assets/235329be-0eb5-4c18-b4db-c5bb1ca9f71d)

**현재 플레이어가 가지고 있는 아이템의 리스트를 출력한다**
- 인벤토리에 아이템이 존재하는지, 아닌지에 따라 출력되는 창이 다르다.
- 인벤토리는 List로 관리하며, 아이템이 있을 경우, 해당 리스트의 값들을 리스트의 크기만큼 출력하는 방식으로 구현했다.
- 아이템의 경우, TextRPG_Item 클래스의 정보값을 인벤토리의 List에 추가하는 방식으로 구현했다.

#### 3-2) 인벤토리 - 장착 관리 화면

**코드: 3. 인벤토리 씬 → TextRPG_InventoryScene.cs → WearScene 메서드**


**선택한 아이템 장착 기능 내용이 담긴 코드: 99. 디폴트 → TextRPG_Player → EquipItem 메서드**

![image](https://github.com/user-attachments/assets/728e0fab-8a7a-4bf2-a194-2ba2f9f4d4a0)
![image](https://github.com/user-attachments/assets/393495e0-b892-48be-ae51-4717b04d95d8)
![image](https://github.com/user-attachments/assets/44c1bd9e-79cd-46e3-b271-fe3c4d5fd1cd)


**인벤토리 내 아이템들을 선택해 장착할 수 있다.**
- 인벤토리에 있는 아이템들 중 하나를 선택하여 장착한다.
- 만약, 이미 선택한 아이템이 이미 장착 중인 아이템일 경우, "해당 아이템은 이미 장착 중입니다" 메시지 출력
- 그렇지 않을 경우, 선택한 아이템을 장착하며, 장착한 아이템이 가지고 있는 능력치만큼 플레이어의 능력치에 추가된다.
- 같은 종류의 아이템 (방어구, 무기)들은 각각 하나의 아이템만 착용이 가능하며, 이미 장착 중인 아이템이 있는 상태에서 아이템 장착을 선택 시, 장착한 아이템이 자동으로 교체된 후에 해당 아이템의 능력치만 적용된다.
- 능력치 적용의 경우, TextRPG_Player 클래스 내에 있는 EquipItem 메서드로 선택한 아이템을 매개변수로 넘겨준 후에 처리한다.
  
![image](https://github.com/user-attachments/assets/4cf13ee1-96dc-4594-98ff-f8d2d89fbaab)
[장착한 아이템의 능력치만큼 플레이어의 능력치 상승]

- 아이템 장착 이후, 인벤토리 화면으로 돌아갈 시, 장착 중임을 표시하는 아이콘이 아이템 정보 옆에 추가된다.

![image](https://github.com/user-attachments/assets/c62cfb33-4c43-4fab-a87e-f858193c7942)


[해당 화면에서 아이템이 중복 장착되었다고 표시되었지만, 플레이어 상태 정보 확인 화면에서는 마지막에 장착한 아이템의 능력치만큼만 올라가있다.]


→ 표시만 중복으로 뜰 뿐, 아이템 능력치는 중복 적용이 아닌 플레이어가 마지막에 장착한 아이템의 능력치만 반영된다.


--- 

### 4) 상점 화면 구성

**코드: 4. 상점 씬 폴더 → TextRPG_ShopScene.cs → ShopScene 메서드**


**아이템 코드: 99. 디폴트 폴더 → TextRPG_Item.cs**

![image](https://github.com/user-attachments/assets/b8f72da5-15d5-465c-872b-0b2408fa3149)

**판매하는 아이템들을 List로 관리하며, 이를 출력해 플레이어에게 명시한다.**
- 아이템의 경우, TextRPG_Item 클래스에 필요한 변수들을 추가한 후, 해당 화면에서 정보를 세팅하는 방식으로 구현했다.
- 모든 아이템 내에 bIsPurchase라는 bool값 변수를 false로 초기화한 후, 플레이어가 아이템을 구매했을 때, 해당 변수값을 true로 바꿔 더 이상의 재구매를 진행하지 못하도록 막았다.
- 해당 화면에서 플레이어는 아이템 구매, 혹은 보유한 아이템을 판매할 수 있다.

#### 4-1) 구매 화면 구성

**코드:  4. 상점 씬 폴더 → TextRPG_ShopScene.cs → PurchaseItem 메서드**

![image](https://github.com/user-attachments/assets/b58594a1-cafc-441e-99e8-9faadb10d3de)
![image](https://github.com/user-attachments/assets/9df141a3-6ead-4aee-9583-40f8c5ba2c03)


**플레이어가 구매하고자 하는 아이템과 플레이어의 정보를 매개변수로 받아 구매 기능을 진행한다.**
- 플레이어가 선택한 아이템의 가격과 플레이어가 현재 보유 중인 골드를 비교한다.
- 만약 플레이어가 아이템을 구매할 정도의 골드를 가지고 있을 때, 해당 아이템을 구매 처리 후, 플레이어의 보유 골드를 아이템 가격만큼 차감하며, 플레이어 인벤토리에 해당 아이템 정보를 추가한다.
- 만약 플레이어가 아이템을 구매할 정도의 골드를 가지고 있지 않을 때, 해당 아이템을 구매하지 못한다는 메시지를 출력하며 구매를 막는다.
- 또한 bIsPurchase의 값이 만약 true일 경우, 해당 아이템은 이미 구매했습니다라는 메시지 출력으로 구매를 막고, false일 경우, 해당 아이템을 구매한 후, bIsPurchase를 true로 바꾼다.
- 구매 완료 후, 상점 화면으로 돌아올 때, 모든 아이템의 bIsPurchase 값이 false로 되어있는게 정상입니다. 해당 부분은 미구현 상태입니다.

#### 4-2) 판매 화면 구성

![image](https://github.com/user-attachments/assets/bf1566f8-f613-418d-9ff0-ab75d9151028)
![image](https://github.com/user-attachments/assets/72c4cc27-f072-4069-b903-43731d5f4ad3)


**코드: 4. 상점 씬 폴더 → TextRPG_ShopScene.cs → SellScene 메서드**

**장착 중인 아이템을 판매했을 때, 해당 아이템의 능력치를 차감하는 메서드: 99. 디폴트 폴더 → TextRPG_Player.cs → UnEquipItem 메서드**

**플레이어의 인벤토리 정보와 플레이어 정보를 매개변수로 받아 판매 기능을 진행한다.**
- 플레이어의 인벤토리 정보를 해당 화면에 출력한다. → 플레이어가 보유한 아이템을 판매하기 위함
- 플레이어가 판매하고자 하는 아이템을 선택했을 때, 해당 아이템의 원가의 85%를 획득한다.
- 만약, 플레이어가 판매하고자 하는 아이템이 이미 장착 중일 경우, 판매 후에 플레이어의 능력치에서 판매한 아이템의 적용된 능력치만큼 다시 뺀다.

--- 

### 5) 던전 입장 화면 구성

**코드: 5. 던전 씬 폴더 → TextRPG_DungeonScene → EnterDungeonScene 메서드**

![image](https://github.com/user-attachments/assets/7379fd66-0797-4ee6-83d9-dce043d64385)

**해당 화면에서 던전에 대한 정보를 확인한 후, 원하는 난이도를 선택해서 입장한다.**
- 각 던전들은 난이도에 따라 권장 방어력 수치가 다르다.
- 난이도가 어려울수록 권장 방어력이 높아지며, 입장 시, 플레이어의 방어력과 권장 방어력의 차에 따라 입는 데미지가 다르다.

#### 5-1) 전투 화면 구성

**코드: 5. 던전 씬 폴더 → TextRPG_DungeonScene → BattleStage 메서드**

**전투 종료 후, 플레이어 체력 차감 메서드 위치: 99. 디폴트 폴더 → TextRPG_Player.cs → SetDamage 메서드**

**전투 종료 후, 플레이어 보상 획득 처리 메서드 위치: 99. 디폴트 폴더 → TextRPG_Player.cs → SetRewardGold 메서드**

**전투 종료 후, 플레이어 경험치 획득 및 레벨 업 메서드 위치: 99. 디폴트 폴더 → TextRPG_Player.cs → LevelUP 메서드**

![image](https://github.com/user-attachments/assets/a682d46d-8a24-4890-8c3a-863c8bbbf962)
![image](https://github.com/user-attachments/assets/867fbb92-9fc4-4f7c-bd6c-b28d3d44a5df)

**각 던전별로 설정된 권장 방어력에 따라 입는 데미지 수치가 달라진다!**
- 플레이어가 던전 선택 후 입장 시, 던전에 설정된 권장 방어력과 현재 플레이어의 방어력를 비교한다.
- 만약 플레이어의 방어력이 권장 방어력보다 높을 경우, 던전 클리어 확률을 100%로 설정한다.
- 만약 플레이어의 방어력이 권장 방어력보다 낮을 경우, 던전 클리어 확률을 60%로 설정한다.
- 던전 클리어 시, 보상으로 경험치와 골드를 획득하게 된다.
- 던전 클리어 실패 시, 아무런 보상을 획득할 수 없다.
- 또한 클리어 여부와 관계 없이, 전투 종료 후에 데미지를 플레이어의 체력에 적용한다.
- 클리어 후 획득한 경험치가 일정 치 이상일 경우, 플레이어의 레벨을 올려주며, 레벨이 오를 때마다 플레이어의 공격력과 방어력이 상승한다.

  ---

### 6) 휴식 화면 구성

**코드: 6. 휴식 씬 폴더 → TextRPG_RestScene.cs**

![image](https://github.com/user-attachments/assets/cb36d13f-03a5-4b07-9f80-421d169e1372)
![image](https://github.com/user-attachments/assets/b014305d-c557-458d-8195-d86a2f014291)
![image](https://github.com/user-attachments/assets/32b67104-5f3d-4ba3-a1dd-26938fab250d)
![image](https://github.com/user-attachments/assets/b1b2b489-325a-4443-a6db-809fcea35333)


**플레이어가 골드를 사용하여 현재 체력을 최대 체력만큼 회복한다.**
- 플레이어가 보유한 골드가 휴식에 필요한 골드보다 많다면, 체력 회복 후에 플레이어의 보유 골드에서 휴식에 필요한 골드만큼 차감한다.
- 플레이어가 보유한 골드가 휴식에 필요한 골드보다 부족하다면, "골드가 부족합니다" 문구 출력 후 회복할 수 없도록 한다.

---

### 7) 저장하기 구현

**코드: 99. 디폴트 폴더 → TextRPG_DataSet.cs → SaveData 메서드**

![image](https://github.com/user-attachments/assets/6a89a296-7d34-414e-9a38-101e5d0dd334)
![image](https://github.com/user-attachments/assets/b00a0e36-4249-42df-a364-128db37fb23b)


**플레이어의 현재 데이터를 json 파일로 모두 저장한다.**
- 해당 기능을 위해 Newtonsoft.Json 네임스페이스를 사용하였다.
- 저장 데이터의 경우, 플레이어 상태 정보, 플레이어 인벤토리 정보들을 json 파일로 저장한다.

---

### 8) 불러오기 구현

**코드: 99. 디폴트 폴더 → TextRPG_DataSet.cs → LoadData 메서드**

![image](https://github.com/user-attachments/assets/75c4b85c-f07e-42d3-846d-16bcfb314132)
![image](https://github.com/user-attachments/assets/34c82ca9-e8bb-42f2-a7c6-788a11fcecaa)

**플레이어가 마지막으로 저장한 데이터를 불러와서 적용한다.**
- 만약, 저장한 데이터가 없을 경우, 기본 데이터를 적용한다.

---

# EOD


