using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Entities
{
    public class Baba
    {
        public static Entity create(Dictionary<Components.Direction, Texture2D> babaTextures, int x, int y, Components.You you)
        {
            var baba = new Entity();

            baba.Add(new Components.BabaComponent(Components.Direction.Down, babaTextures));
            baba.Add(new Components.Appearance(babaTextures[Components.Direction.Down], computeSourceRectangle, 15, Color.Red, Color.White));
            baba.Add(new Components.Position(x, y));
            baba.Add(new Components.Movable());
            baba.Add(new Components.Collision());
            baba.Add(you);

            return baba;
        }

        private static Rectangle computeSourceRectangle(int frame) {
            int x = frame * 24;
            return new Rectangle(x, 0, 24, 24);
        }
    }
}