using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuPrimeiroTeste.PersonClasses
{
    public class PersonManager
    {
        public Person CreatePerson(string first, string last, bool isSupervisor)
        {
            Person ret = null;

            if (!String.IsNullOrEmpty(first))
            {
                if (isSupervisor)
                {
                    ret = new Supervisor();
                }
                else
                {
                    ret = new Employee();
                }

                ret.FirstName = first;
                ret.LastName = last;
            }

            return ret;

        }

        public List<Person> GetPeople()
        {
            List<Person> people = new List<Person>();

            people.Add(new Person() { FirstName = "Douglas", LastName = "Reis" });
            people.Add(new Person() { FirstName = "Laura", LastName = "Antonia" });
            people.Add(new Person() { FirstName = "Helena ", LastName = "Helena" });

            return people;
        }

        public List<Person> GetSupervisor()
        {
            List<Person> people = new List<Person>();

            people.Add(CreatePerson("Douglas", "Reis", true));
            people.Add(CreatePerson("Brune", "Helena", true));

            return people;
        }
    }
}
