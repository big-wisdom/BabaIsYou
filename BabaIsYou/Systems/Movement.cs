using BabaIsYou;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Systems
{
    /// <summary>
    /// This system is responsible for handling the movement of any
    /// entity with a movable & position components.
    /// </summary>
    class Movement : System
    {
        List<List<Entities.Entity>> gameBoard;
        KeyboardModel keyboard;

        public Movement(List<List<Entities.Entity>> gameBoard, KeyboardModel keyboard)
            : base(
                  typeof(Components.Movable),
                  typeof(Components.Position)
                  )
        {
            this.gameBoard = gameBoard;
            this.keyboard = keyboard;
        }

        public override void Update(GameTime gameTime) { 
            foreach (Entities.Entity e in m_entities.Values) { 
                var movable = e.GetComponent<Components.Movable>();
                var pos = e.GetComponent<Components.Position>();
                Components.Direction direction = movable.movementDirection;
                switch(direction) {
                    case Components.Direction.Up:
                        pos.y = pos.y - 1;
                        break;
                    case Components.Direction.Right:
                        pos.x = pos.x + 1;
                        break;
                    case Components.Direction.Down:
                        pos.y = pos.y + 1;
                        break;
                    case Components.Direction.Left:
                        pos.x = pos.x - 1;
                        break;
                }
                movable.movementDirection = Components.Direction.Stopped;
            }
        }
    }
}
