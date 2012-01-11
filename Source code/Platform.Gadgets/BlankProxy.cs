using System;
using System.AddIn.Contract;
using System.AddIn.Pipeline;
using System.Windows;
using System.Windows.Media;
using Elysium.Platform.Communication;
using Elysium.Theme;

namespace Elysium.Platform.Gadgets
{
    public class BlankProxy : Gadget
    {
        public class CallbackProxy : GadgetCallback
        {
            private BlankProxy _proxy;

            internal CallbackProxy(BlankProxy proxy)
            {
                _proxy = proxy;
            }

            public override void AccentColorChanged(Color accentColor)
            {
                if (ThemeManager.Instance.Theme == Theme.Theme.Dark)
                    ThemeManager.Instance.Dark(accentColor);
                else ThemeManager.Instance.Light(accentColor);
            }

            public override void ThemeChanged(Theme.Theme theme)
            {
                if (theme == Theme.Theme.Dark)
                    ThemeManager.Instance.Dark(ThemeManager.Instance.Accent);
                else ThemeManager.Instance.Light(ThemeManager.Instance.Accent);
            }

            public override void SizeChanged(bool isExpanded)
            {
                _proxy._visual.Width = isExpanded ? 210 * 2 + 30 : 210;
                _proxy._visual.Height = isExpanded ? 210 * 2 + 30 : 210;
            }

            public override void VisibilityChanged(bool isVisible)
            {
            }
        }

        public override Info Info
        {
            get
            {
                return _info ??
                       (_info = new Info("Blank", new[] {"Alex F. Sherman & Codeplex Community"}, new[] {new Uri("http://elysium.codeplex.com/license")},
                                         link: new Uri("http://elysium.codeplex.com/")));
            }
        }

        private Info _info;

        public override GadgetCallback Callback
        {
            get { return _callback ?? (_callback = new CallbackProxy(this)); }
        }

        private GadgetCallback _callback;

        public override bool IsExpandable
        {
            get { return true; }
        }

        public override INativeHandleContract Visual
        {
            get { return _handle ?? (_handle = FrameworkElementAdapters.ViewToContractAdapter(_visual ?? (_visual = new Blank()))); }
        }

        private INativeHandleContract _handle;
        private FrameworkElement _visual;
    }
} ;