using BabaIsYou;
using Components;
using Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Systems
{
    public class Collision : System
    {
        GameBoard gameBoard;
        public Collision(GameBoard gameBoard)
            : base(
                  typeof(Components.Position)
                  )
        {
            this.gameBoard = gameBoard;
        }

        /// <summary>
        /// Check to see if any movable components collide with any other
        /// collision components.
        ///
        /// Step 1: find all movable components first
        /// Step 2: Test the movable components for collision with other (but not self) collision components
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // go through the gameBoard
            // if an entity wants to move call move on it
            for (int y=0; y<gameBoard.gameBoard.Count; y++)
            { 
                for (int x=0; x<gameBoard.gameBoard[0].Count; x++)
                {
                    if (gameBoard.gameBoard[y][x].Count > 0)
                    {
                        Entity e = gameBoard.gameBoard[y][x].Last.Value; // TODO: this will only update the top item of each stack
                        Position newP; // TODO: right now when I am a wall, I can go through hedges
                        if ((newP = getTargetDestination(e)) != null)
                        {
                            e.GetComponent<Movable>().movementDirection = move(e, newP);
                        }
                    }
                }
            }
        }

        private Direction move(Entity e, Position target)
        {
            // get what's at target
            // if not empty
            if (gameBoard.gameBoard[target.y][target.x].Count > 0)
            {
                Entity targetEntity = gameBoard.gameBoard[target.y][target.x].Last.Value; // only check the thing on top
                // push
                if (targetEntity.ContainsComponent<PushC>())
                {
                    // if something is push, it should be movable
                    targetEntity.GetComponent<Movable>().movementDirection = e.GetComponent<Movable>().movementDirection;
                    return move(targetEntity, getTargetDestination(targetEntity));
                }

                // stop
                if (targetEntity.ContainsComponent<StopC>())
                {
                    e.GetComponent<Movable>().movementDirection = Direction.Stopped;
                    return Direction.Stopped;
                }

                // kill
                if (targetEntity.ContainsComponent<KillC>())
                    throw new NotImplementedException();

                // sink
                if (targetEntity.ContainsComponent<SinkC>())
                    throw new NotImplementedException();

                // win
                if (targetEntity.ContainsComponent<WinC>())
                    throw new NotImplementedException();
            }

            // just leave movementDirection how it is
            return e.GetComponent<Movable>().movementDirection;
        }

        private Position getTargetDestination(Entity e)
        {
            // if moveable
            Movable m;
            if ((m = e.GetComponent<Movable>()) != null)
            { 
                // if it wants to move
                if (m.movementDirection != Direction.Stopped)
                {
                    // get current position
                    Position p = e.GetComponent<Position>(); // there shouldn't ever be one with is movable without position
                    // increment off of desired
                    int newX = p.x;
                    int newY = p.y;
                    switch (m.movementDirection)
                    {
                        case Direction.Up:
                            newY--;
                            break;
                        case Direction.Down:
                            newY++;
                            break;
                        case Direction.Left:
                            newX--;
                            break;
                        case Direction.Right:
                            newX++;
                            break;
                    }

                    // return new position
                    return new Position(newX, newY);
                }
            }
            // return null
            return null;
        }

        /// <summary>
        /// Public interface that allows an entity with a single cell position
        /// to be tested for collision with anything else in the game.
        /// </summary>
        public bool collidesWithAny(Entity proposed)
        {
            //var aPosition = proposed.GetComponent<Components.Position>();

            //foreach (var entity in m_entities.Values)
            //{
            //    if (entity.ContainsComponent<Components.Collision>() && entity.ContainsComponent<Components.Position>())
            //    {
            //        var ePosition = entity.GetComponent<Components.Position>();

            //        foreach (var segment in ePosition.segments)
            //        {
            //            if (aPosition.x == segment.X && aPosition.y == segment.Y)
            //            {
            //                return true;
            //            }
            //        }
            //    }
            //}

            return false;
        }

        /// <summary>
        /// Returns a collection of all the movable entities.
        /// </summary>
        private List<Entity> findMovable(Dictionary<uint, Entity> entities)
        {
            var movable = new List<Entity>();

            foreach (var entity in m_entities.Values)
            {
                if (entity.ContainsComponent<Components.Movable>() && entity.ContainsComponent<Components.Position>())
                {
                    movable.Add(entity);
                }
            }

            return movable;
        }
        /// <summary>
        /// We know that only the snake is moving and that we only need
        /// to check its head for collision with other entities.  Therefore,
        /// don't need to look at all the segments in the position, with the
        /// exception of the movable itself...a movable can collide with itself.
        /// </summary>
        private bool collides(Entity a, Entity b)
        {
            //var aPosition = a.GetComponent<Components.Position>();
            //var bPosition = b.GetComponent<Components.Position>();

            ////
            //// A movable can collide with itself: Check segment against the rest
            //if (a == b)
            //{
            //    //
            //    // Have to skip the first segment, that's why using a counted for loop
            //    for (int segment = 1; segment < aPosition.segments.Count; segment++)
            //    {
            //        if (aPosition.x == aPosition.segments[segment].X && aPosition.y == aPosition.segments[segment].Y)
            //        {
            //            return true;
            //        }
            //    }

            return false;
            //}

            //return aPosition.x == bPosition.x && aPosition.y == bPosition.y;
        }
    }
}
