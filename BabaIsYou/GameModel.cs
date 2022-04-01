using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace BabaIsYou
{
    public class GameModel
    {
        private const int GRID_SIZE = 20;
        private readonly int WINDOW_WIDTH;
        private readonly int WINDOW_HEIGHT;
        private Systems.Renderer m_sysRenderer;
        //private Systems.Collision m_sysCollision;
        //private Systems.Movement m_sysMovement;
        //private Systems.KeyboardInput m_sysKeyboardInput;

        public GameModel(int width, int height)
        {
            this.WINDOW_WIDTH = width;
            this.WINDOW_HEIGHT = height;
        }

        public void Initialize(ContentManager content, SpriteBatch spriteBatch)
        {
            var babaTexture = content.Load<Texture2D>("baba/bunnyLeft");

            m_sysRenderer = new Systems.Renderer(spriteBatch, WINDOW_WIDTH, WINDOW_HEIGHT, GRID_SIZE);
            //m_sysCollision = new Systems.Collision((entity) =>
            //{
            //    // Remove the existing food pill
            //    m_removeThese.Add(entity);
            //    // Need another food pill
            //    m_addThese.Add(createFood(texSquare));
            //});
            //m_sysMovement = new Systems.Movement();
            //m_sysKeyboardInput = new Systems.KeyboardInput();

            initializeBaba(babaTexture);
        }

        public void Update(GameTime gameTime)
        {
            //m_sysKeyboardInput.Update(gameTime);
            //m_sysMovement.Update(gameTime);
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
            //m_sysKeyboardInput.Add(entity);
            //m_sysMovement.Add(entity);
            //m_sysCollision.Add(entity);
            m_sysRenderer.Add(entity);
        }

        private void RemoveEntity(Entity entity)
        {
            //m_sysKeyboardInput.Remove(entity.Id);
            //m_sysMovement.Remove(entity.Id);
            //m_sysCollision.Remove(entity.Id);
            m_sysRenderer.Remove(entity.Id);
        }

        private void initializeBaba(Texture2D baba)
        {
            AddEntity(Baba.create(baba, 5, 5));
        }
    }
}
