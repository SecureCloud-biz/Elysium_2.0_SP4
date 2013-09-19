using System.Windows.Input;

using JetBrains.Annotations;

namespace Elysium.Controls
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public static class WindowCommands
    {
        public static RoutedUICommand MinimizeCommand
        {
            get { return _minimizeCommand ?? (_minimizeCommand = new RoutedUICommand("Minimize", "Minimize", typeof(Window))); }
        }

        private static RoutedUICommand _minimizeCommand;

        public static RoutedUICommand MaximizeCommand
        {
            get { return _maximizeCommand ?? (_maximizeCommand = new RoutedUICommand("Maximize", "Maximize", typeof(Window))); }
        }

        private static RoutedUICommand _maximizeCommand;

        public static RoutedUICommand RestoreCommand
        {
            get { return _restoreCommand ?? (_restoreCommand = new RoutedUICommand("Restore", "Restore", typeof(Window))); }
        }

        private static RoutedUICommand _restoreCommand;

        public static RoutedUICommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new RoutedUICommand("Close", "Close", typeof(Window))); }
        }

        private static RoutedUICommand _closeCommand;
    }
}