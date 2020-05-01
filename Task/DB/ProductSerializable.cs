using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Task.DB
{
    [Serializable]
    public partial class Product : ISerializable
    {
        protected Product(SerializationInfo info, StreamingContext context)
        {
            CategoryID = info.GetInt32(nameof(CategoryID));
            Discontinued = info.GetBoolean(nameof(Discontinued));
            ProductID = info.GetInt32(nameof(ProductID));
            ProductName = info.GetString(nameof(ProductName));
            QuantityPerUnit = info.GetString(nameof(QuantityPerUnit));
            ReorderLevel = info?.GetInt16(nameof(ReorderLevel));
            SupplierID = info.GetInt32(nameof(SupplierID));
            UnitPrice = info.GetDecimal(nameof(UnitPrice));
            UnitsInStock = info.GetInt16(nameof(UnitsInStock));
            UnitsOnOrder = info.GetInt16(nameof(UnitsOnOrder));
            Supplier = (Supplier)info.GetValue(nameof(Supplier), typeof(Supplier));
            Category = (Category)info.GetValue(nameof(Category), typeof(Category));
            Order_Details = (ICollection<Order_Detail>)info.GetValue(nameof(Order_Details), typeof(ICollection<Order_Detail>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(CategoryID), CategoryID);
            info.AddValue(nameof(Discontinued), Discontinued);
            info.AddValue(nameof(ProductID), ProductID);
            info.AddValue(nameof(ProductName), ProductName);
            info.AddValue(nameof(QuantityPerUnit), QuantityPerUnit);
            info.AddValue(nameof(ReorderLevel), ReorderLevel);
            info.AddValue(nameof(SupplierID), SupplierID);
            info.AddValue(nameof(UnitPrice), UnitPrice);
            info.AddValue(nameof(UnitsInStock), UnitsInStock);
            info.AddValue(nameof(UnitsOnOrder), UnitsOnOrder);

            if (context.Context is SerializationObject serializationContext && serializationContext.Type == typeof(Product))
            {
                serializationContext.ObjectContext.LoadProperty(this, p => p.Supplier);
                serializationContext.ObjectContext.LoadProperty(this, p => p.Category);
                serializationContext.ObjectContext.LoadProperty(this, p => p.Order_Details);
            }

            info.AddValue(nameof(Supplier), Supplier, typeof(Supplier));
            info.AddValue(nameof(Category), Category, typeof(Category));
            info.AddValue(nameof(Order_Details), Order_Details, typeof(ICollection<Order_Detail>));
        }
    }
}
