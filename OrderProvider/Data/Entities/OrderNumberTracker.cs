using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Data.Entities
{
    public class OrderNumberTracker
    {
        [Key]
        public int Id { get; set; }
        public int LastOrderNumber { get; set; }
    }
}
