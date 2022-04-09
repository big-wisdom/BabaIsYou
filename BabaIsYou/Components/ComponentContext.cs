using System.Collections.Generic;
using BabaIsYou.Entities;
using BabaIsYou.Entities.words;
using Entities;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Systems;

namespace Components
{
    public class ComponentContext
    {
        public Dictionary<Words, Appearance> appearances = new Dictionary<Words, Appearance>();
        public You you;

        public Dictionary<Direction, Texture2D> babaTextures;

        public Dictionary<char, Words> letterToWord;
        public Dictionary<Words, Texture2D> images;

        public Dictionary<char, Words> letterToWordWord;
        public Dictionary<Words, Texture2D> wordImages;


        public ComponentContext(ContentManager content, You youComponent)
        {
            this.you = youComponent;

            // create a dictionary that links directions to textures for baba
            babaTextures = new Dictionary<Components.Direction, Texture2D>() {
                { Components.Direction.Up, content.Load<Texture2D>("baba/bunnyUp") },
                { Components.Direction.Right, content.Load<Texture2D>("baba/bunnyRight") },
                { Components.Direction.Down, content.Load<Texture2D>("baba/bunnyDown") },
                { Components.Direction.Left, content.Load<Texture2D>("baba/bunnyLeft") }
            };

            letterToWord = new Dictionary<char, Words>()
            {
                { 'h', Words.Hedge },
                { 'w', Words.Wall },
                { 'r', Words.Rock },
                { 'f', Words.Flag },
                { 'a', Words.Water },
                { 'g', Words.Grass },
                { 'l', Words.Floor },
                { 'v', Words.Lava },
                { 'b', Words.Baba },
            };

            letterToWordWord = new Dictionary<char, Words>()
            {
                { 'B', Words.Baba },
                { 'F', Words.Flag },
                { 'I', Words.Is },
                { 'K', Words.Kill },
                { 'V', Words.Lava },
                { 'P', Words.Push },
                { 'R', Words.Rock },
                { 'N', Words.Sink },
                { 'S', Words.Stop },
                { 'W', Words.Wall },
                { 'A', Words.Water },
                { 'X', Words.Win },
                { 'Y', Words.You },
            };

            images = new Dictionary<Words, Texture2D>()
            {
                { Words.Hedge, content.Load<Texture2D>("objects/hedge") },
                { Words.Wall, content.Load<Texture2D>("objects/wall") },
                { Words.Rock, content.Load<Texture2D>("objects/rock") },
                { Words.Flag, content.Load<Texture2D>("objects/flag") },
                { Words.Water, content.Load<Texture2D>("objects/water") },
                { Words.Grass, content.Load<Texture2D>("objects/grass") },
                { Words.Floor, content.Load<Texture2D>("objects/floor") },
                { Words.Lava, content.Load<Texture2D>("objects/lava") },
            };

            wordImages = new Dictionary<Words, Texture2D>() {
                { Words.Baba, content.Load<Texture2D>("words/word-baba") },
                { Words.Flag, content.Load<Texture2D>("words/word-flag") },
                { Words.Is, content.Load<Texture2D>("words/word-is") },
                { Words.Lava, content.Load<Texture2D>("words/word-lava") },
                { Words.Push, content.Load<Texture2D>("words/word-push") },
                { Words.Rock, content.Load<Texture2D>("words/word-rock") },
                { Words.Sink, content.Load<Texture2D>("words/word-sink") },
                { Words.Stop, content.Load<Texture2D>("words/word-stop") },
                { Words.Wall, content.Load<Texture2D>("words/word-wall") },
                { Words.Water, content.Load<Texture2D>("words/word-water") },
                { Words.Win, content.Load<Texture2D>("words/word-win") },
                { Words.You, content.Load<Texture2D>("words/word-you") },
                { Words.Kill, content.Load<Texture2D>("words/word-kill") },
            };
        }

        public Entity getEntity(char c, int x, int y)
        {
            if (letterToWord.ContainsKey(c))
            {
                Words word = letterToWord[c];
                switch (word)
                {
                    case Words.Wall:
                        return Wall.create(images[word], x, y);
                    case Words.Rock:
                        return Rock.create(images[word], x, y);
                    case Words.Lava:
                        return Lava.create(images[word], x, y);
                    case Words.Baba:
                        return Baba.create(babaTextures, x, y, you);
                    case Words.Floor:
                        return Floor.create(images[word], x, y);
                    case Words.Grass:
                        return Grass.create(images[word], x, y);
                    case Words.Water:
                        return Water.create(images[word], x, y);
                    case Words.Hedge:
                        return Hedge.create(images[word], x, y);
                    case Words.Flag:
                        return Flag.create(images[word], x, y);
                    default:
                        return null;
                }
            }
            else if (letterToWordWord.ContainsKey(c))
            {
                Words word = letterToWordWord[c];
                switch (word)
                {
                    case Words.Wall:
                        return WordWall.create(wordImages[word], x, y);
                    case Words.Rock:
                        return WordRock.create(wordImages[word], x, y);
                    case Words.Flag:
                        return WordFlag.create(wordImages[word], x, y);
                    case Words.Baba:
                        return WordBaba.create(wordImages[word], x, y);
                    case Words.Is:
                        return WordIs.create(wordImages[word], x, y);
                    case Words.Stop:
                        return WordStop.create(wordImages[word], x, y);
                    case Words.Push:
                        return WordPush.create(wordImages[word], x, y);
                    case Words.Lava:
                        return WordLava.create(wordImages[word], x, y);
                    case Words.Water:
                        return WordWater.create(wordImages[word], x, y);
                    case Words.You:
                        return WordYou.create(wordImages[word], x, y);
                    case Words.Win:
                        return WordWin.create(wordImages[word], x, y);
                    case Words.Sink:
                        return WordSink.create(wordImages[word], x, y);
                    case Words.Kill:
                        return WordKill.create(wordImages[word], x, y);
                    default:
                        return null;
                }
            }
            else
                return null;
        }
    }
}
