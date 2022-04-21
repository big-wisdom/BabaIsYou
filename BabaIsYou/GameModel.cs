using System.Collections.Generic;
using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BabaIsYou
{
    public class GameModel
    {
        private const int GRID_SIZE = 20;
        private readonly int WINDOW_WIDTH;
        private readonly int WINDOW_HEIGHT;
        GameBoard gameBoard;
        Components.ComponentContext components;
        Levels levels;
        KeyboardModel keyboard;

        private List<Entity> m_removeThese = new List<Entity>();
        private List<Entity> m_addThese = new List<Entity>();


        private Systems.Renderer m_sysRenderer;
        private Systems.Collision m_sysCollision;
        private Systems.Movement m_sysMovement;
        private Systems.Particles m_sysParticles;
        private Systems.KeyboardInput m_sysKeyboardInput;
        private Systems.Rules m_sysRules;

        public Components.You youComponent;

        public GameModel(int width, int height, Controls controls, KeyboardModel keyboard, Levels levels)
        {
            this.WINDOW_WIDTH = width;
            this.WINDOW_HEIGHT = height;
            this.youComponent = new Components.You(controls);
            this.keyboard = keyboard;
            this.levels = levels;
        }

        public void Initialize(ContentManager content, SpriteBatch spriteBatch)
        {
            // initialize components
            components = new Components.ComponentContext(content, youComponent);

            // initialize gameBoard
            gameBoard = new GameBoard(levels, components);

            int CELL_SIZE = WINDOW_HEIGHT / (int)levels.currentLevel.dimensions.Y;

            m_sysRenderer = new Systems.Renderer(spriteBatch, CELL_SIZE, gameBoard);
            m_sysCollision = new Systems.Collision(gameBoard);
            m_sysMovement = new Systems.Movement(gameBoard, keyboard);
            m_sysParticles = new Systems.Particles(content, gameBoard, CELL_SIZE);
            m_sysKeyboardInput = new Systems.KeyboardInput(keyboard);
            m_sysRules = new Systems.Rules(gameBoard, components, RemoveEntity, AddEntity);

            initializeEntities();
        }

        public void Update(GameTime gameTime)
        {
            m_sysKeyboardInput.Update(gameTime);
            m_sysCollision.Update(gameTime);
            m_sysMovement.Update(gameTime);
            m_sysParticles.Update(gameTime);
            m_sysRules.Update(gameTime);

            //foreach (var entity in m_removeThese)
            //{
            //    RemoveEntity(entity);
            //}
            //m_removeThese.Clear();

            //foreach (var entity in m_addThese)
            //{
            //    AddEntity(entity);
            //}
            m_addThese.Clear();
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
            m_sysRules.Add(entity); // I don't really need to add this as it just goes off of the game board but here we are
        }

        private void RemoveEntity(Entity entity)
        {
            gameBoard.removeEntity(entity);

            m_sysKeyboardInput.Remove(entity.Id);
            m_sysMovement.Remove(entity.Id);
            //m_sysCollision.Remove(entity.Id);
            m_sysRenderer.Remove(entity.Id);
            m_sysRules.Remove(entity.Id); // same, I don't really need to implement this but here we are
        }

        private void initializeEntities()
        {
            foreach (Entity entity in gameBoard.getEntities())
            {
                m_sysKeyboardInput.Add(entity);
                m_sysMovement.Add(entity);
                //m_sysCollision.Add(entity);
                m_sysRenderer.Add(entity);
                m_sysRules.Add(entity); // I don't really need to add this as it just goes off of the game board but here we are
            }
        }
    }
}
