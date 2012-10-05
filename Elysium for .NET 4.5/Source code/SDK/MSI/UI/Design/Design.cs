using System;
using System.Windows;

using GalaSoft.MvvmLight;

namespace Elysium.SDK.MSI.UI.Design
{
    public static class Design
    {
        public static readonly DependencyProperty VisibilityProperty =
            DependencyProperty.RegisterAttached("Visibility", typeof(Visibility), typeof(Design),
                                                new FrameworkPropertyMetadata(Visibility.Hidden, FrameworkPropertyMetadataOptions.None, OnVisibilityChanged));

        public static Visibility GetVisibility(DependencyObject obj)
        {
            return (Visibility)obj.GetValue(VisibilityProperty);
        }

        public static void SetVisibility(DependencyObject obj, Visibility value)
        {
            obj.SetValue(VisibilityProperty, value);
        }

        private static void OnVisibilityChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            if (ViewModelBase.IsInDesignModeStatic)
            {
                var instance = obj as UIElement;
                if (instance != null)
                {
                    instance.Visibility = (Visibility)e.NewValue;
                }
            }
        }
    }
}
