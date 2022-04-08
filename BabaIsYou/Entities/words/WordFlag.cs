using Components;
using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabaIsYou.Entities.words
{
    class WordFlag
    {
        public static Entity create(Texture2D image, int x, int y)
        {
            var flag = new Entity();

            flag.Add(new Appearance(image, computeSourceRectangle, 3, Color.White, Color.White));
            flag.Add(new Position(x, y));
            flag.Add(new Collision());

            return flag;
        }

        private static Rectangle computeSourceRectangle(int frame)
        {
            int x = 24 * frame;
            return new Rectangle(x, 0, 24, 24);
        }
    }
}
