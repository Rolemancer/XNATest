using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace SimpleGame
{
    public class ImageEffect
    {
        protected Image image;
        public bool IsActive;

        public ImageEffect() 
        {
            IsActive = false;
        }

        public virtual void LoadContent(ref Image image)
        {
            this.image = image;
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
