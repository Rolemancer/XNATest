using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SimpleGame
{
    public class SplashScreen: GameScreen
    {
        //[XmlElement("Path")]
        //public List<string> Path { get; set; }
        //Texture2D image;

        //public Vector2 Position { get; set; }

        public Image Image;

        public override void LoadContent()
        {
            base.LoadContent();
            Image.LoadContent();
            //image = content.Load<Texture2D>(Path[1]);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            Image.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Image.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Image.Draw(spriteBatch);
            //spriteBatch.Draw(image, Position, Color.White);
        }
    }
}
