using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Components
{
    public class You: Component
    {
        public Dictionary<Keys, Direction> controls;

        public You(Dictionary<Keys, Direction> controls)
        {
            this.controls = controls;
        }
    }
}
