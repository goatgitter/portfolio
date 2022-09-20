﻿using goatgitter.lib.tools;
using NUnit.Framework;
using System;
using System.Linq;
using Moq;
using static goatgitter.lib.Constants;
using static goatgitter.lib.tests.TestConstants;
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

        private string GetTestFilePath(string folder, string fileName)
        {
            string filePath = null;
            if (folder.IsNotEmpty() && fileName.IsNotEmpty())
            {
                filePath = Path.Combine(testObj.GetAppPathFolder(folder), fileName);
            }
            return filePath;
        }

        private void VerifyCreateDirError(string folder, int numTimes)
        {
            String dirPath = testObj.GetAppPathFolder(folder);
            MockLogger.Verify(m => m.LogExceptionWithData(It.Is<string>(s => s.Equals(ERR_CREATE_DIR)),
                It.Is<object[]>(o => o.Contains<object>(dirPath)),
                It.IsAny<Exception>()), Times.Exactly(numTimes));
        }

        private void VerifyCreateFileError(string filePath, int numTimes)
        {
            MockLogger.Verify(m => m.LogExceptionWithData(It.Is<string>(s => s.Equals(ERR_CREATE_FILE)),
                It.Is<object[]>(o => o.Contains<object>(filePath)),
                It.IsAny<Exception>()), Times.Exactly(numTimes));
        }

        private void VerifySafeGetFilePathError(string folder, string fileName, int numTimes)
        {
            String dirPath = testObj.GetAppPathFolder(folder);
            MockLogger.Verify(m => m.LogExceptionWithData(
                It.Is<string>(s => s.Equals(ERR_GET_PATH)),
                It.Is<object[]>(o => o.Contains<object>(dirPath) && o.Contains<object>(fileName)),
                It.IsAny<Exception>()), Times.Exactly(numTimes));
        }

        private void VerifyRetrieveFileForUpdateError(string folder, string fileName, int numTimes)
        {
            MockLogger.Verify(m => m.LogExceptionWithData(It.Is<string>(s => s.Equals(ERR_GET_FILE_FOR_UPDATE)),
                It.Is<object[]>(o => o.Contains<object>(folder) && o.Contains<object>(fileName)),
                It.IsAny<Exception>()), Times.Exactly(numTimes));
        }

        private void VerifyDeleteDirError(string folder, bool emptyFolder, int numTimes)
        {
            String dirPath = testObj.GetAppPathFolder(folder);
            MockLogger.Verify(m => m.LogExceptionWithData(It.Is<string>(s => s.Equals(ERR_DELETE_DIR)),
                It.Is<object[]>(o => o.Contains<object>(dirPath) && o.Contains<object>(emptyFolder)),
                It.IsAny<Exception>()), Times.Exactly(numTimes));
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

        private void VerifyCreateFileNoErrorTrueResult(bool result, string filePath)
        {
            VerifyCreateFileError(filePath, 0);
            Assert.IsTrue(result);
        }

        private void VerifyCreateFileNoErrorFalseResult(bool result, string filePath)
        {
            VerifyCreateFileError(filePath, 0);
            Assert.IsFalse(result);
        }

        /// <summary>
        /// Tests the SafeGetFilePath method for cases where:
        ///     There are no errors and the result is null.
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

        [TestCase(TEST_DIR_NAME, TEST_FILE_INVALID, false, false)]
        [TestCase(TEST_DIR_NAME, TEST_FILE_INVALID, false, true)]
        [TestCase(TEST_DIR_NAME, TEST_FILE_INVALID, true, false)]
        [TestCase(TEST_DIR_NAME, TEST_FILE_INVALID, true, true)]

        [TestCase(TEST_DIR_INVALID, TEST_FILE_NAME, false, false)]
        [TestCase(TEST_DIR_INVALID, TEST_FILE_NAME, false, true)]
        public void SafeGetFilePathTestForNoErrorNullResult(string folder, string fileName, bool createFolder = false, bool createFile = false)
        {
            testObj = new Filer(MockLogger.Object);
            string result = testObj.SafeGetFilePath(folder, fileName, createFolder, createFile);
            VerifySafeGetFilePathError(folder, fileName, 0);
            Assert.IsNull(result);
            bool deleteResult = testObj.SafeDeleteFolder(folder);
            Assert.IsTrue(deleteResult);
        }

        /// <summary>
        /// Tests the SafeGetFilePath method for cases where:
        ///     There are no errors and the result is NOT null.
        /// </summary>
        [Test]
        [TestCase(TEST_DIR_NAME, TEST_FILE_NAME, true, true)]
        public void SafeGetFilePathTestForNoErrorNotNullResult(string folder, string fileName, bool createFolder = false, bool createFile = false)
        {
            testObj = new Filer(MockLogger.Object);
            string result = testObj.SafeGetFilePath(folder, fileName, createFolder, createFile);

            VerifySafeGetFilePathError(folder, fileName, 0);
            Assert.IsNotNull(result);
            bool deleteResult = testObj.SafeDeleteFolder(folder);
            Assert.IsTrue(deleteResult);
        }


        /// <summary>
        /// Tests the SafeGetFilePath Method method for cases where:
        /// There is one error and the result is null.
        /// </summary>
        [Test]
        [TestCase(TEST_DIR_INVALID, TEST_FILE_NAME, true, true)]
        [TestCase(TEST_DIR_INVALID, TEST_FILE_NAME, true, false)]
        public void SafeGetFilePathForInvalid(string folder, string fileName, bool createFolder = false, bool createFile = false)
        {
            testObj = new Filer(MockLogger.Object);
            string result = testObj.SafeGetFilePath(folder, fileName, createFolder, createFile);
            VerifyCreateDirError(folder, 1);
            Assert.IsNull(result);
        }

        /// <summary>
        /// Tests the SafeCreateDirectory Method for cases where:
        /// No Errors, and result is true
        /// </summary>
        [Test]
        [TestCase(TEST_DIR_NAME)]
        public void SafeCreateDirectoryNoErrorsResultTrueTest(string folder)
        {
            testObj = new Filer(MockLogger.Object);
            bool result = testObj.SafeCreateDirectory(folder);
            VerifyCreateDirNoErrorTrueResult(result, folder);

            bool deleteResult = testObj.SafeDeleteFolder(folder);
            Assert.IsTrue(deleteResult);
        }

        /// <summary>
        /// Tests the SafeCreateFile Method for cases where:
        /// No Errors, and result is true
        /// </summary>
        [Test]
        [TestCase(TEST_DIR_NAME, TEST_FILE_NAME)]
        public void SafeCreateFileNoErrorsResultTrueTest(string folder, string fileName)
        {
            testObj = new Filer(MockLogger.Object);
            bool result = testObj.SafeCreateDirectory(folder);
            VerifyCreateDirNoErrorTrueResult(result, folder);

            string filePath = GetTestFilePath(folder, fileName);
            bool createFileResult = testObj.SafeCreateFile(filePath);
            VerifyCreateFileNoErrorTrueResult(createFileResult, filePath);

            bool deleteResult = testObj.SafeDeleteFolder(folder);
            Assert.IsTrue(deleteResult);
        }

        /// <summary>
        /// Tests the SafeCreateDirectory Method for cases where:
        /// No Errors, and result is False
        /// </summary>
        [Test]
        [TestCase(null)]

        public void SafeCreateDirectoryNoErrorsResultFalseTest(string folder)
        {
            testObj = new Filer(MockLogger.Object);
            bool result = testObj.SafeCreateDirectory(folder);
            VerifyCreateDirError(folder, 0);
            Assert.IsFalse(result);

            bool deleteResult = testObj.SafeDeleteFolder(folder);
            Assert.IsTrue(deleteResult);
        }

        /// <summary>
        /// Tests the SafeCreateFile Method for cases where:
        /// No Errors, and result is False
        /// </summary>
        [Test]
        [TestCase(null, null)]
        [TestCase(null, TEST_FILE_NAME)]
        [TestCase(TEST_DIR_NAME, null)]
        [TestCase(TEST_DIR_NAME, TEST_FILE_INVALID)]
        [TestCase(TEST_DIR_INVALID, TEST_FILE_NAME)]

        public void SafeCreateFileNoErrorsResultFalseTest(string folder, string fileName)
        {
            testObj = new Filer(MockLogger.Object);
            string filePath = GetTestFilePath(folder, fileName);
            bool createFileResult = testObj.SafeCreateFile(filePath);
            VerifyCreateFileNoErrorFalseResult(createFileResult, filePath);

            bool deleteResult = testObj.SafeDeleteFolder(folder);
            Assert.IsTrue(deleteResult);
        }

        /// <summary>
        /// Tests the SafeCreateDirectory Method for cases where:
        /// One Error, and result is false
        /// </summary>
        [Test]
        [TestCase(TEST_DIR_INVALID)]
        public void SafeCreateDirectoryForErrorTest(string folder)
        {
            testObj = new Filer(MockLogger.Object);
            bool result = testObj.SafeCreateDirectory(folder);
            VerifyCreateDirError(folder, 1);
            Assert.IsFalse(result);

            bool deleteResult = testObj.SafeDeleteFolder(folder);
            Assert.IsTrue(deleteResult);
        }

        /// <summary>
        /// Tests the SafeCreateFile Method for cases where:
        /// One Error, and result is false
        /// </summary>
        [Test]
        [TestCase(TEST_DIR_NAME, TEST_FILE_NAME)]
        public void SafeCreateFileForErrorTest(string folder, string fileName)
        {
            testObj = new Filer(MockLogger.Object);
            string filePath = GetTestFilePath(folder, fileName);
            bool createFileResult = testObj.SafeCreateFile(filePath);
            VerifyCreateFileError(filePath, 1);
            Assert.IsFalse(createFileResult);

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
                string filePath = GetTestFilePath(folder, TEST_FILE_NAME);
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
                string filePath = GetTestFilePath(folder, TEST_FILE_NAME);
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

        /// <summary>
        /// Tests the method for retrieving a file for update.
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="fileName"></param>
        /// <param name="isResultEmpty"></param>
        [Test]
        [TestCase(TEST_DIR_NAME, TEST_FILE_NAME, false)]
        [TestCase(null, null)]
        [TestCase(null, TEST_FILE_NAME)]
        [TestCase(TEST_DIR_NAME, null)]
        [TestCase(TEST_DIR_NAME, TEST_FILE_INVALID)]
        [TestCase(TEST_DIR_INVALID, TEST_FILE_NAME)]
        public void RetrieveFileForUpdateNoErrorsResultNotEmptyTest(string folder, string fileName, bool isResultEmpty = true)
        {
            testObj = new Filer(MockLogger.Object);
            FileStream fs = testObj.RetrieveFileForUpdate(folder, fileName);
            VerifyRetrieveFileForUpdateError(folder, fileName, 0);
            Assert.AreEqual(isResultEmpty, fs.IsEmpty());
            if (fs.IsNotEmpty())
            {
                fs.Close();
            }
            // Cleanup test
            bool deleteResult = testObj.SafeDeleteFolder(folder, true);
            VerifyDeleteDirNoErrorTrueResult(deleteResult, folder, true);
        }

        
    }
}
