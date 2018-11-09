using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ZFood.Web.DTO
{
    [DataContract(Name = "Page")]
    public class PageDTO<T>
    {
        [DataMember]
        public IEnumerable<T> Items { get; set; }

        [DataMember]
        public bool HasMore { get; set; }

        [DataMember]
        public int TotalCount { get; set; }
    }
}
