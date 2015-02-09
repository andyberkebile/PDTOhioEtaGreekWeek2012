using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudJourney.Framework.Renderables
{
    public class RenderableCollection : List<Renderable>
    {
        public void Draw()
        {
            foreach (Renderable r in this)
            {
                r.Draw();
            }
        }

        public void Unload()
        {
            foreach (Renderable r in this)
            {
                r.Unload();
            }
        }
    }
}
