using System;
using System.Collections.Generic;
using Components;
using Microsoft.Xna.Framework.Input;

namespace BabaIsYou
{
    public class Controls
    {
        private Dictionary<Keys, Direction> keyFirstControls = new Dictionary<Keys, Direction>()
        {
            { Keys.Up, Direction.Up },
            { Keys.Right, Direction.Right },
            { Keys.Down, Direction.Down },
            { Keys.Left, Direction.Left },
        };

        private Dictionary<Direction, Keys> controlFirstControls;


        public List<Direction> controlsList { 
            get {
                return new List<Direction>(keyFirstControls.Values);
            }
        }

        public Controls()
        {
            this.controlFirstControls = swapDictionary<Keys, Direction>(keyFirstControls);
        }

        public Nullable<Direction> getControl(Keys key)
        {
            if (keyFirstControls.ContainsKey(key))
                return keyFirstControls[key];
            else
                return null;
        }

        public Keys getKey(Direction control)
        { 
            return controlFirstControls[control];
        }

        public bool setControl(Keys key, Direction control)
        { 
            if (keyFirstControls.ContainsKey(key)) // check if key is already used
            {
                if (keyFirstControls[key] == control) // if control is already right just leave it
                    return true;
                else
                    return false; // this eliminate duplicate keys from being added
            }
            else
            {
                Keys currentKey = controlFirstControls[control];
                controlFirstControls[control] = key;
                keyFirstControls.Remove(currentKey); // remove the old control
                keyFirstControls[key] = control;
                return true;
            }
        }

        public Dictionary<TValue, TKey> swapDictionary<TKey, TValue>(Dictionary<TKey, TValue> source)
        {
            Dictionary<TValue, TKey> result = new Dictionary<TValue, TKey>();
            foreach (var entry in source)
            {
                if (!result.ContainsKey(entry.Value))
                {
                    result.Add(entry.Value, entry.Key); // in case there are duplicate values in the original (should't be a problem here)
                }
                else
                {
                    return null;
                }
            }
            return result;
        }
    }
}
