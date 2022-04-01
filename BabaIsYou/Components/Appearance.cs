using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Components
{
    public class Appearance : Component
    {
        public Texture2D image;
        public Color fill;
        public Color stroke;
        public Rectangle sourceRectangle;

        public Appearance(Texture2D image, Color fill, Color stroke)
        {
            this.image = image;
            this.fill = fill;
            this.stroke = stroke;
        }

        public Appearance(Texture2D image, Rectangle sourceRectangle, Color fill, Color stroke)
        {
            this.image = image;
            this.sourceRectangle = sourceRectangle;
            this.fill = fill;
            this.stroke = stroke;
        }
    }
}
