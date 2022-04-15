using BabaIsYou;
using Microsoft.Xna.Framework;
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
        TimeSpan keyHoldInterval = TimeSpan.FromMilliseconds(200);
        bool keyLocked = false;

        public KeyboardInput(KeyboardModel keyboard)
            : base(typeof(Components.You))
        {
            this.keyboard = keyboard;
        }

        public override void Update(GameTime gameTime)
        {
            keyLocked = false;
            foreach (var key in keyboard.GetUnlockedKeys())
            {
                foreach (var entity in m_entities.Values)
                {
                    var movable = entity.GetComponent<Components.Movable>();
                    var input = entity.GetComponent<Components.You>();
                    var baba = entity.GetComponent<Components.BabaComponent>();

                    Nullable<Components.Direction> control = input.controls.getControl(key);
                    if (control != null)
                    {
                        movable.movementDirection  = control.Value;
                        if (baba != null) baba.direction = control.Value;
                        if (!keyLocked)
                        { 
                            keyboard.lockKey(key);
                            keyLocked = true;
                        }
                    }
                }
            }
        }
    }
}
