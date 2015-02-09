using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CloudJourney.Framework.Rendering;
using Microsoft.Xna.Framework.Graphics;

namespace CloudJourney.Framework.Renderables
{
    public class Background : Renderable
    {
        public Vector2 position { get; set; }
        public float depth { get; set; }
        public Object parent { get; protected set; }
        public Vector2 velocity { get; set; }
        public bool wrap { get; set; }
        protected String content;
        protected int width;
        protected int halfwidth;

        public Background(String asset, String directory, Vector2 pos, Vector2 vel, float dep, bool wr, Object par)
        {
            TextureStatic.Load(asset, directory);
            content = asset;
            position = pos;
            velocity = vel;
            parent = par;
            depth = dep;
            wrap = wr;
            width = TextureStatic.Get(asset).Width;
            halfwidth = width / 2;
        }

        public void Draw()
        {
            position += velocity;
            if (position.X < Camera2D.cam.Pos.X - halfwidth && wrap)
                position = new Vector2(Camera2D.cam.Pos.X + halfwidth, position.Y);

            if (wrap)
            {
                int bgnum = (int)Math.Ceiling((Cloud.screenWidth * (1/Camera2D.cam.Zoom)) / width);
                int bgnumhalf = bgnum / 2;
                for (int i = -bgnumhalf -1; i < bgnumhalf + 1; i++)
                {
                    Drawer.Draw(TextureStatic.Get(content),
                        position + new Vector2(width * i, 0),
                        null,
                        Color.White,
                        0.0f,
                        Vector2.Zero,
                        1.0f,
                        SpriteEffects.None,
                        depth
                        );
                }
            }
            else
            {
                Drawer.Draw(TextureStatic.Get(content),
                position,
                null,
                Color.White,
                0.0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                depth
                );
            }
        }

        public void Unload()
        {
            TextureStatic.Unload(content);
        }
    }
}
