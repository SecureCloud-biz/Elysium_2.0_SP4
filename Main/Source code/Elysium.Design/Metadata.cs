using System.Diagnostics.CodeAnalysis;
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
                return builder.CreateTable();
            }
        }
    }
}