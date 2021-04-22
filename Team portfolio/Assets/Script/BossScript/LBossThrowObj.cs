using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LBossThrowObj : MonoBehaviour
{
    public Transform Player;
    Vector3 StartPos;
    Vector3 dir;
    float speed = 40.0f;
    float upSpeed;
    bool Throw = false;
    float time;
    public void Initiate()
    {
        StartPos = this.transform.position;
        time = Vector3.Distance(StartPos, Player.position)/speed;
        upSpeed = 10 * time / 3;
        dir = (Player.position - StartPos);
        dir.y = 0.0f;
        dir.Normalize();
        Throw = true;
        time = 0.0f;
    }
    void Update()
    {
        if(Throw)
        {
            this.transform.Translate(dir * speed* Time.deltaTime);
            this.transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
            upSpeed -= Time.deltaTime * 10f;
            time += Time.deltaTime;
            if (time > 3.0f)
                Destroy(this.gameObject);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == Player)
        {
            Debug.Log("HitThrowObj");
            Player.GetComponent<yPlayerHealth>().OnDamage(30.0f, Vector3.zero, Vector3.zero);
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log(other);
        }
    }
}
