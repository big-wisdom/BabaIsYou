using Components;
using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabaIsYou.Entities.words
{
    class WordKill
    {
        public static Entity create(Texture2D image, int x, int y)
        {
            var kill = new Entity();

            kill.Add(new Appearance(image, computeSourceRectangle, 3, Color.White, Color.White));
            kill.Add(new Position(x, y));
            kill.Add(new Collision());
            kill.Add(new Movable());
            kill.Add(new Word(Systems.Words.Kill));
            kill.Add(new PushC());

            return kill;
        }

        private static Rectangle computeSourceRectangle(int frame)
        {
            int x = 24 * frame;
            return new Rectangle(x, 0, 24, 24);
        }
    }
}
