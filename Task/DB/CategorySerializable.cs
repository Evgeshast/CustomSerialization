using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Task.DB
{
    [Serializable]
    public partial class Category
    {
        [OnSerializing]
        public void OnSerializing(StreamingContext context)
        {
            if (context.Context is SerializationObject serializationObject && serializationObject.Type == typeof(Category))
            {
                serializationObject.ObjectContext.LoadProperty(this, c => c.Products);
            }
        }
    }
}
