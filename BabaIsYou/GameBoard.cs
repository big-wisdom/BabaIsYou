using System;
using System.Collections.Generic;
using Entities;

namespace BabaIsYou
{
    public class GameBoard
    {
        List<List<Entity>> gameBoard = new List<List<Entity>>();
        public int GRID_SIZE;

        public GameBoard(int GRID_SIZE)
        {
            this.GRID_SIZE = GRID_SIZE;

            for (int y=0; y<GRID_SIZE; y++) {
                gameBoard.Add(new List<Entity>());
                for (int x=0; x<GRID_SIZE; x++) {
                    gameBoard[y].Add(null);
                }
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
