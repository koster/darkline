using System;
using System.Collections.Generic;
using UnityEngine;

public class PickOptionUI : UILayerItemBase
{
    static PickOptionUI i;
    
    public List<PickerOptionView> options = new List<PickerOptionView>();
    
    ListPickerModel pickerModel;

    void Awake()
    {
        i = this;
        Hide();
    }

    public static void Setup(ListPickerModel model)
    {
        i.Show(model);
    }

    public static void Close()
    {
        i.Hide();
    }

    public void Show(ListPickerModel model)
    {
        pickerModel = model;

        var availableOptions = pickerModel.options.FindAll(m => m.condition.Evaluate());
        
        for (var i = 0; i < options.Count; i++)
        {
            if (i < availableOptions.Count)
                options[i].Show(pickerModel, availableOptions[i]);
            else
                options[i].Hide();
        }

        var rectTransform = GetComponent<RectTransform>();
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 90 * availableOptions.Count + 30 + 90);
        
        pickerModel.OnSelected += OnSelect;
        
        UILayer.PushToQueue(this);
    }

    protected override void OnHide()
    {
        if (pickerModel != null)
            pickerModel.OnSelected -= OnSelect;
    }

    void OnSelect(int arg0, ListPickerOptionModel arg1)
    {
        UILayer.ReleaseFromQueue(this);
    }

    public static void Restore()
    {
        i.Show();
    }

    public static bool IsVisible()
    {
        return i.gameObject.activeSelf;
    }
}