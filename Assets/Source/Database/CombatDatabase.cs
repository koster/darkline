using System;
using System.Collections.Generic;
using Source.Commands;

namespace Source.Game.Deliveries
{
    public static class CombatDatabase
    {
        public static List<Func<GameQueue.GameQueue>> dangerLadder = new();
        
        public static void Initialize()
        {
            dangerLadder.Add(Danger_Combat01);
            dangerLadder.Add(Danger_Combat02);
            dangerLadder.Add(Danger_Combat03);
        }
        
        public static GameQueue.GameQueue Combat01_Delivery()
        {
            var combat = new Combat();
            
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.boss_general });
            
            var q = new GameQueue.GameQueue();
            q.Add(new GCAlert("You have been ambushed!"));
            q.Add(new GCAmbush(combat));
            return q;
        }
        
        public static GameQueue.GameQueue Combat02_Delivery()
        {
            var combat = new Combat();
            
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.man });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.child });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.man });
            
            var q = new GameQueue.GameQueue();
            q.Add(new GCAlert("You have been ambushed!"));
            q.Add(new GCAmbush(combat));
            return q;
        }
        
        public static GameQueue.GameQueue TestCombat()
        {
            var combat = new Combat();
            
            // combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.meat });
            // combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.sewer });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.man });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.man });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.man });
            // combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.ball });
            // combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.child });
            // combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.child });
            
            var q = new GameQueue.GameQueue();
            q.Add(new GCAlert("You have been ambushed!"));
            q.Add(new GCAmbush(combat));
            return q;
        }

        
        public static GameQueue.GameQueue Scavenge_Combat01()
        {
            var combat = new Combat();
            
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.man });
            
            var q = new GameQueue.GameQueue();
            q.Add(new GCAlert("You have been ambushed!"));
            q.Add(new GCAmbush(combat));
            return q;
        }

        public static GameQueue.GameQueue Danger_Combat01()
        {
            var combat = new Combat();
            
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.man });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.man });
            
            var q = new GameQueue.GameQueue();
            q.Add(new GCAlert("You have been ambushed!"));
            q.Add(new GCAmbush(combat));
            return q;
        }
        
        public static GameQueue.GameQueue Danger_Combat02()
        {
            var combat = new Combat();
            
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.child });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.man });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.child });
            
            var q = new GameQueue.GameQueue();
            q.Add(new GCAlert("You have been ambushed!"));
            q.Add(new GCAmbush(combat));
            return q;
        }
        
        public static GameQueue.GameQueue Danger_Combat03()
        {
            var combat = new Combat();
            
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.meat });
            
            var q = new GameQueue.GameQueue();
            q.Add(new GCAlert("You have been ambushed!"));
            q.Add(new GCAmbush(combat));
            return q;
        }

        public static GameQueue.GameQueue Combat_Cemetery()
        {
            var combat = new Combat();
            
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.dog });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.man });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.child });
            
            var q = new GameQueue.GameQueue();
            q.Add(new GCAlert("You have been ambushed!"));
            q.Add(new GCAmbush(combat));
            return q;
        }

        public static GameQueue.GameQueue Combat_ShoppingMall()
        {
            var combat = new Combat();
            
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.man });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.man });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.man });
            
            var q = new GameQueue.GameQueue();
            q.Add(new GCAlert("You have been ambushed!"));
            q.Add(new GCAmbush(combat));
            return q;
        }
        
        public static GameQueue.GameQueue Combat_3ChildDemons()
        {
            var combat = new Combat();
            
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.child });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.child });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.child });
            
            var q = new GameQueue.GameQueue();
            q.Add(new GCAlert("You have been ambushed!"));
            q.Add(new GCAmbush(combat));
            return q;
        }

        public static GameQueue.GameQueue Combat_StrangeNoiseEnemies()
        {
            var combat = new Combat();
            
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.man });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.man });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.child });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.child });
            
            var q = new GameQueue.GameQueue();
            q.Add(new GCAlert("You have been ambushed!"));
            q.Add(new GCAmbush(combat));
            return q;
        }

        public static GameQueue.GameQueue Combat_Raiders()
        {
            var combat = new Combat();
            
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.child });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.dog });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.child });
            
            var q = new GameQueue.GameQueue();
            q.Add(new GCAlert("You have been ambushed!"));
            q.Add(new GCAmbush(combat));
            return q;
        }

        public static GameQueue.GameQueue CombatDelivery01_01()
        {
            var combat = new Combat();
            
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.man });
            
            var q = new GameQueue.GameQueue();
            q.Add(new GCAlert("You have been ambushed!"));
            q.Add(new GCAmbush(combat));
            return q;
        }

        public static GameQueue.GameQueue Danger_CombatBleeder()
        {
            var combat = new Combat();
            
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.man });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.spiker, say = new List<string> { "I will bleed you!" } });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.man });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.dog });
            
            var q = new GameQueue.GameQueue();
            q.Add(new GCAlert("You have been ambushed!"));
            q.Add(new GCAmbush(combat));
            return q;
        }

        public static GameQueue.GameQueue Danger_CombatHeavy()
        {
            var combat = new Combat();
            
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.man });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.spiker, say = new List<string> { "I will bleed you!" } });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.man });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.dog });
            
            var q = new GameQueue.GameQueue();
            q.Add(new GCAlert("You have been ambushed!"));
            q.Add(new GCAmbush(combat));
            return q;
        }

        public static GameQueue.GameQueue Danger_CombatSewer()
        {
            var combat = new Combat();
            
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.dog });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.sewer, say = new List<string> { "Do you feel sluggish?", "It's because of my smell..." } });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.dog });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.dog });
            
            var q = new GameQueue.GameQueue();
            q.Add(new GCAlert("You have been ambushed!"));
            q.Add(new GCAmbush(combat));
            return q;
        }

        public static GameQueue.GameQueue Danger_CombatAfterSewer()
        {
            var combat = new Combat();

            combat.Loot = () => new GameQueue.GameQueue()
                .Add(new GCAddItem(ItemDatabase.chewingTobacco, 1))
                .Add(new GCAddItem(ItemDatabase.dust, 1));
            
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.dog });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.man });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.child });
            
            var q = new GameQueue.GameQueue();
            q.Add(new GCAlert("You have been ambushed!"));
            q.Add(new GCAmbush(combat));
            return q;
        }

        public static GameQueue.GameQueue AmbushedByKids()
        {
            var combat = new Combat();

            combat.Loot = () => new GameQueue.GameQueue()
                .Add(new GCAddItem(ItemDatabase.ammo, 1))
                .Add(new GCAddItem(ItemDatabase.dust, 1));
            
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.child });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.child });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.child });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.child });
            
            var q = new GameQueue.GameQueue();
            q.Add(new GCAlert("You have been ambushed!"));
            q.Add(new GCAmbush(combat));
            return q;
        }

        public static GameQueue.GameQueue Danger_CombatSewerBleeder()
        {
            var combat = new Combat();

            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.spiker });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.sewer });
            
            var q = new GameQueue.GameQueue();
            q.Add(new GCAlert("You have been ambushed!"));
            q.Add(new GCAmbush(combat));
            return q;
        }

        public static GameQueue.GameQueue Danger_CombatMeat()
        {
            var combat = new Combat();

            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.meat });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.spiker });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.man });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.man });
            
            var q = new GameQueue.GameQueue();
            q.Add(new GCAlert("You have been ambushed!"));
            q.Add(new GCAmbush(combat));
            return q;
        }

        public static GameQueue.GameQueue Danger_CombatDogs()
        {
            var combat = new Combat();

            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.dog });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.dog });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.dog });
            combat.enemies.Add(new CombatEnemy { definition = EnemyDatabase.dog });
            
            var q = new GameQueue.GameQueue();
            q.Add(new GCAlert("You have been ambushed!"));
            q.Add(new GCAmbush(combat));
            return q;
        }
    }
}