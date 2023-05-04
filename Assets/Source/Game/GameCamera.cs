using UnityEngine;

public class GameCamera : MonoBehaviour
{
    static GameCamera i;

    void Awake()
    {
        i = this;
    }

    public static void PunchShake(float intencity)
    {
    }

    public static void Rumble(float intencity, float duration)
    {
    }
}