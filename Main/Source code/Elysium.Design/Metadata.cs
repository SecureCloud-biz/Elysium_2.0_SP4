using System.Windows;

using Microsoft.Windows.Design.Features;
using Microsoft.Windows.Design.Metadata;

namespace Elysium.Design
{
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