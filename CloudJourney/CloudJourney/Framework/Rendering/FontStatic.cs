using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace CloudJourney.Framework.Rendering
{
    /// <summary>
    /// Used for keeping a list of all active fonts in the game.
    /// </summary>
    public static class FontStatic
    {
        /// <summary>
        /// The lookup table of fonts to match with names.
        /// </summary>
        private static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

        /// <summary>
        /// Loads a font, and matches it to the specified font name for easy use later.
        /// </summary>
        public static void Load(string fontName, string directory)
        {
            if (!fonts.ContainsKey(fontName.ToLower()))
            {
                fonts.Add(fontName.ToLower(), Cloud.content.Load<SpriteFont>(directory));
            }
        }

        /// <summary>
        /// Gets the specified font, and returns the corresponding SpriteFont.
        /// </summary>
        /// <param name="fontName">Name of the font.</param>
        /// <returns>A SpriteFont corresponding to the specified font name.</returns>
        public static SpriteFont Get(string fontName)
        {
            if (fonts.ContainsKey(fontName.ToLower()))
            {
                return fonts[fontName.ToLower()];
            }

            return null;
        }
    }
}
