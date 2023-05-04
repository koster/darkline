using DG.Tweening;
using Source.Util;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryUI : MonoBehaviour
{
    static DeliveryUI i;

    public Image vignette;
    
    public Animator character;
    public Transform town;

    public Slider slider;
    public Slider danger;
    public TextMeshProUGUI timeLeft;

    public AudioSource humm;

    public int position;

    const float speed = 1f;
    
    void Awake()
    {
        i = this;
    }

    public static void Walk()
    {
        i.position += 2;
    }

    public static void Hide()
    {
        i.gameObject.SetActive(false);
    }

    public static void Show()
    {
        i.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Game.world.delivery.definition == null)
            return;
        
        slider.value = (float)Game.world.delivery.position / Game.world.delivery.definition.length;
        danger.value = ((float)Game.world.player.GetStat(EnumPlayerStats.TIME)/Game.world.delivery.definition.dangerTime).C01();
        humm.volume = danger.value;
        vignette.color = new Color(1, 1, 1, danger.value);
        
        timeLeft.text = "Time Left: " + Game.world.player.GetStat(EnumPlayerStats.TIME);

        var maxDistanceDelta = Time.deltaTime * speed;
        var tp = new Vector3(-position, 0, 0);

        town.transform.position = Vector3.MoveTowards(town.transform.position, tp, maxDistanceDelta);

        if (Vector3.Distance(town.transform.position, tp) > 0.01f)
        {
            character.enabled = true;
        }
        else
        {
            character.enabled = false;
        }
    }

    public static void Reset()
    {
        i.position = 0;
        i.town.transform.position = Vector3.zero;
    }
}