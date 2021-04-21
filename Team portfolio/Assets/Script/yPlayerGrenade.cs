using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yPlayerGrenade : MonoBehaviour
{
    public GameObject Grenade;      // 사용할 수류탄
    public GameObject Grenade2;     // 수류탄 위치
    public Transform leftHandMount;     // 수류탄의 오른쪽 손잡이, 오른손이 위치할 지점
    public Transform rightHandMount;    // 수류탄의 오른쪽 손잡이, 오른손이 위치할 지점

    yPlayerInput playerInput; // 플레이어의 입력
    public Animator playerAnimator; // 애니메이터 컴포넌트

    //public Transform m_Target;
    //public float m_InitialAngle = 30f; // 처음 날라가는 각도
    //private Rigidbody m_Rigidbody;


    void Awake()
    {
        // 사용할 컴포넌트들을 가져오기
        playerInput = GetComponentInParent<yPlayerInput>();
        playerAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
    }

    // Update is called once per frame
    void Update()
    {


        if (playerInput.fire2)
        {
            //Vector3 velocity = GetVelocity(transform.position, m_Target.position, Quaternion.Euler(new Vector3(0, Camera.main.transform.forward, 0));
            //m_Rigidbody.velocity = velocity;

            // 수류탄 생성
            GameObject instantGrenade = Instantiate(Grenade, Grenade2.transform.position, Quaternion.LookRotation(Camera.main.transform.forward));
            Rigidbody GrenadeRigid =  instantGrenade.GetComponent<Rigidbody>();
            SphereCollider collider = instantGrenade.GetComponent<SphereCollider>();
            Debug.Log(GrenadeRigid);
            // 수류탄을 발사한다
            GrenadeRigid.AddForce(Camera.main.transform.forward * 10.0f + Vector3.up * 3.0f, ForceMode.Impulse);
            StartCoroutine(ThrowOut(collider));
            // 수류탄 던지는 애니메이션 실행
            playerAnimator.SetTrigger("Throw");
            //Destroy(instantGrenade.gameObject, 5f);
        }
    }

    IEnumerator ThrowOut(SphereCollider collider)
    {
        yield return new WaitForSeconds(0.5f);

        collider.isTrigger = false;
    }

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
}
