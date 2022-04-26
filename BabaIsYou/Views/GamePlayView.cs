using System.Collections.Generic;
using BabaIsYou;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace CS5410
{
    public class GamePlayView : GameStateView
    {
        Levels levels;
        Controls controls;
        KeyboardModel keyboard;

        public GamePlayView(Levels levels, Controls controls, KeyboardModel keyboard)
        {
            this.levels = levels;
            this.controls = controls;
            this.keyboard = keyboard;
        }

        ContentManager m_content;
        private GameModel m_gameModel;

        public override void initializeSession()
        {
            m_gameModel = new GameModel(m_graphics.PreferredBackBufferWidth, m_graphics.PreferredBackBufferHeight, controls, keyboard, levels);
            m_gameModel.Initialize(m_content, m_spriteBatch);
        }

        public override void loadContent(ContentManager content)
        {
            m_content = content;
        }

        public override GameStateEnum processInput(GameTime gameTime)
        {
            foreach (Keys key in keyboard.GetUnlockedKeys())
            {
                if (key == Keys.Escape)
                {
                    keyboard.lockKey(Keys.Escape);
                    return GameStateEnum.LevelSelect;
                }
                else if (key == Keys.R)
                {
                    initializeSession();
                    keyboard.lockKey(Keys.R);
                }

            }

            if (m_gameModel.win) return GameStateEnum.LevelSelect;

            return GameStateEnum.GamePlay;
        }

        public override void render(GameTime gameTime)
        {
            m_gameModel.Draw(gameTime);
        }

        public override void update(GameTime gameTime)
        {
            m_gameModel.Update(gameTime);
        }
    }
}
