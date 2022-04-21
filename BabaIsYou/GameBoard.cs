using System.Collections.Generic;
using Entities;
using Microsoft.Xna.Framework;
using Components;
using BabaIsYou.Particles;

namespace BabaIsYou
{
    public class GameBoard
    {
        public List<List<LinkedList<Entity>>> gameBoard = new List<List<LinkedList<Entity>>>();
        ComponentContext components;

        public List<Position> particlePositions = new List<Position>();
        public List<ParticleEmitter> particleEmmiters = new List<ParticleEmitter>();

        private Vector2 dimensions;

        public GameBoard(Levels levels, ComponentContext components)
        {
            this.components = components;

            this.dimensions = levels.currentLevel.dimensions;
            for (int y=0; y<dimensions.Y; y++) {
                gameBoard.Add(new List<LinkedList<Entity>>());
                for (int x=0; x<dimensions.X; x++) {
                    var ll = new LinkedList<Entity>();
                    gameBoard[y].Add(ll);
                }
            }

            initializeBoard(levels.currentLevel);
        }

        private void initializeBoard(Level level)
        {
            for (int y=0; y< level.dimensions.Y; y++)
            {
                for (int x=0; x < level.dimensions.X; x++)
                {
                    LinkedList<char> charList = level.charBoard[y][x];
                    foreach (char c in charList)
                    {
                        addEntity(components.getEntity(c, x, y));
                    }

                }
            }
        }


        public void addEntity(Entity entity)
        {
            if (entity != null)
            {
                Components.Position pos = entity.GetComponent<Components.Position>();
                gameBoard[pos.y][pos.x].AddLast(entity);
            }
        }

        public void removeEntity(Entity entity)
        { 
            Components.Position pos =  entity.GetComponent<Components.Position>();
            gameBoard[pos.y][pos.x].Remove(entity); // TODO: What is it's not on top of the stack?
        }

        public List<Entity> getEntities()
        {
            List<Entity> entities = new List<Entity>();
            foreach (List<LinkedList<Entity>> row in gameBoard)
            {
                foreach (LinkedList<Entity> stack in row)
                { 
                    foreach (Entity e in stack)
                    { 
                        if (e != null)
                        {
                            entities.Add(e);
                        }
                    }
                }
            }
            return entities;
        }
    }
}
