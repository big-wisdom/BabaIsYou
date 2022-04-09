using Components;
using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BabaIsYou.Entities.words
{
    class WordRock
    {
        public static Entity create(Texture2D image, int x, int y)
        {
            var rock = new Entity();

            rock.Add(new Appearance(image, computeSourceRectangle, 3, Color.White, Color.White));
            rock.Add(new Position(x, y));
            rock.Add(new Collision());
            rock.Add(new Movable());
            rock.Add(new Word(Systems.Words.Rock));
            rock.Add(new PushC());

            return rock;
        }

        private static Rectangle computeSourceRectangle(int frame)
        {
            int x = 24 * frame;
            return new Rectangle(x, 0, 24, 24);
        }
    }
}
