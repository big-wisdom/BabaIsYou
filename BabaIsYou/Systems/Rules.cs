using BabaIsYou;
using Microsoft.Xna.Framework;
using Components;
using Entities;
using System.Collections.Generic;
using System;
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
        Kill,
        Hedge, // isn't actually ever a word but this is useful for organising data 
        Floor, // same here
        Grass, // same here
    }

    class Rules : System
    {
        GameBoard gameBoard;
        ComponentContext components;
        Action<Entity> AddEntity;
        Action<Entity> RemoveEntity;
        public Rules(GameBoard gameBoard, ComponentContext components, Action<Entity>RemoveEntity, Action<Entity>AddEntity)
        {
            this.gameBoard = gameBoard;
            this.components = components;
            this.AddEntity = AddEntity;
            this.RemoveEntity = RemoveEntity;
        }

        public override void Update(GameTime gameTime)
        {
            // grab all sentences
            // check for two words following any given word
            int rows = gameBoard.gameBoard.Count;
            int cols = gameBoard.gameBoard[0].Count;
            List<List<Words>> sentences = new List<List<Words>>();

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    if (gameBoard.gameBoard[y][x].Count > 0)
                    {
                        Entity e = gameBoard.gameBoard[y][x].Last.Value; // TODO: Will I ever need to update more than the top one?
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
            applyRules(sentences);
        }


        List<List<Words>> oldRules;
        private void applyRules(List<List<Words>> rules)
        {
            List<List<Words>> undoRules = new List<List<Words>>();
            if (oldRules != null)
            {
                foreach (List<Words> rule in oldRules)
                {
                    bool stillThere = false;
                    foreach (List<Words> r in rules)
                    {
                        if (rule[0] == r[0] && rule[1] == r[1] && rule[2] == r[2]) stillThere = true;
                    }
                    if (!stillThere) undoRules.Add(rule); // if rule that was there last cycle isn't this time, undo it
                }
            }
            oldRules = rules;

            List<Entity> updateList = gameBoard.getEntities();
            for (int i = 0; i < updateList.Count; i++)
            {
                Entity e = updateList[i];
                if (e.ContainsComponent<BabaComponent>())
                { 
                }// remove all components that should only be set by rules
                foreach (List<Words> rule in rules)
                {
                    // if entity has component of first rule
                    if (e.ContainsComponent(getType(rule[0])))
                    {
                        // if entity doesn't have component add it
                        if (!e.ContainsComponent(getType(rule[2])))
                        {
                            // if third is object
                            if (isObject(rule[2]))
                            {
                                // get position from this entity
                                Position p = e.GetComponent<Position>();
                                // replace this entity with a new entity at that position
                                RemoveEntity(e);
                                e = components.getObjectEntity(rule[2], p.x, p.y);
                                AddEntity(e);
                                // add back to update list
                                updateList.Add(e);
                            }
                            else
                            {
                                // get an instance of the new component
                                // if new component is type win or you, add a particle effect
                                // add new component to entity
                                Component newComp = getInstanceOfType(getType(rule[2]));
                                e.Add(newComp); // do I need to make this a more specific type?
                                if (newComp.GetType() == typeof(Components.PushC) && !e.ContainsComponent<Movable>())
                                {
                                    e.Add(new Movable());
                                }
                                else if (newComp.GetType() == typeof(Components.You) || newComp.GetType() == typeof(Components.WinC))
                                {
                                    // particle effect will be created by the particle system
                                    gameBoard.particlePositions.Add(e.GetComponent<Position>());
                                }
                            }
                        }
                    }
                }

                foreach (List<Words> rule in undoRules)
                {
                    if (e.ContainsComponent(getType(rule[0])))
                    {
                        if (e.ContainsComponent(getType(rule[2])) && isQuality(rule[2]))
                        {
                            e.Remove(e.GetComponent(getType(rule[2])));
                        }
                    }
                }

                // re-add entity to systems to update which systems care about this entity
                RemoveEntity(e);
                AddEntity(e);
            }
        }

        private void cleanEntity(Entity e)
        {
            if (!e.ContainsComponent<HedgeC>())
                e.Remove<StopC>();
            if (!e.ContainsComponent<Word>())
                e.Remove<PushC>();
            if (!e.ContainsComponent<WaterC>())
                e.Remove<SinkC>();
            if (!e.ContainsComponent<LavaC>())
                e.Remove<KillC>();
            e.Remove<You>();
            e.Remove<WinC>();
        }

        private Component getInstanceOfType(Type t)
        {
            if (t == typeof(Components.You))
            {
                return components.you;
            } else if (t == typeof(Components.BabaComponent))
            {
                Entity babaEntity = components.getObjectEntity(Words.Baba, 0, 0);
                return babaEntity.GetComponent<BabaComponent>();
            }
            return (Component)Activator.CreateInstance(t);
        }

        private Type getType(Words word)
        {
            switch (word)
            {
                case Words.Wall:
                    return typeof(Components.WallC);
                case Words.Rock:
                    return typeof(Components.RockC);
                case Words.Flag:
                    return typeof(Components.FlagC);
                case Words.Lava:
                    return typeof(Components.LavaC);
                case Words.Baba:
                    return typeof(Components.BabaComponent);
                case Words.Water:
                    return typeof(Components.WaterC);
                case Words.You:
                    return typeof(Components.You);
                case Words.Push:
                    return typeof(Components.PushC);
                case Words.Kill:
                    return typeof(Components.KillC);
                case Words.Win:
                    return typeof(Components.WinC);
                case Words.Sink:
                    return typeof(Components.SinkC);
                case Words.Stop:
                    return typeof(Components.StopC);
                default:
                    throw new Exception("Word: " + word + " does not belong here");
            }
        }

        private List<List<Words>> getSentencesStartingWith(Entity e)
        {
            List<List<Words>> sentences = new List<List<Words>>();
            // check to the right
            List<Words> right = new List<Words>();
            getSentences(e, right, 1, 0, 3);
            sentences.Add(right);

            // check down
            List<Words> down = new List<Words>();
            getSentences(e, down, 0, 1, 3);
            sentences.Add(down);

            return sentences;
        }

        private void getSentences(Entity e, List<Words> soFar, int dx, int dy, int moves)
        {
            if (moves == 0) return;

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
                    if (gameBoard.gameBoard[nextY][nextX].Count > 0)
                    {
                        Entity nextEntity = gameBoard.gameBoard[nextY][nextX].Last.Value;
                        getSentences(nextEntity, soFar, dx, dy, moves-1);
                    }
                }
            }
        }

        private bool validSentence(List<Words> sentence)
        {
            ;
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
            switch (word)
            {
                case Words.Wall:
                case Words.Baba:
                case Words.Flag:
                case Words.Rock:
                case Words.Water:
                case Words.Lava:
                    return true;
                default:
                    return false;
            }
        }

        private bool isQuality(Words word)
        {
            switch (word)
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
