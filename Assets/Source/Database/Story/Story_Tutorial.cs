using System;
using Source.Commands;
using Source.Game.Deliveries;
using Source.GameQueue;
using Source.Util;

public static class Story_Tutorial
{
    public static GameQueue Painkillers()
    {
        var q = new GameQueue();
        q.Add(new CGUIStateRemember());
        q.Add(new GCUIState(UI_STATES.NARRATIVE_PLUS_STATS));
        q.Add(new GCNarrative("My head hurts..."));
        q.Add(new GCNarrative("I should have some painkillers in my backpack..."));
        q.Add(new GCCall(() => Game.world.tutorial.highlightBackpack = true));
        q.Add(new CGUIStateRestore());
        return q;
    }

    public static GameQueue WhatIsThisPlace()
    {
        var q = new GameQueue();
        q.Add(new CGUIStateRemember());
        q.Add(new GCUIState(UI_STATES.NARRATIVE_PLUS_STATS));
        q.Add(new GCNarrative("What is this place?"));
        q.Add(new GCNarrative("It's so foggy... I can't see anything."));
        q.Add(new GCNarrative("I better remember where I'm leaving my car."));
        q.Add(new CGUIStateRestore());
        return q;
    }


    public static GameQueue Lunch()
    {
        var q = new GameQueue();

        q.Add(new GCNarrative("You notice a woman sitting on a park bench."));
        q.Add(new GCNarrative("She smiles and waves at you."));

        q.Add(new CGUIStateRemember());
        q.Add(new GCUIState(UI_STATES.NARRATIVE_ONLY));
        
        q.Add(new GCImage("narrative/woman".LoadSprite()));
        
        q.Add(new GCNarrative("Emily: John!"));
        
        q.Add(new GCNarrative("John: Emily? What are you doing here?"));
        
        q.Add(new GCNarrative("Emily: What?!"));
        q.Add(new GCNarrative("Emily: You're joking right?"));

        q.Add(new GCNarrative("John: I mean, sorry, Emily. I just haven't seen you for so long."));

        q.Add(new GCNarrative("Emily: What? Stop fooling around."));
        q.Add(new GCNarrative("Emily: I've made you a lunch, and you forgot it!"));

        q.Add(new GCNarrative("John: A lunch?"));
        
        q.Add(new GCImage(null));
        
        q.Add(new GCNarrative("John: Emily?"));

        q.Add(new GCNarrative("The woman seems to have vanished into thin air..."));
        
        q.Add(new GCNarrative("John: What is this nonsense. It ought to have been someone else."));

        q.Add(new GCImageHide());
        q.Add(new CGUIStateRestore());

        q.Add(new GCAddItem(ItemDatabase.lunch, 1));

        return q;
    }

    public static GameQueue FindPlank()
    {
        var q = new GameQueue();

        if (Game.world.inventory.HasItem(ItemDatabase.plankOfWood))
            return q;

        q.Add(new CGUIStateRemember());
        q.Add(new GCUIState(UI_STATES.NARRATIVE_ONLY));

        q.Add(new GCImage("narrative/hobo".LoadSprite()));

        q.Add(new GCNarrative("You encounter a homeless man on the street, his eyes full of wisdom and madness."));
        q.Add(new GCNarrative("He hands you a stick with a nail sticking out of it."));
        q.Add(new GCNarrative("\"This will be more useful to you than that lying phone of yours,\" he mutters."));

        q.Add(new GCNarrative("As you take the stick, he leans in closer and whispers, \"You're already dead, and no one will help you.\""));
        
        q.Add(new GCImage(null));
        
        q.Add(new GCNarrative("The homeless man vanishes as mysteriously as he appeared, leaving you to ponder his ominous message."));

        q.Add(new GCAddItem(ItemDatabase.plankOfWood, 1));
        
        q.Add(new GCImageHide());
        q.Add(new CGUIStateRestore());

        return q;
    }

    public static GameQueue FindKnife()
    {
        var q = new GameQueue();

        if (Game.world.inventory.HasItem(ItemDatabase.knife))
            return q;

        q.Add(new GCNarrative("You are walking past a dark alleyway."));
        q.Add(new GCNarrative("It is so dark that you cannot take your eyes off it."));
        q.Add(new GCNarrative("In the middle of the alleyway stands a barely visible figure. The only thing you can clearly see is the knife she is holding to her throat."));

        q.Add(new GCChoices()
            .Add("Stop, don't do it!", (q) =>
            {
            }));
        
        q.Add(new GCNarrative("The figure freezes. What is it about life that makes people cling to it so desperately?"));
        
        q.Add(new GCChoices()
            .Add("Well, I don't really know. But I don't want you to do it, okay?", (q) =>
            {
            })
            .Add("Just give me the knife, I'll need it more.", (q) =>
            {
            }));
        
        q.Add(new GCNarrative("The figure shrugs. \"If that's what you decided, then so be it.\""));
        q.Add(new GCNarrative("When figure comes closer to give you a knife - for a second it seems that he looks exactly like you"));
        q.Add(new GCNarrative("You take the knife in your hand. It's sharp, and it has just missed chance to take a life. It will want another in return."));

        q.Add(new GCAddItem(ItemDatabase.knife, 1));

        return q;
    }

    public static GameQueue FindPistol()
    {
        var q = new GameQueue();

        if (Game.world.inventory.HasItem(ItemDatabase.pistol))
        {
            q.Add(new GCAlert("You found some ammo lying on the ground!"));
            q.Add(new GCAddItem(ItemDatabase.ammo, 4));
            return q;
        }

        q.Add(new GCNarrative("At some point, you notice that the fog on the street has become less dense."));
        q.Add(new GCNarrative("With each subsequent delivery, with each thought you pull out of yourself, it weakens."));
        q.Add(new GCNarrative("Something seems to have stirred right at your chest."));

        q.Add(new GCChoices()
            .Add("Swiftly slip your hand under your jacket. Check what's there without getting nervous.", (q) =>
            {
                q.Add(new GCNarrative("Under your jacket, you find your holster. In it, your trusty 1911 is nestled."));
                q.Add(new GCNarrative("Where did it come from? Has it always been here? Did you just forget that you have the strength?"));
                q.Add(new GCNarrative("The holster with the pistol is pleasantly warm. Let them try to stop you now."));
                q.Add(new GCAddItem(ItemDatabase.pistol, 1));
                q.Add(new GCAddItem(ItemDatabase.ammo, 4));
            }));

        return q;
    }
}

public class CGUIStateRemember : QueueItemBase
{
    public override void Enter()
    {
        base.Enter();
        UIState.RememberState();
        Complete();
    }
}
public class CGUIStateRestore : QueueItemBase
{
    public override void Enter()
    {
        base.Enter();
        UIState.RestoreState();
        Complete();
    }
}