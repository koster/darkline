using System;
using System.Collections.Generic;
using Source.Util;

namespace Source.Game.Deliveries
{
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
        public DeliveryTimelinePoint finalPoint;
        public float scavengingChance = 0.75f;
        public int dangerTime = 100;
        public List<Func<GameQueue.GameQueue>> dangerLadder;
    }

    public static class DeliveriesDatabase
    {
        public static List<DeliveryDefinition> all = new List<DeliveryDefinition>();

        public static void Initialize()
        {
            var delivery0 = new DeliveryDefinition  // tutorial - survival
            {
                item = ItemDatabase.suitcase,
                length = 5,
                dangerTime = 200,
                timeline = new List<DeliveryTimelinePoint>
                {
                    new() { at = 1, queue = Story_Tutorial.Hunger },
                    new() { at = 3, queue = Story_Atmosphere.all.GetRandom() }
                },
                finalPoint = new DeliveryTimelinePoint
                {
                    queue = Story_Main.Delivery0
                }
            };
            var delivery1 = new DeliveryDefinition // tutorial - combat
            {
                item = ItemDatabase.shotgun,
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
                        queue = Story_Atmosphere.FindPlank
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
                finalPoint = new DeliveryTimelinePoint
                {
                    queue = Story_Main.Delivery1
                }
            };
            var delivery2 = new DeliveryDefinition // enemy bleeding
            {
                item = ItemDatabase.powder,
                length = 12,
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
                finalPoint = new DeliveryTimelinePoint
                {
                    queue = Story_Main.Delivery2
                }
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
                        queue = Story_Atmosphere.FindKnife
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
                finalPoint = new DeliveryTimelinePoint
                {
                    queue = Story_Main.Delivery3
                }
            };
            all.Add(delivery0);
            var delivery4 = new DeliveryDefinition
            {
                item = ItemDatabase.pills,
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
                        queue = Story_Atmosphere.FindPistol
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
                finalPoint = new DeliveryTimelinePoint
                {
                    queue = Story_Main.Delivery4
                }
            };
            all.Add(delivery1);
            all.Add(delivery2);
            all.Add(delivery3);
            all.Add(delivery4);
        }
    }
}