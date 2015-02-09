using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CloudJourney.Framework.Renderables;
using CloudJourney.Framework.Scenes;

namespace CloudJourney.Actors
{
    public class Block : Actor
    {
        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; }
        public float depth { get; set; }
        public Object parent { get; private set; }
        public Sprite sprite { get; private set; }

        public Block(Vector2 pos, Scene par)
        {
            position = pos;
            parent = par;
            depth = 0.21f;
            sprite = new Sprite("Blank", "", position, Vector2.Zero, this);
            sprite.blend = Color.White * 0.5f;
            sprite.depth = depth;
        }

        public void Update()
        {
            // nuffin muffin
        }

        public void Draw()
        {
            sprite.position = position;
            sprite.Draw();
        }

        public void Unload()
        {
            sprite.Unload();
        }
    }
}