using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BabaIsYou
{
    // this class contains capability to initialize the list of levels from a file
    // It also tracks current level. The previous and next methods cycle levels forward and backward.
    // getLevelsList() returns a list of the levels by name for the menu select view
    public class Levels
    {
        private List<Level> levels;
        public int currentIndex = 0;
        public Level currentLevel
        {
            get
            {
                return levels[currentIndex];
            }
        }
        public void nextLevel()
        {
            currentIndex++;
            if (currentIndex > levels.Count - 1) currentIndex = 0;
        }

        public void previousLevel()
        {
            currentIndex--;
            if (currentIndex < 0) currentIndex = levels.Count - 1;
        }

        public List<String> getLevelsList()
        {
            return levels.Select(level => level.title).ToList();
        }

        // Right now all this class does is initialize a list of level objects and provide a currentLevel
        public Levels(String path)
        {
            List<Level> levelsList = new List<Level>();
            // while file not over, read level
            Stream freshStream = TitleContainer.OpenStream(path);
            StreamReader stream = new StreamReader(freshStream);
            Level level;
            while((level = readLevel(stream)) != null)
            {
                levelsList.Add(level);
            }
            this.levels = levelsList;
        }

        private Level readLevel(StreamReader stream)
        {
            string titleLine = stream.ReadLine(); // this reads the title line
            if (titleLine == null) return null;
            string dimsString = stream.ReadLine(); // this reads the dimensions
            string[] dims = dimsString.Split("x");
            Vector2 dimensions = new Vector2(int.Parse(dims[0]), int.Parse(dims[1]));
            List<List<LinkedList<char>>> board = initializeList(dimensions);

            // read in background items
            singleScan(stream, dimensions, board);

            // read in foreground items
            singleScan(stream, dimensions, board);

            return new Level(titleLine, dimensions, board);
        }

        private List<List<LinkedList<char>>> singleScan(StreamReader stream, Vector2 dimensions, List<List<LinkedList<char>>> board)
        {
            // verify that dimensions match board
            bool yMatch = dimensions.Y == board.Count;
            bool hasRow = board[0] != null;
            bool xMatch = dimensions.X == board[0].Count;
            if (!(yMatch && hasRow && xMatch))
            {
                throw new Exception("Board mismatched with given dimensions");
            }
            
            string line;
            for (int y = 0; y < dimensions.Y; y++)
            {
                line = stream.ReadLine();
                var row = board[y];
                for (int x = 0; x < dimensions.X; x++)
                {
                    LinkedList<char> cell = board[y][x];
                    if (line[x] != ' ')
                    {
                        cell.AddLast(line[x]);
                    }
                }
            }

            return board;
        }

        private List<List<LinkedList<char>>> initializeList(Vector2 dimensions)
        {
            List<List<LinkedList<char>>> result = new List<List<LinkedList<char>>>();
            for (int i=0; i<dimensions.Y; i++)
            {
                List<LinkedList<char>> row = new List<LinkedList<char>>();
                for (int x=0; x<dimensions.X; x++)
                {
                    row.Add(new LinkedList<char>());
                }
                result.Add(row);
            }
            return result;
        }
    }
}
