using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Threading;
namespace LogComponentTests
{
    using LogTest;
    using System.IO;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestNewDayNewFile()
        {
            AsyncLog logger = new AsyncLog();
            logger.Write("Number with Flush: ");
            Thread.Sleep(1000);
            DateTime plusOne = DateTime.Now.AddDays(1);
            logger.logFile._curDate = plusOne;
            logger.logFile.isTest = true;
            logger.Write("Number with Flush: " );
            Thread.Sleep(1000);
            logger.StopWithoutFlush();

            Assert.IsTrue(File.Exists(logger.logFile.filename));
            Assert.IsTrue(logger.logFile.filename.Contains(plusOne.ToString("yyyyMMdd HHmmss fff")));
        }

        [TestMethod]
        public void TestWriteToLogWithFlush()
        {
            AsyncLog logger = new AsyncLog();
            int numberOfLines = 15;
            for (int i = 1; i < numberOfLines; i++)
            {
                logger.Write("Number with Flush: " + i.ToString());
                Thread.Sleep(50);
            }
            logger.StopWithFlush();

            Thread.Sleep(5000);
            logger.logFile._writer.Close();
            string[] readText = File.ReadAllLines(logger.logFile.filename);
            Assert.IsTrue(File.Exists(logger.logFile.filename));
            Assert.IsTrue(readText.Length==numberOfLines);

            
        }

        [TestMethod]
        public void TestWriteToLogWithOutFlush()
        {
            AsyncLog logger = new AsyncLog();
            int numberOfLines = 100;
            for (int i = 1; i < numberOfLines; i++)
            {
                logger.Write("Number with Flush: " + i.ToString());
                Thread.Sleep(20);
            }

            logger.StopWithoutFlush();

            Thread.Sleep(5000);
            logger.logFile._writer.Close();
            string[] readText = File.ReadAllLines(logger.logFile.filename);
            Assert.IsTrue(File.Exists(logger.logFile.filename));
            Assert.IsTrue(readText.Length < numberOfLines);


        }


       

    }
}
