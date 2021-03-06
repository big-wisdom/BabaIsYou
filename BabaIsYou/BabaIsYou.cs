using BabaIsYou;
using Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;

namespace CS5410
{
    public class BabaIsYou : Game
    {
        private GraphicsDeviceManager m_graphics;
        private IGameState m_currentState;
        private GameStateEnum m_nextStateEnum = GameStateEnum.MainMenu;
        private Dictionary<GameStateEnum, IGameState> m_states;
        public KeyboardModel keyboard = new KeyboardModel();
        private GameBoard gameBoard;

        public Controls controls = new Controls();

        public BabaIsYou()
        {
            m_graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.2F;
            MediaPlayer.Play(this.Content.Load<Song>("audio/ThemeSong"));
        }

        protected override void Initialize()
        {
            m_graphics.PreferredBackBufferWidth = 480;
            m_graphics.PreferredBackBufferHeight = 480;
            //m_graphics.PreferredBackBufferWidth = 1920;
            //m_graphics.PreferredBackBufferHeight = 1080;
            //m_graphics.PreferredBackBufferWidth = 2560;
            //m_graphics.PreferredBackBufferHeight = 1440;


            m_graphics.ApplyChanges();

            //Levels levels = new Levels("../../../levelFiles/levels-all.bbiy");
            Levels levels = new Levels(Content.RootDirectory+"/levelFiles/levels-all.bbiy");

            //You you = new You(controls);
            //ComponentContext components = new ComponentContext(this.Content, you);
            //this.gameBoard = new GameBoard(components);


            // Create all the game states here
            m_states = new Dictionary<GameStateEnum, IGameState>();
            m_states.Add(GameStateEnum.MainMenu, new MainMenuView(keyboard));
            m_states.Add(GameStateEnum.LevelSelect, new LevelSelectView(keyboard, levels));
            m_states.Add(GameStateEnum.GamePlay, new GamePlayView(levels, controls, keyboard));
            m_states.Add(GameStateEnum.Help, new SettingsView(controls, keyboard));
            m_states.Add(GameStateEnum.About, new AboutView());

            // We are starting with the main menu
            m_currentState = m_states[GameStateEnum.MainMenu];

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Give all game states a chance to load their content
            foreach (var item in m_states)
            {
                item.Value.initialize(this.GraphicsDevice, m_graphics);
                item.Value.loadContent(this.Content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            m_nextStateEnum = m_currentState.processInput(gameTime);
            keyboard.update(gameTime.ElapsedGameTime);
            // Special case for exiting the game
            if (m_nextStateEnum == GameStateEnum.Exit)
            {
                Exit();
            }

            m_currentState.update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            m_currentState.render(gameTime);

            if (m_currentState != m_states[m_nextStateEnum])
            {
                m_currentState = m_states[m_nextStateEnum];
                m_currentState.initializeSession();
            }

            base.Draw(gameTime);
        }
    }
}
