using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouping
{
	class Person
	{
		public string Name;
		// Ordered list of preferences
		public List<Person> Preferences = new List<Person>();
		public Person(string Name)
		{
			this.Name = Name;
		}
	}
}
