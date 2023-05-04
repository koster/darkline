using System;
using System.Collections.Generic;
using Source.Util;
using TMPro;
using UnityEngine;

public class StatView : MonoBehaviour
{
    static Dictionary<EnumPlayerStats, StatView> _view_to_stat = new Dictionary<EnumPlayerStats, StatView>();

    public EnumPlayerStats stat;
    public TextMeshProUGUI label;
    public TextMeshProUGUI value;

    void Awake()
    {
        _view_to_stat[stat] = this;
    }

    public static void NotifyChanged(EnumPlayerStats stat, int delta)
    {
        if (_view_to_stat.ContainsKey(stat))
            _view_to_stat[stat].OnChanged(delta);
    }

    void OnChanged(int delta)
    {
        if (delta > 0)
        {
            value.color = Color.green;
            label.color = Color.green;
        }
        else
        {
            value.color = Color.red;
            label.color = Color.red;
        }

        if (delta != 0)
            FloatingText.Show(value.transform.position, delta.ToString());
        
    }

    void Update()
    {
        label.text = stat.ToString();
        value.text = Game.world.player.GetStat(stat).ToString();
    }

    void FixedUpdate()
    {
        value.color = Color.Lerp(value.color, GetColorForValue(), 0.05f);
        label.color = Color.Lerp(label.color, GetColorForValue(), 0.05f);
    }

    Color GetColorForValue()
    {
        return Color.Lerp(Color.red, Color.white, (Game.world.player.GetStat(stat) / 100f).C01());
    }
}