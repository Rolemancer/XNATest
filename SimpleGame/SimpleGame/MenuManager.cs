using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SimpleGame
{
    public class MenuManager
    {
        Menu menu;
        bool isTransitioning;

        void Transition(GameTime gameTime)
        {
            if (isTransitioning)
            {

                for (int i = 0; i < menu.Items.Count; i++)
                {
                    menu.Items[i].Icon.Update(gameTime);
                    float first = menu.Items[0].Icon.Alpha;
                    float last = menu.Items[menu.Items.Count - 1].Icon.Alpha;
                    if (first == 0.0f && last == 0.0f)
                    {
                        menu.ID = menu.Items[menu.ItemNumber].LinkID;
                    }
                    else if(first == 1.0f && last == 1.0f)
                    {
                        isTransitioning = false;
                        foreach (var menuItem in menu.Items)
                        {
                            menuItem.Icon.RestoreEffects();
                        }
                    }
                }
            }
        }

        public MenuManager()
        {
            menu = new Menu();
            menu.MenuChanged += menu_MenuChanged;
        }

        void menu_MenuChanged(object sender, EventArgs e)
        {
            XmlManager<Menu> xmlManager = new XmlManager<Menu>();
            menu.UnloadContent();
            menu = xmlManager.Load(menu.ID);
            menu.LoadContent();
            menu.MenuChanged += menu_MenuChanged;
            menu.Transition(0.0f);

            foreach (var menuItem in menu.Items)
            {
                menuItem.Icon.StoreEffects();
                menuItem.Icon.ActivateEffect("FadeEffect");
            }
        }

        public void LoadContent(string menuPath)
        {
            if (!string.IsNullOrEmpty(menuPath))
            {
                menu.ID = menuPath;
            }
        }

        public void UnloadContent()
        {
            menu.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            if (!isTransitioning)
                menu.Update(gameTime);
            if (InputManager.Instance.KeyPressed(Keys.Enter) && !isTransitioning)
            {
                if (menu.Items[menu.ItemNumber].LinkType == "Screen")
                {
                    ScreenManager.Instance.ChangeScreens(menu.Items[menu.ItemNumber].LinkID);
                }
                else
                {
                    isTransitioning = true;
                    menu.Transition(1.0f);
                }
            }
            Transition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
        }
    }
}
