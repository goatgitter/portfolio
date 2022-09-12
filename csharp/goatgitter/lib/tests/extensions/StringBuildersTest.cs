using goatgitter.lib.extensions;
using NUnit.Framework;
using System;
using System.Text;

namespace goatgitter.lib.tests.extensions
{
    /** 
    * ObjectsTest class Tests the Objects extension class.
    * MIT License
    * Copyright (c) 2022 goatgitter
    * */
    [TestFixture]
    public class StringBuildersTest : TestBase
    {
        private StringBuilder testSb;

        /// <inheritdoc/>
        [SetUp]
        public void SetupTest()
        {
            testSb = new StringBuilder(TEST_NAME.Clone().ToString());
        }

        /// <inheritdoc/>
        [TearDown]
        public void CleanupTest()
        {
            testSb = null;
        }
        private void CheckAppendIf(string valToAdd, bool doesNotChange)
        {
            string val1 = testSb.ToString();
            testSb.AppendIf(valToAdd);
            string val2 = testSb.ToString();
            bool areEqual = val1.Equals(val2);
            Assert.AreEqual(doesNotChange, areEqual);
            if (!doesNotChange)
            {
                bool endsWithNewLine = val2.EndsWith(Environment.NewLine);
                Assert.IsFalse(endsWithNewLine);
            }
        }

        /// <inheritdoc/>
        [Test]
        public void AppendIfTest()
        {
            // String is all spaces.
            CheckAppendIf(TEST_ALL_SPACES_STR, true);

            // String is two words
            CheckAppendIf(TEST_NAME, false);

            // String is null.
            CheckAppendIf(null, true);

            // String is all spaces.
            CheckAppendIf(TEST_ALL_SPACES_STR, true);

            // String has leading spaces.          
            CheckAppendIf(TEST_LEADING_SPACES_STR, false);

            // String has trailing spaces.
            CheckAppendIf(TEST_TRAILING_SPACES_STR, false);
        }

        private void CheckAppendLineIf(string valToAdd, bool doesNotChange)
        {
            string val1 = testSb.ToString();
            testSb.AppendLineIf(valToAdd);
            string val2 = testSb.ToString();
            bool areEqual = val1.Equals(val2);
            Assert.AreEqual(doesNotChange, areEqual);
            if (!doesNotChange)
            {
                bool endsWithNewLine = val2.EndsWith(Environment.NewLine);
                Assert.IsTrue(endsWithNewLine);
            }
        }

        /// <inheritdoc/>
        [Test]
        public void AppendLineIfTest()
        {
            // String is all spaces.
            CheckAppendLineIf(TEST_ALL_SPACES_STR, true);

            // String is two words
            CheckAppendLineIf(TEST_NAME, false);

            // String is null.
            CheckAppendLineIf(null, true);

            // String is all spaces.
            CheckAppendLineIf(TEST_ALL_SPACES_STR, true);

            // String has leading spaces.          
            CheckAppendLineIf(TEST_LEADING_SPACES_STR, false);

            // String has trailing spaces.
            CheckAppendLineIf(TEST_TRAILING_SPACES_STR, false);
        }
    }
}
