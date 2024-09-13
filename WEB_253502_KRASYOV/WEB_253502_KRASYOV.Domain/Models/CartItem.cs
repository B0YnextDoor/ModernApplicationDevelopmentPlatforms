using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEB_253502_KRASYOV.Domain.Entities;

namespace WEB_253502_KRASYOV.Domain.Models
{
    public class CartItem
    {
        public Device Item {  get; set; }
        public int Amount { get; set; }
    }
}
