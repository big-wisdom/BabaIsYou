using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BabaIsYou
{
    class Level
    {
        string title;
        Vector2 dimensions;
        List<List<LinkedList<char>>> charBoard;
        public Level(string title, Vector2 dimensions, List<List<LinkedList<char>>> charBoard)
        {
            this.title = title;
            this.dimensions = dimensions;
            this.charBoard = charBoard;
        }
    }
}
