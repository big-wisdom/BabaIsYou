using System;
using System.Collections.Generic;
using BabaIsYou;
using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Systems
{

    class Renderer : System
    {
        private readonly GameBoard gameBoard;
        private readonly int CELL_SIZE;
        private readonly SpriteBatch m_spriteBatch;

        public Renderer(SpriteBatch spriteBatch, int width, int height, GameBoard gameBoard) :
            base(typeof(Components.Appearance), typeof(Components.Position))
        {
            CELL_SIZE = height / gameBoard.GRID_SIZE;
            m_spriteBatch = spriteBatch;
            this.gameBoard = gameBoard;
        }

        public override void Update(GameTime gameTime)
        {
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
            //foreach (var entity in m_entities.Values)
            //{
            //    renderEntity(entity);
            //}

            m_spriteBatch.End();
        }

        private void renderEntity(Entity entity)
        {
            var appearance = entity.GetComponent<Components.Appearance>();
            var position = entity.GetComponent<Components.Position>();
            var baba = entity.GetComponent<Components.BabaComponent>();

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
