using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Xml.Serialization;

namespace SimpleGame
{
    public class Menu
    {
        public event EventHandler MenuChanged;

        public string Axis;
        public string Effects;
        [XmlElement("Item")]
        public List<MenuItem> Items = new List<MenuItem>();

        int itemNumber;
        string id;

        public string ID
        {
            get { return id; }
            set
            {
                id = value;
                MenuChanged(this, null);
            }
        }

        public void LoadContent()
        {

        }

        public void UnloadContent()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
