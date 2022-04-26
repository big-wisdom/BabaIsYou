using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace BabaIsYou
{
    public enum Event
    {
        Death,
        Win,
        WinSwitch,
        YouSwitch,
        LevelStart,
        Move,
    }
    
    public class Audio
    {
        Dictionary<Event, SoundEffect> soundEffects; 

        public Audio(ContentManager content)
        {
            SoundEffect s = content.Load<SoundEffect>("audio/dead");
            soundEffects = new Dictionary<Event, SoundEffect>
            {
                { Event.Death, content.Load<SoundEffect>("audio/dead") },
                { Event.Win, content.Load<SoundEffect>("audio/win") },
                { Event.WinSwitch, content.Load<SoundEffect>("audio/bloop") },
                { Event.YouSwitch, content.Load<SoundEffect>("audio/aw") },
                { Event.LevelStart, content.Load<SoundEffect>("audio/levelStart") },
                { Event.Move, content.Load<SoundEffect>("audio/move") },
            };
        }

        public void playSound(Event e)
        {
            soundEffects[e].Play();
        }
    }
}
