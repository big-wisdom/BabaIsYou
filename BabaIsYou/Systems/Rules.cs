using BabaIsYou;
using Microsoft.Xna.Framework;
using Components;
using Entities;
using System.Collections.Generic;
using System.Diagnostics;

namespace Systems
{
    public enum Words
    {
        Wall,
        Rock,
        Flag,
        Baba,
        Is,
        Stop,
        Push,
        Lava,
        Water,
        You,
        Win,
        Sink,
        Kill
    }

    class Rules : System
    {
        GameBoard gameBoard;
        public Rules(GameBoard gameBoard)
        {
            this.gameBoard = gameBoard;
        }

        public override void Update(GameTime gameTime)
        {
            // grab all sentences
            // check for two words following any given word
            int rows = gameBoard.gameBoard.Count;
            int cols = gameBoard.gameBoard[0].Count;
            List<List<Words>> sentences = new List<List<Words>>();

            for (int y=0; y<rows; y++)
            {
                for (int x=0; x<cols; x++)
                {
                    Entity e = gameBoard.gameBoard[y][x];
                    if (e != null)
                    {
                        Word word = e.GetComponent<Word>();
                        if (word != null)
                        {
                            sentences.AddRange(getSentencesStartingWith(e));
                        }
                    }
                    
                }
            }

            // validate sentences
            List<List<Words>> removeSentences = new List<List<Words>>();
            foreach (List<Words> sentence in sentences)
            {
                if (!(sentence.Count == 3) || !validSentence(sentence))
                {
                    removeSentences.Add(sentence);
                }
            }
            foreach (List<Words> sentence in removeSentences) sentences.Remove(sentence);

            // apply components
            foreach (List<Words> sentence in sentences)
            { 
                // apply components
                foreach (Words word in sentence)
                {
                    Debug.Write(word + " ");
                }
                Debug.Write("\n");
            }
        }

        private List<List<Words>> getSentencesStartingWith(Entity e)
        {
            List<List<Words>> sentences = new List<List<Words>>();
            // check to the right
            List<Words> right = new List<Words>();
            getSentences(e, right, 1, 0);
            sentences.Add(right);

            // check down
            List<Words> down = new List<Words>();
            getSentences(e, down, 0, 1);
            sentences.Add(down);

            return sentences;
        }

        private void getSentences(Entity e, List<Words> soFar, int dx, int dy)
        {
            // if this entity is a word, add it to the list and move to the next cell
            Word word = e.GetComponent<Word>();
            if (word != null)
            {
                soFar.Add(word.word);

                // move onto the next cell if it's in range
                Position p = e.GetComponent<Position>();
                int nextX = p.x + dx;
                int nextY = p.y + dy;
                if (nextY < gameBoard.gameBoard.Count && nextX < gameBoard.gameBoard[0].Count)
                {
                    Entity nextEntity = gameBoard.gameBoard[nextY][nextX];
                    if (nextEntity != null)
                    {
                        getSentences(nextEntity, soFar, dx, dy);
                    }

                }
            }
        }

        private bool validSentence(List<Words> sentence)
        {;
            // first should be object
            if (!isObject(sentence[0])) return false;

            // second should be is
            if (!(sentence[1] == Words.Is)) return false;

            // third should be object or quality
            if (!isObject(sentence[2]) && !isQuality(sentence[2]))
                return false;

            return true;
        }

        private bool isObject(Words word)
        { 
            switch(word)
            {
                case Words.Wall:
                case Words.Baba:
                case Words.Flag:
                case Words.Rock:
                case Words.Water:
                    return true;
                default:
                    return false;
            }
        }

        private bool isQuality(Words word)
        { 
            switch(word)
            {
                case Words.Kill:
                case Words.Push:
                case Words.Sink:
                case Words.Stop:
                case Words.Win:
                case Words.You:
                    return true;
                default:
                    return false;
            }
        }
    }
}
