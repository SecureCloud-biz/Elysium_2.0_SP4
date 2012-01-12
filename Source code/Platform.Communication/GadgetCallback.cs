using System;
using System.Windows.Media;

namespace Elysium.Platform.Communication
{
    public abstract class GadgetCallback : MarshalByRefObject
    {
        public abstract void AccentColorChanged(Color accentColor);

        public abstract void ThemeChanged(Theme.ThemeType theme);

        public abstract void SizeChanged(bool isExpanded);

        public abstract void VisibilityChanged(bool isVisible);
    }
} ;