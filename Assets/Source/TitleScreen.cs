using System;
using DG.Tweening;
using GameAnalyticsSDK;
using Source.Util;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    public Image line;
    public GameObject txt;
    public TextMeshProUGUI darkline;
    
    public AudioSource humm;
    public AudioSource ambience;

    bool over;

    void Update()
    {
        if (over)
            return;

        humm.volume = Mathf.Sin(Time.time).Abs();

        if (Input.GetMouseButtonDown(0))
        {
            darkline.text = "<color=#cf34eb>DARK</color><color=white>LINE</color>";
            
            over = true;
            txt.SetActive(false);

            line.enabled = true;
            line.transform.DOScaleY(1000, 1f).OnComplete(() => { SceneManager.LoadScene("main"); });

            humm.FadeOutAndDie();
            ambience.FadeOutAndDie();
            
            "sound/select".PlayClip();
        }
    }
}