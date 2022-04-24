using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BabaIsYou
{
    public class Level
    {
        public string title;
        public Vector2 dimensions;
        public List<List<LinkedList<char>>> charBoard;
        public Level(string title, Vector2 dimensions, List<List<LinkedList<char>>> charBoard)
        {
            this.title = title;
            this.dimensions = dimensions;
            this.charBoard = charBoard;
        }
    }
}
