using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Trigger(GameObject trig);
public class LTrapTrigger : MonoBehaviour
{
    public Trigger Trigger;
    public VoidDelVoid onCollision;

    public void OnTrigger(GameObject trig)
    {
        Trigger?.Invoke(trig);
    }
    

}
