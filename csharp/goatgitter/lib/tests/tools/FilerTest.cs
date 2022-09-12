using goatgitter.lib.tools;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goatgitter.lib.tests.tools
{

    /** 
    * MIT License
    * Copyright (c) 2022 goatgitter
    * ClassName : FilerTest
    * Purpose : Tests the Filer Class.
    **/
    public class FilerTest : TestBase
    {
        private Filer testObj;


        /// <summary>
        /// Resets objects modified during the test execution.
        /// </summary>
        [TearDown]
        public void CleanupTest()
        {
            testObj = null;
        }

        /// <summary>
        /// Tests the Constructor
        /// </summary>
        [Test]
        public void ConstructorTest()
        {
            testObj = new Filer();
            Assert.IsNotNull(testObj);
            Assert.IsNotNull(testObj.Notepad);
            Assert.IsNotNull(testObj.Notepad.Log);
            Assert.IsNotNull(testObj.Notepad.LogType);
            Assert.AreEqual(testObj.GetType(), testObj.Notepad.LogType);
        }

    }
}
