# Running Game - API Documentation

---

## 목차

1. [Camera](#1-camera)
2. [CutScene](#2-cutscene)
3. [Helper](#3-helper)
4. [Manager](#4-manager)
5. [Obstacle](#5-obstacle)
6. [Plane](#6-plane)
7. [Player](#7-player)
8. [UI](#8-ui)
9. [Unused](#9-unused)

---

## 1. Camera

### 1.1 CustomCamera

**파일 경로:** `Camera/CustomCamera.cs`

**설명:**  
플레이어를 부드럽게 따라가는 카메라 컨트롤러

**주요 필드:**
- `Transform target` - 추적할 대상 Transform
- `float followSpeed` - 카메라 추적 속도

**주요 메서드:**
- `void LateUpdate()` - 매 프레임 카메라 위치를 대상의 X 위치로 부드럽게 이동

---

## 2. CutScene

### 2.1 BossCutSceneController (PlayerController)

**파일 경로:** `CutScene/BossCutSceneController.cs`

**설명:**  
캐릭터 컨트롤러 및 애니메이션 제어

**주요 필드:**
- `float speed` - 이동 속도 (기본값: 5.0f)
- `CharacterController controller` - 캐릭터 컨트롤러 컴포넌트
- `Animator animator` - 애니메이터 컴포넌트

**주요 메서드:**
- `void Start()` - 컴포넌트 초기화
- `void Update()` - 입력 처리 및 캐릭터 이동, 애니메이션 상태 업데이트

---

### 2.2 EndingCutSceneController

**파일 경로:** `CutScene/EndingCutSceneController.cs`

**설명:**  
엔딩 컷신 타임라인 재생 및 UI Fade In 효과 제어

**주요 필드:**
- `PlayableDirector timelineDirector` - 엔딩 타임라인
- `Canvas endingUICanvas` - Fade In할 UI Canvas
- `float delayBeforeFadeIn` - UI 표시 전 대기 시간 (기본값: 6초)
- `float fadeInDuration` - Fade In 지속 시간 (기본값: 1초)

**주요 메서드:**
- `void Start()` - 타임라인 재생 및 UI Fade In 시작
- `IEnumerator FadeInUIAfterDelay()` - 지연 후 UI Fade In 실행
- `IEnumerator FadeInUI()` - UI 요소들을 서서히 나타나게 함

---

## 3. Helper

### 3.1 Destroyer

**파일 경로:** `Helper/Destroyer.cs`

**설명:**  
플레이어가 트리거에 진입하면 지정된 오브젝트를 파괴

**주요 필드:**
- `GameObject target` - 파괴할 대상 오브젝트

**주요 메서드:**
- `void OnTriggerEnter(Collider other)` - 플레이어 감지 시 target 오브젝트 파괴

---

### 3.2 Helper

**파일 경로:** `Helper/Helper.cs`

**설명:**  
범용 유틸리티 정적 클래스 (배열 셔플 등)

**주요 메서드:**
- `void Swap<T>(ref T v1, ref T v2)` - 두 값을 교환
- `void Shuffle<T>(T[] array)` - 배열을 무작위로 섞음 (Fisher-Yates 알고리즘)

---

### 3.3 Trigger

**파일 경로:** `Helper/Trigger.cs`

**설명:**  
특정 태그의 오브젝트가 트리거에 진입하면 GameManager에 이벤트 전달

**주요 필드:**
- `string targetTag` - 감지할 태그
- `int trigerCode` - 트리거 코드

**주요 메서드:**
- `void OnTriggerEnter(Collider other)` - 태그 일치 시 GameManager.Triggered() 호출

---

## 4. Manager

### 4.1 CutSceneManager

**파일 경로:** `Manager/CutSceneManager.cs`

**설명:**  
컷신 비디오 재생 및 Stage3로 전환 관리

**주요 필드:**
- `VideoPlayer videoPlayer` - 비디오 플레이어
- `float cutSceneDuration` - 비디오 없을 경우 대기 시간 (기본값: 10초)

**주요 메서드:**
- `void Start()` - 깜빡임 효과와 함께 컷신 시작
- `void StartCutScene()` - 비디오 재생 시작
- `void OnVideoEnd(VideoPlayer vp)` - 비디오 종료 시 Stage3로 전환
- `void TransitionToStage3()` - 깜빡임 효과와 함께 Stage3 씬 로드
- `void SkipCutScene()` - ESC 키로 컷신 스킵

---

### 4.2 GameManager

**파일 경로:** `Manager/GameManager.cs`

**설명:**  
게임의 전체 흐름 제어 (스테이지 관리, 게임 상태, 일시정지, 게임오버 등)

**열거형:**
- `GameState` - Ready, Playing, Paused, GameOver, CutScene, Ending

**주요 필드:**
- `PlaneSpawner planeSpawner` - 평면 스포너
- `PlaneSpawner obstacleSpawner` - 장애물 스포너
- `GameObject directionalLight` - 방향성 라이트
- `PlayerEvent playerEvent` - 플레이어 이벤트 컴포넌트
- `float stage1PlayTime` - 스테이지 1 플레이 시간
- `float stage2PlayTime` - 스테이지 2 플레이 시간
- `float stage3PlayTime` - 스테이지 3 플레이 시간
- `int safePlaneCount` - 안전 평면 개수
- `int maxHP` - 최대 체력
- `int currentHP` - 현재 체력

**주요 메서드:**
- `void Awake()` - 싱글톤 패턴 구현
- `void Update()` - 게임 상태 및 스테이지 전환 관리
- `bool IsPlaying()` - 게임이 진행 중인지 확인
- `void InitGame()` - 게임 초기화
- `void GamePause()` - 게임 일시정지
- `void GameResume()` - 게임 재개
- `void GameOver()` - 게임 오버 처리
- `void GameRestart()` - 게임 재시작
- `void GameQuit()` - 게임 종료
- `void Stage1()` - 스테이지 1 시작
- `void Stage2()` - 스테이지 2 전환
- `void PlayCutScene()` - 컷신 재생
- `void Stage3()` - 스테이지 3 시작
- `void GameEnding()` - 엔딩 씬 로드
- `void Triggered(int code, GameObject go)` - 트리거 이벤트 처리
- `float PlayTime { get; }` - 현재 플레이 시간 반환
- `int CurrentStage { get; }` - 현재 스테이지 반환

---

### 4.3 HelperManager

**파일 경로:** `Manager/HelperManager.cs`

**설명:**  
씬별로 필요한 컴포넌트를 GameManager와 UIManager에 연결

**주요 필드:**
- `PlaneSpawner planeSpawner`
- `PlaneSpawner obstacleSpawner`
- `PlayerEvent playerEvent`
- `GameObject mainMenuUI`
- `GameObject howToUI`
- `GameObject pauseUI`
- `GameObject difficultyLevelUI`
- `GameObject directionalLight`
- `TextMeshProUGUI timerUI`
- `Slider runSlider`
- `Slider hpSlider`

**주요 메서드:**
- `void Start()` - 컴포넌트 연결 및 게임/UI 초기화
- `void Stage1()` - GameManager.Stage1() 호출
- `void Stage3()` - GameManager.Stage3() 호출
- `void GameRestart()` - GameManager.GameRestart() 호출
- `void GameResume()` - GameManager.GameResume() 호출
- `void GameQuit()` - GameManager.GameQuit() 호출
- `void OnMainMenuUI()` - UIManager.OnMainMenuUI() 호출
- `void OnHowToUI()` - UIManager.OnHowToUI() 호출
- `void OffHowToUI()` - UIManager.OffHowToUI() 호출
- `void OnDifficultyLevelUI()` - UIManager.OnDifficultyLevelUI() 호출

---

### 4.4 QuizManager

**파일 경로:** `Manager/QuizManager.cs`

**설명:**  
퀴즈 데이터 관리 및 퀴즈 UI 표시 제어

**내부 클래스:**
- `Quiz` - 퀴즈 문제, 선택지, 정답 인덱스

**주요 필드:**
- `Quiz[] quizzes` - 퀴즈 배열
- `TextMeshProUGUI quizText` - 퀴즈 텍스트 UI
- `float quizDist` - 퀴즈 표시 거리

**주요 메서드:**
- `void Awake()` - 싱글톤 패턴
- `void Start()` - 퀴즈 배열 셔플
- `void Update()` - 퀴즈 UI 업데이트
- `void UpdateQuizUI()` - 거리에 따라 퀴즈 UI 표시/숨김
- `Quiz GetQuiz()` - 다음 퀴즈 반환
- `void PushQuiz(QuizObstacle quizObstacle)` - 퀴즈를 큐에 추가
- `void PopQuiz()` - 퀴즈를 큐에서 제거

---

### 4.5 SoundManager

**파일 경로:** `Manager/SoundManager.cs`

**설명:**  
BGM 및 SFX 사운드 재생 관리 (싱글톤)

**내부 클래스:**
- `StageBgmEntry` - BGM 클립 및 개별 볼륨
- `SfxEntry` - SFX 클립 및 개별 볼륨

**주요 필드:**
- `StageBgmEntry[] stageBgms` - 스테이지별 BGM 배열
- `float bgmVolume` - BGM 마스터 볼륨 (기본값: 0.7)
- `SfxEntry[] sfxList` - SFX 배열
- `float sfxVolume` - SFX 마스터 볼륨 (기본값: 1.0)
- `int sfxChannels` - 동시 재생 가능한 SFX 채널 수 (기본값: 8)

**주요 메서드:**
- `void Awake()` - 싱글톤 패턴 및 초기화
- `void Init()` - BGM 및 SFX AudioSource 생성
- `void PlayStageBgm(int stageIndex)` - 스테이지별 BGM 재생
- `void StopBgm()` - BGM 정지
- `void PauseBgm()` - BGM 일시정지
- `void ResumeBgm()` - BGM 재개
- `void SetBgmVolume(float v)` - BGM 마스터 볼륨 설정
- `void PlaySfx(int index)` - SFX 재생
- `void ChangeBgmAfter(float delay, int stageIndex)` - 지연 후 BGM 변경
- `void SetSfxVolume(float v)` - SFX 마스터 볼륨 설정

---

### 4.6 StageManager

**파일 경로:** `Manager/StageManager.cs`

**설명:**  
시간 기반 스테이지 순차 전환 관리 (싱글톤)

**주요 필드:**
- `GameObject[] stageRoots` - 각 스테이지 루트 오브젝트 배열
- `float[] stageDurations` - 각 스테이지 지속 시간 (초)
- `UnityEvent onGameClear` - 모든 스테이지 완료 시 호출되는 이벤트

**주요 메서드:**
- `void Awake()` - 싱글톤 패턴
- `void Start()` - 스테이지 0부터 시작
- `void Update()` - 타이머 업데이트 및 스테이지 전환
- `float GetCurrentStageDuration()` - 현재 스테이지 지속 시간 반환
- `void SetStage(int index)` - 특정 스테이지 활성화 및 BGM 재생
- `void GoToNextStage()` - 다음 스테이지로 전환 또는 게임 클리어
- `int GetCurrentStageIndex()` - 현재 스테이지 인덱스 반환
- `float GetCurrentStageElapsedTime()` - 현재 스테이지 경과 시간 반환
- `float GetCurrentStageRemainTime()` - 현재 스테이지 남은 시간 반환

---

### 4.7 UIManager

**파일 경로:** `Manager/UIManager.cs`

**설명:**  
게임 UI 요소들의 활성화/비활성화 및 타이머 표시 관리 (싱글톤)

**주요 필드:**
- `GameObject mainMenuUI` - 메인 메뉴 UI
- `GameObject howToUI` - 설명 UI
- `GameObject pauseUI` - 일시정지 UI
- `GameObject difficultyLevelUI` - 난이도 선택 UI
- `TextMeshProUGUI timerUI` - 타이머 텍스트
- `Slider runSlider` - 달리기 진행도 슬라이더
- `Slider hpSlider` - 체력 슬라이더

**주요 메서드:**
- `void Awake()` - 싱글톤 패턴
- `void OffAllUI()` - 모든 UI 비활성화
- `void OnMainMenuUI()` - 메인 메뉴 UI 활성화
- `void OffMainMenuUI()` - 메인 메뉴 UI 비활성화
- `void OnHowToUI()` - 설명 UI 활성화
- `void OffHowToUI()` - 설명 UI 비활성화
- `void OnPauseUI()` - 일시정지 UI 활성화
- `void OffPauseUI()` - 일시정지 UI 비활성화
- `void OnDifficultyLevelUI()` - 난이도 선택 UI 활성화
- `void OffDifficultyLevelUI()` - 난이도 선택 UI 비활성화
- `void OnTimerUI()` - 타이머 UI 활성화
- `void OffTimerUI()` - 타이머 UI 비활성화
- `void UpdateTimer(float timer)` - 타이머 텍스트 업데이트
- `string TimerFormating(float timer)` - 시간을 문자열로 포맷 (MM:SS:mmm)
- `void OnRunSlider()` - 달리기 슬라이더 활성화
- `void OffRunSlider()` - 달리기 슬라이더 비활성화
- `void OnHpSlider()` - 체력 슬라이더 활성화
- `void OffHpSlider()` - 체력 슬라이더 비활성화

---

## 5. Obstacle

### 5.1 DragonFollower

**파일 경로:** `Obstacle/DragonFollower.cs`

**설명:**  
Stage3에서 드래곤이 플레이어를 일정 거리 뒤에서 추적하며 애니메이션 자동 전환

**주요 필드:**
- `Transform player` - 추적할 플레이어 Transform
- `float followDistanceZ` - 플레이어 뒤에서 유지할 Z축 거리 (기본값: 20)
- `float offsetX` - X축 오프셋
- `float offsetY` - Y축 오프셋
- `float smoothSpeed` - 드래곤 이동 속도 (부드러움 정도)
- `bool instantFollow` - 즉시 따라가기 (true 시 부드러운 이동 비활성화)
- `Animator dragonAnimator` - 드래곤 Animator
- `string[] animationTriggers` - 랜덤 재생할 애니메이션 트리거 배열
- `float animationChangeInterval` - 애니메이션 전환 간격 (초)
- `bool randomAnimation` - 랜덤 애니메이션 재생 여부
- `bool enableAnimation` - 애니메이션 자동 재생 활성화

**주요 메서드:**
- `void Start()` - 플레이어 및 Animator 자동 탐색, 첫 애니메이션 재생
- `void LateUpdate()` - 목표 위치 계산, 이동, 회전, 애니메이션 업데이트
- `Vector3 CalculateTargetPosition()` - 드래곤의 목표 위치 계산
- `void LookAtPlayer()` - 드래곤이 플레이어를 바라보도록 회전
- `void UpdateAnimation()` - 애니메이션 자동 전환 업데이트
- `void PlayAnimation(int index)` - 특정 인덱스의 애니메이션 재생
- `void PlayAnimationByName(string animationName)` - 외부에서 특정 애니메이션 재생
- `void SetAnimationInterval(float interval)` - 애니메이션 전환 간격 설정
- `float GetDistanceToPlayer()` - 플레이어와의 거리 반환 (Z축 기준)
- `void OnDrawGizmos()` - 기즈모로 목표 위치 및 거리 시각화

---

### 5.2 Lightning

**파일 경로:** `Obstacle/Lightning.cs`

**설명:**  
번개 오브젝트의 왕복 이동 애니메이션

**주요 필드:**
- `Vector3 moveOffset` - 이동 오프셋
- `float speed` - 이동 속도

**주요 메서드:**
- `void Start()` - 속도 랜덤 초기화
- `void Update()` - PingPong을 이용한 왕복 이동

---

### 5.3 MonsterMove

**파일 경로:** `Obstacle/MonsterMove.cs`

**설명:**  
몬스터의 전방 이동 (Z축 30 이하에서만)

**주요 필드:**
- `float speed` - 이동 속도

**주요 메서드:**
- `void Update()` - Z축 30 미만일 때 앞으로 이동

---

### 5.4 QuizCube

**파일 경로:** `Obstacle/QuizCube.cs`

**설명:**  
플레이어가 선택지 큐브에 충돌 시 QuizObstacle에 결과 전달

**주요 필드:**
- `int order` - 큐브 순서
- `TextMeshProUGUI option` - 선택지 텍스트

**주요 메서드:**
- `void Start()` - 부모 QuizObstacle 참조 획득
- `void OnTriggerEnter(Collider other)` - 플레이어 충돌 시 부모에게 알림

---

### 5.5 QuizObstacle

**파일 경로:** `Obstacle/QuizObstacle.cs`

**설명:**  
퀴즈 장애물 로직 - QuizManager에서 퀴즈 가져와 선택지 배치

**주요 필드:**
- `QuizCube[] quizCubes` - 3개의 선택지 큐브
- `QuizManager.Quiz quiz` - 현재 퀴즈 데이터
- `bool triggered` - 이미 트리거 되었는지 여부
- `int answerCubeOrder` - 정답 큐브의 order

**주요 메서드:**
- `void Start()` - QuizManager에서 퀴즈 가져와 선택지 배치
- `void ImplantOptions()` - 큐브를 셔플하고 선택지 텍스트 설정
- `void Triggered(int order, PlayerEvent player)` - 정답/오답 처리

---

### 5.6 Trap

**파일 경로:** `Obstacle/Trap.cs`

**설명:**  
플레이어와 충돌 시 데미지를 입히는 함정

**주요 필드:**
- `int contactDamage` - 접촉 시 피해량 (기본값: 1)

**주요 메서드:**
- `void OnTriggerEnter(Collider other)` - 플레이어 감지 시 PlayerEvent.OnTrapHit() 호출

---

## 6. Plane

### 6.1 DrawLane

**파일 경로:** `Plane/DrawLane.cs`

**설명:**  
에디터 기즈모로 레인 선 시각화

**주요 필드:**
- `float laneInterval` - 레인 간격
- `float laneLength` - 레인 길이

**주요 메서드:**
- `void OnDrawGizmos()` - 3개의 레인 라인을 빨간색으로 그림

---

### 6.2 Plane

**파일 경로:** `Plane/Plane.cs`

**설명:**  
평면 타일에 장애물을 랜덤 배치하고 Z축 방향으로 이동

**내부 클래스:**
- `Obstacle` - 장애물 프리팹, 스폰 확률, 높이 인덱스
- `ObstacleSet` - 장애물 배열 및 이동/수평 여부

**주요 필드:**
- `Transform[] frontSpawnPoints` - 앞쪽 스폰 포인트 (3개)
- `Transform[] backSpawnPoints` - 뒤쪽 스폰 포인트 (3개)
- `ObstacleSet[] frontObstacleSets` - 앞쪽 장애물 세트 배열
- `ObstacleSet[] backObstacleSets` - 뒤쪽 장애물 세트 배열
- `float speed` - 평면 이동 속도
- `float obstacleHeight` - 장애물 높이 오프셋

**주요 메서드:**
- `void Start()` - 앞뒤 장애물 배치
- `void ImplantObstacle(Transform[] spawnPoints, ObstacleSet[] obstacleSets)` - 장애물 생성 로직
- `void Update()` - 평면을 Z축 음의 방향으로 이동

---

### 6.3 PlaneSpawner

**파일 경로:** `Plane/PlaneSpawner.cs`

**설명:**  
평면 타일을 순환 스폰하여 무한 맵 생성

**내부 클래스:**
- `PlaneCycle` - 평면 프리팹 배열

**주요 필드:**
- `PlaneCycle[] planeCycles` - 평면 사이클 배열
- `GameObject endQuad` - 종료 지점 표시 오브젝트
- `float planeLength` - 평면 길이
- `float endQuadHeight` - 종료 지점 높이
- `int planeCount` - 초기 평면 개수

**주요 메서드:**
- `void Awake()` - 큐 초기화
- `void Start()` - 종료 지점 생성
- `void Init()` - 모든 평면을 사이클 0으로 스폰
- `void Init(int safe)` - safe 개수만큼 안전 평면 생성 후 나머지 스폰
- `void LateUpdate()` - 평면 파괴 및 재생성
- `void DestroyPlane()` - 뒤로 넘어간 평면 파괴 및 앞쪽에 재생성
- `void SpawnPlane(Vector3 spawnPos)` - 평면 인스턴스 생성
- `void ChangeCycle(int index)` - 평면 사이클 변경

---

## 7. Player

### 7.1 PlayerEvent

**파일 경로:** `Player/PlayerEvent.cs`

**설명:**  
플레이어 체력 및 피격 이벤트 처리 (무적 시간, 깜빡임 효과 포함)

**열거형:**
- `DifficultyLevel` - EASY, NORMAL, HARD

**주요 필드:**
- `int maxHP` - 최대 체력 (기본값: 5)
- `int easyHP` - 쉬움 난이도 체력
- `int normalHP` - 보통 난이도 체력
- `int hardHP` - 어려움 난이도 체력
- `float invincibleDuration` - 무적 지속 시간 (기본값: 3초)
- `float flickerInterval` - 깜빡임 간격 (기본값: 0.15초)
- `Renderer[] targetRenderers` - 깜빡임 효과 적용 대상
- `Slider hpSlider` - 체력 슬라이더
- `GameObject HitStage12Effect` - 스테이지 1,2 피격 이펙트
- `GameObject HitStage3Effect` - 스테이지 3 피격 이펙트
- `GameObject quizCorrectEffect` - 퀴즈 정답 이펙트
- `bool isInvincible` - 무적 상태 여부
- `bool isDead` - 사망 여부
- `int currentHP` - 현재 체력

**주요 메서드:**
- `void Awake()` - 체력 초기화, Renderer 자동 탐색
- `void Start()` - HP 슬라이더 초기 설정
- `void TakeDamage(int damage)` - 데미지 처리, 사망 또는 무적 코루틴 시작
- `void OnTrapHit(int baseDamage)` - 함정 충돌 시 호출
- `void OnWrongAnswer(int baseDamage)` - 오답 시 호출
- `void Die()` - 사망 처리, GameManager.GameOver() 호출
- `IEnumerator InvincibleRoutine()` - 무적 + 깜빡임 코루틴
- `void SetRenderersVisible(bool visible)` - Renderer 보이기/숨기기
- `void SetHP(int max, int curr)` - 체력 설정
- `void ChangeDifficultyLevel(int level)` - 난이도별 체력 설정
- `void PlayTrapHitFeedback(Vector3 hitPosition)` - 피격 이펙트 및 사운드 재생
- `void OnQuizWrong(int damage)` - 퀴즈 오답 시 피해 처리
- `void OnQuizCorrect()` - 퀴즈 정답 시 이펙트 및 사운드 재생

---

### 7.2 PlayerInput

**파일 경로:** `Player/PlayerInput.cs`

**설명:**  
키보드 및 모바일 터치 입력 처리

**주요 필드:**
- `bool moveLeft` - 왼쪽 이동 입력
- `bool moveRight` - 오른쪽 이동 입력
- `bool jump` - 점프 입력
- `bool slide` - 슬라이드 입력

**주요 메서드:**
- `void Start()` - 입력 상태 초기화
- `void Update()` - 게임 진행 중일 때 키보드 및 터치 입력 감지

---

### 7.3 PlayerMovement

**파일 경로:** `Player/PlayerMovement.cs`

**설명:**  
플레이어 이동, 점프, 슬라이드 제어

**주요 필드:**
- `PlayerInput playerInput` - 입력 컴포넌트
- `Rigidbody playerRigidbody` - 리지드바디
- `CapsuleCollider playerCollider` - 콜라이더
- `Animator playerAnimator` - 애니메이터
- `float smoothTime` - 이동 부드러움 정도
- `float jumpForce` - 점프 힘
- `float slideTime` - 슬라이드 지속 시간
- `float groundCheckLength` - 지면 체크 레이캐스트 길이
- `float groundCheckTime` - 지면 체크 간격
- `int minLane` - 최소 레인 (기본값: 왼쪽)
- `int maxLane` - 최대 레인 (기본값: 오른쪽)
- `int laneInterval` - 레인 간격
- `int maxJumpCount` - 최대 점프 횟수

**주요 메서드:**
- `void Start()` - 변수 초기화
- `void Update()` - 지면 체크, 입력 처리, 위치 업데이트
- `void GroundCheck()` - 레이캐스트로 지면 감지, 점프 카운트 리셋
- `void Movement()` - 좌우 이동, 점프, 슬라이드 입력 처리
- `void Jump()` - 점프 애니메이션 및 물리 적용
- `IEnumerator SlideRoutine()` - 슬라이드 코루틴 (콜라이더 크기 조정)
- `void UpdatePos()` - 부드러운 X축 위치 보간
- `void OnDrawGizmos()` - 지면 체크 레이 시각화

---

### 7.4 RunBar

**파일 경로:** `Player/RunBar.cs`

**설명:**  
GameManager의 전체 플레이 시간을 슬라이더로 표시

**주요 필드:**
- `Slider runSlider` - 진행도 슬라이더

**주요 메서드:**
- `void Start()` - GameManager 및 슬라이더 참조
- `void Update()` - 플레이 시간 / 전체 시간 비율로 슬라이더 값 업데이트

---

## 8. UI

### 8.1 BlinkEffect

**파일 경로:** `UI/BlinkEffect.cs`

**설명:**  
화면 깜빡임(Fade Out/In) 효과 제공 (씬 전환 시 사용)

**주요 필드:**
- `Image blackScreen` - 검은 화면 이미지
- `Canvas canvas` - 최상위 Canvas

**주요 메서드:**
- `void Awake()` - 싱글톤 패턴, Canvas 및 Image 설정
- `void SetupBlinkEffect()` - Canvas, CanvasScaler, Image 초기화
- `void PlayBlink(float blinkDuration, Action onComplete)` - 깜빡임 효과 재생 (Fade Out + Fade In)
- `void PlayBlinkWithSceneTransition(float fadeOutDuration, float fadeInDuration, Action onMiddle)` - 씬 전환용 깜빡임 (중간에 콜백 실행)
- `IEnumerator BlinkCoroutine(float duration, Action onComplete)` - 깜빡임 코루틴
- `IEnumerator BlinkWithSceneTransitionCoroutine(float fadeOutDuration, float fadeInDuration, Action onMiddle)` - 씬 전환 깜빡임 코루틴
- `IEnumerator FadeOut(float duration)` - 화면을 검게 전환
- `IEnumerator FadeIn(float duration)` - 화면을 투명하게 전환

---

## 9. Unused

### 9.1 CubeMover

**파일 경로:** `Unused/CubeMover.cs`

**설명:**  
큐브를 전방으로 이동 (테스트용)

**주요 필드:**
- `float speed` - 이동 속도 (기본값: 5)

**주요 메서드:**
- `void Update()` - 전방(-Z) 이동

---

### 9.2 QuizUIManager

**파일 경로:** `Unused/QuizUIManager.cs`

**설명:**  
퀴즈 질문을 일정 시간 동안 표시 (테스트용)

**주요 필드:**
- `TextMeshProUGUI questionText` - 질문 텍스트
- `float showTime` - 표시 시간 (기본값: 3초)

**주요 메서드:**
- `void ShowQuestion(string text)` - 질문 표시 코루틴 시작
- `IEnumerator Show(string text)` - 일정 시간 후 자동으로 숨김

---

### 9.3 TestQuizSpawner

**파일 경로:** `Unused/TestQuizSpawner.cs`

**설명:**  
퀴즈 큐브 세트를 일정 간격으로 스폰 (테스트용)

**주요 필드:**
- `Transform[] spawnPoints` - 3개의 스폰 포인트
- `GameObject answerCubePrefab` - 정답 큐브 프리팹
- `GameObject wrongCubePrefab` - 오답 큐브 프리팹
- `float spawnInterval` - 스폰 간격 (기본값: 3초)

**주요 메서드:**
- `void Update()` - 타이머로 일정 간격마다 SpawnQuizSet() 호출
- `void SpawnQuizSet()` - 3개의 레인 중 랜덤하게 정답 큐브 배치, 나머지는 오답 큐브

---

### 9.4 Tile

**파일 경로:** `Unused/Tile.cs`

**설명:**  
타일의 길이 및 시작/끝 지점 참조 (무한 맵 테스트용)

**주요 필드:**
- `Transform startPoint` - 타일 시작 지점
- `Transform endPoint` - 타일 끝 지점

**주요 메서드:**
- `float GetLength()` - 타일의 Z축 길이 반환
- `float GetStartZ()` - 타일 시작 지점의 Z 좌표 반환
- `float GetEndZ()` - 타일 끝 지점의 Z 좌표 반환

---

### 9.5 TileManager

**파일 경로:** `Unused/TileManager.cs`

**설명:**  
타일을 순차적으로 스폰하고 재활용하여 무한 맵 생성 (테스트용)

**주요 필드:**
- `Transform player` - 플레이어 Transform
- `Tile[] tilePrefabs` - 타일 프리팹 배열 (순환)
- `int startTileCount` - 초기 타일 개수 (기본값: 4)
- `float moveSpeed` - 타일 이동 속도 (기본값: 5)
- `float recycleDistance` - 타일 재활용 거리 (기본값: 10)

**주요 메서드:**
- `void Start()` - 초기 타일 스폰
- `void Update()` - 타일 이동 및 재활용
- `Tile CreateTile(Tile prefab, float zPos)` - 타일 인스턴스 생성
- `void MoveTiles()` - 모든 타일을 -Z 방향으로 이동
- `void RecycleTilesIfNeeded()` - 플레이어 뒤로 넘어간 타일을 앞쪽으로 재배치

---

## 문서 정보

**문서 버전:** 1.0  
**작성일:** 2025년 12월 7일  
**게임 프로젝트:** Running Game - GP_Running_Team1  
**엔진:** Unity  
**언어:** C#

---

## 참고 사항

- 모든 Manager 클래스는 싱글톤 패턴을 사용합니다.
- GameManager는 게임의 중심 허브 역할을 하며, 다른 매니저들과 상호작용합니다.
- SoundManager는 BGM과 SFX를 개별 볼륨과 마스터 볼륨으로 분리 관리합니다.
- BlinkEffect는 씬 전환 시 시각적 효과를 제공합니다.
- Unused 폴더의 스크립트는 개발 중 테스트 목적으로 사용되었으며, 현재 게임 빌드에서는 사용되지 않을 수 있습니다.

---

**문서 끝**
