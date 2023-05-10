using System.Collections.Generic;
using Source.Commands;
using Source.GameQueue;
using Source.Util;
using UnityEngine;

namespace Source.Game.Deliveries
{
    public static class Story_Main
    {
        public static GameQueue.GameQueue VendingMachine()
        {
            var q = new GameQueue.GameQueue();

            q.Add(new GCUIState(UI_STATES.NARRATIVE_ONLY));
            
            q.Add(new GCImage(null));
            q.Add(new GCTintBackground(Color.black, 2f));
            q.Add(new GCImage("narrative/vending_machine".LoadSprite()));

            q.Add(new GCCall(DeliveryUI.Reset));
            
            var lines = new List<string>()
            {
                "You see an ominous vending machine on the street corner.", 
                "This vending machine again.", 
                "You stumble upon a familliar vending machine.", 
                "Vending machine doesn't seem so creepy now.", 
                "You approach a familliar vending machine."
            };
            
            q.Add(new GCNarrative(lines.GetSafely(global::Game.world.deliveryIndex)));
            q.Add(new GCNarrative($"You have {(global::Game.world.inventory.GetItemAmount(ItemDatabase.coin))} COIN."));

            /*
             * stamina health hunger
             * health  mental thirst
             * thirst  mental hunger
             * stamina hunger health
             */

            var vend1 = new List<InventoryItemDefinition> { ItemDatabase.beer,              ItemDatabase.painkillers,  ItemDatabase.burger      };
            var vend2 = new List<InventoryItemDefinition> { ItemDatabase.bandage,           ItemDatabase.vape,         ItemDatabase.waterBottle };
            var vend3 = new List<InventoryItemDefinition> { ItemDatabase.crowbar,           ItemDatabase.cannedCoffee, ItemDatabase.painkillers };
            var vend4 = new List<InventoryItemDefinition> { ItemDatabase.tranquilizers,     ItemDatabase.energyDrink,  ItemDatabase.lunch       };

            var vendingByDeliver = new List<List<InventoryItemDefinition>> { vend1, vend2, vend3, vend4 };
            
            Vend(q, vendingByDeliver.GetSafely(global::Game.world.deliveryIndex));
            
            q.Add(new GCUIState(UI_STATES.NARRATIVE_ONLY));
            
            q.Add(new GCImageHide());

            return q;
        }

        static void Vend(GameQueue.GameQueue q, List<InventoryItemDefinition> inventoryItemDefinitions)
        {
            var gc = new GCChoices(UI_STATES.PICK_OPTION);
            foreach (var v in inventoryItemDefinitions)
            {
                gc.Add(v.name, sq =>
                {
                    sq.Add(new GCAddItem(v, 1));
                    sq.Add(new GCAddItem(ItemDatabase.coin, -1));

                    if (global::Game.world.inventory.GetItemAmount(ItemDatabase.coin) == 1)
                        gc.Finish();
                }).DoesNotExit().SetDetailString("[COSTS 1 COIN]");
            }

            gc.Add("Don't buy anything (save the coin)", q => { });

            q.Add(gc);
        }

        public static GameQueue.GameQueue Intro()
        {
            var q = new GameQueue.GameQueue();

            q.Add(new GCImage(null, instant: true));
            q.Add(new GCTintBackground(Color.black, 0f));
            q.Add(new GCUIState(UI_STATES.NARRATIVE_ONLY));

            q.Add(new GCNarrative("When will it be truly over?"));

            q.Add(new GCBackgroundMusic("sound/silent_hill_vibe", 0.5f));
            
            q.Add(new GCImage("narrative/hometown_3".LoadSprite(), offsetY: 0));
            
            q.Add(new GCNarrative("Hometown."));
            q.Add(new GCNarrative("Time does fly, huh?"));
            q.Add(new GCNarrative("It's been five years since you've been discharged from military service."));
            q.Add(new GCNarrative("Yet, it still feels like yesterday."));
            q.Add(new GCNarrative("You still see the nightmares."));
            q.Add(new GCNarrative("You can sill hear the gunshots at night."));
            
            q.Add(new GCChoices(UI_STATES.PICK_OPTION_NO_STATS)
                .Add("Think about why you're here.", (q) =>
                {
                    q.Add(new GCNarrative("I wasn't planning on ever coming back here..."));
                    q.Add(new GCNarrative("But I got a letter, from my friend Red."));
                    q.Add(new GCNarrative("Lieutenant Redcliff."));
                    q.Add(new GCNarrative("We haven't spoke since the war."));
                    q.Add(new GCNarrative("But a few months ago, I got a letter from him."));
                    q.Add(new GCNarrative("He sounded scared."));
                    q.Add(new GCNarrative("I wrote him back, but I never sent the letter."));
                    q.Add(new GCNarrative("I thought, well... I don't know what I was thinking."));
                    q.Add(new GCNarrative("But now, I've gathered some stuff and came all the way here."));
                }).DoesNotExit()
                .Add("Think about hometown.", (q) =>
                {
                    q.Add(new GCNarrative("'Whispering Pines' that's the town name."));
                    q.Add(new GCNarrative("You always thought it was stupid, but now come to think about it. It does make sense."));
                    q.Add(new GCNarrative("When I look at it right now, it feels as if I never left."));
                }).DoesNotExit()
                .Add("Think about service.", (q) =>
                {
                    q.Add(new GCNarrative("The service wasn't that bad."));
                    q.Add(new GCNarrative("When the war started, I volunteered."));
                    q.Add(new GCNarrative("I guess I wanted to prove myself?"));
                    q.Add(new GCNarrative("Emily just went silent when she heard about this."));
                    q.Add(new GCNarrative("I told her I'll be back, we'll have kids and all that family stuff..."));
                    q.Add(new GCNarrative("Then I got a telegram, \"I've got married John, don't write me.\""));
                    q.Add(new GCNarrative("And we never spoke ever since."));
                }).DoesNotExit()
                .Add("Go back to the car.", (q) =>
                {
                    q.Add(new GCNarrative("On your way, you see something glittering in the leaves."));
                    q.Add(new GCNarrative("A coin?"));
                    q.Add(new GCImage("items/coin".LoadSprite(), offsetY: 0));
                    q.Add(new GCNarrative("Strange one... Is this a pentagram?"));
                }));

            // q.Add(new GCImage("narrative/hometown_4".LoadSprite(), offsetY: 0));
            
            // q.Add(new GCNarrative("The weather is not quiet rainy, but it's damp, it's early morning."));
            // q.Add(new GCNarrative("You get inside your car and start the engine."));
            
            q.Add(new GCImage(null, instant: true, offsetY: 0));
            q.Add(new GCTintBackground(Color.black, 0f));
            
            q.Add(new GCSound("sound/car_engine"));
            q.Add(new GCWait(3f));

            q.Add(new GCBackgroundMusic("sound/silent_hill_vibe", 0f));
            
            q.Add(new GCQueue(Intro_Phone()));

            return q;
        }

        public static GameQueue.GameQueue Intro_Phone()
        {
            var q = new GameQueue.GameQueue();
            
            q.Add(new GCImage(null, offsetY: 0));
            q.Add(new GCTintBackground(Color.black, 1f));
            q.Add(new GCUIState(UI_STATES.NARRATIVE_ONLY));
            
            // q.Add(new GCNarrative("You reach the town. But you're running out of gas."));
            // q.Add(new GCNarrative("You see a gas station, and you stop to refill."));
            
            // q.Add(new GCImage("narrative/gas_station".LoadSprite(), offsetY: 0));
            
            q.Add(new GCNarrative("Some time later."));
            q.Add(new GCNarrative("The gas station looks like it's been abandoned... Years ago."));

            q.Add(new GCSound("sound/phone_vibrate", 0.25f));

            q.Add(new GCNarrative("You hear a phone vibrating. But you don't have one."));
            q.Add(new GCNarrative("You look around and notice a vending machine..."));

            q.Add(new GCImage("narrative/vending_machine".LoadSprite()));

            q.Add(new GCNarrative("The phone is vibrating inside the vending machine..."));

            q.Add(new GCSound("sound/phone_vibrate", 0.25f));
            
            q.Add(new GCNarrative("You remember that coin..."));
            q.Add(new GCNarrative("You toss the coin into the vending machine."));

            q.Add(new GCSound("sound/coin_vending", 0.25f));
            
            q.Add(new GCNarrative("Phone falls out of the slot."));
            
            q.Add(new GCImage("narrative/phone_2".LoadSprite()));
            q.Add(new GCTintBackground(Color.white, 3f));

            // tbd search the car look around meet a person then find a phone
            // search the car
            // 1. a phone
            // 2. a... letter? SH2 duh
            //  Clain, I never thought I'd say this, but I'm scared.
            //  I've tried to be strong, but the burden has become too heavy, and I feel as if I'm sinking into an abyss that I can't escape.
            //  I've been getting those calls... Have you? 
            //  I need you here, Clain. 
            //
            //  Red.
            // 3. pack of cigarettes
            // 4. a lighter
            // 5. water bottle
            // 6. flare (used to blind the monsters for 1 turn)
            // 7. painkillers
            
            q.Add(new GCSound("sound/buzz"));
            q.Add(new GCNarrative("Phone: *crackling*"));
            q.Add(new GCNarrative("Phone: Good morning John."));
            
            q.Add(new GCNarrative("John: How do you know my name?"));

            q.Add(new GCNarrative("Phone: We've been waiting for you, John."));
            
            q.Add(new GCSound("sound/buzz"));
            q.Add(new GCNarrative("Phone: *crackling*"));

            q.Add(new GCImageHide());
            
            q.Add(new GCNarrative("Phone: Darkline."));
            q.Add(new GCNarrative("John: What?"));
            
            q.Add(new GCSound("sound/buzz"));
            q.Add(new GCNarrative("Phone: *crackling*"));
            
            q.Add(new GCNarrative("Phone: The suitcase is in your car."));
            q.Add(new GCNarrative("Phone: You need to make a delivery."));
            q.Add(new GCNarrative("Phone: Deliver the suitcase to the Hospital, 33rd street."));
            q.Add(new GCNarrative("Phone: You will receive new instructions after this delivery."));
            
            q.Add(new GCSound("sound/phone_hangup", 0.5f));
            q.Add(new GCWait(1f));

            q.Add(new GCSound("sound/car_signal"));
            
            q.Add(new GCImage("items/suitcase".LoadSprite()));
            
            q.Add(new GCNarrative("Is this some type of spy operation?"));
            
            q.Add(new GCImageHide());
            return q;
        }

        public static GameQueue.GameQueue Delivery0_Doctor()
        {
            var q = new GameQueue.GameQueue();

            q.Add(new GCUIState(UI_STATES.NARRATIVE_ONLY));

            q.Add(new GCImage(null));

            q.Add(new GCNarrative("Turns out the Hospital is just around the corner."));
            q.Add(new GCNarrative("The brown brick building looks like it has seen a lot."));
            q.Add(new GCNarrative("John: Hello? There's, uhm a delivery.."));
            
            q.Add(new GCImage("narrative/doctor".LoadSprite()));
            
            q.Add(new GCNarrative("Doctor: Right..."));
            q.Add(new GCNarrative("Doctor: John Clain?"));
            q.Add(new GCNarrative("John: Yes."));
            q.Add(new GCNarrative("Doctor opens the suitcase. It has a syringe in it."));
            q.Add(new GCNarrative("Doctor: Yes... Yes..."));
            q.Add(new GCNarrative("Doctor: In fact we've been waiting for you here, John."));
            q.Add(new GCNarrative("John: Oh yeah? So I had an appointment?"));
            q.Add(new GCNarrative("Doctor: Please lie down on the bed."));

            q.Add(new GCImage("narrative/syringe".LoadSprite()));

            q.Add(new GCNarrative("Doctor: You are not one of them?! Don't you?"));
            
            q.Add(new GCNarrative("The glowing purple liquid in a syringe looks oddly familiar to you."));
            q.Add(new GCNarrative("You instinctively take a step back."));
            
            q.Add(new GCImage("narrative/doctor".LoadSprite()));
            
            q.Add(new GCNarrative("John: I think I'm feeling better doc."));
            q.Add(new GCNarrative("John: Cancel the appointment."));
            q.Add(new GCNarrative("John: I've got to go!"));
            
            q.Add(new GCNarrative("Doctor: There is nowhere to go, you are dead, John."));

            q.Add(new GCNarrative("John: Back off!"));
            q.Add(new GCNarrative("John: I'm leaving! I'm going home!"));

            q.Add(new GCImageHide());
            
            q.Add(new GCNarrative("You swiftly run out of the Hospital. You can hear the doctors laugh behind you."));
            q.Add(new GCNarrative("What is this nightmare?"));
            
            q.Add(new GCSound("sound/creepy_ambient_1"));

            q.Add(new GCImageHide());

            return q;
        }

        public static GameQueue.GameQueue Delivery1_WifeIntro()
        {
            var q = new GameQueue.GameQueue();

            q.Add(new GCSound("sound/phone_vibrate"));

            q.Add(new GCWait(1f));
            
            q.Add(new GCSound("sound/buzz"));
            q.Add(new GCNarrative("Phone: *crackling*"));

            q.Add(new GCNarrative("John: You think this is funny?!"));
            q.Add(new GCNarrative("John: You drugged me?"));
            q.Add(new GCNarrative("John: I'm going to call the police!"));
            q.Add(new GCNarrative("John: Better still, I'll find you myself and..."));

            q.Add(new GCSound("sound/buzz"));
            q.Add(new GCNarrative("Phone: *crackling*"));
            
            q.Add(new GCNarrative("Phone: You can try. It's not going to work."));
            
            q.Add(new GCSound("sound/buzz"));
            q.Add(new GCNarrative("Phone: *crackling*"));

            q.Add(new GCNarrative("For some reason. I belive him..."));

            q.Add(new GCNarrative("Phone: Your next delivery is in your backpack."));
            q.Add(new GCNarrative("Phone: The wedding ring."));
            
            return q;
        }

        public static GameQueue.GameQueue Delivery1_WifeOutro()
        {
            var q = new GameQueue.GameQueue();

            q.Add(new GCUIState(UI_STATES.NARRATIVE_ONLY));

            q.Add(new GCImage(null));

            q.Add(new GCNarrative("You approach a neat one-story house surrounded by fields of blooming flowers."));
            q.Add(new GCNarrative("The house itself is covered in thorns that pierce your soul, reaching deep into your heart."));

            q.Add(new GCImage("narrative/woman".LoadSprite()));
            q.Add(new GCSound("sound/guitar_noise_1", 0.25f));
            
            q.Add(new GCNarrative("She opens the door, her light greeting getting lost in a coughing fit."));

            q.Add(new GCImage("narrative/woman_and_man".LoadSprite()));

            q.Add(new GCNarrative("He runs up to her."));
            q.Add(new GCNarrative("Man: Don't worry, my dear, I'll take care of everything."));
            q.Add(new GCNarrative("Man: Oh, John. Have you finished wasting your time on madness and decided to show some care."));
            q.Add(new GCNarrative("Man: I hope this new skill will be useful to you. But here, I taking care of her for a long time."));

            q.Add(new GCChoices(UI_STATES.PICK_OPTION_NO_STATS)
                .Add("Why are you coughing Emily?", (q) =>
                {
                    q.Add(new GCNarrative("Emily: Yeah."));
                    q.Add(new GCNarrative("Emily: *cough*"));
                    q.Add(new GCNarrative("Emily: I've got sick a few years ago."));
                    q.Add(new GCNarrative("Emily: They say it has something to do with that military base you were stationed at."));
                    q.Add(new GCNarrative("Emily: But it's a long story, we couldn't prove anything..."));
                }).DoesNotExit()
                .Add("Give her the ring.", (q) =>
                {
                    q.Add(new GCNarrative("You pass Emily the ring."));
                    q.Add(new GCNarrative("She takes it and you see a tear forming in her eyes."));
                }));

            q.Add(new GCNarrative("Man: We've had enough John!"));
            q.Add(new GCNarrative("Man: For the love of good, stop stressing about this lunatic, or you'll get worse!"));
            q.Add(new GCNarrative("Man: You're still here John?!"));
            q.Add(new GCNarrative("Man: Ok you've got what you wanted, now leave!"));
            
            q.Add(new GCImage(null));
            
            q.Add(new GCNarrative("Silent coughs are still heard after you closed the door"));
            
            q.Add(new GCImageHide());

            return q;
        }

        public static GameQueue.GameQueue Delivery2_FriendIntro()
        {
            var q = new GameQueue.GameQueue();

            q.Add(new GCSound("sound/phone_vibrate"));

            q.Add(new GCWait(2f));

            q.Add(new GCSound("sound/phone_vibrate"));

            q.Add(new GCWait(2f));
            
            q.Add(new GCSound("sound/buzz"));
            q.Add(new GCNarrative("Phone: *crackling*"));
            q.Add(new GCNarrative("Phone: You don't have to pick up the phone."));
            q.Add(new GCNarrative("John: Am I crazy?"));
            q.Add(new GCNarrative("Phone: No."));
            q.Add(new GCNarrative("Phone: It's the darkline."));
            q.Add(new GCNarrative("John: Darkline?"));
            q.Add(new GCNarrative("Phone: Go to Redcliff now."));
            q.Add(new GCNarrative("Phone: He knows."));
            
            return q;
        }
        
        public static GameQueue.GameQueue Delivery2_FriendOutro()
        {
            var q = new GameQueue.GameQueue();
            
            // why are we here again?
            
            // the project was military
            // john is aware of unreality
            // john never sent a letter

            q.Add(new GCUIState(UI_STATES.NARRATIVE_ONLY));

            q.Add(new GCImage(null));

            q.Add(new GCNarrative("You open the doors of the mansion and see Lieutenant Redcliff."));

            q.Add(new GCSound("sound/guitar_noise_2_slow", 0.25f));

            q.Add(new GCImage("narrative/redcliff".LoadSprite()));
            
            q.Add(new GCNarrative("He's wearing the uniform that you wouldn't mistake for anything else, it smells of the jungle, napalm, and inhumanity."));
            
            q.Add(new GCWait(1f));
            
            q.Add(new GCNarrative("Redcliff: Clain?"));
            q.Add(new GCNarrative("You're surprised to find out that the gunshot that tore apart the Lieutenant's face doesn't prevent him from speaking."));
            q.Add(new GCNarrative("John: A letter..."));
            q.Add(new GCNarrative("Redcliff: How touching."));
            q.Add(new GCNarrative("Redcliff: Working at the postal office now Clain? After you've been discharged."));
            
            q.Add(new GCChoices(UI_STATES.PICK_OPTION_NO_STATS)
                .Add("\"I'm sorry I never replied\"", (q) =>
                {
                    q.Add(new GCNarrative("John: I'm sorry Red. Sorry, I never replied to your letter..."));
                    q.Add(new GCNarrative("Redcliff: Apologies accepted."));
                    q.Add(new GCNarrative("Redcliff: There is nothing to worry about now Clain."));
                }).DoesNotExit()
                .Add("\"None of this is real\"", (q) => {
                }));
            
            q.Add(new GCNarrative("John: None of this is real, Red."));
            q.Add(new GCNarrative("Redcliff: What are you talking about?"));
            q.Add(new GCNarrative("John: You're dead."));
            q.Add(new GCNarrative("Redcliff: Aren't you dead too?"));
            q.Add(new GCNarrative("John: No. I'm alive."));
            q.Add(new GCNarrative("Redcliff: How do you know that?"));
            q.Add(new GCNarrative("Redcliff: After all they've done to us?"));
            q.Add(new GCNarrative("Redcliff: Don't you remember? The Darkline."));
            q.Add(new GCNarrative("John: Everyone keeps mentioning that but..."));

            q.Add(new GCSound("sound/gun_cock"));
            q.Add(new GCImage("narrative/gun_pixelated_darkened".LoadSprite()));
            
            q.Add(new GCNarrative("Redcliff: Who you've been talking to! Traitor!"));
            q.Add(new GCNarrative("Redcliff pulls out a Colt 1911 and points it at you."));

            q.Add(new GCChoices(UI_STATES.PICK_OPTION_NO_STATS)
                .Add("\"Come on, shoot\"", (q) =>
                {
                    q.Add(new GCNarrative("Redcliff: You've grown some balls, Clain?"));
                    q.Add(new GCNarrative("Redcliff: Think I won't pull the trigger because we used to be friends?"));
                    q.Add(new GCNarrative("John: Red-"));
                    
                    q.Add(new GCSound("sound/combat/pistol"));
                    
                    q.Add(new GCNarrative("Redcliff shoots."));
                    q.Add(new GCNarrative("You feel a sting in your chest. You've been shot before. You know the feeling."));
                    q.Add(new GCNarrative("But instead of blood, you see a black goo pouring out of your wound."));
                    q.Add(new GCNarrative("Strength is leaving your body, you drop to the ground."));
                    q.Add(new GCNarrative("Redcliff: Goodnight, Clain."));
                    
                    q.Add(new GCTintBackground(Color.black, 3f));
                    
                    q.Add(new GCImage(null));
                    q.Add(new GCImageHide());
                    
                    q.Add(new GCNarrative("You wake up in a pool of black goo."));
                    q.Add(new GCNarrative("You don't seem to have a wound anymore."));
                    q.Add(new GCNarrative("Was it all dream?"));
                    q.Add(new GCNarrative("You find a bullet in your pocket."));
                    q.Add(new GCAddItem(ItemDatabase.ammo, 1));
                })
                .Add("\"No one! It's the calls!\"", (q) =>
                {
                    q.Add(new GCImage("narrative/redcliff".LoadSprite()));

                    q.Add(new GCNarrative("Redcliff: You've been getting them too!"));
                    q.Add(new GCNarrative("Redcliff: I think it's because of what happened in the Nam."));
                    q.Add(new GCNarrative("Redcliff: They've been doing something to us."));
                    q.Add(new GCNarrative("Redcliff: But they won't get anything from me..."));
                    q.Add(new GCNarrative("Redcliff: Nor you, nor them!"));

                    q.Add(new GCNarrative("John: Wait! You've got to help me!"));
                    
                    q.Add(new GCNarrative("Redcliff: Hahaha!"));
                    q.Add(new GCNarrative("Redcliff: Only you can help yourself, John."));
                    q.Add(new GCNarrative("Redcliff: It's the first rule."));

                    q.Add(new GCImage("narrative/gun_pixelated_darkened".LoadSprite()));

                    q.Add(new GCNarrative("Redcliff: Leave."));
                    
                    q.Add(new GCImage(null));
                    q.Add(new GCImageHide());
                }));

            return q;
        }

        public static GameQueue.GameQueue Delivery3_MilitaryIntro()
        {
            var q = new GameQueue.GameQueue();
            
            q.Add(new GCSound("sound/phone_vibrate"));
            q.Add(new GCWait(2f));
            
            q.Add(new GCSound("sound/buzz"));
            q.Add(new GCNarrative("Phone: *crackling*"));
            q.Add(new GCNarrative("Phone: The Darkline, you remember now?"));

            q.Add(new GCWait(1f));
            
            q.Add(new GCNarrative("Phone: John?"));
            
            q.Add(new GCNarrative("John: Am I dead?"));
            
            q.Add(new GCSound("sound/buzz"));
            q.Add(new GCNarrative("Phone: *crackling*"));
            
            q.Add(new GCNarrative("Phone: No."));
            q.Add(new GCNarrative("Phone: You have a new delivery to make."));
            q.Add(new GCNarrative("Phone: You must keep going."));

            return q;
        }
        
        public static GameQueue.GameQueue Delivery3_MilitaryOutro()
        {
            var q = new GameQueue.GameQueue();

            q.Add(new GCUIState(UI_STATES.NARRATIVE_ONLY));

            q.Add(new GCImage(null));

            q.Add(new GCNarrative("You come to the most polished square building in the block. All the lawns around are aligned with the ruler, and perhaps even painted green."));
            q.Add(new GCNarrative("The doors are opened by Sergeant Calibi. His smile knows no bounds. He smiled the same way when he recruited you."));

            q.Add(new GCImage("narrative/sergeant".LoadSprite()));

            q.Add(new GCNarrative("Calibi: Oh, John! You finally got there!"));
            
            q.Add(new GCChoices(UI_STATES.PICK_OPTION_NO_STATS)
                .Add("Salute him.", (q) =>
                {
                    q.Add(new GCNarrative("You salute him."));
                    q.Add(new GCNarrative("Calibi: Old habits, right?"));
                })
                .Add("Say hello.", (q) =>
                {
                    q.Add(new GCNarrative("John: Hello."));
                    q.Add(new GCNarrative("Calibi: Right, you've been discharged."));
                    q.Add(new GCNarrative("Calibi: Was expecting a salute there for a second there."));
                })
                .Add("Ignore him.", (q) =>
                {
                    q.Add(new GCNarrative("Calibi: Right, you've been discharged."));
                    q.Add(new GCNarrative("Calibi: Was expecting a salute or 'hi' there for a second there."));
                }));
            
            q.Add(new GCNarrative("Calibi: We are so incredibly happy that you came!"));
            q.Add(new GCNarrative("Calibi: The fact that you came here will help us a lot to create a positive image of our armed forces."));
            q.Add(new GCNarrative("Calibi: We can't even put into words how grateful we are for your service!"));
            
            q.Add(new GCNarrative("Calibi: Did you bring your medal of honor?"));
            q.Add(new GCNarrative("John: Yes."));

            q.Add(new GCTintBackground(Color.black, 3f));
            
            q.Add(new GCNarrative("Without reducing the brightness of his smile, Calibi opens some kind of window in the wall. Behind it, the fire of the crematorium roars furiously."));

            q.Add(new GCSound("sound/guitar_noise_3", 0.25f));

            q.Add(new GCImage("narrative/furnace".LoadSprite()));
            
            q.Add(new GCNarrative("He takes a half step to the side and points to the door with an inviting gesture."));
            
            q.Add(new GCNarrative("Throw it in there John. What are you waiting for?"));

            q.Add(new GCChoices(UI_STATES.PICK_OPTION_NO_STATS)
                .Add("It does not determine my worth Calibi.", (q) =>
                {
                    q.Add(new GCNarrative("John: The medal doesn't mean anything... It's just some metal."));
                    q.Add(new GCNarrative("Calibi: Oh yeah? Then what's the problem, toss it in."));
                }).DoesNotExit()
                .Add("Throw it in.", (q) =>
                {
                    q.Add(new GCNarrative("You toss the medal into the crematorium."));
                    q.Add(new GCNarrative("It flares for a second, then turns black."));
                })
                .Add("Don't throw it in.", (q) =>
                {
                    q.Add(new GCNarrative("John: No."));
                }));
            
            q.Add(new GCNarrative("Calibi: You've failed everybody John."));
            q.Add(new GCNarrative("Calibi: Your wife."));
            q.Add(new GCNarrative("Calibi: Your best friend."));
            q.Add(new GCNarrative("Calibi: You've been kicked out from the service for drinking."));
            q.Add(new GCNarrative("Calibi: I think you should throw yourself in there as well."));
            
            q.Add(new GCNarrative("John: I would fail myself if I were to listen to you."));
            q.Add(new GCNarrative("John: I came here to ask questions."));
            q.Add(new GCNarrative("John: Darkline. What is it?"));
            q.Add(new GCNarrative("John: I've spoke to Red, he says they've done something to us."));
            q.Add(new GCNarrative("John: What have you done?!"));
            
            q.Add(new GCImage("narrative/sergeant_drip".LoadSprite()));

            q.Add(new GCNarrative("Calibi: You ask questions I cannot answer, John."));
            q.Add(new GCNarrative("Calibi: What is worse than the war?"));
            q.Add(new GCNarrative("John: ... Defeat."));
            q.Add(new GCNarrative("Calibi: I don't have to explain this to you, you've been there."));
            q.Add(new GCNarrative("Calibi: You know all about it."));
            q.Add(new GCNarrative("Calibi: They've made us forget, but it was for your own good."));
            q.Add(new GCNarrative("Calibi: I think it's time for you to go."));
            q.Add(new GCNarrative("Calibi: It was nice seeing you."));

            q.Add(new GCImageHide());
            
            return q;
        }

        public static GameQueue.GameQueue Delivery4_SelfIntro()
        {
            var q = new GameQueue.GameQueue();

            q.Add(new GCSound("sound/phone_vibrate"));
            q.Add(new GCWait(2f));
            
            q.Add(new GCSound("sound/buzz"));
            q.Add(new GCNarrative("Phone: *crackling*"));
            q.Add(new GCNarrative("Phone: Are you starting to remember?"));
            
            q.Add(new GCNarrative("John: Some bits and pieces..."));
            q.Add(new GCNarrative("John: Why does it matter to you?"));
            
            q.Add(new GCSound("sound/buzz"));
            q.Add(new GCNarrative("Phone: *crackling*"));
            q.Add(new GCNarrative("Phone: It's important, John."));
            q.Add(new GCNarrative("Phone: Lives are at stake?"));
            
            q.Add(new GCNarrative("John: Who's lives?"));
            
            q.Add(new GCSound("sound/radio_break"));
            
            q.Add(new GCNarrative("The signal broke."));

            q.Add(new GCNarrative("???: John, do you hear me, John?"));
            
            q.Add(new GCSound("sound/radio_break"));
            q.Add(new GCNarrative("???: I am doctor #%$#$^#$%@@"));
            q.Add(new GCNarrative("???: I need to talk to you."));
            q.Add(new GCNarrative("???: Come to the address I've sent-"));
            
            q.Add(new GCSound("sound/phone_hangup"));
            
            q.Add(new GCWait(2f));
            
            return q;
        }
        
        public static GameQueue.GameQueue Delivery4_SelfOutro()
        {
            var q = new GameQueue.GameQueue();

            q.Add(new GCUIState(UI_STATES.NARRATIVE_ONLY));

            q.Add(new GCImage(null));

            q.Add(new GCNarrative("You approach a small apartment complex."));
            q.Add(new GCNarrative("You ring the buzzer and you hear that voice again."));

            q.Add(new GCNarrative("???: John, it's you? Come in, quick!"));
            
            q.Add(new GCImage("narrative/professor".LoadSprite()));

            q.Add(new GCNarrative("Profressor: We don't have much time John!"));

            q.Add(new GCChoices(UI_STATES.PICK_OPTION_NO_STATS)
                .Add("Do I know you?", (q) =>
                {
                    q.Add(new GCNarrative("Profressor: Not personally, but I am very familliar with your case!"));
                    global::Game.world.player.factsKnown.Add("professor_case");
                }).DoesNotExit()
                .Add("My case?", (q) =>
                {
                    q.Add(new GCNarrative("Profressor: Yes! Project Darkline. Vietnam. Secret experiments."));
                    q.Add(new GCNarrative("Profressor: You are not insane John!"));
                }).ConditionFact("professor_case").DoesNotExit()
                .Add("Can you help me?", (q) =>
                {
                    q.Add(new GCNarrative("Professor: Well, yes... And no."));
                    q.Add(new GCNarrative("Professor: Some things cannot be undone."));
                    q.Add(new GCNarrative("Professor: But I can help with those those hallucinations you are having!"));
                }).DoesNotExit()
                .Add("What do you want?", (q) =>
                {
                }));

            q.Add(new GCNarrative("Professor: I just need a sample of your blood to verify something."));

            q.Add(new GCSound("sound/buzzer_short"));
            q.Add(new GCImage("narrative/doctor".LoadSprite()));

            q.Add(new GCNarrative("Professor: Please, lie down on the bed."));

            q.Add(new GCTintBackground(Color.black, 3f));

            q.Add(new GCNarrative("Professor: John? Is there something wrong?"));
            
            q.Add(new GCChoices(UI_STATES.PICK_OPTION_NO_STATS)
                .Add("Lie down", (q) =>
                {
                    q.Add(new GCNarrative("You lie down on the bed."));
                    q.Add(new GCNarrative("Professor straps you in with a bunch of belts."));
                    q.Add(new GCNarrative("Professor: I am sorry but this is necessary."));
                    q.Add(new GCNarrative("He takes out a syringe and injects you with something."));
                    q.Add(new GCNarrative("It hurts."));
                    q.Add(new GCNarrative("You scream."));
                    q.Add(new GCNarrative("Professor: It will get better in a second, I promise..."));
                })
                .Add("Refuse", (q) =>
                {
                    q.Add(new GCNarrative("John: I'm not going to do this."));
                    q.Add(new GCNarrative("Professor: Well I am afraid you don't have much choice here John."));
                    q.Add(new GCNarrative("John: I do."));
                    q.Add(new GCSound("sound/gun_cock"));
                    q.Add(new GCNarrative("You pull out your 1911."));

                    q.Add(new GCChoices(UI_STATES.PICK_OPTION_NO_STATS)
                        .Add("Shoot him", (q) =>
                        {
                            q.Add(new GCSound("sound/combat/pistol"));
                            q.Add(new GCImage("narrative/doctor_headless".LoadSprite()));

                            q.Add(new GCNarrative("Professors head explodes into pieces."));
                            q.Add(new GCNarrative("But he keeps standing."));

                            CharacterDatabase.professor.voice = "sound/buzz_talk";
                            
                            q.Add(new GCNarrative("Professor: You cannot kill me, I am not real."));
                        })
                        .Add("Put the gun down", (q) =>
                        {
                            q.Add(new GCNarrative("Professor: Yes, that's the right choice."));
                        }));

                    q.Add(new GCUIState(UI_STATES.NARRATIVE_ONLY));
                }));

            q.Add(new GCUIState(UI_STATES.NARRATIVE_ONLY));
            
            q.Add(new GCNarrative("Professor: You are dead, John."));
            
            q.Add(new GCSound("sound/heart_monitor"));
            
            q.Add(new GCNarrative("Professor: I am sorry."));
            
            q.Add(new GCNarrative("John: How am I speaking then?!"));
            
            q.Add(new GCNarrative("Professor: It's just your brain."));
            q.Add(new GCNarrative("Professor: We are trying to recover something here, John."));
            q.Add(new GCNarrative("Professor: Right now, you are connected to a special machine."));
            q.Add(new GCNarrative("Professor: It can send electrical impulses through the regions of your brain."));
            q.Add(new GCNarrative("Professor: Stimulating your memories and keeping you 'alive' for a brief moment."));
            q.Add(new GCNarrative("Professor: But our time is running out."));
            
            q.Add(new GCImage("narrative/jungle".LoadSprite()));

            q.Add(new GCNarrative("Professor: Tell us what you remember John."));
            q.Add(new GCNarrative("Professor: The Darkline."));
            
            q.Add(new GCSound("sound/guitar_noise_4", 0.25f));
            
            q.Add(new GCNarrative("You feel a shock, as if you've been hit by an electric current."));
            q.Add(new GCNarrative("And your start to remember."));
            q.Add(new GCNarrative("The jungle the gunfire."));

            q.Add(new GCImage("narrative/barrels".LoadSprite()));

            q.Add(new GCNarrative("The barrels with black liquid."));
            q.Add(new GCNarrative("DARKLINE stamped over them."));
            q.Add(new GCNarrative("You were pouring the liquid into the water sources."));

            q.Add(new GCNarrative("It caused horrible mutations in the enemy."));
            
            q.Add(new GCImage("narrative/zombies".LoadSprite()));

            q.Add(new GCNarrative("Their skin falling off the flesh, mind turning into a blind rage."));
            q.Add(new GCNarrative("It was supposed to spread like a wildfire through the isolated enemy camps."));
            q.Add(new GCNarrative("And die off afterwards."));
            q.Add(new GCNarrative("But they were just as combat effective against us, the project has been scrapped."));
            q.Add(new GCNarrative("Testing site was carpet bombed."));

            q.Add(new GCImage("narrative/jungle".LoadSprite()));

            q.Add(new GCNarrative("But you still remember the coordinates."));
            
            q.Add(new GCNarrative("John: You are not the American government?"));
            q.Add(new GCNarrative("John: If you were, you'd have the information."));

            q.Add(new GCImage("narrative/doctor".LoadSprite()));
            q.Add(new GCTintBackground(Color.white, 1f));

            q.Add(new GCNarrative("Professor: You are right."));
            q.Add(new GCNarrative("Professor: But I cannot tell you who we are."));
            q.Add(new GCNarrative("Professor: We are trying to make an antidote for it, John."));

            q.Add(new GCNarrative("John: It's impossible."));
            
            q.Add(new GCNarrative("Professor: It is possible, we brought you back to life."));
            q.Add(new GCNarrative("Professor: Just tell us the coordinates, and we will ressurect you!"));
            q.Add(new GCNarrative("Professor: You will get a second chance."));
            
            q.Add(new GCChoices(UI_STATES.PICK_OPTION_NO_STATS)
                .Add("Tell them the coordinates", (q) =>
                {
                    q.Add(new GCTintBackground(Color.white, 1f));

                    q.Add(new GCNarrative("You've told them the coordinates."));
                    
                    q.Add(new GCNarrative("Profressor: You're a true hero, John!"));
                    q.Add(new GCNarrative("Profressor: Your sacrifices are not in vain."));
                    q.Add(new GCNarrative("Profressor: We will keep your brain alive."));
                    q.Add(new GCNarrative("Profressor: You will be resuscitated."));
                    q.Add(new GCNarrative("Profressor: Just wait until we get to the test site."));

                    q.Add(new GCTintBackground(Color.black, 1f));
                    q.Add(new GCImage(null));
                    
                    q.Add(new GCNarrative("You feel your consciousness fading..."));
            
                    q.Add(new GCSound("sound/heart_monitor"));

                    q.Add(new GCTintBackground(Color.white, 3f).Solid());
                    
                    q.Add(new GCQueue(HappyEndingQuestionMark()));
                })
                .Add("No", (q) =>
                {
                    q.Add(new GCTintBackground(Color.black, 1f));

                    q.Add(new GCNarrative("Profressor: You're a fool John..."));
                    q.Add(new GCNarrative("Profressor: You could have fixed everything!"));
                    q.Add(new GCNarrative("Profressor: And now what?"));
                    q.Add(new GCNarrative("Profressor: We will turn you off."));

                    q.Add(new GCNarrative("John: It is my decision"));

                    q.Add(new GCNarrative("Profressor: Goodbye John."));
                    q.Add(new GCImage(null));

                    q.Add(new GCNarrative("You feel your consciousness fading..."));

                    q.Add(new GCSound("sound/flatline"));

                    q.Add(new GCImage("narrative/credits".LoadSprite()));
                }));
            
            q.Add(new GCStop());
            
            return q;
        }

        static GameQueue.GameQueue HappyEndingQuestionMark()
        {
            var q = new GameQueue.GameQueue();
            
            q.Add(new GCUIState(UI_STATES.NARRATIVE_ONLY));
            q.Add(new GCImage(null));

            q.Add(new GCSound("sound/guitar_noise_5", 0.25f));
            
            q.Add(new GCNarrative("You approach a neat one-story house surrounded by fields of blooming flowers."));
            q.Add(new GCNarrative("Emily opens the door."));
            q.Add(new GCNarrative("Emily: John, you're back already?"));
            
            q.Add(new GCImage("narrative/woman".LoadSprite()));
            
            q.Add(new GCNarrative("John: Yeah, I've decided to take a day off today."));
            q.Add(new GCNarrative("John: We can spend it together."));

            q.Add(new GCImage("narrative/credits_on_white".LoadSprite()));
            
            return q;
        }
    }

    public class GCStop : QueueItemBase
    {
    }
}