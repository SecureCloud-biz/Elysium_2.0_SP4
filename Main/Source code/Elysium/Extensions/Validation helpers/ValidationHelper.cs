using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;

using JetBrains.Annotations;

namespace Elysium.Extensions
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    [DebuggerNonUserCode]
    [System.Diagnostics.Contracts.Pure]
    internal static class ValidationHelper
    {
        [ContractAnnotation("halt <= argument: null")]
        [ContractArgumentValidator]
        internal static void NotNull<T>([ValidatedNotNull] T argument, [NotNull] Expression<Func<T>> parameterExpression)
        {
            NotNull(argument, ((MemberExpression)parameterExpression.Body).Member.Name);
        }

        [ContractAnnotation("halt <= argument: null")]
        [ContractArgumentValidator]
        internal static void NotNull<T>([ValidatedNotNull] T argument, [NotNull] string parameterName)
        {
// ReSharper disable CompareNonConstrainedGenericWithNull
            if (argument == null)
// ReSharper restore CompareNonConstrainedGenericWithNull
            {
                throw new ArgumentNullException(parameterName);
            }
            Contract.EndContractBlock();
        }

        [ContractAnnotation("halt <= argument: null")]
        [ContractArgumentValidator]
        internal static void NotNullOrWhitespace([ValidatedNotNull] string argument, [NotNull] Expression<Func<string>> parameterExpression)
        {
            NotNullOrWhitespace(argument, ((MemberExpression)parameterExpression.Body).Member.Name);
        }

        [ContractAnnotation("halt <= argument: null")]
        [ContractArgumentValidator]
        internal static void NotNullOrWhitespace([ValidatedNotNull] string argument, [NotNull] string parameterName)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentException(parameterName + " can't be null, empty or contains only whitespaces", parameterName);
            }
            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        internal static void NotNullAll<TEnumerable, TElement>(TEnumerable argument, [NotNull] Expression<Func<TEnumerable>> parameterExpression)
            where TEnumerable : IEnumerable<TElement>
        {
            NotNullAll<TEnumerable, TElement>(argument, ((MemberExpression)parameterExpression.Body).Member.Name);
        }

        [ContractArgumentValidator]
        internal static void NotNullAll<TEnumerable, TElement>(TEnumerable argument, [NotNull] string parameterName)
            where TEnumerable : IEnumerable<TElement>
        {
// ReSharper disable CompareNonConstrainedGenericWithNull
// ReSharper disable SimplifyLinqExpression
            if (!argument.All(item => item != null))
// ReSharper restore SimplifyLinqExpression
// ReSharper restore CompareNonConstrainedGenericWithNull
            {
                throw new ArgumentException(parameterName + " contains null element", parameterName);
            }
            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        internal static void NotOfType<T>(T argument, [NotNull] Expression<Func<T>> parameterExpression, [NotNull] Type type, string reason = null)
        {
            NotOfType(argument, ((MemberExpression)parameterExpression.Body).Member.Name, type, reason);
        }

        [ContractArgumentValidator]
        internal static void NotOfType<T>(T argument, [NotNull] string parameterName, [NotNull] Type type, string reason = null)
        {
            if (type.IsInstanceOfType(argument))
            {
                throw new ArgumentException(reason ?? parameterName + " must not be of type: " + type.Name, parameterName);
            }
            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        internal static void OfType<T>(T argument, [NotNull] Expression<Func<T>> parameterExpression, [NotNull] Type type, string reason = null)
        {
            OfType(argument, ((MemberExpression)parameterExpression.Body).Member.Name, type, reason);
        }

        [ContractArgumentValidator]
        internal static void OfType<T>(T argument, [NotNull] string parameterName, [NotNull] Type type, string reason = null)
        {
            if (!type.IsInstanceOfType(argument))
            {
                throw new ArgumentException(reason ?? parameterName + " must be of type: " + type.Name, parameterName);
            }
            Contract.EndContractBlock();
        }

        [ContractArgumentValidator]
        internal static void OfTypes<T>(T argument, [NotNull] Expression<Func<T>> parameterExpression, [NotNull] Type firstType, [NotNull] Type secondType, string reason = null)
        {
            OfTypes(argument, ((MemberExpression)parameterExpression.Body).Member.Name, firstType, secondType, reason);
        }

        [ContractArgumentValidator]
        internal static void OfTypes<T>(T argument, [NotNull] string parameterName, [NotNull] Type firstType, [NotNull] Type secondType, string reason = null)
        {
            if (!(firstType.IsInstanceOfType(argument) || secondType.IsInstanceOfType(argument)))
            {
                throw new ArgumentException(reason ?? parameterName + " must belong to one of the types: " + firstType.Name + ", " + secondType.Name, parameterName);
            }
            Contract.EndContractBlock();
        }
    }
}