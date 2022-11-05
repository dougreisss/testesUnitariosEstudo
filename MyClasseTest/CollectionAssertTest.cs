using MeuPrimeiroTeste.PersonClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClasseTest
{
    [TestClass]
    public class CollectionAssertTest
    {
        [TestMethod]
        [Owner("Douglas")] 
        public void AreCollectionEqualFailsTest()
        {
            PersonManager PerMgr = new PersonManager();
            List<Person> peopleExpected = new List<Person>();   
            List<Person> peopleActual = new List<Person>();

            peopleExpected.Add(new Person() { FirstName = "Douglas", LastName = "Reis" });
            peopleExpected.Add(new Person() { FirstName = "Laura", LastName = "Antonia" });
            peopleExpected.Add(new Person() { FirstName = "Helena ", LastName = "Helena" });

            //You shall not pass!
            //peopleActual = PerMgr.GetPeople();

            peopleActual = peopleExpected;

            CollectionAssert.AreEqual(peopleExpected, peopleActual);
        }

        [TestMethod]
        [Owner("Douglas")]
        public void AreCollectionEqualWithComparerTest()
        {
            PersonManager PerMgr = new PersonManager();
            List<Person> peopleExpected = new List<Person>();
            List<Person> peopleActual = new List<Person>();

            peopleExpected.Add(new Person() { FirstName = "Douglas", LastName = "Reis" });
            peopleExpected.Add(new Person() { FirstName = "Laura", LastName = "Antonia" });
            peopleExpected.Add(new Person() { FirstName = "Helena ", LastName = "Helena" });

            peopleActual = PerMgr.GetPeople();

            CollectionAssert.AreEqual(peopleExpected, peopleActual, Comparer<Person>.Create((x, y) => x.FirstName == y.FirstName && x.LastName == y.LastName ? 0 : 1));
        }

        [TestMethod]
        [Owner("Douglas")]
        public void AreCollectionEquivalentTest()
        {
            PersonManager PerMgr = new PersonManager();
            List<Person> peopleExpected = new List<Person>();
            List<Person> peopleActual = new List<Person>();

            //peopleActual = PerMgr.GetPeople();

            //peopleExpected.Add(peopleActual[1]);
            //peopleExpected.Add(peopleActual[2]);
            //peopleExpected.Add(peopleActual[0]);


            //CollectionAssert.AreEquivalent(peopleExpected, peopleActual);

            peopleActual = PerMgr.GetSupervisor();

            CollectionAssert.AllItemsAreInstancesOfType(peopleActual, typeof(Supervisor));

        }
    }
}
