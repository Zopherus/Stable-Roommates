using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Grouping
{
	class Program
	{
		// Number of preferences that each person submits
		const int NUM_PREFERENCES = 5;
		// Size of each room
		const int SIZEROOM = 4;
		static List<Person> People = new List<Person>();
		static void Main(string[] args)
		{
			StreamReader streamReader = new StreamReader("C:/Users/1012746/Desktop/People.txt");
			int numberOfPeople = int.Parse(streamReader.ReadLine());
			for (int counter = 0; counter < numberOfPeople; counter++)
			{
				Person person = new Person(streamReader.ReadLine());
				People.Add(person);
			}

			streamReader = new StreamReader("C:/Users/1012746/Desktop/Preferences.txt");
			for (int personNumber = 0; personNumber < People.Count; personNumber++)
			{
				Person person = findPerson(streamReader.ReadLine());
				for (int counter = 0; counter < NUM_PREFERENCES; counter++)
				{
					string name = streamReader.ReadLine().Split('.')[1];
					person.Preferences.Add(findPerson(name));
				}
			}



		}

		static Person findPerson(string name)
		{
			foreach (Person person in People)
			{
				if (person.Name == name)
					return person;
			}
			return null;
		}

	}
}
