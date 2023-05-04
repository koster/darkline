using System;
using Source.Commands;
using Source.GameQueue;
using Source.Util;

namespace Source.Game.Deliveries
{
    public static class Story_Main
    {

        public static GameQueue.GameQueue Intro()
        {
            var q = new GameQueue.GameQueue();
            
            q.Add(new GCImage(null, instant: true));

            q.Add(new GCUIState(UI_STATES.NARRATIVE_ONLY));

            q.Add(new GCNarrative("You vaguely remember what you were doing before this."));
            q.Add(new GCNarrative("You were planning to finally fix the damn car and leave these remnants of life in the dust clouds."));
            q.Add(new GCNarrative("But then, a dense fog descended on the city. "));
            q.Add(new GCSound("sound/phone_vibrate"));
            q.Add(new GCNarrative("And your phone rang. "));
            q.Add(new GCNarrative("Didn't you pawn it to buy a carburetor?"));

            q.Add(new GCImage("narrative/phone_2".LoadSprite()));
            
            q.Add(new GCNarrative("Phone: *crackling*"));
            q.Add(new GCNarrative("Phone: The suitcase. It's under your bed. There's an address on it. Deliver it."));
            q.Add(new GCNarrative("Phone: Understood?"));

            q.Add(new GCChoices(UI_STATES.PICK_OPTION_NO_STATS)
                .Add("Since when am I a courier?", (q) =>
                {
                    q.Add(new GCNarrative("Phone: Everyone gets what they deserve."));
                }).DoesNotExit()
                .Add("How did I get a suitcase under my bed?", (q) =>
                {
                    q.Add(new GCNarrative("Phone: It's always been there."));
                    q.Add(new GCNarrative("Phone: *crackling*"));
                    q.Add(new GCNarrative("Phone: Now you just see it."));
                }).DoesNotExit()
                .Add("Um, okay? But why do I need this?", (q) =>
                {
                    q.Add(new GCNarrative("Phone: Deliver everything on time - and your dream will come true."));
                }));

            q.Add(new GCChoices(UI_STATES.PICK_OPTION_NO_STATS)
                .Add("Dream? Will you bring it back to me?", (q) =>
                {
                    q.Add(new GCNarrative("Phone: *crackling*"));
                    q.Add(new GCNarrative("Phone: *laughs*"));
                    q.Add(new GCNarrative("Phone: No, you don't deserve that kind of dream."));
                    q.Add(new GCNarrative("Phone: You don't deserve happiness."));
                    q.Add(new GCNarrative("Phone: *crackling*"));
                    q.Add(new GCNarrative("Phone: But you can get some peace."));
                    q.Add(new GCNarrative("Phone: The engine will roar again. You'll be able to leave."));
                    q.Add(new GCNarrative("Phone: *beeps*"));
                }));

            q.Add(new GCImageHide());

            return q;
        }

        public static GameQueue.GameQueue Delivery0()
        {
            var q = new GameQueue.GameQueue();
            
            q.Add(new GCUIState(UI_STATES.NARRATIVE_ONLY));

            q.Add(new GCImage(null));

            q.Add(new GCNarrative("\"Hello?\"")); 
            q.Add(new GCNarrative("\"There's, uhm a delivery for you...\"")); 
            q.Add(new GCImage("narrative/professor".LoadSprite()));
            q.Add(new GCNarrative("\"What!? Who are you!\"")); 
            q.Add(new GCNarrative("\"You are not one of them?! Don't you?\"")); 

            q.Add(new GCChoices(UI_STATES.PICK_OPTION_NO_STATS)
                .Add("\"One of who?\"", (q) =>
                {
                    q.Add(new GCNarrative("Then why are you hare!?"));
                })
                .Add("\"No I am not\"", (q) =>
                {
                    q.Add(new GCNarrative("Then why are you hare!?"));
                    q.Add(new GCNarrative("You're not supposed to be here..."));
                })
                .Add("\"Just give him the package\"", (q) =>
                {
                }));

            q.Add(new GCNarrative("\"I have a package for you\"."));
            q.Add(new GCNarrative("\"W- What?\""));
            q.Add(new GCImageHide());
            q.Add(new GCNarrative("After you close the door - you hear loud explosion."));
            q.Add(new GCNarrative("Is it salvation you delivered, or it is something else?"));
            q.Add(new GCSound("sound/creepy_ambient_1"));

            return q;
        }
        
        public static GameQueue.GameQueue Delivery1()
        {
            var q = new GameQueue.GameQueue();
            
            q.Add(new GCUIState(UI_STATES.NARRATIVE_ONLY));

            q.Add(new GCImage(null));
            
            q.Add(new GCNarrative("You open the doors of the mansion and see Lieutenant Redcliff.")); 
            q.Add(new GCImage("enemies/creepy_man_1".LoadSprite()));
            q.Add(new GCNarrative("He's wearing the uniform that you wouldn't mistake for anything else, it smells of the jungle, napalm, and inhumanity." )); 
            q.Add(new GCNarrative("\"Oh, it's you, John?\"")); 
            q.Add(new GCNarrative("You're surprised to find out that the gunshot that tore apart the Lieutenant's face doesn't prevent him from speaking.")); 
            q.Add(new GCNarrative("Did you bring my order? Is Freedom waiting for me? By the way, there should be enough for you too, since you're with me, private."));

            q.Add(new GCChoices(UI_STATES.PICK_OPTION_NO_STATS)
                .Add("Give him full shotgun", (q)=>{})
                .Add("Give him shotgun with 1 bullet", (q)=>{})
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
        
        public static GameQueue.GameQueue Delivery2()
        {
            var q = new GameQueue.GameQueue();

            q.Add(new GCUIState(UI_STATES.NARRATIVE_ONLY));

            q.Add(new GCImage(null));

            q.Add(new GCNarrative("You approach a neat one-story house surrounded by fields of blooming flowers."));
            q.Add(new GCNarrative("The house itself is covered in thorns that pierce your soul, reaching deep into your heart."));
            
            q.Add(new GCImage("narrative/woman".LoadSprite()));
            
            q.Add(new GCNarrative("She opens the door, her light greeting getting lost in a coughing fit."));
            
            q.Add(new GCImage("narrative/woman_and_man".LoadSprite()));

            q.Add(new GCNarrative("He runs up to her. \"Don't worry, my dear, I'll take care of everything.\""));
            q.Add(new GCNarrative("\"Oh, John. Have you finished wasting your time on madness and decided to show some care?\""));
            q.Add(new GCNarrative("\"I hope this new skill will be useful to you. But here, I taking care of her for a long time, give me the medicine and leave.\""));

            q.Add(new GCChoices(UI_STATES.PICK_OPTION_NO_STATS)
                .Add("Give him full vial", (q) => { })
                .Add("Give him 2/3 vial", (q) => { })
                .Add("Give him 1/3 vial", (q) => { })
                .Add("Do not give him vial", (q) => { }));

            q.Add(new GCNarrative("Here darling, take this, its new and experimental, it will totally help this time, I promise."));
            q.Add(new GCNarrative("And for the love of good, stop stressing about this bloody delivery boy, or you get worse!"));
            q.Add(new GCImage(null));
            q.Add(new GCNarrative("Silent coughs are still heard after you closed the door"));
            q.Add(new GCNarrative("Was the label a lie? Do you feel your heart cured now?"));
            q.Add(new GCImageHide());

            return q;
        }
        
        public static GameQueue.GameQueue Delivery3()
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

        public static GameQueue.GameQueue Delivery4()
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