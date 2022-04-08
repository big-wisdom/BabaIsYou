using Components;
using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabaIsYou.Entities.words
{
    class WordWin
    {
        public static Entity create(Texture2D image, int x, int y)
        {
            var win = new Entity();

            win.Add(new Appearance(image, computeSourceRectangle, 3, Color.White, Color.White));
            win.Add(new Position(x, y));
            win.Add(new Collision());
            win.Add(new Word(Systems.Words.Win));

            return win;
        }

        private static Rectangle computeSourceRectangle(int frame)
        {
            int x = 24 * frame;
            return new Rectangle(x, 0, 24, 24);
        }
    }
}
