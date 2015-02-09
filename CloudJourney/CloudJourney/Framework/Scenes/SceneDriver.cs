using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudJourney.Framework.Scenes
{
    /// <summary>
    /// Captain of the scenes. It handles all the scenes.
    /// </summary>
    public class SceneDriver
    {
        /// <summary>
        /// Gets the currently running scene.
        /// </summary>
        /// <value>The current scene.</value>
        public Scene currentScene { get; private set; }

        /// <summary>
        /// Gets the previously running scene.
        /// </summary>
        /// <value>The previous scene.</value>
        public Scene previousScene { get; private set; }

        /// <summary>
        /// Gets whether the stack is empty or not.
        /// </summary>
        /// <value>Is the stack empty?</value>
        public bool empty { get; private set; }

        /// <summary>
        /// The scene stack. Yay.
        /// </summary>
        /// <value>The scene stack.</value>
        private Stack<Scene> scenes;

        public SceneDriver()
        {
            scenes = new Stack<Scene>();
        }

        /// <summary>
        /// Pushes a scene to the scene stack. Control is switched to this scene.
        /// The previous scene's resources are unloaded.
        /// </summary>
        public void Push(Scene scene)
        {
            if (currentScene != null)
            {
                previousScene = currentScene;
                currentScene.Unfocus();
            }
            //currentScene.Unload();
            scenes.Push(scene);
            currentScene = scene;
            currentScene.Load();
            currentScene.Focus();
            empty = false;
        }

        /// <summary>
        /// Pops the top (current) scene from the stack. Control is switched to the scene before it.
        /// </summary>
        public void Pop()
        {
            currentScene.Unfocus();
            currentScene.Unload();
            scenes.Pop();
            currentScene = scenes.Peek();
            //currentScene.Load();
            currentScene.Focus();
            empty = scenes.Count == 0;
        }

        /// <summary>
        /// Updates the current scene. You don't need to call this yourself.
        /// </summary>
        public void Update()
        {
            currentScene.Update();
        }

        /// <summary>
        /// Draws the current scene. You don't need to call this yourself.
        /// </summary>
        public void Draw()
        {
            currentScene.Draw();
        }
    }
}
