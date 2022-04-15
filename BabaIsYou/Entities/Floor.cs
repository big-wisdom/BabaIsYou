using Components;
using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabaIsYou.Entities
{
    class Floor
    {
        public static Entity create(Texture2D image, int x, int y)
        {
            var floor = new Entity();

            floor.Add(new Appearance(image, computeSourceRectangle, 3, Color.White, Color.Gray));
            floor.Add(new Position(x, y));
            floor.Add(new Collision());

            return floor;
        }

        private static Rectangle computeSourceRectangle(int frame)
        {
            int x = 24 * frame;
            return new Rectangle(x, 0, 24, 24);
        }
    }
}
