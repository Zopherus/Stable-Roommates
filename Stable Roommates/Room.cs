using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouping
{
	class Room
	{
		public List<Person> Roommates = new List<Person>();

		public Room(List<Person> Roommates)
		{
			this.Roommates = Roommates;
		}


		public int Score()
		{
			int score = 0;
			// Look at each pair of people in the room
			// Needs to look at both viewpoints of the people
			for (int firstPerson = 0; firstPerson < Roommates.Count; firstPerson++)
			{
				for (int secondPerson = 0; secondPerson < Roommates.Count; secondPerson++)
				{
					if (firstPerson == secondPerson)
						continue;
					score += preference(Roommates[firstPerson], Roommates[secondPerson]);
				}
			}
			return score;
		}

		// Gives how much the first person likes the second person
		public int preference(Person firstPerson, Person secondPerson)
		{
			for (int counter = 0; counter < firstPerson.Preferences.Count; counter++)
			{
				Person person = firstPerson.Preferences[counter];
				if (person.Equals(secondPerson))
					return 5 - counter;
			}
			return 0;
		}

        public override string ToString()
        {
            string[] names = Roommates.Select(x => x.Name).ToArray();
            return string.Join(", ", names) + Score().ToString();
        }
	}
}
