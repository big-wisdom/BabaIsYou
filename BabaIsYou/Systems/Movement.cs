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
        GameBoard gameBoard;
        KeyboardModel keyboard;

        public Movement(GameBoard gameBoard, KeyboardModel keyboard)
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
                gameBoard.removeEntity(e); // remove the entity from it's old position
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
                gameBoard.addEntity(e); // add the entity back in it's new position
                movable.movementDirection = Components.Direction.Stopped;
            }
        }
    }
}
