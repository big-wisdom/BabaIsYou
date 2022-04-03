using System;
using System.Collections.Generic;
using BabaIsYou;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410
{
    public class SettingsView : GameStateView
    {
        private SpriteFont m_font;
        private const string MESSAGE = "This is how to play the game";

        private Dictionary<Keys, Components.Direction> keyFirstControls;
        private Dictionary<Components.Direction, Keys> controlFirstControls;
        private List<Components.Direction> controls;

        KeyboardModel keyboard;
        Components.Direction selectedControl;
        private int selectedIndex;

        Dictionary<Components.Direction, bool> set = new Dictionary<Components.Direction, bool>();
        private bool setLock = false;
        private bool error = false;

        public SettingsView(Dictionary<Keys, Components.Direction> controls, KeyboardModel keyboard) {
            this.keyFirstControls = controls;
            this.controlFirstControls = swapDictionary<Keys, Components.Direction>(controls);

            this.controls = new List<Components.Direction>(controls.Values);
            this.selectedIndex = 0;
            selectedControl = this.controls[selectedIndex];

            // initialize set dictionary
            foreach (Components.Direction d in this.controls) {
                set[d] = false;
            }

            this.keyboard = keyboard;
        }

        public override void loadContent(ContentManager contentManager)
        {
            m_font = contentManager.Load<SpriteFont>("Fonts/menu");
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            foreach (Keys k in keyboard.GetUnlockedKeys())
            {
                if (setLock)
                {
                    foreach (Components.Direction control in set.Keys)
                    {
                        if (set[control])
                        {
                            List<Keys> pressedKeys = keyboard.GetUnlockedKeys();
                            if (pressedKeys.Count > 0)
                            { 
                                Keys oldKey = controlFirstControls[control];
                                controlFirstControls[control] = pressedKeys[0];
                                Dictionary<Keys, Components.Direction> potential = swapDictionary<Components.Direction, Keys>(controlFirstControls);
                                if (potential != null)
                                {
                                    set[control] = false;
                                    setLock = false;
                                    error = false;
                                    keyFirstControls = potential;
                                    // saveSomething();
                                    // gameView.controls = potential; // TODO: this line is supposed to make this effect globally
                                    break;
                                }
                                else
                                {
                                    controlFirstControls[control] = oldKey; // reset the control
                                    error = true;
                                }
                            }
                        }
                    }
                }
                else
                { 
                    // Menu Navigation
                    if (k == Keys.Escape)
                    {
                        return GameStateEnum.MainMenu;
                    }

                    if (k == Keys.Up)
                    { 
                        selectedIndex--;
                        keyboard.lockKey(Keys.Up);
                    }
                    if (k == Keys.Down)
                    { 
                        selectedIndex++;
                        keyboard.lockKey(Keys.Down);
                    }

                    if (selectedIndex < 0)
                        selectedIndex = this.controls.Count - 1;
                    if (selectedIndex == this.controls.Count)
                        selectedIndex = 0;

                    selectedControl = this.controls[selectedIndex];

                    // select one to edit
                    if (k == Keys.Enter)
                    {
                        setLock = true;
                        set[selectedControl] = true;
                        keyboard.lockKey(Keys.Enter);
                    }
                }
            }
            return GameStateEnum.Help;
        }

        public override void render(GameTime gameTime)
        {
            m_spriteBatch.Begin();

            float bottom = 200;
            float previousBottom = bottom;
            foreach (Components.Direction d in controlFirstControls.Keys) {
                String textPart2;
                if (setLock)
                { 
                    if (set[d])
                    {
                        if (error)
                            textPart2 = "Please enter a unique key";
                        else
                            textPart2 = "Press any key to set";
                    }
                    else
                    { 
                        textPart2 = controlFirstControls[d].ToString();
                    }
                }
                else
                {
                    textPart2 = controlFirstControls[d].ToString();
                }
                String text = d.ToString() + ": " + textPart2;

                bottom = drawMenuItem(
                    m_font, 
                    text,
                    previousBottom, 
                    d == selectedControl ? Color.Yellow: Color.Blue);
                previousBottom = bottom;
            }

            m_spriteBatch.End();
        }

        public override void update(GameTime gameTime)
        {
        }

        private float drawMenuItem(SpriteFont font, string text, float y, Color color)
        {
            Vector2 stringSize = font.MeasureString(text);
            m_spriteBatch.DrawString(
                font,
                text,
                new Vector2(m_graphics.PreferredBackBufferWidth / 2 - stringSize.X / 2, y),
                color);

            return y + stringSize.Y;
        }

        private Dictionary<TValue, TKey> swapDictionary<TKey, TValue>(Dictionary<TKey, TValue> source)
        {
            Dictionary<TValue, TKey> result = new Dictionary<TValue, TKey>();
            foreach (var entry in source)
            {
                if (!result.ContainsKey(entry.Value))
                {
                    result.Add(entry.Value, entry.Key); // in case there are duplicate values in the original (should't be a problem here)
                }
                else
                {
                    return null;
                }
            }
            return result;
        }
    }
}
