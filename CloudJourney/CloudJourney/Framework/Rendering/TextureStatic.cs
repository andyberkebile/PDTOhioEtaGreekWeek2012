using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CloudJourney.Framework.Rendering
{
    /// <summary>
    /// This is a static public class for storing and receiving static
    /// Texture2Ds from ImageType enums.
    /// </summary>
    public static class TextureStatic
    {
        /// <summary>
        /// The lookup table of images to match with names.
        /// </summary>
        private static Dictionary<string, Texture2D> images = new Dictionary<string, Texture2D>();

        /// <summary>
        /// Keeps track of how many instances of a texture are being used.
        /// </summary>
        private static Dictionary<string, int> usage = new Dictionary<string, int>();

        /// <summary>
        /// The origins of each image, for quick reference.
        /// </summary>
        private static Dictionary<string, Vector2> origins = new Dictionary<string, Vector2>();

        /// <summary>
        /// Loads a Texture2D, and matches it to the specified image name for easy use later.
        /// </summary>
        public static void Load(string imageName, string directory)
        {
            if (!images.ContainsKey(imageName.ToLower()))
            {
                images.Add(imageName.ToLower(), Cloud.content.Load<Texture2D>(directory + @"\" + imageName));
                origins.Add(imageName.ToLower(), new Vector2(images[imageName.ToLower()].Width / 2f, images[imageName.ToLower()].Height / 2f));
                usage.Add(imageName.ToLower(), 1);
            }
            else
            {
                usage[imageName.ToLower()]++;
            }
        }

        /// <summary>
        /// Unloads a texture.
        /// </summary>
        public static void Unload(string imageName)
        {
            if (!images.ContainsKey(imageName.ToLower()))
            {
                usage[imageName.ToLower()]--;
                if (usage[imageName.ToLower()] <= 0)
                {
                    images.Remove(imageName.ToLower());
                    origins.Remove(imageName.ToLower());
                    usage.Remove(imageName.ToLower());
                }
            }
        }

        /// <summary>
        /// Gets the specified image type, and returns the corresponding Texture2D.
        /// </summary>
        /// <param name="imageType">Type of the image.</param>
        /// <returns>A Texture2D corresponding to the specified image type.</returns>
        public static Texture2D Get(string imageType)
        {
            if (images.ContainsKey(imageType.ToLower()))
            {
                return images[imageType.ToLower()];
            }

            return null;
        }

        /// <summary>
        /// Gets the origin of the image.
        /// </summary>
        /// <param name="imageType">Type of the image.</param>
        /// <returns>The origin of the image.</returns>
        public static Vector2 GetOrigin(string imageType)
        {
            if (origins.ContainsKey(imageType.ToLower()))
            {
                return origins[imageType.ToLower()];
            }

            return Vector2.Zero;
        }
    }
}
