using Entities;
using Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BabaIsYou.Entities
{
    class Hedge
    {
        public static Entity create(Texture2D image, int x, int y)
        {
            var hedge = new Entity();

            hedge.Add(new Appearance(image, computeSourceRectangle, 3, Color.White, Color.White));
            hedge.Add(new Position(x, y));
            hedge.Add(new Collision());
            hedge.Add(new StopC());

            return hedge;
        }

        private static Rectangle computeSourceRectangle(int frame)
        {
            int x = 24 * frame;
            return new Rectangle(x, 0, 24, 24);
        }

    }
}
