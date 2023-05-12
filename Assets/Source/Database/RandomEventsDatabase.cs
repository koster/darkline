using System;
using System.Collections.Generic;
using Source.Commands;
using Source.Util;

namespace Source.Game.Deliveries
{
    public static class RandomEventsDatabase
    {
        public static List<Func<GameQueue.GameQueue>> all = new List<Func<GameQueue.GameQueue>>();

        public static void Initialize()
        {
            // all.Add(MeetHomelessPerson);
            // all.Add(MeetCreepyChild);
            all.Add(ChildrenWithACat);
            all.Add(DisfiguredBicycleCorpse);
            all.Add(PileOfTrash);
            // all.Add(RaidersHarassingLocals);
            all.Add(SmallGarden);
            // all.Add(BuildingFire);
            all.Add(SmallStream);
            // all.Add(AbandonedCar);
            all.Add(TrashCan);
            all.Add(StrangeNoiseInAlley);
            // all.Add(CardboardBoxShelter);
        }

        public static GameQueue.GameQueue MeetHomelessPerson()
        {
            var q = new GameQueue.GameQueue();
            q.Add(new GCAlert("You see a homeless person!"));
            q.Add(new GCAlert("What do you do?"));
            q.Add(new GCChoices()
                .Add("Nothing!", (sq) =>
                {
                    sq.Add(new GCNarrative("I wont do nothing"));
                })
                .Add("Rob Him!", (sq) =>
                {
                    sq.Add(new GCNarrative("Well, he has nothing..."));
                })
            );
            q.Add(new GCNarrative("So you left."));
            return q;
        }
        
        public static GameQueue.GameQueue MeetCreepyChild()
        {
            var q = new GameQueue.GameQueue();
            q.Add(new GCAlert("You hear a child crying!"));
            q.Add(new GCAlert("What do you do?"));
            q.Add(new GCChoices()
                .Add("Help Him!", (sq) =>
                {
                    sq.Add(new GCNarrative("Well, he has nothing..."));
                })
                .Add("Rob Him!", (sq) =>
                {
                    sq.Add(new GCNarrative("Well, he has nothing..."));
                })
                .Add("Nothing!", (sq) =>
                {
                    sq.Add(new GCNarrative("I wont do nothing"));
                })
            );
            q.Add(new GCNarrative("So you left."));
            return q;
        }
        
        public static GameQueue.GameQueue ChildrenWithACat()
        {
            var q = new GameQueue.GameQueue();
            q.Add(new GCNarrative("You come across a group, children crying for help. Their cat stuck on a tree"));
            q.Add(new GCChoices()
                .Add("Help the children", (sq) =>
                {
                    sq.Add(new GCAddItem(ScavengingItems.GetFoodItem()));
                })
                .Add("Ignore the children", (sq) =>
                {
                    sq.Add(new GCAddStat(EnumPlayerStats.MENTAL, -10, AddStatMode.FLOAT_TEXT_ALERT));
                })
                // .Add("Shoot the cat", (sq) =>
                // {
                //     sq.Add(new GCAddItem(ItemDatabase.ammo, -1));
                //     sq.Add(new GCQueue(CombatDatabase.Combat_3ChildDemons()));
                // }, new ChoiceCost(EnumItemTag.FIREARM, EnumItemTag.AMMO))
            );
            return q;
        }
        
        public static GameQueue.GameQueue DisfiguredBicycleCorpse()
        {
            var q = new GameQueue.GameQueue();
            q.Add(new GCNarrative("You stumble upon a disfigured corpse of a bicycle."));
            q.Add(new GCChoices()
                .Add("Bicycle disfigured corpse? Why such a wording?", (sq) =>
                {
                    sq.Add(new GCAddStat(EnumPlayerStats.MENTAL, -20, AddStatMode.FLOAT_TEXT_ALERT));
                })
                .Add("Ignore its existence", (sq) =>
                {
                    sq.Add(new GCAddStat(EnumPlayerStats.MENTAL, 5, AddStatMode.FLOAT_TEXT_ALERT));
                })
            );
            return q;
        }
        public static GameQueue.GameQueue PileOfTrash()
        {
            var q = new GameQueue.GameQueue();
            q.Add(new GCNarrative("You find a pile of trash."));
            q.Add(new GCChoices()
                .Add("Search it for valuables", (sq) =>
                {
                    sq.Add(new GCAddStat(EnumPlayerStats.MENTAL, -5, AddStatMode.FLOAT_TEXT_ALERT));
                    sq.Add(new GCAddItem(ScavengingItems.GetFoodItem()));
                })
                .Add("Sympathize with it... wait what?", (sq) =>
                {
                    sq.Add(new GCAddStat(EnumPlayerStats.MENTAL, -5, AddStatMode.FLOAT_TEXT_ALERT));
                })
            );
            return q;
        }
        public static GameQueue.GameQueue RaidersHarassingLocals()
        {
            var q = new GameQueue.GameQueue();
            q.Add(new GCNarrative("You see a group of raiders harassing locals."));
            q.Add(new GCChoices()
                .Add("Hey! Leave them alone!", (sq) =>
                {
                    sq.Add(new GCQueue(CombatDatabase.Combat_Raiders())); 
                    sq.Add(new GCAddItem(ItemDatabase.waterBottle, 1)); 
                    sq.Add(new GCAddItem(ItemDatabase.burger, 1)); 
                })
                .Add("It is what it is.", (sq) =>
                {
                    sq.Add(new GCAddStat(EnumPlayerStats.MENTAL, 5, AddStatMode.FLOAT_TEXT_ALERT));
                })
            );
            return q;
        }
        public static GameQueue.GameQueue SmallGarden()
        {
            var q = new GameQueue.GameQueue();
            q.Add(new GCNarrative("You come across a small garden with some fresh vegetables growing."));
            q.Add(new GCChoices()
                .Add("Steal them.", (sq) =>
                {
                    sq.Add(new GCAddItem(ItemDatabase.tomatoes, 5)); 
                })
                .Add("Appreciate how beautiful it is", (sq) =>
                {
                    sq.Add(new GCAddStat(EnumPlayerStats.MENTAL, 10, AddStatMode.FLOAT_TEXT_ALERT));
                })
            );
            return q;
        }
        public static GameQueue.GameQueue BuildingFire()
        {
            var q = new GameQueue.GameQueue();
            q.Add(new GCNarrative("You hear someone calling for help from a nearby building, there is a fire."));
            q.Add(new GCChoices()
                .Add("Throw a bottle of water there", (sq) =>
                {
                    sq.Add(new GCAddItem(ItemDatabase.waterBottle, -1)); 
                    sq.Add(new GCAddStat(EnumPlayerStats.MENTAL, 5, AddStatMode.FLOAT_TEXT_ALERT));
                }, new ChoiceCost(ItemDatabase.waterBottle, 1))
                .Add("Convince yourself that there is nothing you can do", (sq) =>
                {
                    sq.Add(new GCAddStat(EnumPlayerStats.MENTAL, 1, AddStatMode.FLOAT_TEXT_ALERT));
                })
                .Add("Stand here, stranded by your flashbacks, and watch people die in fire", (sq) =>
                {
                    sq.Add(new GCAddStat(EnumPlayerStats.MENTAL, -30, AddStatMode.FLOAT_TEXT_ALERT));
                    sq.Add(new GCAddItem(ScavengingItems.GetRandomHumanItem()));
                })
            );
            return q;
        }
        public static GameQueue.GameQueue SmallStream()
        {
            var q = new GameQueue.GameQueue();
            q.Add(new GCNarrative("You find a small stream with fresh water."));
            q.Add(new GCChoices()
                .Add("Drink some of this obviously safe water", (sq) =>
                {
                    sq.Add(new GCAddStat(EnumPlayerStats.THIRST, 50, AddStatMode.FLOAT_TEXT_ALERT));
                    sq.Add(new GCAddStat(EnumPlayerStats.MENTAL, -10, AddStatMode.FLOAT_TEXT_ALERT));
                    sq.Add(new GCAddStat(EnumPlayerStats.HEALTH, -10, AddStatMode.FLOAT_TEXT_ALERT));
                    sq.Add(new GCAddStat(EnumPlayerStats.STAMINA, -10, AddStatMode.FLOAT_TEXT_ALERT));
                })
                .Add("Ignore it", (sq) =>
                {
                    sq.Add(new GCAddStat(EnumPlayerStats.MENTAL, 5, AddStatMode.FLOAT_TEXT_ALERT));
                })
            );
            return q;
        }
        
        public static GameQueue.GameQueue AbandonedCar()
        {
            var q = new GameQueue.GameQueue();
            q.Add(new GCNarrative("You come across an abandoned car."));
            q.Add(new GCChoices()
                .Add("Spend a ridiculous amount of time in a desperate attempt to find spare parts for your own car", (sq) =>
                {
                    sq.Add(new GCAddStat(EnumPlayerStats.MENTAL, -10, AddStatMode.FLOAT_TEXT_ALERT));
                    sq.Add(new GCAddStat(EnumPlayerStats.HEALTH, -10, AddStatMode.FLOAT_TEXT_ALERT));
                    sq.Add(new GCAddStat(EnumPlayerStats.STAMINA, -10, AddStatMode.FLOAT_TEXT_ALERT));
                    sq.Add(new GCAddItem(ScavengingItems.GetToolItem())); 
                })
                .Add("Rob the trunk quickly", (sq) =>
                {
                    sq.Add(new GCAddItem(ScavengingItems.GetRandomHumanItem()));
                })
            );
            return q;
        }
        
        public static GameQueue.GameQueue TrashCan()
        {
            var q = new GameQueue.GameQueue();
            q.Add(new GCNarrative("You come across a trash can."));
            q.Add(new GCChoices()
                .Add("Search it.", (sq) =>
                {
                    sq.Add(new GCAlert("It is disgusting."));
                    sq.Add(new GCAddStat(EnumPlayerStats.MENTAL, -10));
                    sq.Add(new GCAddItem(ScavengingItems.GetRandomTrashItem()));
                })
                .Add("Leave it.", (sq) =>
                {
                    sq.Add(new GCAddStat(EnumPlayerStats.MENTAL, +10));
                })
            );
            return q;
        }
        
        public static GameQueue.GameQueue StrangeNoiseInAlley()
        {
            var q = new GameQueue.GameQueue();
            q.Add(new GCNarrative("You hear a strange noise coming from a nearby alleyway."));
            q.Add(new GCChoices()
                .Add("Walk past.", (sq) =>
                {
                    sq.Add(new GCAddStat(EnumPlayerStats.MENTAL, 5, AddStatMode.FLOAT_TEXT_ALERT));
                })
                .Add("Investigate.", (sq) =>
                {
                    sq.Add(new GCQueue(CombatDatabase.Combat_StrangeNoiseEnemies())); 
                })
            );
            return q;
        }
        public static GameQueue.GameQueue CardboardBoxShelter()
        {
            var q = new GameQueue.GameQueue();
            q.Add(new GCNarrative("You find a small shelter made of cardboard boxes."));
            q.Add(new GCChoices()
                .Add("This man probably used to the unfairness of life, rob him", (sq) =>
                {
                    sq.Add(new GCAddStat(EnumPlayerStats.MENTAL, -5, AddStatMode.FLOAT_TEXT_ALERT));
                    sq.Add(new GCAddItem(ScavengingItems.GetFoodItem())); 
                })
                .Add("Decently walk further", (sq) =>
                {
                    sq.Add(new GCAddStat(EnumPlayerStats.MENTAL, 5, AddStatMode.FLOAT_TEXT_ALERT));
                })
                .Add("Leave some food here", (sq) =>
                {
                    sq.Add(new GCAddStat(EnumPlayerStats.MENTAL, 20, AddStatMode.FLOAT_TEXT_ALERT));
                    // Add code to remove a food item from the player's inventory
                })
            );
            return q;
        }

    }
}