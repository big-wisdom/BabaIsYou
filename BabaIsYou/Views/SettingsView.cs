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

        private Controls controls;
        private List<Components.Direction> controlsList;

        KeyboardModel keyboard;
        Components.Direction selectedControl;
        private int selectedIndex;

        Dictionary<Components.Direction, bool> set = new Dictionary<Components.Direction, bool>();
        private bool setLock = false;
        private bool error = false;

        public SettingsView(Controls controls, KeyboardModel keyboard) {
            this.controls = controls;
            this.controlsList = controls.controlsList;

            this.selectedIndex = 0;
            selectedControl = this.controlsList[selectedIndex];

            // initialize set dictionary
            foreach (Components.Direction d in this.controlsList) {
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
                                Keys oldKey = controls.getKey(control); // this should never be null
                                if (controls.setControl(pressedKeys[0], control))
                                { 
                                    set[control] = false;
                                    setLock = false;
                                    error = false;
                                    // saveSomething();
                                    break;
                                }
                                else {
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
                        selectedIndex = this.controlsList.Count - 1;
                    if (selectedIndex == this.controlsList.Count)
                        selectedIndex = 0;

                    selectedControl = this.controlsList[selectedIndex];

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
            foreach (Components.Direction d in controlsList) {
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
                        textPart2 = controls.getKey(d).ToString();
                    }
                }
                else
                {
                    textPart2 = controls.getKey(d).ToString();
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

    }
}
