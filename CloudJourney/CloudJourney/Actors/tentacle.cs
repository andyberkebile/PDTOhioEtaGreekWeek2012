using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using CloudJourney.Framework.Renderables;
using CloudJourney.Framework.Scenes;
using CloudJourney.Framework.BoneAnimation;
using CloudJourney.Framework.BoneAnimator.Stances;
using CloudJourney.Framework.Rendering;

namespace CloudJourney.Actors
{
    class tentacle : Actor
    {
        private BoneAnimator animator;
        public Vector2 position { get; set; }
        public Vector2 velocity { get { return vel; } set { vel = value; } }
        public float depth { get; set; }
        public Object parent { get; private set; }
        public Sprite sprite { get; private set; }
        private Vector2 vel;

        public tentacle(Vector2 pos, Scene par)
        {
            string dir = "BonePix";
            TextureStatic.Load("tentacle_tip_1", dir);
            TextureStatic.Load("tentacle_2", dir);
            TextureStatic.Load("tentacle_3", dir);
            TextureStatic.Load("tentacle_4", dir);
            TextureStatic.Load("tentacle_5", dir);
            TextureStatic.Load("tentacle_base", dir);
            position = pos;
            depth = 0.9f;
            parent = par;
            vel = new Vector2(0, 0);
            animator = new BoneAnimator(null, StaticStancetentacle.GetBoneSet(), StaticStancetentacle.Get(tentacleStance.Idle), this.position.X, this.position.Y);
            animator.Depth = .2f;

            sprite = new Sprite("bus", @"Art", new Vector2(10000,10000), new Vector2(150, 150), this);//anispr;
            sprite.bounding = new Vector2(1, 1);
            sprite.bposition = new Vector2(1, 1);
        }

        public void Update()
        {
            animator.SetAnimation(StaticStancetentacle.GetAnimation(tentacleAnimation.Idle));
            animator.Update();
        }

        public void Draw()
        {
            animator.Draw();
        }

        public void Unload()
        {

        }
    }
}
