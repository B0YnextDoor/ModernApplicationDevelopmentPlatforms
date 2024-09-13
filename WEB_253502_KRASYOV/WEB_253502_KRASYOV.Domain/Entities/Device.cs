using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEB_253502_KRASYOV.Domain.Entities
{
	public class Device : Entity
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public Category? Category { get; set; }

		public int CategoryId { get; set; }

		public decimal Price { get; set; }

		public string? Image {  get; set; }
	}
}
