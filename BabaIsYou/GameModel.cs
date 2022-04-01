using System;
namespace BabaIsYou
{
    public class GameModel
    {
        private readonly int WINDOW_WIDTH;
        private readonly int WINDOW_HEIGHT;

        public GameModel(int width, int height)
        {
            this.WINDOW_WIDTH = width;
            this.WINDOW_HEIGHT = height;
        }

        public void Initialize(ContentManager content, SpriteBatch spriteBatch)
        {
            var texSquare = content.Load<Texture2D>("Images/square");

            m_sysRenderer = new Systems.Renderer(spriteBatch, texSquare, WINDOW_WIDTH, WINDOW_HEIGHT, GRID_SIZE);
            m_sysCollision = new Systems.Collision((entity) =>
            {
                // Remove the existing food pill
                m_removeThese.Add(entity);
                // Need another food pill
                m_addThese.Add(createFood(texSquare));
            });
            m_sysMovement = new Systems.Movement();
            m_sysKeyboardInput = new Systems.KeyboardInput();

            initializeBorder(texSquare);
            initializeObstacles(texSquare);
            initializeSnake(texSquare);
            AddEntity(createFood(texSquare));
        }
    }
}
