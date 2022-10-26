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
        private const string BAD_FILE_NAME = @"C:\BadFileName.bat";
        private string _GoodFileName;

        public TestContext TestContext { get; set; }

        public void setGoodFileName()
        {
            _GoodFileName = ConfigurationManager.AppSettings["GoodFileName"];
            if (_GoodFileName.Contains("[AppPath]"))
            {
                _GoodFileName = _GoodFileName.Replace("[AppPath]", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
        }

        [TestMethod]
        public void FileNameDoesExists()
        {
            //Tecnica AAA
            //Arange --> Instância classes e variaveis necessarias para rodar o teste
            FileProcess fileProcess = new FileProcess();
            bool fromCall;

            //Action --> Realiza a ação de teste
            setGoodFileName();
            TestContext.WriteLine($"Creating file: {_GoodFileName}");
            File.AppendAllText(_GoodFileName, "Some Text");

            TestContext.WriteLine($"Testing file: {_GoodFileName}");
            fromCall = fileProcess.FileExists(_GoodFileName);

            TestContext.WriteLine($"Deleting file: {_GoodFileName}");
            File.Delete(_GoodFileName);

            //Assert --> Verifica a ação
            Assert.IsTrue(fromCall);
        }

        [TestMethod]
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
        public void FileNameNullOrEmpty_ThrowsArgumentNullException()
        {
            //Exception Handling
            FileProcess fileProcess = new FileProcess();

            fileProcess.FileExists("");
        }

        [TestMethod]
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
    }
}
