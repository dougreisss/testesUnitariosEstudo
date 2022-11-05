using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using System;
using System.Configuration;
using System.IO;

namespace MyClasseTest
{
    [TestClass]
    public class FileProcessTest
    {
        #region Propriedades e constantes 
        private const string BAD_FILE_NAME = @"C:\BadFileName.bat";
        private const string FILE_NAME = @"FileToDeploy.txt";
        private string _GoodFileName;

        public TestContext TestContext { get; set; }
        #endregion

        #region Test Initialize e CleanUp

        [TestInitialize]
        public void TestInitialize()
        {
            if (TestContext.TestName == "FileNameDoesExists")
            {
                setGoodFileName();
                if (!string.IsNullOrEmpty(_GoodFileName))
                {
                    TestContext.WriteLine($"Creating file: {_GoodFileName}");
                    File.AppendAllText(_GoodFileName, "Some Text");
                }
            }
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            if (TestContext.TestName == "FileNameDoesExists")
            {
                if (!string.IsNullOrEmpty(_GoodFileName))
                {
                    TestContext.WriteLine($"Deleting file: {_GoodFileName}");
                    File.Delete(_GoodFileName);
                }
            }
        }

        #endregion

        #region Metodo auxiliar
        public void setGoodFileName()
        {
            _GoodFileName = ConfigurationManager.AppSettings["GoodFileName"];
            if (_GoodFileName.Contains("[AppPath]"))
            {
                _GoodFileName = _GoodFileName.Replace("[AppPath]", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
        }
        #endregion

        #region AreEqual/AreNotEqual Tests

        [TestMethod]
        [Owner("Douglas")]
        public void AreEqualTest() 
        {
            string str1 = "Douglas";
            string str2 = "Douglas";

            Assert.AreEqual(str1, str2);

        }

        [TestMethod]
        [Owner("Douglas")]
        [ExpectedException(typeof(AssertFailedException))]
        [Ignore]
        public void AreEqualCaseSensitiveTest() 
        {
            string str1 = "douglas";
            string str2 = "douglas";

            Assert.AreEqual(str1, str2, false);
        }


        [TestMethod]
        [Owner("Douglas")]
        public void AreNotEqualTest()
        {
            string str1 = "Douglas";
            string str2 = "Douglas2";

            Assert.AreNotEqual(str1, str2);
        }

        #endregion

        #region AreSame/AreNotSame Tests

        [TestMethod]
        [Owner("Douglas")]
        public void AreSameTest()
        {
            FileProcess x = new FileProcess();
            FileProcess y = x;

            Assert.AreSame(x, y);
        }

        [TestMethod]
        [Owner("Douglas Reis")]
        public void AreNotSameTest()
        {
            FileProcess x = new FileProcess();
            FileProcess y = new FileProcess();

            Assert.AreNotSame(x, y);
        }

        #endregion

        #region Tests

        [TestMethod]
        [Owner("Douglas")]
        [DataSource("System.Data.SqlClient", 
            @"ConnectionString", 
            "FileProcessTest", DataAccessMethod.Sequential)]
        public void FileExistsTestFromDB()
        {
            FileProcess fp = new FileProcess();
            string fileName;
            bool expectedValue;
            bool causesException;
            bool fromCall;

            fileName = TestContext.DataRow["FileName"].ToString();
            expectedValue = Convert.ToBoolean(TestContext.DataRow["ExpectedValue"]);
            causesException = Convert.ToBoolean(TestContext.DataRow["CauseException"]);

            try
            {
                fromCall = fp.FileExists(fileName);
                Assert.AreEqual(expectedValue, fromCall, $"FileName: {fileName} has failed. METHOD: FileExistsTestFromDB");
            }
            catch (ArgumentException)
            {
                Assert.IsTrue(causesException);
            }

        }

        [TestMethod]
        [Owner("Douglas")]
        [DeploymentItem(FILE_NAME)]
        public void FileNameDoesExistsUsingDeploymentItem()
        {
            //Tecnica AAA
            //Arange --> Instância classes e variaveis necessarias para rodar o teste
            FileProcess fileProcess = new FileProcess();
            string fileName;
            bool fromCall;

            //Action --> Realiza a ação de teste

            fileName = $@"{TestContext.DeploymentDirectory}\{FILE_NAME}";
            TestContext.WriteLine($"Checking file: {FILE_NAME}");
            fromCall = fileProcess.FileExists(FILE_NAME);

            //Assert --> Verifica a ação
            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        [Timeout(3100)]
        [Priority(1)]
        public void SimulateTimeout()
        {
            System.Threading.Thread.Sleep(3000);
        }

        [TestMethod]
        [Description("Check to see if a file does exists.")]
        [Owner("Douglas")]
        [Priority(1)]
        [TestCategory("NoException")]
        public void FileNameDoesExists()
        {
            //Tecnica AAA
            //Arange --> Instância classes e variaveis necessarias para rodar o teste
            FileProcess fileProcess = new FileProcess();
            bool fromCall;

            //Action --> Realiza a ação de teste
          

            TestContext.WriteLine($"Testing file: {_GoodFileName}");
            fromCall = fileProcess.FileExists(_GoodFileName);

             //Assert --> Verifica a ação
            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        [Description("Check to see if a file does not exists.")]
        [Owner("Douglas")]
        [Priority(1)]
        [TestCategory("NoException")]
        public void FileNameDoesNotExists()
        {
            //Tecnica AAA
            //Arange --> Instância classes e variaveis necessarias para rodar o teste
            FileProcess fileProcess = new FileProcess();
            bool fromCall;

            //Action --> Realiza a ação de teste
            fromCall = fileProcess.FileExists(BAD_FILE_NAME);

            //Assert --> Verifica a ação
            Assert.IsFalse(fromCall);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        [Description("Check to see if a file is null or empty")]
        [Owner("Douglas")]
        [Priority(0)]
        [TestCategory("Exception")]
        public void FileNameNullOrEmpty_ThrowsArgumentNullException()
        {
            //Exception Handling
            FileProcess fileProcess = new FileProcess();

            fileProcess.FileExists("");
        }

        [TestMethod]
        [Owner("Douglas")]
        [Description("Check to see if a file is null or empty using try Catch")]
        [Priority(0)]
        [TestCategory("Exception")]
        public void FileNameNullOrEmpty_ThrowsArgumentNullException_UsingTryCatch()
        {
            FileProcess fileProcess = new FileProcess();

            try
            {
                fileProcess.FileExists("");
            }
            catch (ArgumentException)
            {
                //Isso foi um sucesso
                return;
            }

            Assert.Fail("Fail expected");
        }
        #endregion
    }
}
