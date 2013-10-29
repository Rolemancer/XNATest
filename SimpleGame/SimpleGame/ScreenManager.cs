using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SimpleGame
{
    public class ScreenManager
    {
        private static ScreenManager _instance;

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

        [XmlIgnore]
        public Vector2 Dimesions { private set; get; }
        [XmlIgnore]
        public ContentManager Content { private set; get; }
        GameScreen currentScreen;
        XmlManager<GameScreen> xmlGameScreenManager = new XmlManager<GameScreen>();
        [XmlIgnore]
        public GraphicsDevice GraphicsDevice;
        [XmlIgnore]
        public SpriteBatch SpriteBatch;

        public Image image;

        [XmlIgnore]
        public bool IsTransitioning { get; private set; }

        private GameScreen newScreen;

        private ScreenManager()
        {
            Dimesions = new Vector2(640, 480);
            currentScreen = new SplashScreen();
            xmlGameScreenManager.ThisType = currentScreen.ThisType;
            currentScreen = xmlGameScreenManager.Load("Load/SplashScreen.xml");
        }

        public void ChangeScreens(string screenName)
        {
            newScreen = (GameScreen)Activator.CreateInstance(Type.GetType("SimpleGame." + screenName));
            image.IsActive = true;
            image.fadeEffect.Increase = true;
            image.Alpha = 0.0f;
            IsTransitioning = true;
        }

        void Transition(GameTime gameTime)
        {
            if (IsTransitioning)
            {
                image.Update(gameTime);
                if (image.Alpha == 1.0f)
                {
                    currentScreen.UnloadContent();
                    currentScreen = newScreen;
                    xmlGameScreenManager.ThisType = currentScreen.ThisType;
                    if (File.Exists(currentScreen.XmlPath))
                    {
                        currentScreen = xmlGameScreenManager.Load(currentScreen.XmlPath);
                    }
                    currentScreen.LoadContent();
                }
                else if (image.Alpha == 0.0f)
                {
                    image.IsActive = false;
                    IsTransitioning = false;
                }
            }
        }

        public void LoadContent(ContentManager manager)
        {
            Content = new ContentManager(manager.ServiceProvider, "Content");
            currentScreen.LoadContent();
            image.LoadContent();
        }

        public void UnloadContent()
        {
            currentScreen.UnloadContent();
            image.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            currentScreen.Update(gameTime);
            Transition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);
            if (IsTransitioning)
            {
                image.Draw(spriteBatch);
            }
        }

    }
}
