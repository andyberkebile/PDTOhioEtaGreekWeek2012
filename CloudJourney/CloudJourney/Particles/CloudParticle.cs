using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudJourney.Framework.Renderables;
using Microsoft.Xna.Framework;
using CloudJourney.Framework;
using CloudJourney.Framework.Scenes;
using CloudJourney.Scenes;

namespace CloudJourney.Particles
{
    public class CloudParticle : Renderable
    {
        public Vector2 position { get; set; }
        public float depth { get; set; }
        public Object parent { get; private set; }

        private Sprite sprite;
        private float alpha;
        private float theta;
        private Vector2 velocity;

        public CloudParticle (int type, Vector2 pos, float basedepth, Object par)
        {
            parent = par;
            position = pos;
            alpha = 8.0f;
            switch (type)
            {
                case 1:
                    sprite = new Sprite("cloud_1", @"Art/Particles", position, new Vector2(160, 90), this);
                    break;
                case 2:
                    sprite = new Sprite("cloud_2", @"Art/Particles", position, new Vector2(155, 100), this);
                    break;
                case 3:
                    sprite = new Sprite("cloud_3", @"Art/Particles", position, new Vector2(175, 115), this);
                    break;
                case 4:
                    sprite = new Sprite("cloud_4", @"Art/Particles", position, new Vector2(125, 80), this);
                    break;
                case 5:
                    sprite = new Sprite("cloud_5", @"Art/Particles", position, new Vector2(42, 27), this);
                    break;
                default:
                    sprite = new Sprite("cloud_1", @"Art/Particles", position, new Vector2(160, 90), this);
                    break;
            }
            theta = 0.0f;
            velocity = new Vector2(-4, 0);
            sprite.depth = Math.Min(1.0f, basedepth + ((Cloud.randCloud.Next(10) / 10.0f)) * 0.01f);
        }

        public void Draw()
        {
            position += velocity - (Vector2.UnitX * GameScene.vanSpeed);

            if (theta > 4)
            {
                (parent as Scene).RemoveRenderable(this);
            }

            alpha -= 0.05f;

            sprite.blend = Color.White * alpha;
            sprite.position = position;
            sprite.Draw();
        }

        public void Unload()
        {
            sprite.Unload();
        }
    }
}
