﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Entities;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using BabaIsYou.Entities;
using Microsoft.Xna.Framework.Graphics;

namespace BabaIsYou
{
    public class GameBoard
    {
        List<List<Entity>> gameBoard = new List<List<Entity>>();
        public int GRID_SIZE;
        Dictionary<char, Texture2D> images;

        public GameBoard(int GRID_SIZE, Dictionary<char, Texture2D> images)
        {
            this.GRID_SIZE = GRID_SIZE;
            this.images = images;

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
                    Debug.WriteLine(c);
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
            switch(c)
            {
                //case 'w':
                //    return Wall.create();
                //case 'r':
                //    return Rock.create();
                //case 'f':
                //    return Flag.create();
                //case 'b':
                //    return Baba.create();
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
                    throw new Exception("Unknown entity type while reading in file");
            }
        }

        public void addEntity(Entity entity)
        { 
            Components.Position pos =  entity.GetComponent<Components.Position>();
            gameBoard[pos.y][pos.x] = entity;
        }

        public void removeEntity(Entity entity)
        { 
            Components.Position pos =  entity.GetComponent<Components.Position>();
            gameBoard[pos.y][pos.x] = null;
        }
    }
}