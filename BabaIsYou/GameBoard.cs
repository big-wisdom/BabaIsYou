using System.Collections.Generic;
using Entities;
using Microsoft.Xna.Framework;
using Components;

namespace BabaIsYou
{
    public class GameBoard
    {
        public List<List<LinkedList<Entity>>> gameBoard = new List<List<LinkedList<Entity>>>();
        public int GRID_SIZE;
        ComponentContext components;

        public GameBoard(int GRID_SIZE, ComponentContext components)
        {
            this.GRID_SIZE = GRID_SIZE;
            this.components = components;

            for (int y=0; y<GRID_SIZE; y++) {
                gameBoard.Add(new List<LinkedList<Entity>>());
                for (int x=0; x<GRID_SIZE; x++) {
                    var ll = new LinkedList<Entity>();
                    gameBoard[y].Add(ll);
                }
            }

            initializeBoard();
        }

        private void initializeBoard()
        {
            System.IO.StreamReader stream = new System.IO.StreamReader("../../../levelFiles/level-2.bbiy");
            string line;

            line = stream.ReadLine(); // this reads the title line
            line = stream.ReadLine(); // this reads the dimensions
            string[] dims = line.Split("x");
            Vector2 dimensions = new Vector2(int.Parse(dims[0]), int.Parse(dims[1]));

            // read in background items
            for (int y = 0; y < dimensions.Y; y++)
            {
                line = stream.ReadLine();
                for (int x = 0; x < dimensions.X; x++)
                {
                    addEntity(components.getEntity(line[x], x, y));
                }
            }

            // read in foreground items
            for (int y=0; y< dimensions.Y; y++)
            {
                line = stream.ReadLine();
                for (int x=0; x < dimensions.X; x++)
                {
                    addEntity(components.getEntity(line[x], x, y));
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
