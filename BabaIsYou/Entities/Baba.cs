using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Entities
{
    public class Baba
    {
        public static Entity create(Texture2D texture, int x, int y)
        {
            var border = new Entity();

            border.Add(new Components.Appearance(texture, Color.Red, Color.Black));
            border.Add(new Components.Position(x, y));
            border.Add(new Components.Collision());

            return border;
        }
    }
}