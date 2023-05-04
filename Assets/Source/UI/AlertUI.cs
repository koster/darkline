using Source.Util;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AlertUI : UILayerItemBase
{
    static AlertUI i;
    static float _cooldown = 0.5f;
    
    public UnityAction OnHideCallback;
    
    public TextMeshProUGUI message;
    public float cooldown = 0f;
    public Slider cooldownhint;

    void Awake()
    {
        if (i == null)
        {
            i = this;
            Hide();
        }
    }

    public static AlertUI Show(string text)
    {
        var newAlert = UILayer.Instantiate(i);
        
        UILayer.PushToQueue(newAlert);
        newAlert.message.text = text;
        newAlert.cooldown = _cooldown;
        
        "sound/boop".PlayClip();

        return newAlert;
    }

    void Update()
    {
        if (UILayer.IsInFocus(this))
        {
            cooldown -= Time.deltaTime;
            cooldownhint.value = (cooldown / _cooldown).C01();

            if (cooldown < 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    UILayer.ReleaseFromQueue(this);
                }
            }
        }
    }

    protected override void OnHide()
    {
        if (i == this)
            return;
        
        OnHideCallback?.Invoke();
        Destroy(gameObject);
    }
}