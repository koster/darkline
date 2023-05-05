using Source.Commands;
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

            q.Add(new GCWait(1f));

            q.Add(new GCSound("sound/phone_vibrate"));

            q.Add(new GCWait(1f));
            
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
            q.Add(new GCNarrative("You pull out a double barrel shotgun."));
            q.Add(new GCNarrative("Phone: You will need to deliver this."));
            q.Add(new GCNarrative("John: Is this... I know this weapon. It has the inscription on it 'L.R.', 'Salvation'"));

            return q;
        }
        
        public static GameQueue.GameQueue Delivery2_FriendOutro()
        {
            var q = new GameQueue.GameQueue();

            q.Add(new GCUIState(UI_STATES.NARRATIVE_ONLY));

            q.Add(new GCImage(null));

            q.Add(new GCNarrative("You open the doors of the mansion and see Lieutenant Redcliff."));
            
            q.Add(new GCImage("enemies/creepy_man_1".LoadSprite()));
            
            q.Add(new GCNarrative("He's wearing the uniform that you wouldn't mistake for anything else, it smells of the jungle, napalm, and inhumanity."));
            q.Add(new GCNarrative("Redcliff: Oh, it's you, John?"));
            q.Add(new GCNarrative("You're surprised to find out that the gunshot that tore apart the Lieutenant's face doesn't prevent him from speaking."));
            q.Add(new GCNarrative("Redcliff: Did you bring my order?"));
            q.Add(new GCNarrative("Redcliff: Is Freedom waiting for me?"));
            q.Add(new GCNarrative("Redcliff: By the way, there should be enough for you too, since you're with me, private."));

            q.Add(new GCChoices(UI_STATES.PICK_OPTION_NO_STATS)
                .Add("Give him full shotgun", (q) => { })
                .Add("Give him shotgun with 1 bullet", (q) => { })
                .Add("Do not give him shotgun", (q) =>
                {
                    // q.Add(new GCImageHide());
                    // q.Add(new GCQueue(CombatDatabase.Combat01_Delivery()));
                }));

            q.Add(new GCNarrative("Ah, freedom again!"));
            q.Add(new GCImage(null));
            q.Add(new GCNarrative("After you close the door - you hear loud explosion."));
            q.Add(new GCNarrative("Is it salvation you delivered, or it is something else?"));
            q.Add(new GCImageHide());

            return q;
        }

        public static GameQueue.GameQueue Delivery3_Military()
        {
            var q = new GameQueue.GameQueue();

            q.Add(new GCUIState(UI_STATES.NARRATIVE_ONLY));

            q.Add(new GCImage(null));

            q.Add(new GCNarrative("You come to the most polished square building in the block. All the lawns around are aligned with the ruler, and perhaps even painted green."));
            q.Add(new GCNarrative("The doors are opened by Sergeant Calibi. His smile knows no bounds. He smiled the same way when he recruited you."));

            q.Add(new GCImage("narrative/sergeant".LoadSprite()));

            q.Add(new GCNarrative("\"Oh, John! You finally got there! Incredibly happy! The fact that you came here will help us a lot to create a positive image of our armed forces."));
            q.Add(new GCNarrative("We can't even put into words how grateful we are for your service!"));
            q.Add(new GCNarrative("Did you bring the uniform? Give it here!\""));

            q.Add(new GCChoices(UI_STATES.PICK_OPTION_NO_STATS)
                .Add("Give him the uniform", (q) => { })
                .Add("Give him the damaged uniform", (q) => { })
                .Add("Do not give him the uniform", (q) => { }));

            q.Add(new GCNarrative("Without reducing the brightness of his smile, Calibi opens some kind of window in the wall. Behind it, the fire of the crematorium roars furiously."));
            q.Add(new GCNarrative("He tosses the uniform in there, then takes a half step to the side and points to the door with an inviting gesture."));
            q.Add(new GCNarrative("Come on John, get in, the best thing you can do in civilian life for our brilliant troops!"));
            q.Add(new GCNarrative("What are you waiting?"));
            q.Add(new GCImageHide());

            return q;
        }

        public static GameQueue.GameQueue Delivery4_Self()
        {
            var q = new GameQueue.GameQueue();

            q.Add(new GCUIState(UI_STATES.NARRATIVE_ONLY));

            q.Add(new GCImage(null));

            q.Add(new GCNarrative("You're approaching... did you make a circle? The house is one to one similar to yours. A familiar smell hits the nose. It only reminds you despair."));
            q.Add(new GCNarrative("You climb this familiar staircase, instinctively stepping over loose step. You should have fixed it long time ago."));
            q.Add(new GCNarrative("You knock on the scorched door with your number on it. Darkness opens the door"));

            q.Add(new GCImage("narrative/darkness".LoadSprite()));

            q.Add(new GCNarrative("Darkness: *crackling*"));
            q.Add(new GCNarrative("Darkness: Wow, you really made it."));
            q.Add(new GCNarrative("Darkness: *crackling*"));
            q.Add(new GCNarrative("Darkness: I hope you didn't eat them."));
            q.Add(new GCNarrative("Darkness: Give them here. Understood?"));


            q.Add(new GCChoices(UI_STATES.PICK_OPTION_NO_STATS)
                .Add("You are me?", (q) =>
                {
                    q.Add(new GCNarrative("Darkness: Wrong guess. You are me."));
                    q.Add(new GCNarrative("Darkness: But you need to stop it right away"));
                })
                .Add("Why are you torturing me?", (q) =>
                {
                    q.Add(new GCNarrative("Darkness: Because life is pain"));
                    q.Add(new GCNarrative("Darkness: *crackling*"));
                    q.Add(new GCNarrative("Darkness: And if you didn't bring the pills, then I will also break your hand"));
                    q.Add(new GCNarrative("Darkness: Well Intentioned"));
                })
                .Add("Give pills", (q) => { })
                .Add("Give an incomplete pack of pills", (q) =>
                {
                    q.Add(new GCNarrative("Darkness: You tasted them, you fool"));
                    q.Add(new GCNarrative("Darkness: But not much is missing either"));
                    q.Add(new GCNarrative("Darkness: *crackling*"));
                    q.Add(new GCNarrative("Darkness: means there is hope"));
                    q.Add(new GCNarrative("Darkness: Protect yourself"));
                }));

            q.Add(new GCNarrative("Darkness: Yes, leave these nonsense here in the dark"));
            q.Add(new GCNarrative("Darkness: And break out of here. Live"));

            q.Add(new GCNarrative("You quickly ran away from your own house - and saw your car on the street"));
            q.Add(new GCNarrative("Turn of the key - and the engine explodes with a roar, making the whole car shake with anticipation."));
            q.Add(new GCNarrative("There is nothing holding you back in this city. There's no point in dying here"));
            q.Add(new GCNarrative("You press the pedal and the streets and houses, everything merges into an indistinguishability"));
            q.Add(new GCNarrative("You break through the fog, and only from afar the darkness watches with a grin"));

            q.Add(new GCQueue(Outro()));

            return q;
        }

        public static GameQueue.GameQueue Outro()
        {
            var q = new GameQueue.GameQueue();
            q.Add(new GCNarrative("Voice: Three cc of adrenaline, now"));
            q.Add(new GCNarrative("Voice: Brain functions stabilised, dynamics online"));
            q.Add(new GCNarrative("You have encountered a very distant touch. As if you were touched through a hundred quilts."));
            q.Add(new GCNarrative("Voice: Are you still a fighter, John? I'm glad you got out in the end"));

            q.Add(new GCLoadScene("credits_screen"));
            return q;
        }
    }
}