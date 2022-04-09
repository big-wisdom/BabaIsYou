using Components;
using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabaIsYou.Entities.words
{
    class WordIs
    {
        public static Entity create(Texture2D image, int x, int y)
        {
            var wordIs = new Entity();

            wordIs.Add(new Appearance(image, computeSourceRectangle, 3, Color.White, Color.White));
            wordIs.Add(new Position(x, y));
            wordIs.Add(new Collision());
            wordIs.Add(new Movable());
            wordIs.Add(new Word(Systems.Words.Is));
            wordIs.Add(new PushC());

            return wordIs;
        }

        private static Rectangle computeSourceRectangle(int frame)
        {
            int x = 24 * frame;
            return new Rectangle(x, 0, 24, 24);
        }
    }
}
