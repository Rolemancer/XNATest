using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace SimpleGame
{
    public class FadeEffect:ImageEffect
    {
        public float FadeSpeed;
        public bool Increase;

        public FadeEffect()
        {
            FadeSpeed = 1;
            Increase = false;
        }

        public override void LoadContent(ref Image image)
        {
            base.LoadContent(ref image);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime time)
        {
            base.Update(time);

            if (image.IsActive)
            {
                if (Increase)
                {
                    image.Alpha += FadeSpeed * (float)time.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    image.Alpha -= FadeSpeed * (float)time.ElapsedGameTime.TotalSeconds;
                }

                if (image.Alpha < 0.0f)
                {
                    Increase = true;
                    image.Alpha = 0.0f;
                }
                else if (image.Alpha > 1.0f)
                {
                    Increase = false;
                    image.Alpha = 1.0f;
                }
            }
            else
                image.Alpha = 1.0f;
        }
    }
}
