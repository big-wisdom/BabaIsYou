using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace BabaIsYou
{
    public class GameModel
    {
        private const int GRID_SIZE = 20;
        private readonly int WINDOW_WIDTH;
        private readonly int WINDOW_HEIGHT;
        GameBoard gameBoard;
        KeyboardModel keyboard;

        private Systems.Renderer m_sysRenderer;
        //private Systems.Collision m_sysCollision;
        private Systems.Movement m_sysMovement;
        private Systems.KeyboardInput m_sysKeyboardInput;

        public Components.You youComponent;

        public GameModel(int width, int height, Controls controls, KeyboardModel keyboard)
        {
            this.WINDOW_WIDTH = width;
            this.WINDOW_HEIGHT = height;
            this.youComponent = new Components.You(controls);
            this.keyboard = keyboard;
        }

        public void Initialize(ContentManager content, SpriteBatch spriteBatch)
        {
            // create a dictionary that links directions to textures for baba
            var babaTextures = new Dictionary<Components.Direction, Texture2D>() {
                { Components.Direction.Up, content.Load<Texture2D>("baba/bunnyUp") },
                { Components.Direction.Right, content.Load<Texture2D>("baba/bunnyRight") },
                { Components.Direction.Down, content.Load<Texture2D>("baba/bunnyDown") },
                { Components.Direction.Left, content.Load<Texture2D>("baba/bunnyLeft") }
            };

            var images = new Dictionary<char, Texture2D>()
            {
                { 'h', content.Load<Texture2D>("objects/hedge") },
                { 'w', content.Load<Texture2D>("objects/wall") },
                { 'r', content.Load<Texture2D>("objects/rock") },
                { 'f', content.Load<Texture2D>("objects/flag") },
            };

            // initialize gameBoard
            gameBoard = new GameBoard(GRID_SIZE, images, babaTextures, youComponent);


            m_sysRenderer = new Systems.Renderer(spriteBatch, WINDOW_WIDTH, WINDOW_HEIGHT, gameBoard);
            //m_sysCollision = new Systems.Collision((entity) =>
            //{
            //    // Remove the existing food pill
            //    m_removeThese.Add(entity);
            //    // Need another food pill
            //    m_addThese.Add(createFood(texSquare));
            //});
            m_sysMovement = new Systems.Movement(gameBoard, keyboard);
            m_sysKeyboardInput = new Systems.KeyboardInput(keyboard);

            initializeEntities();
        }

        public void Update(GameTime gameTime)
        {
            m_sysKeyboardInput.Update(gameTime);
            m_sysMovement.Update(gameTime);
            //m_sysCollision.Update(gameTime);

            //foreach (var entity in m_removeThese)
            //{
            //    RemoveEntity(entity);
            //}
            //m_removeThese.Clear();

            //foreach (var entity in m_addThese)
            //{
            //    AddEntity(entity);
            //}
            //m_addThese.Clear();
        }

        public void Draw(GameTime gameTime)
        {
            m_sysRenderer.Update(gameTime);
        }

        private void AddEntity(Entity entity)
        {
            gameBoard.addEntity(entity);

            m_sysKeyboardInput.Add(entity);
            m_sysMovement.Add(entity);
            //m_sysCollision.Add(entity);
            m_sysRenderer.Add(entity);
        }

        private void RemoveEntity(Entity entity)
        {
            gameBoard.removeEntity(entity);

            m_sysKeyboardInput.Remove(entity.Id);
            m_sysMovement.Remove(entity.Id);
            //m_sysCollision.Remove(entity.Id);
            m_sysRenderer.Remove(entity.Id);
        }

        private void initializeEntities()
        {
            foreach (Entity e in gameBoard.getEntities())
            {
                AddEntity(e);
            }
        }
    }
}
