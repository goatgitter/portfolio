using goatgitter.lib.tools;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using static goatgitter.lib.Constants;

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
        /// Creates initial state of the objects for test execution.
        /// </summary>
        [SetUp]
        public void SetupTest()
        {
            TestLogType = typeof(Filer);
            SetupMocks();
        }

        /// <summary>
        /// Resets objects modified during the test execution.
        /// </summary>
        [TearDown]
        public void CleanupTest()
        {
            ResetMocks();
            testObj = null;
        }

        /// <summary>
        /// Tests the Constructor
        /// </summary>
        [Test]
        public void ConstructorTest()
        {

            testObj = new Filer(MockLogger.Object);
            Assert.IsNotNull(testObj);
            Assert.IsNotNull(testObj.Notepad);
            Assert.IsNotNull(testObj.Notepad.Log);
            Assert.IsNotNull(testObj.Notepad.LogType);
            Assert.AreEqual(testObj.GetType(), testObj.Notepad.LogType);
        }

        private void VerifyErrorCreateDir(string folder, int numTimes)
        {
            MockLogger.Verify(m => m.LogExceptionWithData(It.Is<string>(s => s.Equals(ERR_CREATE_DIR)),
                It.Is<object[]>(o => o.Contains<object>(folder)),
                It.IsAny<Exception>()), Times.Exactly(numTimes));
        }

        private void VerifyNoErrorNullResult(string result, string folder)
        {
            VerifyErrorCreateDir(folder, 0);
            Assert.IsNull(result);
        }


        /// <summary>
        /// Tests the Constructor
        /// </summary>
        [Test]
        public void SafeGetFilePathTest()
        {
            testObj = new Filer(MockLogger.Object);
            string folder = null;
            string fileName = null;

            // Null Folder, null File, Do NOT Create folder
            
            string result = testObj.SafeGetFilePath(folder, fileName, false);
            VerifyNoErrorNullResult(result, folder);

            // Null Folder, null File, Do Create folder
            result = testObj.SafeGetFilePath(folder, fileName, true);
            VerifyNoErrorNullResult(result, folder);

            // Null Folder, NOT null File, Do NOT Create folder
            fileName = TEST_FILE_NAME;
            result = testObj.SafeGetFilePath(folder, fileName, false);
            VerifyNoErrorNullResult(result, folder);

            // Null Folder, NOT null File, Do Create folder
            result = testObj.SafeGetFilePath(folder, fileName, true);
            VerifyNoErrorNullResult(result, folder);

            // NOT Null Folder, null File, Do NOT Create folder
            folder = TEST_DIR_NAME;
            fileName = null;
            result = testObj.SafeGetFilePath(folder, fileName, false);
            VerifyNoErrorNullResult(result, folder);

            // NOT Null Folder, null File, Do Create folder
            result = testObj.SafeGetFilePath(folder, fileName, true);
            VerifyNoErrorNullResult(result, folder);

            // NOT Null Folder, NOT null File, Do NOT Create folder
            fileName = TEST_FILE_NAME;
            result = testObj.SafeGetFilePath(folder, fileName, false);
            VerifyNoErrorNullResult(result, folder);

            // NOT Null Folder, NOT null File, Do Create folder
            result = testObj.SafeGetFilePath(folder, fileName, true);
            VerifyNoErrorNullResult(result, folder);

            bool deleteResult = testObj.SafeDeleteFolder(folder);
            Assert.IsTrue(deleteResult);
        }


        /// <summary>
        /// Tests the SafeGetFilePath Method 
        /// For an Invalid directory name.
        /// This will generate an error that is logged and return a null result.
        /// </summary>
        [Test]
        public void SafeGetFilePathForInvalid()
        {
            testObj = new Filer(MockLogger.Object);
            string result = testObj.SafeGetFilePath(TEST_DIR_INVALID,TEST_FILE_NAME, true);
            VerifyErrorCreateDir(TEST_DIR_INVALID, 1);
            Assert.IsNull(result);
        }

        /// <summary>
        /// Tests the SafeGetFilePath Method 
        /// For an Invalid file name.
        /// This will generate an error that is logged and return a null result.
        /// </summary>
        [Test]
        public void SafeGetFilePathForInvalidFile()
        {
            testObj = new Filer(MockLogger.Object);
            string result = testObj.SafeGetFilePath(TEST_DIR_NAME, TEST_FILE_INVALID, true);
            VerifyNoErrorNullResult(result, TEST_DIR_NAME);
        }

    }
}
