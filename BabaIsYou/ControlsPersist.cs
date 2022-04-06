using System;
using System.Collections.Generic;
using Components;
using Microsoft.Xna.Framework.Input;

namespace BabaIsYou
{
    public class ControlsPersist
    {
        public string up { get; set; }
        public string right { get; set; }
        public string down { get; set; }
        public string left { get; set; }

        public ControlsPersist() // XML Serializer needs a parameterless constructor
        { 
        }

        public ControlsPersist(Keys up, Keys right, Keys down, Keys left)
        {
            this.up = up.ToString();
            this.right = right.ToString();
            this.down = down.ToString();
            this.left = left.ToString();
        }

        public static ControlsPersist fromDictionary(Dictionary<Direction, Keys> controlFirstControls) { 
            Keys up = controlFirstControls[Direction.Up];
            Keys right = controlFirstControls[Direction.Right];
            Keys down = controlFirstControls[Direction.Down];
            Keys left = controlFirstControls[Direction.Left];
            return new ControlsPersist(up, right, down, left);
        }

        public Dictionary<Direction, Keys> ControlsFirstDictionary()
        {
            return new Dictionary<Direction, Keys>()
            {
                { Direction.Up, (Keys)Enum.Parse(typeof(Keys), up) },
                { Direction.Right, (Keys)Enum.Parse(typeof(Keys), right) },
                { Direction.Down, (Keys)Enum.Parse(typeof(Keys), down) },
                { Direction.Left, (Keys)Enum.Parse(typeof(Keys), left) },
            };
        }
    }
}
