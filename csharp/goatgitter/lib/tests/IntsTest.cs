using goatgitter.lib.extensions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace goatgitter.lib.tests
{/** 
    * ObjectsTest class Tests the Objects extension class.
    * MIT License
    * Copyright (c) 2022 goatgitter
    * */
    [TestFixture]
    public class IntsTest : TestBase
    {
        private int testObj = 0;
        /// <inheritdoc/>
        [SetUp]
        public void SetupTest()
        {
            testObj = TEST_ID;
        }

        /// <inheritdoc/>
        [TearDown]
        public void CleanupTest()
        {
            testObj = 0;
        }

        /// <inheritdoc/>
        [Test]
        public void IsPositiveTest()
        {
            // Test Id, positive value
            Assert.IsTrue(testObj.IsPositive());
            // Zero is not positive
            testObj = 0;
            Assert.IsFalse(testObj.IsPositive());
            // Negative number is not positive
            testObj = -123;
            Assert.IsFalse(testObj.IsPositive());
            // Min Int val is not positive
            testObj = Int32.MinValue;
            Assert.IsFalse(testObj.IsPositive());
        }

        /// <inheritdoc/>
        [Test]
        public void ToSafeStringTest()
        {
            // Test Id, positive value
            string result = testObj.ToSafeString();
            Assert.IsNotNull(result);
            // Zero is not positive
            testObj = 0;
            result = testObj.ToSafeString();
            Assert.IsNull(result);
            // Negative number is not positive
            testObj = -123;
            result = testObj.ToSafeString();
            Assert.IsNull(result);
            // Min Int val is not positive
            testObj = Int32.MinValue;
            result = testObj.ToSafeString();
            Assert.IsNull(result);
        }
    }
}
