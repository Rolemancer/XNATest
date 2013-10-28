using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SimpleGame
{
    public class ScreenManager
    {
        private static ScreenManager _instance = new ScreenManager();

        public static ScreenManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    XmlManager<ScreenManager> xml = new XmlManager<ScreenManager>();
                    _instance = xml.Load("Load/ScreenManager.xml");
                }
                return _instance;
            }
        }

        public Vector2 Dimesions { private set; get; }
        public ContentManager Content { private set; get; }
        GameScreen currentScreen;
        XmlManager<GameScreen> xmlGameScreenManager = new XmlManager<GameScreen>();
        public GraphicsDevice GraphicsDevice;
        public SpriteBatch SpriteBatch;

        public Image image;

        private ScreenManager()
        {
            Dimesions = new Vector2(640, 480);
            currentScreen = new SplashScreen();
            xmlGameScreenManager.ThisType = currentScreen.ThisType;
            currentScreen = xmlGameScreenManager.Load("Load/SplashScreen.xml");
        }

        public void ChangeScreens(string screenName)
        {
            var screen = (GameScreen)Activator.CreateInstance(Type.GetType("SimpleGame." + screenName));
        }

        void Transition()
        {

        }

        public void LoadContent(ContentManager manager)
        {
            Content = new ContentManager(manager.ServiceProvider, "Content");
            currentScreen.LoadContent();
        }

        public void UnloadContent()
        {
            currentScreen.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
        }

    }
}
