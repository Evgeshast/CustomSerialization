using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task
{
    public class SerializationObject
    {
        public ObjectContext ObjectContext { get; set; }
        public Type Type { get; set; }
    }
}
