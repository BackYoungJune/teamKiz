using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform Player;


    private void LateUpdate()
    {
        Vector3 newPosition = Player.position;
        newPosition.y = this.transform.position.y;
        this.transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, Player.eulerAngles.y, 0f);
    }
}
