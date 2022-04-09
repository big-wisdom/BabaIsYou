using Components;
using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabaIsYou.Entities.words
{
    class WordStop
    {
        public static Entity create(Texture2D image, int x, int y)
        {
            var stop = new Entity();

            stop.Add(new Appearance(image, computeSourceRectangle, 3, Color.White, Color.White));
            stop.Add(new Position(x, y));
            stop.Add(new Collision());
            stop.Add(new Movable());
            stop.Add(new Word(Systems.Words.Stop));
            stop.Add(new PushC());

            return stop;
        }

        private static Rectangle computeSourceRectangle(int frame)
        {
            int x = 24 * frame;
            return new Rectangle(x, 0, 24, 24);
        }
    }
}
