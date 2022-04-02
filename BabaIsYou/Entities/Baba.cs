using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Entities
{
    public class Baba
    {
        public static Entity create(Dictionary<Components.Direction, Texture2D> babaTextures, int x, int y)
        {
            var border = new Entity();

            border.Add(new Components.BabaComponent(Components.Direction.Down, babaTextures));
            border.Add(new Components.Appearance(babaTextures[Components.Direction.Down], computeSourceRectangle, 15, Color.Red, Color.White));
            border.Add(new Components.Position(x, y));
            border.Add(new Components.Collision());

            return border;
        }

        private static Rectangle computeSourceRectangle(int frame) {
            int x = frame * 24;
            return new Rectangle(x, 0, 24, 24);
        }
    }
}