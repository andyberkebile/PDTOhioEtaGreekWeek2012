using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudJourney.Framework.Renderables;

namespace CloudJourney.Framework.Scenes
{
    /// <summary>
    /// A scene is like a game "screen". Should be used as an individual game concept. Eg. the title screen, gameplay, menus, etc.
    /// </summary>
    public interface Scene
    {
        /// <summary>
        /// Called when the scene is loaded. Use the content loader to load any resources you need.
        /// </summary>
        void Load();

        /// <summary>
        /// Called when the scene is unloaded. Unload any content you loaded and no longer need here.
        /// </summary>
        void Unload();

        /// <summary>
        /// Called when the scene is focused.
        /// DO NOT SET THINGS UP IN HERE. THIS CAN GET CALLED MULTIPLE TIMES.
        /// </summary>
        void Focus();

        /// <summary>
        /// Called when the scene is unfocused.
        /// DO NOT SET THINGS UP IN HERE. THIS CAN GET CALLED MULTIPLE TIMES.
        /// </summary>
        void Unfocus();

        /// <summary>
        /// Called each "step" of the game. Update 
        /// </summary>
        void Update();

        /// <summary>
        /// Called when the scene is loaded. Use the content loader to load any resources you need.
        /// </summary>
        void Draw();

        void AddRenderable(Renderable rd);

        void RemoveRenderable(Renderable rd);

    }
}
