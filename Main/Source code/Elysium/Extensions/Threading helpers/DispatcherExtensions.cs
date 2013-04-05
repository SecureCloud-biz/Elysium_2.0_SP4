using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Threading;

using JetBrains.Annotations;

namespace Elysium.Extensions
{
    [PublicAPI]
    public static class DispatcherExtensions
    {
        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "It's extension method.")]
        public static DispatcherOperation BeginInvoke(this Dispatcher dispatcher, Action callback)
        {
            return dispatcher.BeginInvoke(callback);
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "It's extension method.")]
        public static DispatcherOperation BeginInvoke(this Dispatcher dispatcher, Action callback, DispatcherPriority priority)
        {
            return dispatcher.BeginInvoke(callback, priority);
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "It's extension method.")]
        public static object Invoke(this Dispatcher dispatcher, Action callback)
        {
            return dispatcher.Invoke(callback);
        }

        [PublicAPI]
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "It's extension method.")]
        public static object Invoke(this Dispatcher dispatcher, Action callback, DispatcherPriority priority)
        {
            return dispatcher.Invoke(callback, priority);
        }
    }
}