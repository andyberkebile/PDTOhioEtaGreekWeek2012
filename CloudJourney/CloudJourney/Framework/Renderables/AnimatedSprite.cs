using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CloudJourney.Framework.Rendering;
using Microsoft.Xna.Framework.Graphics;

namespace CloudJourney.Framework.Renderables
{
    public class AnimatedSprite : Sprite
    {
        public float speed { get; set;}
        public int frame { get { return (int)framef; } set { framef = value; } }
        public int frameSet { get; set; }
        public List<int> frameCount { get; set; }
        private float framef;
        private Rectangle frameR;

        public AnimatedSprite(String asset, String directory, Vector2 pos, Vector2 org, Vector2 frameDim, Object parentObj)
            : base(asset, directory, pos, org, parentObj)
        {
            speed = 1.0f;
            frame = 0;
            frameSet = 0;
            dimensions = frameDim;
            framef = frame;
            frameR = new Rectangle((int)(frame * dimensions.X), (int)(frameSet * dimensions.Y), (int)dimensions.X, (int)dimensions.Y);
            frameCount = new List<int>();

            Console.Out.WriteLine(frameR.ToString());
        }

        public void Update()
        {
            framef = (framef + speed) % frameCount[frameSet];

            frameR.X = (int)(frame * dimensions.X);
            frameR.Y = (int)(frameSet * dimensions.Y);
            /*frameR.Width = (int)dimensions.X;
            frameR.Height = (int)dimensions.Y;*/
        }

        public override void Draw()
        {
            Drawer.Draw(TextureStatic.Get(content),
                position,
                frameR,
                blend,
                rotation,
                origin,
                scale,
                flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                depth
                );

            //Drawer.DrawRectangle(new Rectangle((int)(position.X - origin.X), (int)(position.Y - origin.Y), (int)dimensions.X, (int)dimensions.Y), 1.0f, depth - 0.01f, Color.Red);
        }
    }
}
