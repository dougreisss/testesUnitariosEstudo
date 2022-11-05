using MeuPrimeiroTeste.PersonClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyClasseTest
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class PersonManagerTest
    {
      
        [TestMethod]
        [Owner("Douglas")]
        public void CreatePerson_OfTypeEmployeeTest()
        {
            PersonManager PerMgr = new PersonManager();
            Person per;

            per = PerMgr.CreatePerson("Douglas", "Reis", false);

            Assert.IsInstanceOfType(per, typeof(Employee));
        }

        [TestMethod]
        [Owner("Douglas")]
        public void DoEmployeeExistTest()
        {
            Supervisor super = new Supervisor();

            super.Employees = new List<Employee>();

            super.Employees.Add(new Employee()
            {
                FirstName = "Douglas",
                LastName = "Reis"
            });

            Assert.IsTrue(super.Employees.Count > 0);

        }

    }
}
