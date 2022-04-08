using System.Collections.Generic;
using Entities;
using Microsoft.Xna.Framework;
using BabaIsYou.Entities;
using Microsoft.Xna.Framework.Graphics;
using Components;
using BabaIsYou.Entities.words;

namespace BabaIsYou
{
    public class GameBoard
    {
        public List<List<Entity>> gameBoard = new List<List<Entity>>();
        public int GRID_SIZE;
        Dictionary<char, Texture2D> images;
        Dictionary<Direction, Texture2D> babaTextures;
        You you;

        public GameBoard(int GRID_SIZE, Dictionary<char, Texture2D> images, Dictionary<Direction, Texture2D> babaTextures, You you)
        {
            this.GRID_SIZE = GRID_SIZE;
            this.images = images;
            this.babaTextures = babaTextures;
            this.you = you;

            for (int y=0; y<GRID_SIZE; y++) {
                gameBoard.Add(new List<Entity>());
                for (int x=0; x<GRID_SIZE; x++) {
                    gameBoard[y].Add(null);
                }
            }

            initializeBoard();
        }

        private void initializeBoard()
        {
            System.IO.StreamReader stream = new System.IO.StreamReader("../../../levelFiles/level-1.bbiy");
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
                    addEntity(getEntity(line[x], x, y));
                }
            }

            // read in foreground items
            for (int y=0; y< dimensions.Y; y++)
            {
                line = stream.ReadLine();
                for (int x=0; x < dimensions.X; x++)
                {
                    addEntity(getEntity(line[x], x, y));
                }
            }
        }

        private Entity getEntity(char c, int x, int y)
        {
            switch (c)
            {
                case 'w':
                    return Wall.create(images[c], x, y);
                case 'r':
                    return Rock.create(images[c], x, y);
                case 'f':
                    return Lava.create(images[c], x, y);
                case 'b':
                    return Baba.create(babaTextures, x, y, you);
                case 'l':
                    return Floor.create(images[c], x, y);
                case 'g':
                    return Grass.create(images[c], x, y);
                case 'a':
                    return Water.create(images[c], x, y);
                case 'v':
                    return Lava.create(images[c], x, y);
                case 'h':
                    return Hedge.create(images[c], x, y);
                case 'W':
                    return WordWall.create(images[c], x, y);
                case 'R':
                    return WordRock.create(images[c], x, y);
                case 'F':
                    return WordFlag.create(images[c], x, y);
                case 'B':
                    return WordBaba.create(images[c], x, y);
                case 'I':
                    return WordIs.create(images[c], x, y);
                case 'S':
                    return WordStop.create(images[c], x, y);
                case 'P':
                    return WordPush.create(images[c], x, y);
                case 'V':
                    return WordLava.create(images[c], x, y);
                case 'A':
                    return WordWater.create(images[c], x, y);
                case 'Y':
                    return WordYou.create(images[c], x, y);
                case 'X':
                    return WordWin.create(images[c], x, y);
                case 'N':
                    return WordSink.create(images[c], x, y);
                case 'K':
                    return WordKill.create(images[c], x, y);
                default:
                    return null;
            }
        }

        public void addEntity(Entity entity)
        {
            if (entity != null)
            {
                Components.Position pos = entity.GetComponent<Components.Position>();
                gameBoard[pos.y][pos.x] = entity;
            }
        }

        public void removeEntity(Entity entity)
        { 
            Components.Position pos =  entity.GetComponent<Components.Position>();
            gameBoard[pos.y][pos.x] = null;
        }

        public List<Entity> getEntities()
        {
            List<Entity> entities = new List<Entity>();
            foreach (List<Entity> row in gameBoard)
            {
                foreach (Entity e in row)
                {
                    if (e != null)
                    {
                        entities.Add(e);
                    }
                }
            }
            return entities;
        }
    }
}
