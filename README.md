# EnterTheGungeon

2023-03-13 Init Set, Project Start

Project URP Change - Built in to URP.

[HT]
총알을 발사 할 때, RayCast가 총알의 Layer에 막혀 로직에 문제가 발생
Raycast 에서 Bullet의 LayerMask를 제외하고 Hit 체크를 하도록 설정

2023-03-15

[HT]
prob: 각각의 애니메이션마다 조건을 설정해서 동작하도록 하다보니 animator가 너무 복잡해지고 관리가 힘들어짐.\n
slove: blendtree를 사용해서 상태별로 애니메이션 관리를 하도록 변경하였음

prob: 공격 모션이 있는 적들이 모션 도중 움직이지 않도록 설정할 때 animation 재생 시간을 측정하고 bool 값을 주는 방식으로 기존 작업을 진행했는데
새로운 적을 추가할 때마다 시간 측정하고 입력해줘야 하기 때문에 확장성에 문제가 발생.
slove: GetCurrentAnimatorStateInfo를 사용하여 애니메이션이 동작할 경우와 아닐 경우의 bool값을 쉽게 구할 수 있게 변경.

prob: 현재 오브젝트에서 특정 angle방향으로 특정 distance값 만큼의 position을 구하려고함.
not slove yet:
