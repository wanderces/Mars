using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Mars.NavigateTerrain.Tests
{
    [TestClass]
    public class RobotTests
    {
        
        [TestMethod]
        public void Test_Result()
        {
            Robot marsRobot = new Robot();

            marsRobot.MatrixInput = "5x5";
            marsRobot.CommandsInput = "FFRFLFLF";
            String result1 = marsRobot.GetResult();
                        
            Assert.AreEqual("1, 4, West", result1);
            
        }
    }
}
