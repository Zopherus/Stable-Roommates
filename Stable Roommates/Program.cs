using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

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
            // Use stopwatch to time the program
            Stopwatch stopwatch = Stopwatch.StartNew();
            
            // Read the names of the people from a text file
			StreamReader streamReader = new StreamReader("People1.txt");

            // The first line holds the total number of people
			int numberOfPeople = int.Parse(streamReader.ReadLine());

            // The next lines hold the names of the people
            // Add the people to the Person list
			for (int counter = 0; counter < numberOfPeople; counter++)
			{
				Person person = new Person(streamReader.ReadLine().Split('.')[1]);
				People.Add(person);
			}

            // Read in the preferences from a text file
            streamReader = new StreamReader("Preferences1.txt");
			for (int personNumber = 0; personNumber < People.Count; personNumber++)
			{
                // Use findPerson to find the person that the preferences are about
				Person person = findPerson(streamReader.ReadLine());

                // Loop through the next couple lines to read the data of the preferences
				for (int counter = 0; counter < NUM_PREFERENCES; counter++)
				{

					string name = streamReader.ReadLine().Split('.')[1];
					person.Preferences.Add(findPerson(name));
				}
			}

            List<List<Person>> permutations = Permutations(People).ToList<List<Person>>();
            int Maximum = Int32.MinValue;
            List<Room> BestRooms = new List<Room>();
            foreach(List<Person> people in permutations)
            {
                List<Room> rooms = PermutationToRooms(people);
                int score = ListRoomScore(rooms);
                if (score > Maximum)
                {
                    Maximum = score;
                    BestRooms = rooms;
                }
            }
            foreach (Room room in BestRooms)
            {
                Console.WriteLine(room.ToString());
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Console.ReadLine();
		}

        static int ListRoomScore(List<Room> rooms)
        {
            int result = 0;
            foreach(Room room in rooms)
            {
                result += room.Score();
            }
            return result;
        }

        static List<Room> PermutationToRooms(List<Person> people)
        {
            List<Room> rooms = new List<Room>();
            for (int startPosition = 0; startPosition < people.Count - SIZEROOM; startPosition += SIZEROOM)
            {
                List<Person> roommates = new List<Person>();
                for (int counter = startPosition; counter < startPosition + SIZEROOM; counter++)
                {
                    roommates.Add(people[counter]);
                }
                Room room = new Room(roommates);
                rooms.Add(room);
            }
            int peopleLeft = people.Count % SIZEROOM;
            List<Person> roommatesLeft = new List<Person>();
            for (int counter = 1; counter < peopleLeft + 1; counter++)
            {
                roommatesLeft.Add(people[people.Count - counter]);
            }
            Room roomLeft = new Room(roommatesLeft);
            rooms.Add(roomLeft);
            return rooms;
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

        static IEnumerable<List<Person>> Permutations(List<Person> people)
        {
            if (people.Count <= 0)
                yield return Enumerable.Empty<Person>().ToList();
            if (people.Count <= 1)
                yield return people;
            else
            {
                for (int startingElementIndex = 0; startingElementIndex < people.Count; startingElementIndex++)
                {
                    Person person = people[startingElementIndex];
                    List<Person> remainingPeople = new List<Person>(people);
                    remainingPeople.Remove(person);
                    foreach (List<Person> permutationOfRemainder in Permutations(remainingPeople))
                    {
                        permutationOfRemainder.Insert(0, person);
                        yield return permutationOfRemainder;
                    }
                }
            }
        }

        private static List<Person> AllExcept(List<Person> sequence, int indexToSkip)
        {
            sequence.RemoveAt(indexToSkip);
            return sequence;
        }
    }
}
