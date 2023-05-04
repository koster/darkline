using System;

public enum UI_STATES
{
    NOTHING,
    NARRATIVE_ONLY,
    DELIVERY,
    COMBAT,
    COMBAT_PICK_TARGET,
    COMBAT_ENEMY_TURN,
    PICK_OPTION,
    PICK_OPTION_NO_STATS,
    COMBAT_INVENTORY,
    NARRATIVE_PLUS_STATS
}

public class UIState
{
    public static UI_STATES CurrentState;
    static UI_STATES LastState;

    public static void RememberState()
    {
        LastState = CurrentState;
    }

    public static void RestoreState()
    {
        DoState(LastState);
    }
    
    public static void DoState(UI_STATES targetState)
    {
        CurrentState = targetState;
        
        switch (targetState)
        {
            case UI_STATES.NOTHING:
                PickOptionUI.Close();
                NarrativeBoxUI.Close();
                ControlPanelUI.Hide();
                StatPanelUI.Hide();
                DeliveryUI.Hide();
                CombatUI.Close();
                OptionPanelUI.Close();
                break;
            case UI_STATES.NARRATIVE_ONLY:
                PickOptionUI.Close();
                NarrativeBoxUI.Restore();
                ControlPanelUI.Hide();
                StatPanelUI.Hide();
                DeliveryUI.Hide();
                CombatUI.Close();
                OptionPanelUI.Close();
                break;
            case UI_STATES.NARRATIVE_PLUS_STATS:
                PickOptionUI.Close();
                NarrativeBoxUI.Restore();
                ControlPanelUI.Hide();
                StatPanelUI.Show();
                DeliveryUI.Hide();
                CombatUI.Close();
                OptionPanelUI.Close();
                break;
            case UI_STATES.DELIVERY:
                PickOptionUI.Close();
                NarrativeBoxUI.Restore();
                ControlPanelUI.Show();
                StatPanelUI.Show();
                DeliveryUI.Show();
                CombatUI.Close();
                OptionPanelUI.Close();
                break;
            case UI_STATES.COMBAT:
                PickOptionUI.Close();
                NarrativeBoxUI.Restore();
                ControlPanelUI.Hide();
                StatPanelUI.Show();
                DeliveryUI.Hide();
                CombatUI.Restore();
                OptionPanelUI.Restore();
                break;
            case UI_STATES.COMBAT_PICK_TARGET:
                PickOptionUI.Close();
                NarrativeBoxUI.Restore();
                ControlPanelUI.Hide();
                StatPanelUI.Show();
                DeliveryUI.Hide();
                CombatUI.Restore();
                OptionPanelUI.Close();
                break;
            case UI_STATES.COMBAT_INVENTORY:
                PickOptionUI.Close();
                NarrativeBoxUI.Restore();
                ControlPanelUI.Hide();
                StatPanelUI.Show();
                DeliveryUI.Hide();
                CombatUI.Restore();
                OptionPanelUI.Close();
                break;
            case UI_STATES.COMBAT_ENEMY_TURN:
                PickOptionUI.Close();
                NarrativeBoxUI.Restore();
                ControlPanelUI.Hide();
                StatPanelUI.Show();
                DeliveryUI.Hide();
                CombatUI.Restore();
                OptionPanelUI.Close();
                break;
            case UI_STATES.PICK_OPTION:
                PickOptionUI.Restore();
                NarrativeBoxUI.Restore();
                ControlPanelUI.Hide();
                StatPanelUI.Show();
                DeliveryUI.Hide();
                CombatUI.Close();
                OptionPanelUI.Close();
                break;
            case UI_STATES.PICK_OPTION_NO_STATS:
                PickOptionUI.Restore();
                NarrativeBoxUI.Restore();
                ControlPanelUI.Hide();
                StatPanelUI.Hide();
                DeliveryUI.Hide();
                CombatUI.Close();
                OptionPanelUI.Close();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(targetState), targetState, null);
        }
    }
}