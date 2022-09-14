﻿using goatgitter.lib.tools;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using static goatgitter.lib.Constants;
using goatgitter.lib.extensions;
using System.IO;

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

        private void VerifyCreateDirError(string folder, int numTimes)
        {
            String dirPath = testObj.GetAppPathFolder(folder);
            MockLogger.Verify(m => m.LogExceptionWithData(It.Is<string>(s => s.Equals(ERR_CREATE_DIR)),
                It.Is<object[]>(o => o.Contains<object>(dirPath)),
                It.IsAny<Exception>()), Times.Exactly(numTimes));
        }

        private void VerifyCreateFileError(string folder, int numTimes)
        {
            String dirPath = testObj.GetAppPathFolder(folder);
            MockLogger.Verify(m => m.LogExceptionWithData(It.Is<string>(s => s.Equals(ERR_CREATE_FILE)),
                It.Is<object[]>(o => o.Contains<object>(dirPath)),
                It.IsAny<Exception>()), Times.Exactly(numTimes));
        }

        private void VerifyCreateDirNoErrorNullResult(string result, string folder)
        {
            VerifyCreateDirError(folder, 0);
            Assert.IsNull(result);
            bool deleteResult = testObj.SafeDeleteFolder(folder);
            Assert.IsTrue(deleteResult);
        }

        private void VerifyDeleteDirError(string folder, bool emptyFolder, int numTimes)
        {
            String dirPath = testObj.GetAppPathFolder(folder);
            MockLogger.Verify(m => m.LogExceptionWithData(It.Is<string>(s => s.Equals(ERR_DELETE_DIR)),
                It.Is<object[]>(o => o.Contains<object>(dirPath) && o.Contains<object>(emptyFolder)),
                It.IsAny<Exception>()), Times.Exactly(numTimes));
        }

        private void VerifyDeleteDirNoErrorFalseResult(bool result, string folder, bool emptyFolder)
        {
            VerifyDeleteDirError(folder, emptyFolder, 0);
            Assert.IsFalse(result);
        }

        private void VerifyCreateDirNoErrorTrueResult(bool result, string folder)
        {
            VerifyCreateDirError(folder, 0);
            Assert.IsTrue(result);
        }

        private void VerifyDeleteDirNoErrorTrueResult(bool result, string folder, bool emptyFolder)
        {
            VerifyDeleteDirError(folder, emptyFolder, 0);
            Assert.IsTrue(result);
        }

        private void VerifyCreateFileNoErrorTrueResult(bool result, string folder)
        {
            VerifyCreateFileError(folder, 0);
            Assert.IsTrue(result);
        }

        /// <summary>
        /// Tests the SafeGetFilePath method for cases where there are no errors and the result is null.
        /// </summary>
        [Test]
        [TestCase(null, null, false, false)]
        [TestCase(null, null, false, true)]
        [TestCase(null, null, true, false)]
        [TestCase(null, null, true, true)]

        [TestCase(null, TEST_FILE_NAME, false, false)]
        [TestCase(null, TEST_FILE_NAME, false, true)]
        [TestCase(null, TEST_FILE_NAME, true, false)]
        [TestCase(null, TEST_FILE_NAME, true, true)]

        [TestCase(TEST_DIR_NAME, null, false, false)]
        [TestCase(TEST_DIR_NAME, null, false, true)]
        [TestCase(TEST_DIR_NAME, null, true, false)]
        [TestCase(TEST_DIR_NAME, null, true, true)]

        [TestCase(TEST_DIR_NAME, TEST_FILE_NAME, false, false)]
        [TestCase(TEST_DIR_NAME, TEST_FILE_NAME, false, true)]
        [TestCase(TEST_DIR_NAME, TEST_FILE_NAME, true, false)]
        public void SafeGetFilePathTestForNoErrorNullResult(string folder, string fileName, bool createFolder = false, bool createFile = false)
        {
            testObj = new Filer(MockLogger.Object);
            string result = testObj.SafeGetFilePath(folder, fileName, createFolder, createFile);
            VerifyCreateDirNoErrorNullResult(result, folder);
        }

        /// <summary>
        /// Tests the SafeGetFilePath method for cases where there are no errors and the result is not null.
        /// </summary>
        [Test]
        [TestCase(TEST_DIR_NAME, TEST_FILE_NAME, true, true)]
        public void SafeGetFilePathTestForNoErrorNotNullResult(string folder, string fileName, bool createFolder = false, bool createFile = false)
        {
            testObj = new Filer(MockLogger.Object);
            string result = testObj.SafeGetFilePath(folder, fileName, createFolder, createFile);
            VerifyCreateDirError(folder, 0);
            Assert.IsNotNull(result);
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
            VerifyCreateDirError(TEST_DIR_INVALID, 1);
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
            VerifyCreateDirNoErrorNullResult(result, TEST_DIR_NAME);
        }

        /// <summary>
        /// Tests the SafeCreateDirectory Method 
        /// </summary>
        [Test]
        public void SafeCreateDirectoryTest()
        {
            testObj = new Filer(MockLogger.Object);
            string folder = TEST_DIR_NAME;
            bool result = testObj.SafeCreateDirectory(folder);
            VerifyCreateDirNoErrorTrueResult(result, folder);

            bool deleteResult = testObj.SafeDeleteFolder(folder);
            Assert.IsTrue(deleteResult);
        }

        /// <summary>
        /// Tests the SafeDeleteFolder Method 
        /// For cases where the method should not error and return a result of true.
        /// </summary>
        [Test]
        [TestCase(null, false)]
        [TestCase(null, true)]
        [TestCase(TEST_DIR_DNE, false)]
        [TestCase(TEST_DIR_DNE, true)]
        [TestCase(TEST_DIR_NAME, false, true)]
        [TestCase(TEST_DIR_NAME, true, true)]
        [TestCase(TEST_DIR_NAME, true, true, true)]
        public void SafeDeleteFolderTestNoErrorTrueResult(string folder, bool emptyFolder, bool createFolder = false, bool createFile = false)
        {
            testObj = new Filer(MockLogger.Object);
            if (createFolder)
            {
                bool createDirResult = testObj.SafeCreateDirectory(folder);
                VerifyCreateDirNoErrorTrueResult(createDirResult, folder);
            }
            if (createFile)
            {
                string filePath = Path.Combine(testObj.GetAppPathFolder(folder), TEST_FILE_NAME);
                bool createFileResult = testObj.SafeCreateFile(filePath);
                VerifyCreateFileNoErrorTrueResult(createFileResult, folder);
            }
            bool result = testObj.SafeDeleteFolder(folder, emptyFolder);
            VerifyDeleteDirNoErrorTrueResult(result, folder, emptyFolder);
        }

        /// <summary>
        /// Tests the SafeDeleteFolder Method 
        /// For cases where the method should error and return a result of false.
        /// </summary>
        [Test]
        [TestCase(TEST_DIR_NAME, false, true, true)]
        public void SafeDeleteFolderTestErrorFalseResult(string folder, bool emptyFolder, bool createFolder = false, bool createFile = false)
        {
            testObj = new Filer(MockLogger.Object);
            if (createFolder)
            {
                bool createDirResult = testObj.SafeCreateDirectory(folder);
                VerifyCreateDirNoErrorTrueResult(createDirResult, folder);
            }
            if (createFile)
            {
                string filePath = Path.Combine(testObj.GetAppPathFolder(folder), TEST_FILE_NAME);
                bool createFileResult = testObj.SafeCreateFile(filePath);
                VerifyCreateFileNoErrorTrueResult(createFileResult, folder);
            }
            bool result = testObj.SafeDeleteFolder(folder, emptyFolder);
            VerifyDeleteDirError(folder, emptyFolder, 1);
            Assert.IsFalse(result);

            // Cleanup test
            bool deleteResult = testObj.SafeDeleteFolder(folder, true);
            VerifyDeleteDirNoErrorTrueResult(deleteResult, folder, true);
        }        
    }
}
