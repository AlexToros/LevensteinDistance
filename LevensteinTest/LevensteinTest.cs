using System;
using LevensteinDistanceCalculation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LevensteinTest
{
    [TestClass]
    public class LevensteinTest
    {
        [TestMethod]
        public void TestDistance()
        {
            Assert.AreEqual(0, Program.GetLevensteinDistance("тест", "тест"));
            Assert.AreEqual(4, Program.GetLevensteinDistance("", "тест"));
            Assert.AreEqual(2, Program.GetLevensteinDistance("тетс", "тест"));
            Assert.AreEqual(1, Program.GetLevensteinDistance("тест", "теста"));
            Assert.AreEqual(1, Program.GetLevensteinDistance("тест", "тес"));
            Assert.AreEqual(1, Program.GetLevensteinDistance("тест", "тост"));
        }
    }
}
