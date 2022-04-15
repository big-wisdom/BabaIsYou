using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace BabaIsYou
{
    public class KeyboardModel
    {
        Dictionary<Keys, TimeSpan> locked = new Dictionary<Keys, TimeSpan>();

        public List<Keys> GetUnlockedKeys() {
            KeyboardState keyboard = Keyboard.GetState();
            List<Keys> unlockedKeys = new List<Keys>( keyboard.GetPressedKeys());

            // remove all locked keys
            List<Keys> unlockList = new List<Keys>();
            foreach (Keys k in locked.Keys) {
                if (!unlockedKeys.Remove(k)) {
                    unlockList.Add(k); // if key no longer pressed then unlock it
                }
            }
            foreach (Keys k in unlockList) locked.Remove(k); // here's the loop where it's actually unlocked

            return unlockedKeys;
        }

        public void update(TimeSpan elapsedTime)
        {
            List<Keys> keys = new List<Keys>(locked.Keys);
            foreach (Keys key in keys)
            {
                if (locked[key] != TimeSpan.MaxValue)
                    locked[key] -= elapsedTime;

                if (locked[key] < TimeSpan.Zero)
                    locked.Remove(key);
            }
        }

        public void lockKey(Keys key) {
            locked.Add(key, TimeSpan.MaxValue);
        }

        public void lockKey(Keys key, TimeSpan duration) {
            locked.Add(key, duration);
        }

        public void unlockKey(Keys key) {
            locked.Remove(key);
        }
    }
}
