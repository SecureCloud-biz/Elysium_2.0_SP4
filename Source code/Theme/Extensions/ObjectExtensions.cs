using System;
using System.Linq.Expressions;

namespace Elysium.Theme.Extensions
{
    public static class ObjectExtensions
    {
        private static string GetObjectName<T>(Expression<Func<T>> expression)
        {
            return ((MemberExpression)expression.Body).Member.Name;
        }
    }
} ;