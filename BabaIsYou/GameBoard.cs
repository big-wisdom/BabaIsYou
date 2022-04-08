using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Entities;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using BabaIsYou.Entities;
using Microsoft.Xna.Framework.Graphics;
using Components;

namespace BabaIsYou
{
    public class GameBoard
    {
        List<List<Entity>> gameBoard = new List<List<Entity>>();
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
            for (int i=0; i<dimensions.Y; i++)
            {
                line = stream.ReadLine();
                foreach (char c in line)
                {
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
                    return Flag.create(images[c], x, y);
                case 'b':
                    return Baba.create(babaTextures, x, y, you);
                //case 'l':
                //    return Floor.create();
                //case 'g':
                //    return Grass.create();
                //case 'a':
                //    return Water.create();
                //case 'v':
                //    return Lava.create();
                case 'h':
                    return Hedge.create(images[c], x, y);
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
