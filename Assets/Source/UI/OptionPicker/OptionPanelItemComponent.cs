using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OptionPanelItemComponent : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public TextMeshProUGUI label;
    public TextMeshProUGUI costLabel;

    public GameObject selected;
    public GameObject highlight;
    public GameObject push;

    public CanvasGroup cg;
    
    OptionPanelModel modelPanel;
    OptionActionModel modelAction;
    OptionSectionModel modelSection;
    
    bool interactable;
    string reason;

    void Awake()
    {
        selected.SetActive(false);
        push.SetActive(false);
        highlight.SetActive(false);
    }

    public void Show(OptionPanelModel panelModel, OptionActionModel model)
    {
        cg.alpha = 0;
        cg.DOFade(1f, 0.5f);
        
        push.SetActive(false);
        highlight.SetActive(false);
        
        modelPanel = panelModel;
        modelAction = model;
        
        gameObject.SetActive(true);
        label.text = model.optionName;
        
        if (model.weaponAction != null)
        {
            CalculateConditions(panelModel, model);

            costLabel.text = "AP: " + model.weaponAction.apCost;
        }
        else
        {
            interactable = true;
            costLabel.text = "";
        }
    }

    void CalculateConditions(OptionPanelModel panelModel, OptionActionModel model)
    {
        interactable = true;

        if (model.weaponAction.oncePerTurn)
        {
            if (Game.world.combat.itemsUsedThisTurn.Contains(model.itemDefinition))
            {
                interactable = false;
                reason = "Can only be used once per turn";
            }
        }
        
        if (model.weaponAction.apCost > panelModel.actionPoints)
        {
            interactable = false;
            reason = "Not enough AP";
        }

        var ammo = model.weaponAction.ammo;
        if (ammo != null)
        {
            if (!Game.world.inventory.HasItem(ammo))
            {
                interactable = false;
                reason = "Not enough ammo";
            }
        }
    }

    public void Show(OptionPanelModel panelModel, OptionSectionModel model)
    {
        modelPanel = panelModel;
        modelSection = model;

        gameObject.SetActive(true);
        label.text = model.sectionName;

        if (panelModel.selection == model)
        {
            Selected();
        }
        else
        {
            Deselected();
        }

        interactable = true;
    }

    void Update()
    {
        label.alpha = interactable ? 1f : 0.5f;
        if (costLabel != null)
            costLabel.alpha = interactable ? 1f : 0.5f;
    }

    void Selected()
    {
        selected.SetActive(true);
    }

    void Deselected()
    {
        selected.SetActive(false);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!interactable)
        {
            FloatingText.Show(transform.position, reason);
            return;
        }

        if (modelSection != null)
            modelPanel.SetSelection(modelSection);
        if (modelAction != null)
            modelAction.onPicked?.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (modelAction is { weaponAction: not null })
        {
            UITooltip.Show(modelAction.weaponAction.GetTooltip(modelAction.itemDefinition, modelAction.optionName));
        }
        
        highlight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UITooltip.Hide();
        
        highlight.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        push.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        push.SetActive(false);
    }

    public void OnShowMessage()
    {
        push.SetActive(false);
        highlight.SetActive(false);
    }
}