using Source.Commands;
using Source.GameQueue;
using Source.Util;
using UnityEngine;

namespace Source.Game.Deliveries
{
    public static class Story_Main
    {
        public static GameQueue.GameQueue Intro()
        {
            var q = new GameQueue.GameQueue();

            q.Add(new GCImage(null, instant: true));
            q.Add(new GCTintBackground(Color.black, 0f));
            q.Add(new GCUIState(UI_STATES.NARRATIVE_ONLY));

            q.Add(new GCSound("sound/heart_monitor"));
            q.Add(new GCWait(4f));
            
            q.Add(new GCNarrative("Where am I?"));

            q.Add(new GCSound("sound/heart_monitor"));
            q.Add(new GCWait(4f));
            
            q.Add(new GCNarrative("Fog."));
            q.Add(new GCNarrative("Just 'fog', that's the only thought."));
            q.Add(new GCNarrative("My head hurts."));
            
            q.Add(new GCSound("sound/phone_vibrate"));

            q.Add(new GCImage("narrative/phone_2".LoadSprite()));
            q.Add(new GCTintBackground(Color.white, 3f));
            q.Add(new GCWait(3f));

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
            
            q.Add(new GCNarrative("Is that my name? I can't seem to remember it..."));
            
            q.Add(new GCNarrative("Phone: John?"));
            
            q.Add(new GCNarrative("John: Yeah, uh. Hello. Who is this?"));
            
            q.Add(new GCSound("sound/buzz"));
            q.Add(new GCNarrative("Phone: *crackling*"));

            q.Add(new GCImageHide());
            
            q.Add(new GCNarrative("Phone: Darkline."));
            q.Add(new GCNarrative("Phone: You need to make a delivery."));

            q.Add(new GCNarrative("John: What? Are you joking?"));
            
            q.Add(new GCSound("sound/buzz"));
            q.Add(new GCNarrative("Phone: *crackling*"));
            
            q.Add(new GCNarrative("Phone: The suitcase is in your car."));

            q.Add(new GCNarrative("My car... Right I have a car."));
            q.Add(new GCNarrative("Did I drove here?"));

            q.Add(new GCNarrative("Phone: Deliver the suitcase to the Hospital, 33rd street."));
            q.Add(new GCNarrative("Phone: You will receive new instructions after this delivery."));
            
            q.Add(new GCSound("sound/phone_hangup", 0.5f));
            q.Add(new GCWait(5f));

            q.Add(new GCSound("sound/car_signal"));
            
            q.Add(new GCWait(0.5f));
            
            q.Add(new GCNarrative("There is a suitcase in my car."));
            q.Add(new GCNarrative("And no gas..."));
            q.Add(new GCNarrative("I guess I'll just walk..."));
            q.Add(new GCNarrative("I feel like I need to see a doctor anyway."));

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
            q.Add(new GCNarrative("John: Yes. I think I've hit my head or something."));
            q.Add(new GCNarrative("John: I can't seem to remember anything... I need help...?"));
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

            q.Add(new GCNarrative("John: What is this, some sort of prank?!"));
            q.Add(new GCNarrative("John: You drugged me?"));
            q.Add(new GCNarrative("John: I'm going to call the police!"));

            q.Add(new GCNarrative("Phone: You can try. It's not going to work/"));
            q.Add(new GCNarrative("Phone: *crackling*"));

            q.Add(new GCNarrative("For some reason. I belive him..."));

            q.Add(new GCNarrative("Phone: Your next delivery is in your backpack."));
            q.Add(new GCNarrative("Phone: Address is on the prescription label."));
            
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
            q.Add(new GCNarrative("Man: Oh, John. Have you finished wasting your time on madness and decided to show some care"));
            q.Add(new GCNarrative("Man: I hope this new skill will be useful to you. But here, I taking care of her for a long time, give me the medicine and leave."));

            var husband = "Man";
            
            q.Add(new GCChoices(UI_STATES.PICK_OPTION_NO_STATS)
                .Add("Who the hell is this, Emily?!", (q) =>
                {
                    q.Add(new GCNarrative("Emily: John... Again?"));
                    q.Add(new GCNarrative("Emily: This is Damian, my new husband."));
                    q.Add(new GCNarrative("Emily: We divorced years ago..."));
                    q.Add(new GCNarrative("Emily: What is it with you?"));
                    q.Add(new GCNarrative("Emily: *cough*"));

                    husband = "Damian";
                    
                    q.Add(new GCNarrative("Damian: We've had enough John!"));
                    q.Add(new GCNarrative("Damian: Give me the medicine!"));
                }).DoesNotExit()
                .Add("Give him the vial.", (q) =>
                {
                    q.Add(new GCNarrative("You pass the vial."));
                }));

            q.Add(new GCNarrative($"{husband}: Here darling, take this, its new and experimental, it will totally help this time, I promise."));
            q.Add(new GCNarrative($"{husband}: And for the love of good, stop stressing about this lunatic, or you get worse!"));
            q.Add(new GCNarrative($"{husband}: You're still here John?!"));
            
            q.Add(new GCImage(null));
            
            q.Add(new GCNarrative("Silent coughs are still heard after you closed the door"));
            q.Add(new GCNarrative("Was the label a lie? Do you feel your heart cured now?"));
            
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
            q.Add(new GCNarrative("Phone: Are you in your car?"));
            q.Add(new GCNarrative("John: Yes, it was parked around the house somehow."));
            q.Add(new GCNarrative("Phone: Check under the drivers seat."));
            q.Add(new GCWait(1f));
            q.Add(new GCNarrative("You pull out a letter."));
            q.Add(new GCNarrative("Phone: You know what to do with it."));
            
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
                .Add("\"I'm sorry I never came\"", (q) =>
                {
                    q.Add(new GCNarrative("John: I'm sorry Red. Sorry, I never came..."));
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