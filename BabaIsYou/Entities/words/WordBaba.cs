using Components;
using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabaIsYou.Entities.words
{
    class WordBaba
    {
        public static Entity create(Texture2D image, int x, int y)
        {
            var baba = new Entity();

            baba.Add(new Appearance(image, computeSourceRectangle, 3, Color.White, Color.DeepPink));
            baba.Add(new Position(x, y));
            baba.Add(new Collision());
            baba.Add(new Movable());
            baba.Add(new Word(Systems.Words.Baba));
            baba.Add(new PushC());

            return baba;
        }

        private static Rectangle computeSourceRectangle(int frame)
        {
            int x = 24 * frame;
            return new Rectangle(x, 0, 24, 24);
        }
    }
}
