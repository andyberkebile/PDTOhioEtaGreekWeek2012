using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudJourney.Framework.Scenes;
using CloudJourney.Framework.Renderables;
using CloudJourney.Framework.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CloudJourney.Framework;
using CloudJourney.Framework.Input;

namespace CloudJourney.Scenes
{
    public class OverScene : Scene
    {
        private float omega;

        public OverScene()
        {
            Camera2D.cam.Zoom = 1.0f;
            Camera2D.cam.rumble = 0.0f;
        }

        public void Update()
        {
            omega += 0.02f;
            if (Cloud.controller.ContainsBool(ActionType.Pause))
            {
                Cloud.sceneDriver.Pop();
                Cloud.sceneDriver.Pop();
                GameScene.counter = 0;
                GameScene.timer = 0;
                GameScene.isDead = false;
            }
        }

        public void Draw()
        {
            Drawer.DrawString(Cloud.fontMain,
                "Game Over!",
                new Vector2(-500, -120),
                Color.White,
                0.0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0.9f
                );

            Drawer.DrawString(Cloud.fontFixed,
                String.Format("{0:00}:{1:00}:{2:000}", Math.Floor(GameScene.timer / 3600.0), Math.Floor(GameScene.timer / 60.0) % 60, Math.Floor(((GameScene.timer % 60) / 60.0f) * 100.0f)),
                new Vector2(-500, 0),
                Color.White,
                0.0f,
                Vector2.Zero,
                1.0f,
                SpriteEffects.None,
                0.99f
                );

            if ((int)omega % 2 == 0)
            {
                Drawer.DrawString(Cloud.fontMain,
                    "Press Start",
                    new Vector2(-300, 100),
                    Color.White,
                    0.0f,
                    Vector2.Zero,
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
