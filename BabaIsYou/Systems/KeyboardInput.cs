using BabaIsYou;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Systems
{
    /// <summary>
    /// This system knows how to accept keyboard input and use that
    /// to move an entity, based on the entities 'KeyboardControlled'
    /// component settings.
    /// </summary>
    class KeyboardInput : System
    {
        KeyboardModel keyboard;

        public KeyboardInput(KeyboardModel keyboard)
            : base(typeof(Components.You))
        {
            this.keyboard = keyboard;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var entity in m_entities.Values)
            {
                var movable = entity.GetComponent<Components.Movable>();
                var input = entity.GetComponent<Components.You>();
                var baba = entity.GetComponent<Components.BabaComponent>();

                foreach (var key in keyboard.GetUnlockedKeys())
                {
                    if (input.controls.ContainsKey(key))
                    {
                        movable.movementDirection  = input.controls[key];
                        if (baba != null) baba.direction = input.controls[key];
                        keyboard.lockKey(key);
                    }
                }
            }
        }
    }
}
