using Components;
using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabaIsYou.Entities
{
    class Grass
    {
        public static Entity create(Texture2D image, int x, int y)
        {
            var grass = new Entity();

            grass.Add(new Appearance(image, computeSourceRectangle, 3, Color.White, Color.White));
            grass.Add(new Position(x, y));
            grass.Add(new Collision());

            return grass;
        }

        private static Rectangle computeSourceRectangle(int frame)
        {
            int x = 24 * frame;
            return new Rectangle(x, 0, 24, 24);
        }
    }
}
