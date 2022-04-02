using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Components
{
    public class BabaComponent: Component
    {
        private Direction previousDirection;
        public Direction direction { get; set; }
        public int directionInARow = 0;
        Dictionary<Direction, Texture2D> babaTextures;

        public BabaComponent(Direction direction, Dictionary<Direction, Texture2D> babaTextures)
        {
            this.direction = direction;
            this.babaTextures = babaTextures;
        }

        public Texture2D image {
            get {
                previousDirection = direction;
                return babaTextures[direction];
            }
        }
    }
}
