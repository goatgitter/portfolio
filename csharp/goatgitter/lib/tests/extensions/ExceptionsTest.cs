using goatgitter.lib.extensions;
using NUnit.Framework;
using System;

namespace goatgitter.lib.tests.extensions
{
    /** 
    * ObjectsTest class Tests the Objects extension class.
    * MIT License
    * Copyright (c) 2022 goatgitter
    * */
    [TestFixture]
    public class ExceptionsTest : TestBase
    {
        private Exception testObj = null;
        /// <inheritdoc/>
        [SetUp]
        public void SetupTest()
        {
            testObj = GetTestExceptionWithInner();
        }

        /// <inheritdoc/>
        [TearDown]
        public void CleanupTest()
        {
            testObj = null;
        }

        /// <inheritdoc/>
        [Test]
        public void LogPrintTest()
        {
            string logMsg = testObj.LogPrint();
            Assert.IsNotNull(logMsg);
        }

    }
}
