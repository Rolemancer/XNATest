using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace SimpleGame
{
    public class ImageEffect
    {
        Image Image;

        public ImageEffect() { }

        public virtual void LoadContent(ref Image image)
        {
            Image = image;
        }

        public virtual void UnloadContent()
        {
            //Image.UnloadContent();
        }

        public virtual void Update(GameTime time)
        {

        }
        
    }
}
