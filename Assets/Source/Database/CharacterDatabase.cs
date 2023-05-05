using System.Collections.Generic;
using Source.Util;

public class StoryCharacter
{
    public string prefix;
    public string voice;
}

public static class CharacterDatabase
{
    public static List<StoryCharacter> all = new List<StoryCharacter>();
    public static StoryCharacter phone;
    public static StoryCharacter doctor;
    public static StoryCharacter woman;
    public static StoryCharacter any;

    public static void Initialize()
    {
        phone = all.New();
        phone.voice = "sound/buzz_talk";
        phone.prefix = "Phone:";
        
        doctor = all.New();
        doctor.voice = "sound/buzz_talk";
        doctor.prefix = "Doctor:";
        
        woman = all.New();
        woman.voice = "sound/MidVoice_2";
        woman.prefix = "Emily:";
        
        any = new StoryCharacter();
        any.voice = "sound/MidVoice_2";
    }

    public static StoryCharacter GetCharacter(this string line)
    {
        if (line == null)
            return any;
        
        foreach (var a in all)
            if (line.StartsWith(a.prefix))
                return a;

        return any;
    }
}