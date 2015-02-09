using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CloudJourney.Framework.Rendering;
using Microsoft.Xna.Framework.Graphics;

namespace CloudJourney.Framework.Renderables
{
    public class Sprite : Renderable
    {
        public Vector2 position { get; set; }
        public Vector2 origin { get; set; }
        public float depth { get; set; }
        public bool flip { get; set; }
        public Color blend { get; set; }
        public float rotation { get; set; }
        public float scale { get; set; }
        public Vector2 dimensions { get; protected set; }
        public Vector2 bounding { get; set; }
        public Vector2 bposition { get; set; }
        public Object parent { get; protected set; }
        protected String content;

        /// <summary>
        /// The origins of each image, for quick reference.
        /// </summary>
        /// <param name="asset">The name of the asset you want to use.</param>
        /// <param name="directory">The directory containing the asset.</param>
        public Sprite(String asset, String directory, Vector2 pos, Vector2 org, Object parentObj)
        {
            TextureStatic.Load(asset, directory);
            position = pos;
            origin = org;
            depth = 1.0f;
            flip = false;
            blend = Color.White;
            rotation = 0.0f;
            scale = 1.0f;
            content = asset;
            parent = parentObj;
            Texture2D t = TextureStatic.Get(asset);
            dimensions = new Vector2(t.Width, t.Height);
            bounding = dimensions;
            bposition = origin;
        }

        public virtual void Draw()
        {
            Drawer.Draw(TextureStatic.Get(content),
                position,
                null,
                blend,
                rotation,
                origin,
                scale,
                flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                depth
                );
        }

        public virtual void Unload()
        {
            TextureStatic.Unload(content);
        }
    }
}
