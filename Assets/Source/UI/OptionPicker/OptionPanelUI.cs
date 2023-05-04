using System.Collections.Generic;
using TMPro;

public class OptionPanelUI : UILayerItemBase
{
    static OptionPanelUI i;

    public TextMeshProUGUI actionPointLabel;

    public List<OptionPanelItemComponent> sections = new List<OptionPanelItemComponent>();
    public List<OptionPanelItemComponent> options = new List<OptionPanelItemComponent>();

    OptionPanelModel optionModel;

    void Awake()
    {
        i = this;
        Hide();
    }


    public static void Show(OptionPanelModel model)
    {
        i.optionModel = model;

        model.onRefresh += i.OnRefreshState;
        model.OnShow();

        i.Show();
    }

    public static void Close()
    {
        i.Hide();
    }

    void OnRefreshState()
    {
        i.actionPointLabel.text = "AP: " + optionModel.actionPoints;

        if (optionModel.selection == null)
        {
            for (var j = 0; j < options.Count; j++)
            {
                options[j].Hide();
            }
        }

        for (var i = 0; i < sections.Count; i++)
        {
            if (i < optionModel.sections.Count)
            {
                var section = optionModel.sections[i];
                sections[i].Show(optionModel, section);

                if (i == optionModel.sectionIndex)
                {
                    ShowSubsection(section);
                }
            }
            else
            {
                sections[i].Hide();
            }
        }
    }

    void ShowSubsection(OptionSectionModel section)
    {
        for (var j = 0; j < options.Count; j++)
        {
            if (j < section.actions.Count)
            {
                options[j].Show(optionModel, section.actions[j]);
            }
            else
            {
                options[j].Hide();
            }
        }
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