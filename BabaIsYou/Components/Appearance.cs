using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Components
{
    public class Appearance : Component
    {
        public Texture2D image;
        public Color fill;
        public Color stroke;
        private Func<int, Rectangle> computeSourceRectangle;
        private int frames;
        public int frame { get; set; }

        public Appearance(Texture2D image, Color fill, Color stroke)
        {
            this.image = image;
            this.fill = fill;
            this.stroke = stroke;
        }

        public Appearance(Texture2D image, Func<int, Rectangle> computeSourceRectangle, int frames, Color fill, Color stroke)
        {
            this.image = image;
            this.computeSourceRectangle = computeSourceRectangle;
            this.frames = frames;
            this.fill = fill;
            this.stroke = stroke;
        }

        public void nextFrame() {
            frame++;
            if (frame == frames) frame = 0;
        }

        public Nullable<Rectangle> sourceRectangle { 
            get {
                if (computeSourceRectangle == null) return null;
                return computeSourceRectangle(frame);
            }
        }
    }
}
