﻿using goatgitter.lib.extensions;
using NUnit.Framework;

namespace goatgitter.lib.tests
{
    /** 
    * ObjectsTest class Tests the Objects extension class.
    * MIT License
    * Copyright (c) 2022 goatgitter
    * */
    [TestFixture]
    public class ObjectsTest : TestBase
    {
        private object testObj;
        private string testStr;

        /// <inheritdoc/>
        [SetUp]
        public void SetupTest()
        {
            testObj = new object();
            testStr = TEST_NAME.Clone().ToString();
        }

        /// <inheritdoc/>
        [TearDown]
        public void CleanupTest()
        {
            testObj = null;
            testStr = null;
        }

        /// <inheritdoc/>
        [Test]
        public void IsEmptyTest()
        {
            Assert.IsFalse(testObj.IsEmpty());
            testObj = null;
            Assert.IsTrue(testObj.IsEmpty());
        }

        /// <inheritdoc/>
        [Test]
        public void IsNotEmptyTest()
        {
            Assert.IsTrue(testObj.IsNotEmpty());
            testObj = null;
            Assert.IsFalse(testObj.IsNotEmpty());
        }

        /// <inheritdoc/>
        [Test]
        public void SafeEqualsTest()
        {
            string str1 = null, str2 = null;
            // Objects that are both null are Equal.
            Assert.IsTrue(str1.SafeEquals(str2));
            Assert.IsTrue(str2.SafeEquals(str1));

            // If one has value, and the other is empty, they are NOT Equal.
            str1 = testStr;
            Assert.IsFalse(str1.SafeEquals(str2));
            Assert.IsFalse(str2.SafeEquals(str1));

            // If the objects have the same value, they are Equal.
            str2 = testStr;
            Assert.IsTrue(str1.SafeEquals(str2));
            Assert.IsTrue(str2.SafeEquals(str1));

            // If one object has a different value, they are NOT Equal.
            str2 = testStr + "something else";
            Assert.IsFalse(str1.SafeEquals(str2));
            Assert.IsFalse(str2.SafeEquals(str1));

            // If one object has a different type, they are NOT Equal.
            str1 = TEST_ID.SafeToString();
            int int1 = TEST_ID;
            Assert.IsFalse(str1.SafeEquals(int1));
            Assert.IsFalse(int1.SafeEquals(str1));
        }
    }
}