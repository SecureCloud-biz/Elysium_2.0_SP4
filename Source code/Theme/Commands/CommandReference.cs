using System;
using System.Windows;
using System.Windows.Input;

namespace Elysium.Theme.WPF.Commands
{
    /// <summary>
    /// This class facilitates associating a key binding in XAML markup to a command
    /// defined in a View Model by exposing a Command dependency property.
    /// The class derives from Freezable to work around a limitation in WPF when data-binding from XAML.
    /// </summary>
    public class CommandReference : Freezable, ICommand
    {
        #region Public members

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(CommandReference),
                                                                                                new PropertyMetadata(OnCommandChanged));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var commandReference = d as CommandReference;
            var oldCommand = e.OldValue as ICommand;
            var newCommand = e.NewValue as ICommand;

            if (oldCommand != null && commandReference != null) oldCommand.CanExecuteChanged -= commandReference.CanExecuteChanged;
            if (newCommand != null && commandReference != null) newCommand.CanExecuteChanged += commandReference.CanExecuteChanged;
        }

        #endregion

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return Command != null && Command.CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            if (Command != null)
            {
                Command.Execute(parameter);
            }
        }

        public event EventHandler CanExecuteChanged;

        #endregion

        #region Freezable

        protected override Freezable CreateInstanceCore()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
} ;