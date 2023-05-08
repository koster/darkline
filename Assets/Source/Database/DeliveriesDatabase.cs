using System;
using System.Collections.Generic;
using Source.Util;

namespace Source.Game.Deliveries
{
    public enum DeliveryFeature
    {
        CAR_AT_THE_START,
        CAR_AT_THE_END
    }
    
    public class DeliveryTimelinePoint
    {
        public int at;
        public Func<GameQueue.GameQueue> queue;
    }

    public class DeliveryDefinition
    {
        public InventoryItemDefinition item;
        public int length;
        public List<DeliveryTimelinePoint> timeline;
        public Func<GameQueue.GameQueue> introPoint;
        public Func<GameQueue.GameQueue> finalPoint;
        public float scavengingChance = 0.75f;
        public int dangerTime = 100;
        public List<Func<GameQueue.GameQueue>> dangerLadder;
        public List<DeliveryFeature> features = new List<DeliveryFeature>();
    }

    public static class DeliveriesDatabase
    {
        public static List<DeliveryDefinition> all = new List<DeliveryDefinition>();

        public static void Initialize()
        {
            var delivery0 = new DeliveryDefinition  // tutorial - survival
            {
                item = ItemDatabase.suitcase,
                length = 7,
                dangerTime = 200,
                features = new List<DeliveryFeature>()
                {
                    DeliveryFeature.CAR_AT_THE_START
                },
                timeline = new List<DeliveryTimelinePoint>
                {
                    new() { at = 1, queue = Story_Tutorial.Painkillers },
                    new() { at = 3, queue = Story_Atmosphere.all.GetRandom() },
                    new() { at = 4, queue = Story_Tutorial.WhatIsThisPlace },
                    new() { at = 6, queue = Story_Tutorial.Lunch }
                },
                finalPoint = Story_Main.Delivery0_Doctor
            };
            var delivery1 = new DeliveryDefinition // tutorial - combat
            {
                item = ItemDatabase.powder,
                length = 10,
                dangerLadder = new List<Func<GameQueue.GameQueue>>
                {
                    CombatDatabase.Danger_Combat01,
                    CombatDatabase.Danger_Combat02,
                    CombatDatabase.Danger_Combat03
                },
                timeline = new List<DeliveryTimelinePoint>
                {
                    new()
                    {
                        at = 1,
                        queue = Story_Atmosphere.SomeoneIsLooking
                    },
                    new()
                    {
                        at = 2,
                        queue = Story_Atmosphere.Footsteps
                    },
                    new()
                    {
                        at = 3,
                        queue = CombatDatabase.CombatDelivery01_01
                    },
                    new()
                    {
                        at = 4,
                        queue = Story_Tutorial.FindPlank
                    },
                    new()
                    {
                        at = 6,
                        queue = Story_Scavenging.GasStation
                    },
                    new()
                    {
                        at = 8,
                        queue = Story_Atmosphere.SomeoneIsLooking
                    }
                },
                introPoint = Story_Main.Delivery1_WifeIntro,
                finalPoint = Story_Main.Delivery1_WifeOutro
            };
            var delivery2 = new DeliveryDefinition // enemy bleeding
            {
                item = ItemDatabase.letter,
                length = 12,
                features = new List<DeliveryFeature> { DeliveryFeature.CAR_AT_THE_START },
                dangerLadder = new List<Func<GameQueue.GameQueue>>
                {
                    CombatDatabase.Danger_CombatBleeder,
                    CombatDatabase.Danger_Combat02,
                    CombatDatabase.Danger_Combat03
                },
                timeline = new List<DeliveryTimelinePoint>
                {
                    new()
                    {
                        at = 1,
                        queue = ScavengingEventsDatabase.all.GetRandom()
                    },
                    new()
                    {
                        at = 2,
                        queue = Story_Atmosphere.Blood
                    },
                    new()
                    {
                        at = 3,
                        queue = Story_Atmosphere.SomeoneIsLooking
                    },
                    new()
                    {
                        at = 4,
                        queue = Story_Atmosphere.BloodTracks
                    },
                    new()
                    {
                        at = 5,
                        queue = Story_Atmosphere.FoundDust
                    },
                    new()
                    {
                        at = 7,
                        queue = ScavengingEventsDatabase.all.GetRandom()
                    },
                    new()
                    {
                        at = 10,
                        queue = ScavengingEventsDatabase.all.GetRandom()
                    }
                },
                introPoint = Story_Main.Delivery2_FriendIntro,
                finalPoint = Story_Main.Delivery2_FriendOutro
            };
            var delivery3 = new DeliveryDefinition
            {
                item = ItemDatabase.uniform,
                length = 13,
                dangerLadder = new List<Func<GameQueue.GameQueue>>
                {
                    CombatDatabase.Danger_CombatSewer,
                    CombatDatabase.Danger_CombatAfterSewer
                },
                timeline = new List<DeliveryTimelinePoint>
                {
                    new()
                    {
                        at = 1,
                        queue = ScavengingEventsDatabase.all.GetRandom()
                    },
                    new()
                    {
                        at = 2,
                        queue = Story_Atmosphere.DisgustingSmell
                    },
                    new()
                    {
                        at = 3,
                        queue = RandomEventsDatabase.all.GetRandom()
                    },
                    new()
                    {
                        at = 4,
                        queue = Story_Atmosphere.FindHolyWater
                    },
                    new()
                    {
                        at = 5,
                        queue = Story_Tutorial.FindKnife
                    },
                    new()
                    {
                        at = 10,
                        queue = RandomEventsDatabase.all.GetRandom()
                    },
                    new()
                    {
                        at = 11,
                        queue = ScavengingEventsDatabase.all.GetRandom()
                    }
                },
                introPoint = Story_Main.Delivery3_MilitaryIntro,
                finalPoint = Story_Main.Delivery3_MilitaryOutro
            };
            all.Add(delivery0);
            var delivery4 = new DeliveryDefinition
            {
                item = ItemDatabase.blood,
                length = 12,
                dangerLadder = new List<Func<GameQueue.GameQueue>>
                {
                    CombatDatabase.Danger_CombatDogs,
                    CombatDatabase.Danger_CombatMeat
                },
                timeline = new List<DeliveryTimelinePoint>
                {
                    new()
                    {
                        at = 1,
                        queue = Story_Tutorial.FindPistol
                    },
                    new()
                    {
                        at = 2,
                        queue = CombatDatabase.AmbushedByKids
                    },
                    new()
                    {
                        at = 3,
                        queue = RandomEventsDatabase.all.GetRandom()
                    },
                    new()
                    {
                        at = 5,
                        queue = Story_Atmosphere.Dogs
                    },
                    new()
                    {
                        at = 8,
                        queue = RandomEventsDatabase.all.GetRandom()
                    },
                    new()
                    {
                        at = 10,
                        queue = RandomEventsDatabase.all.GetRandom()
                    }
                },
                introPoint = Story_Main.Delivery4_SelfIntro,
                finalPoint = Story_Main.Delivery4_SelfOutro
            };
            all.Add(delivery1);
            all.Add(delivery2);
            all.Add(delivery3);
            all.Add(delivery4);
        }
    }
}