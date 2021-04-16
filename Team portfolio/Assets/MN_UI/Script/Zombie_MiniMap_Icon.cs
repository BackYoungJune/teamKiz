using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_MiniMap_Icon : MonoBehaviour
{

    SpriteRenderer zombieRenderer;
    Color StartColor;
    private void Awake()
    {
        zombieRenderer = GetComponent<SpriteRenderer>();

        StartColor = zombieRenderer.color;

        StartCoroutine(IConAlphaChangeFirst());
    }

    IEnumerator IConAlphaChangeFirst()
    {
        float alpha = 0f;
        while(alpha < 1f)
        {
            alpha += Time.deltaTime * 2f;
            zombieRenderer.color = new Color(1f, 0f, 0f, alpha);

            yield return null;

        }
        StartCoroutine(IConAlphaChangeSecond());
    }
    IEnumerator IConAlphaChangeSecond()
    {
        float alpha = 1f;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * 2f;
            zombieRenderer.color = new Color(1f, 0f, 0f, alpha);

            yield return null;

        }
        StartCoroutine(IConAlphaChangeFirst());
    }

}
