using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq.Expressions;

using JetBrains.Annotations;

namespace Elysium.Extensions
{
    [UsedImplicitly]
    internal static class ValidationHelper
    {
        [DebuggerHidden]
        [UsedImplicitly]
        [AssertionMethod]
        [ContractArgumentValidator]
        internal static void NotNull<T>([AssertionCondition(AssertionConditionType.IS_NOT_NULL)] T argument, [NotNull] Expression<Func<T>> parameterExpression)
            where T : class
        {
            NotNull(argument, ((MemberExpression)parameterExpression.Body).Member.Name);
        }

        [DebuggerHidden]
        [UsedImplicitly]
        [AssertionMethod]
        [ContractArgumentValidator]
        internal static void NotNull<T>([AssertionCondition(AssertionConditionType.IS_NOT_NULL)] T argument, [NotNull] string parameterName)
            where T : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(parameterName);
            }
            Contract.EndContractBlock();
        }

        [DebuggerHidden]
        [UsedImplicitly]
        [AssertionMethod]
        [ContractArgumentValidator]
        internal static void NotNullOrWhitespace([AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string argument,
                                                 [NotNull] Expression<Func<string>> parameterExpression)
        {
            NotNullOrWhitespace(argument, ((MemberExpression)parameterExpression.Body).Member.Name);
        }

        [DebuggerHidden]
        [UsedImplicitly]
        [AssertionMethod]
        [ContractArgumentValidator]
        internal static void NotNullOrWhitespace([AssertionCondition(AssertionConditionType.IS_NOT_NULL)] string argument, [NotNull] string parameterName)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentException(parameterName + " can't be null, empty or contains only whitespaces", parameterName);
            }
            Contract.EndContractBlock();
        }

        [DebuggerHidden]
        [UsedImplicitly]
        [ContractArgumentValidator]
        internal static void OfType<T>(T argument, [NotNull] Expression<Func<T>> parameterExpression, [NotNull] Type type, [NotNull] string typeName)
        {
            OfType(argument, ((MemberExpression)parameterExpression.Body).Member.Name, type, typeName);
        }

        [DebuggerHidden]
        [UsedImplicitly]
        [ContractArgumentValidator]
        internal static void OfType<T>(T argument, [NotNull] string parameterName, [NotNull] Type type, [NotNull] string typeName)
        {
            if (!(argument.GetType() == type))
            {
                throw new ArgumentException(parameterName + " must be of type: " + typeName, parameterName);
            }
            Contract.EndContractBlock();
        }

        [DebuggerHidden]
        [UsedImplicitly]
        [ContractArgumentValidator]
        [SuppressMessage("Microsoft.Contracts", "Nonnull-2-0")]
        internal static void OfTypes<T>(T argument, [NotNull] Expression<Func<T>> parameterExpression,
                                        [NotNull] Type firstType, [NotNull] Type secondType,
                                        [NotNull] string firstTypeName, [NotNull] string secondTypeName)
        {
            OfTypes(argument, ((MemberExpression)parameterExpression.Body).Member.Name, firstType, secondType, firstTypeName, secondTypeName);
        }

        [DebuggerHidden]
        [UsedImplicitly]
        [ContractArgumentValidator]
        internal static void OfTypes<T>(T argument, [NotNull] string parameterName,
                                        [NotNull] Type firstType, [NotNull] Type secondType,
                                        [NotNull] string firstTypeName, [NotNull] string secondTypeName)
        {
            if (!(argument.GetType() == firstType) && !(argument.GetType() == secondType))
            {
                throw new ArgumentException(parameterName + " must belong to one of the types: " + firstTypeName + ", " + secondTypeName, parameterName);
            }
            Contract.EndContractBlock();
        }
    }
} ;