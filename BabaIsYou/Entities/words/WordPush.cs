using Components;
using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabaIsYou.Entities.words
{
    class WordPush
    {
        public static Entity create(Texture2D image, int x, int y)
        {
            var push = new Entity();

            push.Add(new Appearance(image, computeSourceRectangle, 3, Color.White, Color.White));
            push.Add(new Position(x, y));
            push.Add(new Collision());
            push.Add(new Word(Systems.Words.Push));

            return push;
        }

        private static Rectangle computeSourceRectangle(int frame)
        {
            int x = 24 * frame;
            return new Rectangle(x, 0, 24, 24);
        }
    }
}
