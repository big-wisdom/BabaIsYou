using Components;
using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabaIsYou.Entities.words
{
    class WordWall
    {
        public static Entity create(Texture2D image, int x, int y)
        {
            var wall = new Entity();

            wall.Add(new Appearance(image, computeSourceRectangle, 3, Color.White, Color.DeepPink));
            wall.Add(new Position(x, y));
            wall.Add(new Collision());
            wall.Add(new Movable());
            wall.Add(new Word(Systems.Words.Wall));
            wall.Add(new PushC());

            return wall;
        }

        private static Rectangle computeSourceRectangle(int frame)
        {
            int x = 24 * frame;
            return new Rectangle(x, 0, 24, 24);
        }
    }
}
