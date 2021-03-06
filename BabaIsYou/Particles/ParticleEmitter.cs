using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BabaIsYou.Particles
{
    public class ParticleEmitter
    {
        private TimeSpan emitterLifetime = TimeSpan.FromMilliseconds(1000);
        public bool done = false;

        private Dictionary<int, Particle> m_particles = new Dictionary<int, Particle>();
        private Texture2D m_texFire;
        private MyRandom m_random = new MyRandom();

        private TimeSpan m_rate;
        private int m_sourceX;
        private int m_sourceY;
        private int m_sarticleSize;
        private int m_speed;
        private int CELL_SIZE;
        private TimeSpan m_lifetime;
        private TimeSpan m_delay;

        public Vector2 Gravity { get; set; }

        public ParticleEmitter(ContentManager content, TimeSpan rate, int sourceX, int sourceY, int CELL_SIZE, int speed, TimeSpan lifetime, TimeSpan delay)
        {
            m_rate = rate;
            m_sourceX = sourceX;
            m_sourceY = sourceY;
            m_sarticleSize = CELL_SIZE/5;
            m_speed = speed;
            this.CELL_SIZE = CELL_SIZE;
            m_lifetime = lifetime;
            m_delay = delay;

            m_texFire = content.Load<Texture2D>("objects/fire");

            this.Gravity = new Vector2(0, 0);
        }

        private TimeSpan m_accumulated = TimeSpan.Zero;

        /// <summary>
        /// Generates new particles, updates the state of existing ones and retires expired particles.
        /// </summary>
        public void update(GameTime gameTime)
        {
            if (m_delay > TimeSpan.Zero)
            {
                m_delay -= gameTime.ElapsedGameTime;
            }
            else
            {
                emitterLifetime -= gameTime.ElapsedGameTime;
                if (emitterLifetime < (TimeSpan.Zero - m_lifetime)) // also wait for the last particles to fizzle
                {
                    done = true;
                }

                //
                // Generate particles at the specified rate
                m_accumulated += gameTime.ElapsedGameTime;
                while (m_accumulated > m_rate && emitterLifetime > TimeSpan.Zero)
                {
                    m_accumulated -= m_rate;

                    Vector2 pos = new Vector2(m_sourceX, m_sourceY);
                    Vector2 dir = m_random.nextCircleVector();

                    Particle p = new Particle(
                        m_random.Next(),
                        pos + (dir*(CELL_SIZE/2)),
                        dir,
                        m_speed,
                        //(float)m_random.nextGaussian(m_speed, 1),
                        m_lifetime);

                    if (!m_particles.ContainsKey(p.name))
                    {
                        m_particles.Add(p.name, p);
                    }
                }


                //
                // For any existing particles, update them, if we find ones that have expired, add them
                // to the remove list.
                List<int> removeMe = new List<int>();
                foreach (Particle p in m_particles.Values)
                {
                    p.lifetime -= gameTime.ElapsedGameTime;
                    if (p.lifetime < TimeSpan.Zero)
                    {
                        //
                        // Add to the remove list
                        removeMe.Add(p.name);
                    }
                    //
                    // Update its position
                    p.position += (p.direction * p.speed);
                    //
                    // Have it rotate proportional to its speed
                    p.rotation += p.speed / 50.0f;
                    //
                    // Apply some gravity
                    p.direction += this.Gravity;
                }

                //
                // Remove any expired particles
                foreach (int Key in removeMe)
                {
                    m_particles.Remove(Key);
                }
            }
        }

        /// <summary>
        /// Renders the active particles
        /// </summary>
        public void draw(SpriteBatch spriteBatch)
        {
            Rectangle r = new Rectangle(0, 0, m_sarticleSize, m_sarticleSize);
            foreach (Particle p in m_particles.Values)
            {
                r.X = (int)p.position.X;
                r.Y = (int)p.position.Y;
                spriteBatch.Draw(
                    m_texFire,
                    r,
                    null,
                    Color.White,
                    p.rotation,
                    new Vector2(m_texFire.Width / 2, m_texFire.Height / 2),
                    SpriteEffects.None,
                    0);
            }
        }
    }
}
