using System;
using System.Linq.Expressions;

namespace Elysium.Theme.WPF.Extensions
{
    public static class ObjectExtensions
    {
        private static string GetObjectName<T>(Expression<Func<T>> expression)
        {
            return (expression.Body as MemberExpression).Member.Name;
        }

        public static string GetName(this object obj)
        {
            return GetObjectName(() => obj);
        }
    }
} ;