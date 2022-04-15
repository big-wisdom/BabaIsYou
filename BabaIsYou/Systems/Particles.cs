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
        private TimeSpan lifetime = TimeSpan.FromMilliseconds(1000);
        private TimeSpan switchover = TimeSpan.FromMilliseconds(1000);

        public Particles(ContentManager content, GameBoard gameBoard, int CELL_SIZE)
        {
            this.gameBoard = gameBoard;
            this.content = content;
            this.CELL_SIZE = CELL_SIZE;
        }

        public override void Update(GameTime gameTime)
        {
            // go through particle positions in gameBoard
            foreach (Position p in gameBoard.particlePositions)
            {
                // create a particle emitter for each
                int sourceX = (p.x * CELL_SIZE) + (CELL_SIZE / 2);
                int sourceY = (p.y * CELL_SIZE) + (CELL_SIZE / 2);
                gameBoard.particleEmmiters.Add(new ParticleEmitter(content, rate, sourceX, sourceY, CELL_SIZE/2, 1, lifetime, switchover));
            }
            gameBoard.particlePositions.Clear();


            // update all particle emitters
            foreach (ParticleEmitter p in gameBoard.particleEmmiters)
            {
                p.update(gameTime);
            }
        }
    }
}
