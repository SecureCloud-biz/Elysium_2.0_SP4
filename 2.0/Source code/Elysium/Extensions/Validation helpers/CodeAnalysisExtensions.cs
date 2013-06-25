// ReSharper disable CheckNamespace
namespace System.Diagnostics.CodeAnalysis
{
    [ConditionalAttribute("CODE_ANALYSIS")]
    [AttributeUsageAttribute(AttributeTargets.Parameter, Inherited = false, AllowMultiple = false)]
    internal sealed class ValidatedNotNullAttribute : Attribute
    {
    }
}
// ReSharper restore CheckNamespace