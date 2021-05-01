using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_Money : MonoBehaviour
{
    public J_Item item;

    public AudioSource audioSource;
    public AudioClip moneySound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(GetMoney());
        }
    }

    IEnumerator GetMoney()
    {
        Sound.I.PlayEffectSound(moneySound, audioSource);
        J_ItemManager.instance.remainMoney += item.restore;

        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject);
    }
}
