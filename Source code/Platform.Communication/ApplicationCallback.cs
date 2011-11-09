using System;
using System.AddIn.Contract;
using System.Windows.Media;

namespace Elysium.Platform.Communication
{
    public abstract class ApplicationCallback : MarshalByRefObject
    {
        public abstract void AccentColorChanged(Color accentColor);

        public abstract void ThemeChanged(Theme.Theme theme);

        public abstract void AttachmentChanged(bool isAttached);

        public abstract void VisibilityChanged(bool isVisible);
    }
} ;