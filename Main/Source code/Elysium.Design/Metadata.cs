using System.Diagnostics.CodeAnalysis;
using System.Windows;

using Microsoft.Windows.Design.Features;
using Microsoft.Windows.Design.Metadata;

namespace Elysium.Design
{
    [SuppressMessage("Microsoft.Naming", "CA1724:TypeNamesShouldNotMatchNamespaces")]
    public class Metadata : IProvideAttributeTable
    {
        public AttributeTable AttributeTable
        {
            get
            {
                var builder = new AttributeTableBuilder();
                builder.AddCustomAttributes(typeof(FrameworkElement), new FeatureAttribute(typeof(Manager)));
                return builder.CreateTable();
            }
        }
    }
}