using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Xml.Serialization;

namespace SimpleGame
{
    public class Menu
    {
        public event EventHandler MenuChanged;

        public string Axis;
        public string Effects;
        [XmlElement("Item")]
        public List<MenuItem> Items;

        int itemNumber;
        string id;

        public int ItemNumber
        {
            get { return itemNumber; }
        }

        public string ID
        {
            get { return id; }
            set
            {
                id = value;
                MenuChanged(this, null);
            }
        }

        public void Transition(float alpha)
        {
            foreach (var menuItem in Items)
            {
                menuItem.Icon.IsActive = true;
                menuItem.Icon.Alpha = alpha;
                if (alpha == 0.0f)
                {
                    menuItem.Icon.fadeEffect.Increase = true;
                }
                else
                {
                    menuItem.Icon.fadeEffect.Increase = false;
                }
            }
        }

        private void AlignMenuItems()
        {
            Vector2 dimentions = Vector2.Zero;
            
            foreach (MenuItem item in Items)
            {
                dimentions += new Vector2(item.Icon.SourceRect.Width,
                    item.Icon.SourceRect.Height);
            }
            dimentions = new Vector2((ScreenManager.Instance.Dimesions.X - dimentions.X) / 2,
                (ScreenManager.Instance.Dimesions.Y - dimentions.Y) / 2);
            
            foreach (MenuItem item in Items)
            {
                if (Axis == "X")
                {
                    item.Icon.Position = new Vector2(dimentions.X,
                        (ScreenManager.Instance.Dimesions.Y - item.Icon.SourceRect.Height) / 2);
                }
                else if (Axis == "Y")
                {
                    item.Icon.Position = new Vector2((ScreenManager.Instance.Dimesions.X - item.Icon.SourceRect.Width) / 2,
                        dimentions.Y);
                }

                dimentions += new Vector2(item.Icon.SourceRect.Width,
                    item.Icon.SourceRect.Height);
            }
        }

        public Menu()
        {
            id = String.Empty;
            itemNumber = 0;
            Effects = String.Empty;
            Axis = "Y";
            Items = new List<MenuItem>();
        }

        public void LoadContent()
        {
            string[] split = Effects.Split(':');

            foreach (var menuItem in Items)
            {
                menuItem.Icon.LoadContent();
                for (int i = 0; i < split.Length; i++)
                {
                    menuItem.Icon.ActivateEffect(split[i]);
                }
            }
            AlignMenuItems();

        }

        public void UnloadContent()
        {
            foreach (MenuItem item in Items)
            {
                item.Icon.UnloadContent();
            }
        }

        public void Update(GameTime gameTime)
        {
            if (Axis == "X")
            {
                if (InputManager.Instance.KeyPressed(Keys.Right))
                {
                    itemNumber++;
                }
                else if (InputManager.Instance.KeyPressed(Keys.Left))
                {
                    itemNumber--;
                }
            }
            else if (Axis == "Y")
            {
                if (InputManager.Instance.KeyPressed(Keys.Down))
                {
                    itemNumber++;
                }
                else if (InputManager.Instance.KeyPressed(Keys.Up))
                {
                    itemNumber--;
                }
            }

            if (itemNumber < 0)
            {
                itemNumber = 0;
            }
            else if (itemNumber > Items.Count - 1)
            {
                itemNumber = Items.Count - 1;
            }

            for (int i = 0; i < Items.Count; i++)
            {
                if (i == itemNumber)
                {
                    Items[i].Icon.IsActive = true;
                }
                else
                {
                    Items[i].Icon.IsActive = false;
                }
                Items[i].Icon.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Icon.Draw(spriteBatch);
            }
        }
    }
}
