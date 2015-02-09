using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudJourney.Framework.Scenes;
using CloudJourney.Framework.Rendering;
using CloudJourney.Framework.Renderables;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CloudJourney.Framework.Input;

namespace CloudJourney.Scenes
{
    public class TitleScene : Scene
    {
        private Sprite logo;
        private Vector2 logopos;
        private float theta;
        private float omega;

        public TitleScene()
        {
            logopos = new Vector2(-260, 0);
            logo = new Sprite("drivelogo", "Art", logopos, new Vector2(157, 157), this);
        }

        public void Update()
        {
            omega += 0.02f;
            theta = (theta + 0.01f) % (2.0f * (float)Math.PI);
            logopos.X = -260 + ((float)Math.Sin(theta) * 200.0f);
            logopos.Y = ((float)Math.Sin(theta * 2.0f) * 180.0f);

            if (Cloud.controller.ContainsBool(ActionType.Pause))
                Cloud.sceneDriver.Push(new GameScene());
        }

        public void Draw()
        {
            logo.position = logopos;
            logo.Draw();

            if ((int)omega % 2 == 0)
            {
                Drawer.DrawString(Cloud.fontMain,
                    "Press Start",
                    new Vector2(250, 0),
                    Color.White,
                    0.0f,
                    new Vector2(120, 36),
                    1.0f,
                    SpriteEffects.None,
                    0.9f
                    );
            }
        }

        #region wackytimes
        public void Load()
        {

        }

        public void Unload()
        {

        }

        public void Focus()
        {

        }

        public void Unfocus()
        {

        }
#endregion

        #region renderable management
        public void AddRenderable(Renderable r)
        {

        }

        public void RemoveRenderable(Renderable r)
        {

        }
        #endregion
    }
}
