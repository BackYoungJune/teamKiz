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

    StoreController storeController;    // 상점 컨트롤러

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

        storeController = FindObjectOfType<StoreController>();
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
        if (storeController != null)
        {
            if (playerInput.fire2 && !playerInput.tab && !storeController.StopFire)
            {
                Fire();
            }
        }
        else
        {
            if (playerInput.fire2 && !playerInput.tab)
            {
                Fire();
            }
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

    void PinOut()
    {
        Sound.I.PlayEffectSound(PinClip, myAudioSource);
    }
}
