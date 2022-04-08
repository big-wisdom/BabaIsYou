using Components;
using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabaIsYou.Entities.words
{
    class WordSink
    {
        public static Entity create(Texture2D image, int x, int y)
        {
            var sink = new Entity();

            sink.Add(new Appearance(image, computeSourceRectangle, 3, Color.White, Color.White));
            sink.Add(new Position(x, y));
            sink.Add(new Collision());

            return sink;
        }

        private static Rectangle computeSourceRectangle(int frame)
        {
            int x = 24 * frame;
            return new Rectangle(x, 0, 24, 24);
        }
    }
}
