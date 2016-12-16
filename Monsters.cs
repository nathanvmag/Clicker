using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Clicker
{
    class Monsters
    {
        public Vector2 Position;
        public Texture2D Texture;
        public int w;
        public int h;
        int x;
        int y;
        public Vector2 Velocity;
        public Random random = new Random();
        Rectangle area;
        
        public void Draw(SpriteBatch spriteBatch)
        {
            w = 70;
            h = 70;
            x = Convert.ToInt32(Position.X);
            y = Convert.ToInt32(Position.Y);
            spriteBatch.Draw(Texture, new Rectangle(x,y,w,h), Color.White);
        }
        public virtual void Move(Vector2 amount)
        {
            area = new Rectangle(x, y, w, h);
            
            Position += amount;
            if (Position.X<=0)
            {
                Velocity.X *= -1;
            }
            if (Position.X>=Game1.ScreenWidth-w)
            {
                Velocity.X *= -1;
            }
            if (Position.Y <= 0)
            {
                Velocity.Y *= -1;
            }
            if (Position.Y >= Game1.ScreenHeight-h)
            {
                Velocity.Y *= -1;
            }
           
            
        }
        public bool CheckMouse()
        {
            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                // Do cool stuff here
                if (area.Contains(mousePosition))
                {
                    return true;
                }
                else return false;
            }
            else return false;

        }
        public void Launch(float speed)
        {
           //Position = new Vector2(random.Next(w, Game1.ScreenWidth - w), random.Next(h, Game1.ScreenHeight - h));
           // Position = new Vector2(Game1.ScreenWidth / 2 - Texture.Width / 2, Game1.ScreenHeight / 2 - Texture.Height / 2);
            // get a random + or - 60 degrees angle to the right
            float rotation = (float)(Math.PI / 2 + (random.NextDouble() * (Math.PI / 1.5f) - Math.PI / 3));

            Velocity.X = (float)Math.Sin(rotation);
            Velocity.Y = (float)Math.Cos(rotation);

            // 50% chance whether it launches left or right
            if (random.Next(2) == 1)
            {
                Velocity.X *= -1; //launch to the left
            }

            Velocity *= speed;
        }
    }
}
