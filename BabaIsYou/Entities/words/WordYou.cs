using Components;
using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabaIsYou.Entities.words
{
    class WordYou
    {
        public static Entity create(Texture2D image, int x, int y)
        {
            var you = new Entity();

            you.Add(new Appearance(image, computeSourceRectangle, 3, Color.White, Color.White));
            you.Add(new Position(x, y));
            you.Add(new Collision());
            you.Add(new Word(Systems.Words.You));

            return you;
        }

        private static Rectangle computeSourceRectangle(int frame)
        {
            int x = 24 * frame;
            return new Rectangle(x, 0, 24, 24);
        }
    }
}
