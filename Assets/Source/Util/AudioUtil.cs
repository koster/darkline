using DG.Tweening;
using UnityEngine;

public static class AudioUtil
{
    public static void PlayClip(this string clip, float vol = 1)
    {
        AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>(clip), Camera.main.transform.position + Vector3.forward, vol);
    }

    public static void FadeOutAndDie(this AudioSource source, float duration = 2f)
    {
        GameObject.DontDestroyOnLoad(source);
        source.DOFade(0f, duration).OnComplete(() => { GameObject.Destroy(source); });
    }
}