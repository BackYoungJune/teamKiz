using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LBossAnimEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public VoidDelVoid RoarEnd;
    public VoidDelVoid FlexEnd;

    public void OnRoarEnd()
    {
        RoarEnd?.Invoke();
    }
    public void OnFlexEnd()
    {
        FlexEnd?.Invoke();
    }

}
