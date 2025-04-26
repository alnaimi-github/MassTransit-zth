using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Application.Data.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Comments { get; set; } = string.Empty;
    }
}
