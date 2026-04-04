using AddressBookSystem.Models;

namespace AddressBookSystem.Utilities
{
    public class SortingUtility
    {
        public delegate int PersonComparator(Person p1, Person p2);

        public static void SortByName(List<Person> persons)
        {
            PersonComparator comparator = (p1, p2) =>
            {
                int firstNameComparison = string.Compare(p1.FirstName, p2.FirstName, StringComparison.OrdinalIgnoreCase);
                if (firstNameComparison != 0)
                    return firstNameComparison;
                return string.Compare(p1.LastName, p2.LastName, StringComparison.OrdinalIgnoreCase);
            };

            QuickSort(persons, 0, persons.Count - 1, comparator);
        }

        public static void SortByCityStateZip(List<Person> persons)
        {
            PersonComparator comparator = (p1, p2) =>
            {
                int cityComparison = string.Compare(p1.City, p2.City, StringComparison.OrdinalIgnoreCase);
                if (cityComparison != 0)
                    return cityComparison;

                int stateComparison = string.Compare(p1.State, p2.State, StringComparison.OrdinalIgnoreCase);
                if (stateComparison != 0)
                    return stateComparison;

                return string.Compare(p1.Zip, p2.Zip, StringComparison.OrdinalIgnoreCase);
            };

            QuickSort(persons, 0, persons.Count - 1, comparator);
        }

        private static void QuickSort(List<Person> persons, int low, int high, PersonComparator comparator)
        {
            if (low < high)
            {
                int pi = Partition(persons, low, high, comparator);
                QuickSort(persons, low, pi - 1, comparator);
                QuickSort(persons, pi + 1, high, comparator);
            }
        }

        private static int Partition(List<Person> persons, int low, int high, PersonComparator comparator)
        {
            Person pivot = persons[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (comparator(persons[j], pivot) < 0)
                {
                    i++;
                    Swap(persons, i, j);
                }
            }

            Swap(persons, i + 1, high);
            return i + 1;
        }

        private static void Swap(List<Person> persons, int i, int j)
        {
            Person temp = persons[i];
            persons[i] = persons[j];
            persons[j] = temp;
        }
    }
}
