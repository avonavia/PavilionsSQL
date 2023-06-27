using Microsoft.VisualStudio.TestTools.UnitTesting;
using PasswordLevelLibrary;
using System.Collections.Generic;

namespace UnitTestsPassw
{
    [TestClass]
    public class PasswordTest
    {
        [TestMethod]
        public void PasswordTests()
        {
            var passtest = new PasswordCheck();
            var passtest2 = new PasswordCheck();

            string pass1 = "fff";

                int result = passtest.CheckPassword(pass1);
                Assert.AreEqual(result, 0);
        }

        [TestMethod]
        public void PasswordTests2()
        {
            var passtest = new PasswordCheck();
            var passtest2 = new PasswordCheck();

            string pass2 = "fffFfffff";

            int result = passtest.CheckPassword(pass2);
            Assert.AreEqual(result, 2);
        }

        [TestMethod]
        public void PasswordTests3()
        {
            var passtest = new PasswordCheck();
            var passtest2 = new PasswordCheck();

            string pass2 = "fffFffff3";

            int result = passtest.CheckPassword(pass2);
            Assert.AreEqual(result, 3);
        }

        [TestMethod]
        public void PasswordTests4()
        {
            var passtest = new PasswordCheck();
            var passtest2 = new PasswordCheck();

            string pass2 = "fffFfff@3";

            int result = passtest.CheckPassword(pass2);
            Assert.AreEqual(result, 4);
        }

        [TestMethod]
        public void PasswordTests5()
        {
            var passtest = new PasswordCheck();
            var passtest2 = new PasswordCheck();

            string pass2 = "ffF";

            int result = passtest.CheckPassword(pass2);
            Assert.AreEqual(result, 1);
        }
    }
}
