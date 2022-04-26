using BabaIsYou;
using BabaIsYou.Particles;
using Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;

namespace Systems
{
    class Particles : System
    {
        GameBoard gameBoard;
        ContentManager content;
        private int CELL_SIZE;
        private TimeSpan rate = TimeSpan.FromMilliseconds(10);
        private TimeSpan lifetime = TimeSpan.FromMilliseconds(500);
        GameState gameState;
        private bool fireworksLock = false;
        public bool fireworksDone = false;

        public Particles(ContentManager content, GameBoard gameBoard, int CELL_SIZE, GameState gameState)
        {
            this.gameBoard = gameBoard;
            this.content = content;
            this.CELL_SIZE = CELL_SIZE;
            this.gameState = gameState;
        }

        public override void Update(GameTime gameTime)
        {
            // go through particle positions in gameBoard
            foreach (Position p in gameBoard.particlePositions)
            {
                // create a particle emitter for each
                int sourceX = (p.x * CELL_SIZE) + (CELL_SIZE / 2);
                int sourceY = (p.y * CELL_SIZE) + (CELL_SIZE / 2);
                gameBoard.particleEmmiters.Add(new ParticleEmitter(content, rate, sourceX, sourceY, CELL_SIZE, 1, lifetime, TimeSpan.Zero));
            }
            gameBoard.particlePositions.Clear();


            // update all particle emitters
            foreach (ParticleEmitter p in gameBoard.particleEmmiters)
            {
                p.update(gameTime);
            }

            if (gameState.win)
            {
                if (fireworksLock && gameBoard.particleEmmiters.Count == 0)
                {
                    fireworksDone = true;
                }
                else
                    fireworks();
            }

            // remove all emitters that are complete
            gameBoard.particleEmmiters.RemoveAll((emitter) => emitter.done);
        }

        private void fireworks()
        {
            if (fireworksLock) return;

            TimeSpan fireworkRate = TimeSpan.FromMilliseconds(10);
            Random random = new Random();
            int fireworkX;
            int fireworkY;
            TimeSpan delay;
            for (int i=0; i<10; i++)
            {
                fireworkX = random.Next(0, gameBoard.gameBoard[0].Count * CELL_SIZE);
                fireworkY = random.Next(0, gameBoard.gameBoard.Count * CELL_SIZE);
                delay = TimeSpan.FromMilliseconds(random.Next(0, 3000));
                gameBoard.particleEmmiters.Add(new ParticleEmitter(content, fireworkRate, fireworkX, fireworkY, CELL_SIZE, 1, lifetime, delay));
            }

            fireworksLock = true; // ensure this only runs once
        }
    }
}
