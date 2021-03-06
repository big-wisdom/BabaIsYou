using Components;
using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabaIsYou.Entities
{
    class Lava
    {
        public static Entity create(Texture2D image, int x, int y)
        {
            var lava = new Entity();

            lava.Add(new Appearance(image, computeSourceRectangle, 3, Color.White, Color.Orange));
            lava.Add(new Position(x, y));
            lava.Add(new Movable());
            lava.Add(new Collision());
            lava.Add(new LavaC());

            return lava;
        }

        private static Rectangle computeSourceRectangle(int frame)
        {
            int x = 24 * frame;
            return new Rectangle(x, 0, 24, 24);
        }
    }
}
