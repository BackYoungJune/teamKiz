using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yPlayerGrenade : MonoBehaviour
{
    public enum STATE
    {
        READY, // 발사 준비됨
        EMPTY, // 수류탄 갯수 0
        SHOOT  // 던지는 중
    }

    public STATE myState = STATE.READY; 

    public GameObject ThrowGrenade; // 던질 수류탄
    public GameObject Grenade;      // 모형 수류탄
    public Transform leftHandMount;     // 수류탄의 오른쪽 손잡이, 오른손이 위치할 지점
    public Transform rightHandMount;    // 수류탄의 오른쪽 손잡이, 오른손이 위치할 지점

    yPlayerInput playerInput; // 플레이어의 입력
    public Animator playerAnimator; // 애니메이터 컴포넌트
    yPlayerAnimEvent playerAnimEvent;   // 애니메이션 이벤트

    //public int GrenadeRemain = 2; // 남은 전체 수류탄 갯수

    public float timeBetFire = 0.12f; // 총알 발사 간격
    public float throwTime = 1.0f;    // 던지는 소요 시간
    float lastFireTime; // 수류탄을 마지막으로 발사한 시점

    J_ItemManager itemManager;

    public AudioClip PinClip;
    public AudioSource myAudioSource;

    //Sound
    public AudioClip PinSound;
    void Awake()
    {
        // 사용할 컴포넌트들을 가져오기
        playerInput = GetComponentInParent<yPlayerInput>();
        playerAnimator = GetComponent<Animator>();
        playerAnimEvent = GetComponent<yPlayerAnimEvent>();
        itemManager = GetComponent<J_ItemManager>();
        myAudioSource = GetComponent<AudioSource>();
        //rigid = GetComponent<Rigidbody>();

        // 수류탄 던지는 동작중에 던지는 프레임까지 오면 이벤트를 실행한다
        playerAnimEvent.shoot = OnShoot;

        //MN_UIManager.Instance.Granade = itemManager.remainGrenade;
    }

    private void OnEnable()
    {
        // 수류탄 슈터가 활성화될 떄 모형 수류탄도 활성화 
        Grenade.gameObject.SetActive(true);

        // 수류탄 슈터의 현재 상태를 수류탄을 쏠 준비가 된 상태로 변경
        myState = STATE.READY;
        // 마지막으로 수류탄을 쏜 시점을 초기화
        lastFireTime = 0;

        /* 유석 
         UI 처음 수류탄 갯수 갱신 (GrenadeRemain)
        */
        //MN_UIManager.Instance.Granade = itemManager.remainGrenade;

        //V0 = rigid.velocity;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.fire2)
        {
            Fire();


            //Vector3 velocity = GetVelocity(transform.position, m_Target.position, Quaternion.Euler(new Vector3(0, Camera.main.transform.forward, 0));
            //m_Rigidbody.velocity = velocity;
        }

        if(myState == STATE.EMPTY && J_ItemManager.instance.remainGrenade > 0)
        {
            // 모형 수류탄을 활성화한다
            Grenade.gameObject.SetActive(true);
            myState = STATE.READY;
        }
    }

    // 발사 시도
    public bool Fire()
    {
        // 현재 상태가 발사 가능한 상태
        // && 마지막 수류탄 발사 시점에서 timeBetFire 이상의 시간이 지남
        if (myState == STATE.READY && Time.time >= lastFireTime + timeBetFire && J_ItemManager.instance.remainGrenade > 0 && !playerInput.tab)
        {
            // 마지막 총 발사 시점 갱신
            lastFireTime = Time.time;
            // 실제 발사 처리 실행
            Shot();
            return true;
        }
        return false;
    }

    void Shot()
    {
        // 수류탄 던지는 애니메이션 실행
        playerAnimator.SetTrigger("Throw");
        myState = STATE.SHOOT;
    }

    
    void OnShoot()
    {
        // 들고있는 모형 수류탄을 끈다
        Grenade.gameObject.SetActive(false);
        // 던지는 수류탄 생성
        GameObject instantGrenade = Instantiate(ThrowGrenade, Grenade.transform.position, Quaternion.LookRotation(Camera.main.transform.forward));
        Rigidbody GrenadeRigid = instantGrenade.GetComponent<Rigidbody>();

        // 던지는 수류탄을 발사한다
        GrenadeRigid.AddForce(Camera.main.transform.forward * 10.0f + Vector3.up * 3.0f, ForceMode.Impulse);

        // 남은 수류탄 갯수를 -1
        //itemManager.remainGrenade--;

        /* 유석 
         UI 수류탄 갯수 갱신
        */
        //MN_UIManager.Instance.Granade = itemManager.remainGrenade;

        // 애니메이션 실행되는 시간 이후로 GrenadeThrowOut를 발동시킨다
        Invoke("GrenadeThrowOut", throwTime);
    }

    void GrenadeThrowOut()
    {
        // 남아있는 수류탄의 갯수가 0개라면
        if (J_ItemManager.instance.remainGrenade <= 0)
        {
            myState = STATE.EMPTY;
        }

        // 남아있는 수류탄의 갯수가 1개 이상이라면
        if ((J_ItemManager.instance.remainGrenade > 0))
        {
            // 모형 수류탄을 활성화한다
            Grenade.gameObject.SetActive(true);
            myState = STATE.READY;
        }
    }

<<<<<<< HEAD
    void PlayGranadePinSound()
    {
        Sound.I.PlayEffectSound(PinSound, GetComponent<AudioSource>());
    }
    //public void Line()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit hit;
    //    Vector3 Target;

    //    if (Physics.Raycast(ray, out hit, 999.0f))
    //    {
    //        Target = hit.transform.position;
    //    }
        
        
    //    float gravity = Physics.gravity.magnitude;
    //    time = Time.time;
    //    Vector3 velocity = rigid.velocity;
    //    velocity.x = gravity * time + V0.x;
    //    rigid.velocity = velocity;


        



    //    Plane playerPlane = new Plane(Vector3.up, transform.position);
    //    float hitdist = 0;


    //    if (playerPlane.Raycast(ray, out hitdist))
    //    {
    //        //Vector3 targetPoint = ray.GetPoint;
    //        //Vector3 center = (transform.position + targetPo)
    //    }
    //}

    //public Vector3 GetDisplacement(float t, float gravity, Vector3 v0, Vector3 x0)
    //{



    //    Vector3 Displacement = Vector3.zero;
    //    Displacement.x = gravity * (0.5f * Mathf.Pow(t, 2)) + v0.x * t + 0;
    //    return Displacement;
    //}

    //private void setTrajectoryPoints(Vector3 pStartPosition, Vector3 pVelocity) // 시작 위치, addForce에 들어는 
    //{
    //    float velocity = Mathf.Sqrt((pVelocity.x * pVelocity.x) + (pVelocity.y * pVelocity.y)); // 속도 구하기
    //    float angle = Mathf.Rad2Deg * (Mathf.Atan2(pVelocity.y, pVelocity.x));  // 각도 구하기
    //    float fTime = 0;
    //    int tragectoryNum = 10; // 궤도 개수
    //    List<Vector3> trajectoryPoints = new List<Vector3>();

    //    fTime += 0.1f;
    //    for (int i = 0; i < tragectoryNum; i++)
    //    {
    //        float dx = velocity * fTime * Mathf.Cos(angle * Mathf.Deg2Rad);
    //        float dy = velocity * fTime * Mathf.Sin(angle * Mathf.Deg2Rad) - (Physics2D.gravity.magnitude * fTime * fTime / 2.0f);
    //        Vector3 pos = new Vector3(pStartPosition.x + dx, pStartPosition.y + dy, -2);
    //        trajectoryPoints[i].transform.position = pos;
    //        trajectoryPoints[i].gameObject.SetActive(true);
    //        trajectoryPoints[i].transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(pVelocity.y - (Physics.gravity.magnitude) * fTime, pVelocity.x) * Mathf.Rad2Deg); // 보고있는 각도 계산
    //        fTime += 0.1f;
    //    }
    //}

    //public Vector3 GetVelocity(Vector3 player, Vector3 target, float initialAngle)
    //{
    //    float gravity = Physics.gravity.magnitude;
    //    float angle = initialAngle * Mathf.Deg2Rad;

    //    Vector3 planarTarget = new Vector3(target.x, 0, target.z);
    //    Vector3 planarPosition = new Vector3(player.x, 0, player.z);

    //    float distance = Vector3.Distance(planarTarget, planarPosition);
    //    float yOffset = player.y - target.y;

    //    float initialVelocity
    //        = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

    //    Vector3 velocity
    //        = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

    //    float angleBetweenObjects
    //        = Vector3.Angle(Vector3.forward, planarTarget - planarPosition) * (target.x > player.x ? 1 : -1);
    //    Vector3 finalVelocity
    //        = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

    //    return finalVelocity;
    //}


=======
    void PinOut()
    {
        Sound.I.PlayEffectSound(PinClip, myAudioSource);
    }
>>>>>>> e8ba20ba98d7feba00175c8da74371f1ca0f7f47
}
