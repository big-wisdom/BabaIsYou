using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Components
{
    public class BabaComponent: Component
    {
        private Direction previousDirection;
        public Direction direction {
            set
            {
                if (value == previousDirection)
                    directionInARow++;
                else
                {
                    previousDirection = value;
                    directionInARow = 0;
                }
            }
            get => previousDirection;
        }

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
