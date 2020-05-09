using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.DB;
using Task.TestHelpers;
using System.Runtime.Serialization;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using Task.Surrogates;

namespace Task
{
	[TestClass]
	public class SerializationSolutions
	{
		Northwind dbContext;

		[TestInitialize]
		public void Initialize()
		{
			dbContext = new Northwind();
		}

		[TestMethod]
		public void SerializationCallbacks()
		{
			dbContext.Configuration.ProxyCreationEnabled = false;

            var serializationContext = new SerializationObject
            {
                ObjectContext = (dbContext as IObjectContextAdapter).ObjectContext,
                Type = typeof(Category)
            };
            var xmlSerializer = new NetDataContractSerializer(new StreamingContext(StreamingContextStates.All, serializationContext));

            var serializer = new XmlDataContractSerializerTester<IEnumerable<Category>>(xmlSerializer);
            var tester = new SerializationTester<IEnumerable<Category>>(serializer, true);
            var categories = dbContext.Categories.ToList();

            tester.SerializeAndDeserialize(categories);
		}

		[TestMethod]
		public void ISerializable()
		{
            dbContext.Configuration.ProxyCreationEnabled = false;

            var serializationContext = new SerializationObject()
            {
                ObjectContext = (dbContext as IObjectContextAdapter).ObjectContext,
                Type = typeof(Product)
            };
            var xmlSerializer = new NetDataContractSerializer(new StreamingContext(StreamingContextStates.All, serializationContext));
            var serializer = new XmlDataContractSerializerTester<IEnumerable<Product>>(xmlSerializer);
            var tester = new SerializationTester<IEnumerable<Product>>(serializer, true);
            var products = dbContext.Products.ToList();

            tester.SerializeAndDeserialize(products);
		}


		[TestMethod]
		public void ISerializationSurrogate()
		{
            dbContext.Configuration.ProxyCreationEnabled = false;

            var serializationContext = new SerializationObject()
            {
                ObjectContext = (dbContext as IObjectContextAdapter).ObjectContext,
                Type = typeof(Order_Detail)
            };

            var xmlSerializer = new NetDataContractSerializer()
            {
                SurrogateSelector = new OrderDetailSurrogateSelector(),
                Context = new StreamingContext(StreamingContextStates.All, serializationContext)
            };
            
            var serializer = new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(xmlSerializer);
            var tester = new SerializationTester<IEnumerable<Order_Detail>>(serializer, true);
            var orderDetails = dbContext.Order_Details.ToList();

            tester.SerializeAndDeserialize(orderDetails);
        }

		[TestMethod]
		public void IDataContractSurrogate()
		{
            dbContext.Configuration.ProxyCreationEnabled = false;
            dbContext.Configuration.LazyLoadingEnabled = false;

            var xmlSerializer = new DataContractSerializer(typeof(IEnumerable<Order>),
            new DataContractSerializerSettings
            {
                DataContractSurrogate = new OrderDataContractSurrogate()
            });
            var serializer = new XmlDataContractSerializerTester<IEnumerable<Order>>(xmlSerializer);
            var tester = new SerializationTester<IEnumerable<Order>>(serializer, true);
            var orders = dbContext.Orders.ToList();

            tester.SerializeAndDeserialize(orders);
        }
	}
}
