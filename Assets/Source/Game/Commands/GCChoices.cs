using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Animations;

namespace Source.Game.Deliveries
{
    public class ChoiceCost
    {
        public int time;
        public int money;

        public InventoryItemDefinition itemDef;
        public int itemDefAmount;
        
        public List<EnumItemTag> needsItems = new List<EnumItemTag>();

        public ChoiceCost(params EnumItemTag[] tags)
        {
            needsItems = tags.ToList();
        }
        
        public ChoiceCost(InventoryItemDefinition def, int amount)
        {
            itemDef = def;
            itemDefAmount = amount;
        }
        
        public string Requirenment()
        {
            if (global::Game.world.player.GetStat(EnumPlayerStats.TIME) < time)
            {
                return $"Need {time} time";
            }

            if (global::Game.world.inventory.GetItemAmount(ItemDatabase.money) < money)
            {
                return $"Need {money} money";
            }

            return null;
        }
    }

    public enum EnumItemTag
    {
        FIREARM,
        AMMO
    }

    public class GCChoices : QueueItemBase
    {
        readonly UI_STATES uistate;

        ListPickerModel listModel = new();

        bool listIsCompleted;
        ListPickerOptionModel lastModel;
        bool setupPending;

        public GCChoices(UI_STATES statestyle = UI_STATES.PICK_OPTION)
        {
            uistate = statestyle;
            listModel.OnSelected += (_, opm) =>
            {
                if (opm.doesNotExit)
                {
                    setupPending = true;
                    listModel.options.Remove(opm);
                }
                else
                {
                    listIsCompleted = true;
                }
            };
        }
        
        public override void Enter()
        {
            base.Enter();
            
            UIState.RememberState();
            PickOptionUI.Setup(listModel);
            UIState.DoState(uistate);
        }

        public GCChoices Add(string option, Action<GameQueue.GameQueue> callback)
        {
            lastModel = new ListPickerOptionModel
            {
                text = option,
                optionCallback = () => callback(subqueue)
            };
            listModel.options.Add(lastModel);
            return this;
        }

        public GCChoices Add(string option, Action<GameQueue.GameQueue> callback, ChoiceCost cost)
        {
            lastModel = new ListPickerOptionModel
            {
                text = option,
                cost = cost,
                optionCallback = () => callback(subqueue)
            };
            listModel.options.Add(lastModel);
            return this;
        }

        public override void Update()
        {
            base.Update();

            if (subqueue.IsEmpty())
            {
                if (listIsCompleted)
                {
                    Complete();
                }
                else if (setupPending)
                {
                    setupPending = false;
                    PickOptionUI.Setup(listModel);
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
            
            UIState.RestoreState();
        }

        public GCChoices DoesNotExit()
        {
            lastModel.doesNotExit = true;
            return this;
        }
    }
}