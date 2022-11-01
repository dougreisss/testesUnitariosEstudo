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
    public class AssertClassTest
    {
        #region IsInstanceType Test

        [TestMethod]
        [Owner("Douglas")]
        public void IsInstanceTypeTest()
        {
            PersonManager mgr = new PersonManager();

            Person per;

            per = mgr.CreatePerson("Douglas", "Reis", true);

            Assert.IsInstanceOfType(per, typeof(Supervisor));

        }

        [TestMethod]
        [Owner("Douglas")]
        public void IsNullTest()
        {
            PersonManager mgr = new PersonManager();

            Person per;

            per = mgr.CreatePerson("", "Reis", true);

            Assert.IsNull(per);
        }


        #endregion
    }
}
