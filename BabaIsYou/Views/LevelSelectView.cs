using BabaIsYou;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS5410
{
    class LevelSelectView : GameStateView
    {
        private SpriteFont m_fontMenu;
        private SpriteFont m_fontMenuSelect;
        private KeyboardModel keyboard;
        private Levels levels;

        public LevelSelectView(KeyboardModel keyboard, Levels levels)
        {
            this.keyboard = keyboard;
            this.levels = levels;
        }

        public override void loadContent(ContentManager contentManager)
        {
            m_fontMenu = contentManager.Load<SpriteFont>("Fonts/menu");
            m_fontMenuSelect = contentManager.Load<SpriteFont>("Fonts/menu-select");
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            foreach (Keys key in keyboard.GetUnlockedKeys())
            {
                if (key == Keys.Escape)
                {
                    keyboard.lockKey(Keys.Escape);
                    return GameStateEnum.MainMenu;
                }

                // Arrow keys to navigate the menu
                if (key == Keys.Down)
                {
                    levels.nextLevel();
                    keyboard.lockKey(Keys.Down);
                }
                if (key == Keys.Up)
                {
                    levels.previousLevel();
                    keyboard.lockKey(Keys.Up);
                }

                // If enter is pressed, return the appropriate new state
                if (key == Keys.Enter)
                {
                    keyboard.lockKey(Keys.Enter);
                    return GameStateEnum.GamePlay;
                }
            }

            return GameStateEnum.LevelSelect;
        }

        public override void update(GameTime gameTime)
        {
        }
        public override void render(GameTime gameTime)
        {
            m_spriteBatch.Begin();

            float bottom = 100;
            foreach (string level in levels.getLevelsList())
            {
                bool current = levels.currentLevel.title == level;
                SpriteFont font = current ? m_fontMenuSelect : m_fontMenu;
                Color color = current ? Color.Yellow : Color.Blue;
                bottom = drawMenuItem(font, level, bottom, color);
            }

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
