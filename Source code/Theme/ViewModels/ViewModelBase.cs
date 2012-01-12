using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

namespace Elysium.Theme.ViewModels
{
    /// <summary>
    /// Provides common functionality for ViewModel classes
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool ThrowOnInvalidPropertyName
        {
            get { return _throwOnInvalidPropertyName; }
            set { _throwOnInvalidPropertyName = value; }
        }

        private bool _throwOnInvalidPropertyName = true;

        protected bool Disposed { get; set; }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }
            Contract.EndContractBlock();
            OnPropertyChanged(((MemberExpression)propertyExpression.Body).Member.Name);
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                var message = "Invalid property name: " + propertyName;

                if (ThrowOnInvalidPropertyName)
                {
                    throw new MemberAccessException(message);
                }
                Debug.Fail(message);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ViewModelBase()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            Disposed = true;
        }
    }
} ;