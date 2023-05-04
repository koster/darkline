using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CombatantView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TextMeshProUGUI header;
    public TextMeshProUGUI health;
    public TextMeshProUGUI statusBar;
    
    public Image icon;
    public Slider slider;

    public GameObject targetSelectHighlight;
    public CanvasGroup canvasGroup;
    
    bool canTargetPick;
    UniversalCombatEntity combatantModel;
    UnityAction<CombatEntity> onPickedTarget;

    public void Show(CombatEntity entt, bool fadeIn=true)
    {
        gameObject.SetActive(true);

        if (fadeIn)
        {
            canvasGroup.alpha = 0;
            canvasGroup.DOFade(1f, 1f);
        }
        else
        {
            canvasGroup.alpha = 1;
        }

        combatantModel = new UniversalCombatEntity(entt);

        transform.localScale = combatantModel.GetScale();
        
        header.text = combatantModel.GetName();
        statusBar.text = "";
        icon.sprite = combatantModel.GetIcon();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (combatantModel == null)
        {
            Hide();
            return;
        }

        statusBar.text = combatantModel.GetStatusString();

        if (combatantModel.GetHP() > 0)
        {
            health.text = combatantModel.GetHP() + " HP";
        }
        else
        {
            health.text = "DEAD";
        }

        slider.value = combatantModel.GetHPNormalized();
    }

    public void PrimeForTargetPicking(UnityAction<CombatEntity> callback)
    {
        canTargetPick = true;
        onPickedTarget = callback;
    }

    public void DisengageForTargetPicking()
    {
        canTargetPick = false;
        onPickedTarget = null;
        targetSelectHighlight.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!canTargetPick) return;
        targetSelectHighlight.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!canTargetPick) return;
        targetSelectHighlight.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!canTargetPick) return;
        canTargetPick = false;
        onPickedTarget?.Invoke(combatantModel.GetRaw());
        CombatUI.DisengagePickTargetMode();
    }

    public bool Is(CombatEnemy enmy)
    {
        if (combatantModel == null) return false;
        return combatantModel.GetRaw() == enmy;
    }

    public void VisualizeDamage(int dmg, DamageSpecial special)
    {
        var text = dmg.ToString();
        
        switch (special)
        {
            case DamageSpecial.NORMAL:
                break;
            
            case DamageSpecial.VULNERABLE:
                text = "<color=red>" + text + "</color>";
                break;
            
            case DamageSpecial.CRIT:
                text = "<color=red><size=60>" + text + "</size></color>";
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(special), special, null);
        }

        FloatingText.Show(transform.position, text);
    }

    public void Die()
    {
        canvasGroup.DOFade(0f, 1f).OnComplete(Hide);
    }

    public void ShowFloatText(string misses)
    {
        FloatingText.Show(transform.position, misses);
    }
}