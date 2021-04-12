using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ySpringArm : MonoBehaviour
{
    public LayerMask CrashMask;  
    public Transform myCam;        

    public float RotSpeed = 1.0f;   
    public float RotSmoothSpeed = 10.0f;
    public Vector3 TargetRot;

    public float ZoomSpeed = 3.0f;
    public float ZoomSmoothSpeed = 10.0f;
    public float CurDist = 0.0f;
    public float TargetDist = 0.0f;
    public float CollisionOffset = 1.0f;

    public Vector2 LookUpArea;
    public Vector2 ZoomArea;

    public bool ControllerRotate = true;

    // Start is called before the first frame update

    void Awake()
    {
        myCam = Camera.main.transform;
        CurDist = TargetDist = Vector3.Distance(transform.position, myCam.position);
        if (ControllerRotate)
        {
            TargetRot.x = transform.localRotation.eulerAngles.x;
            TargetRot.y = transform.parent.eulerAngles.y;
        }
        else
        {
            TargetRot = transform.rotation.eulerAngles;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            // LookUp
            TargetRot.x += -Input.GetAxis("Mouse Y") * RotSpeed * Time.smoothDeltaTime;
            if (TargetRot.x > 180.0f)
                TargetRot.x -= 360.0f;
            TargetRot.x = Mathf.Clamp(TargetRot.x, LookUpArea.x, LookUpArea.y);

            // TurnRight
            TargetRot.y += Input.GetAxis("Mouse X") * RotSpeed * Time.smoothDeltaTime;
        }

        if (ControllerRotate)
        {
            transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, Quaternion.Euler(new Vector3(0, TargetRot.y, 0f)), Time.smoothDeltaTime * RotSmoothSpeed);

            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(new Vector3(TargetRot.x, 0f, 0f)), Time.smoothDeltaTime * RotSmoothSpeed);
        }
        else
        {
            // TargetRot.x, TargetRot.y를 Slerp한다
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(TargetRot), Time.smoothDeltaTime * RotSmoothSpeed);
        }


        // 1 ~ 7
        if (Input.GetAxis("Mouse ScrollWheel") > Mathf.Epsilon || Input.GetAxis("Mouse ScrollWheel") < -Mathf.Epsilon)
        {
            TargetDist += -Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed * Time.deltaTime;
            TargetDist = Mathf.Clamp(TargetDist, ZoomArea.x, ZoomArea.y);
        }
        CurDist = Mathf.Lerp(CurDist, TargetDist, Time.smoothDeltaTime * ZoomSmoothSpeed);

        Ray ray = new Ray();
        ray.origin = transform.position;    // ray 시작지점
        ray.direction = -transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, CurDist + CollisionOffset, CrashMask))
        {
            myCam.position = hit.point - ray.direction * CollisionOffset;
            // 벽보다 CollisionOffset아래를 체크하고 반대방향으로 CollisionOffset만큼 카메라를 이동시킨다
            //float RayDist = (CurDist > TargetDist) ? TargetDist : CurDist;
            //조건 : if (CurDist > Vector3.Distance(this.transform.position, hit.point))


            CurDist = Vector3.Distance(transform.position, hit.point);
            // CurDist가 크면 Curdist는 같다
        }
        else
        {
            myCam.position = transform.position + (-transform.forward * CurDist);
        }
    }
}
