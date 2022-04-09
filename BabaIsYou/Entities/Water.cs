using Components;
using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabaIsYou.Entities
{
    class Water
    {
        public static Entity create(Texture2D image, int x, int y)
        {
            var water = new Entity();

            water.Add(new Appearance(image, computeSourceRectangle, 3, Color.White, Color.White));
            water.Add(new Position(x, y));
            water.Add(new Collision());
            water.Add(new WaterC());

            return water;
        }

        private static Rectangle computeSourceRectangle(int frame)
        {
            int x = 24 * frame;
            return new Rectangle(x, 0, 24, 24);
        }
    }
}
