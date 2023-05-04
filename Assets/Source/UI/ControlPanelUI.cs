using System;
using System.Collections.Generic;
using GameAnalyticsSDK;
using Source.Util;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanelUI : MonoBehaviour
{
    static ControlPanelUI i;
    
    public Button WalkButton;
    public Button RunButton;
    public Button SprintButton;
    
    public Button InventoryButton;
    public Button ScavengeButton;

    public static void Show()
    {
        i.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        i.gameObject.SetActive(false);
    }
    
    void Awake()
    {
        i = this;
    }

    void Start()
    {
        WalkButton.onClick.AddListener(OnClickWalk);
        RunButton.onClick.AddListener(OnClickRun);
        SprintButton.onClick.AddListener(OnClickSprint);
        
        InventoryButton.onClick.AddListener(OnClickInventory);
        ScavengeButton.onClick.AddListener(OnClickScavenge);
    }

    void Update()
    {
        ScavengeButton.interactable = Game.world.delivery.isScavengable && !Game.world.delivery.isWalking;

        HighlightForTutorial();

        InventoryButton.interactable =
            WalkButton.interactable =
                RunButton.interactable =
                    SprintButton.interactable =
                        !Game.world.delivery.isWalking && 
                        !NarrativeBoxUI.IsWriting() && 
                        !PickOptionUI.IsVisible();
    }

    void HighlightForTutorial()
    {
        var walk = WalkButton.GetComponent<Image>();
        if (Game.world.tutorial.highlightWalk)
        {
            if (WalkButton.interactable)
                walk.color = Color.Lerp(Color.white, Color.blue, Mathf.Sin(Time.time * 2f).Abs());
        }
        else
            walk.color = Color.white;

        var inventory = InventoryButton.GetComponent<Image>();
        if (Game.world.tutorial.highlightBackpack)
        {
            if (InventoryButton.interactable)
                inventory.color = Color.Lerp(Color.white, Color.blue, Mathf.Sin(Time.time * 2f).Abs());
        }
        else
            inventory.color = Color.white;
    }

    void OnClickWalk()
    {
        Game.world.tutorial.highlightWalk = false;
        Game.world.delivery.Step(WalkPace.WALK);
    }

    void OnClickRun()
    {
        Game.world.delivery.Step(WalkPace.RUN);
    }

    void OnClickSprint()
    {
        Game.world.delivery.Step(WalkPace.SPRINT);
    }

    void OnClickScavenge()
    {
        Game.contextQueue.Add(new GCQueue(Game.world.scavenging.Scavenge()));
    }

    void OnClickInventory()
    {
        Game.world.tutorial.highlightBackpack = false;
        
        var model = new InventoryModel();
        model.FromInventory(Game.world.inventory);
        InventoryUI.i.Show(model);
    }
}