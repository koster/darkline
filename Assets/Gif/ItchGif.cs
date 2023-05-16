using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class ItchGif : MonoBehaviour
{
    public List<SpriteRenderer> characters = new List<SpriteRenderer>();

    void Start()
    {
        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        characters[0].color = new Color(1f, 1f, 1f, 1f);
        for (var i = 1; i < characters.Count; i++)
            characters[i].color = new Color(1, 1, 1, 0);
        
        yield return new WaitForSeconds(1f);

        while (true)
        {
            for (var i = 0; i < characters.Count; i++)
            {
                if (i > 0)
                    characters[i - 1].DOFade(0f, .5f);
                else
                    characters.Last().DOFade(0f, .5f);
                
                characters[i].DOFade(1f, .5f);
                yield return new WaitForSeconds(0.25f);
            }
        }
    }
}
