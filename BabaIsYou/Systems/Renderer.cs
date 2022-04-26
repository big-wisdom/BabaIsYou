using System;
using System.Collections.Generic;
using BabaIsYou;
using BabaIsYou.Particles;
using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Systems
{
    class Renderer : System
    {
        TimeSpan animationTime = TimeSpan.FromMilliseconds(100);
        TimeSpan timeLeft = TimeSpan.FromMilliseconds(150);
        bool animate = false;
        private readonly GameBoard gameBoard;
        private readonly int CELL_SIZE;
        private readonly SpriteBatch m_spriteBatch;

        public Renderer(SpriteBatch spriteBatch, int CELL_SIZE, GameBoard gameBoard) :
            base(typeof(Components.Appearance), typeof(Components.Position))
        {
            this.CELL_SIZE = CELL_SIZE;
            m_spriteBatch = spriteBatch;
            this.gameBoard = gameBoard;
        }

        public override void Update(GameTime gameTime)
        {
            timeLeft -= gameTime.ElapsedGameTime;
            if (timeLeft < TimeSpan.Zero)
            {
                timeLeft = animationTime;
                animate = true;
            }

            m_spriteBatch.Begin();

            foreach (List<LinkedList<Entity>> row in gameBoard.gameBoard)
            { 
                foreach (LinkedList<Entity> cell in row)
                { 
                    foreach (Entity e in cell)
                    {
                        if (e != null)
                            renderEntity(e);
                    }
                }
            }
            animate = false;

            // render each particle emitter on the gameBoard
            foreach (ParticleEmitter p in gameBoard.particleEmmiters)
            {
                p.draw(m_spriteBatch);
            }

            m_spriteBatch.End();
        }

        private void renderEntity(Entity entity)
        {
            var appearance = entity.GetComponent<Components.Appearance>();

            var position = entity.GetComponent<Components.Position>();
            var baba = entity.GetComponent<Components.BabaComponent>();

            if (animate) appearance.nextFrame();

            Rectangle area = new Rectangle();

            area.X = position.x * CELL_SIZE;
            area.Y = position.y * CELL_SIZE;
            area.Width = CELL_SIZE;
            area.Height = CELL_SIZE;

            // compute texture
            Texture2D texture;
            if (baba == null) {
                texture = appearance.image;
            }
            else {
                texture = baba.image;
                appearance.frame = baba.directionInARow;
            }
            // compute source rectangle
            Nullable<Rectangle> sourceRectangle = appearance.sourceRectangle;

            // if there is a sourceRectangle, use it
            if (appearance.sourceRectangle != null)
            {
                m_spriteBatch.Draw(texture, area, sourceRectangle, appearance.stroke);
            }
            else
            {
                m_spriteBatch.Draw(texture, area, appearance.stroke);
            }

            

            //for (int segment = 0; segment < position.segments.Count; segment++)
            //{
            //    area.X = OFFSET_X + position.segments[segment].X * CELL_SIZE;
            //    area.Y = OFFSET_Y + position.segments[segment].Y * CELL_SIZE;
            //    area.Width = CELL_SIZE;
            //    area.Height = CELL_SIZE;

            //    m_spriteBatch.Draw(appearance.image, area, appearance.stroke);

            //    area.X = OFFSET_X + position.segments[segment].X * CELL_SIZE + 1;
            //    area.Y = OFFSET_Y + position.segments[segment].Y * CELL_SIZE + 1;
            //    area.Width = CELL_SIZE - 2;
            //    area.Height = CELL_SIZE - 2;
            //    float fraction = MathHelper.Min(segment / 30.0f, 1.0f);
            //    var color = new Color(
            //        (int)lerp(appearance.fill.R, 0, fraction),
            //        (int)lerp(appearance.fill.G, 0, fraction),
            //        (int)lerp(appearance.fill.B, 255, fraction));
            //    m_spriteBatch.Draw(appearance.image, area, color);
            //}
        }

        //private float lerp(float a, float b, float f)
        //{
        //    return a + f * (b - a);
        //}

    }
}
