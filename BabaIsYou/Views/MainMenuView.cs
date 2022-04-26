using BabaIsYou;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace CS5410
{
    public class MainMenuView : GameStateView
    {
        private SpriteFont m_fontMenu;
        private SpriteFont m_fontMenuSelect;
        private KeyboardModel keyboard;

        private enum MenuState
        {
            NewGame,
            Help,
            About,
            Quit
        }

        private int states
        {
            get {
                return Enum.GetNames(typeof(MenuState)).Length;
            }
        }

        private MenuState m_currentSelection = MenuState.NewGame;

        public MainMenuView(KeyboardModel keyboard)
        {
            this.keyboard = keyboard;
        }

        public override void loadContent(ContentManager contentManager)
        {
            m_fontMenu = contentManager.Load<SpriteFont>("Fonts/menu");
            m_fontMenuSelect = contentManager.Load<SpriteFont>("Fonts/menu-select");
        }
        public override GameStateEnum processInput(GameTime gameTime)
        {
            // This is the technique I'm using to ensure one keypress makes one menu navigation move
            foreach (Keys key in keyboard.GetUnlockedKeys())
            {
                // Arrow keys to navigate the menu
                if (key == Keys.Down)
                {
                    m_currentSelection = m_currentSelection + 1;
                    if ((int)m_currentSelection > states-1) m_currentSelection = (MenuState)0;
                    keyboard.lockKey(Keys.Down);
                }
                if (key == Keys.Up)
                {
                    m_currentSelection = m_currentSelection - 1;
                    if ((int)m_currentSelection < 0) m_currentSelection = (MenuState)(states - 1);
                    keyboard.lockKey(Keys.Up);
                }

                // If enter is pressed, return the appropriate new state
                if (key == Keys.Enter)
                {
                    keyboard.lockKey(Keys.Enter);
                    if (m_currentSelection == MenuState.NewGame)
                    {
                        return GameStateEnum.LevelSelect;
                    }
                    if (m_currentSelection == MenuState.Help)
                    {
                        return GameStateEnum.Help;
                    }
                    if (m_currentSelection == MenuState.About)
                    {
                        return GameStateEnum.About;
                    }
                    if (m_currentSelection == MenuState.Quit)
                    {
                        return GameStateEnum.Exit;
                    }
                }
            }
            return GameStateEnum.MainMenu;
        }

        public override void update(GameTime gameTime)
        {
        }
        public override void render(GameTime gameTime)
        {
            m_spriteBatch.Begin();

            // I split the first one's parameters on separate lines to help you see them better
            float bottom = drawMenuItem(
                m_currentSelection == MenuState.NewGame ? m_fontMenuSelect : m_fontMenu, 
                "New Game",
                100, 
                m_currentSelection == MenuState.NewGame ? Color.Yellow : Color.Blue);
            bottom = drawMenuItem(m_currentSelection == MenuState.Help ? m_fontMenuSelect : m_fontMenu, "Settings", bottom, m_currentSelection == MenuState.Help ? Color.Yellow : Color.Blue);
            bottom = drawMenuItem(m_currentSelection == MenuState.About ? m_fontMenuSelect : m_fontMenu, "About", bottom, m_currentSelection == MenuState.About ? Color.Yellow : Color.Blue);
            drawMenuItem(m_currentSelection == MenuState.Quit ? m_fontMenuSelect : m_fontMenu, "Quit", bottom, m_currentSelection == MenuState.Quit ? Color.Yellow : Color.Blue);

            m_spriteBatch.End();
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