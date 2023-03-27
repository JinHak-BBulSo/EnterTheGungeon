# EnterTheGungeon

2023-03-13 Init Set, Project Start

Project URP Change - Built in to URP.

## [HT]    
총알을 발사 할 때, RayCast가 총알의 Layer에 막혀 로직에 문제가 발생    
Raycast 에서 Bullet의 LayerMask를 제외하고 Hit 체크를 하도록 설정   

2023-03-15   
prob: 각각의 애니메이션마다 조건을 설정해서 동작하도록 하다보니 animator가 너무 복잡해지고 관리가 힘들어짐.   
slove: blendtree를 사용해서 상태별로 애니메이션 관리를 하도록 변경하였음   

prob: 공격 모션이 있는 적들이 모션 도중 움직이지 않도록 설정할 때 animation 재생 시간을 측정하고 bool 값을 주는 방식으로 기존 작업을 진행했는데   
새로운 적을 추가할 때마다 시간 측정하고 입력해줘야 하기 때문에 확장성에 문제가 발생.   
slove: GetCurrentAnimatorStateInfo를 사용하여 애니메이션이 동작할 경우와 아닐 경우의 bool값을 쉽게 구할 수 있게 변경.   

2023-03-16
prob: 기존 bullet script에 bullet의 velocity를 입력해 놓고 작업하였는데 bullet이 생성된 후 날아가는 방향이 공격 패턴에 따라 달라짐에 따라 방향 조절에 문제가 발생   
solve: enemy script에서 bullet을 생성 할 때 원하는 방향으로 bullet에 addforce를 주는 방식으로 변경   

## [Junil]    
2023/03/14 / v0.0.1 / Add PlayerMarine Animater and Move, dodge    
2023/03/14 / v0.0.2 / Fix Player Move    
2023/03/14 / v0.0.2 / Add PlayerMarine Less Animater    
2023/03/15 / v0.0.3 / Add proto Weapon and fix rotate player.    
2023/03/15 / v0.0.3 / Add playerMarine normal Weapon prototype    
2023/03/16 / v0.0.4 / Add Weapon Laser prototype and Weapon Swap    
2023/03/17 / fix Laser Weapon and Laser reflect    
2023/03/20 / Fix PlayerCamera move and Add Inventory proto    
2023/03/22 / Making Inven    
2023/03/23 / Add Inven Tab Menu    

## [KJH]    
2023/03/14 타일맵의 게임오브젝트 브러쉬 맵툴 준비    
2023/03/15 맵툴 활용 타일맵 제작    
2023/03/16 BSP 알고리즘 활용 랜덤맵 제작 알고리즘 작성    
2023/03/20 MapGenerator에서 각 맵을 트리형식으로 구성, 리프 노드 간의 연결을 구현

## [HyeokJin]    

## [Yuiver]
    
