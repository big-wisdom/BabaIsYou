using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Entities
{
    public class Baba
    {
        public static Entity create(Texture2D texture, int x, int y)
        {
            var border = new Entity();
            Rectangle rec = new Rectangle(0, 0, 24, 24);

            border.Add(new Components.Appearance(texture, rec, Color.Red, Color.White));
            border.Add(new Components.Position(x, y));
            border.Add(new Components.Collision());

            return border;
        }
    }
}