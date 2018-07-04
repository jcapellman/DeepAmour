using System;

using deepamour.cli;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DeepAmour.cli.unittests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void RunWithNullArguments()
        {
            try
            {
                Program.Run(null);
            }
            catch (Exception)
            {
                Assert.Fail("Shouldn't have thrown an exception");
            }
        }
    }
}