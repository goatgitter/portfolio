using goatgitter.lib.tools;
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

        private void VerifyRetrieveFileError(string folder, string fileName, int numTimes)
        {
            MockLogger.Verify(m => m.LogExceptionWithData(It.Is<string>(s => s.Equals(ERR_GET_FILE)),
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

        private void SetupFolder(string folder, bool createFolder, int createDirErrors = 0, bool expectedResult = true)
        {
            if (createFolder)
            {
                bool createDirResult = testObj.SafeCreateDirectory(folder);
                VerifyCreateDirError(folder, createDirErrors);
                Assert.AreEqual(expectedResult, createDirResult);
            }
        }

        private void SetupFiles(string folder, string fileName, bool createFolder, bool createFile)
        {
            SetupFolder(folder, createFolder);
            if (createFile)
            {
                string filePath = GetTestFilePath(folder, TEST_FILE_NAME);
                bool createFileResult = testObj.SafeCreateFile(filePath);
                VerifyCreateFileNoErrorTrueResult(createFileResult, folder);
            }
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

        /// <summary>
        /// Tests the SafeGetFilePath method
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
        [TestCase(TEST_DIR_NAME, TEST_FILE_NAME, true, true, false, 0)]
        [TestCase(TEST_DIR_INVALID, TEST_FILE_NAME, true, true, true, 1)]
        [TestCase(TEST_DIR_INVALID, TEST_FILE_NAME, true, false, true, 1)]
        public void SafeGetFilePathTest(string folder, string fileName, bool createFolder = false, bool createFile = false, 
            bool isResultEmpty = true, int createDirErrors = 0, int getPathErrors = 0)
        {
            testObj = new Filer(MockLogger.Object);
            string result = testObj.SafeGetFilePath(folder, fileName, createFolder, createFile);
            if (createFolder)
            {
                VerifyCreateDirError(folder, createDirErrors);
            }
            
            VerifySafeGetFilePathError(folder, fileName, getPathErrors);
            Assert.AreEqual(isResultEmpty, result.IsEmpty());
            bool deleteResult = testObj.SafeDeleteFolder(folder);
            Assert.IsTrue(deleteResult);
        }

        /// <summary>
        /// Tests the SafeCreateDirectory Method
        /// </summary>
        [Test]
        [TestCase(null, false, false, 0)]
        [TestCase(TEST_DIR_NAME, false, true, 0)]
        [TestCase(TEST_DIR_INVALID, false, false, 1)]
        public void SafeCreateDirectoryTest(string folder, bool createFolder = false,
            bool expectedResult = true, int numErrors = 0)
        {
            testObj = new Filer(MockLogger.Object);
            bool result = testObj.SafeCreateDirectory(folder);
            VerifyCreateDirError(folder, numErrors);
            Assert.AreEqual(expectedResult, result);

            bool deleteResult = testObj.SafeDeleteFolder(folder);
            Assert.IsTrue(deleteResult);
        }

        /// <summary>
        /// Tests the SafeCreateFile Method
        /// </summary>
        [Test]
        [TestCase(TEST_DIR_NAME, TEST_FILE_NAME, true, true, 0, true, 0)]
        [TestCase(TEST_DIR_NAME, TEST_FILE_NAME, false, true, 0, false, 1)]
        [TestCase(null, null, true, false, 0, false, 0)]
        [TestCase(null, TEST_FILE_NAME, true, false, 0, false, 0)]
        [TestCase(TEST_DIR_NAME, null, true, true, 0, false, 0)]
        [TestCase(TEST_DIR_NAME, TEST_FILE_INVALID, true, true, 0, false, 0)]
        [TestCase(TEST_DIR_INVALID, TEST_FILE_NAME, true, false, 1, false, 0)]
        public void SafeCreateFileTest(string folder, string fileName, bool createFolder = true,
             bool expectedCreateDirResult = true, int createDirErrors = 0, 
             bool expectedCreateFileResult = true, int createFileErrors = 0)
        {
            testObj = new Filer(MockLogger.Object);
            SetupFolder(folder, createFolder, createDirErrors, expectedCreateDirResult);

            string filePath = GetTestFilePath(folder, fileName);
            bool createFileResult = testObj.SafeCreateFile(filePath);
            VerifyCreateFileError(filePath, createFileErrors);
            Assert.AreEqual(expectedCreateFileResult, createFileResult);       

            bool deleteResult = testObj.SafeDeleteFolder(folder);
            Assert.IsTrue(deleteResult);
        }

        /// <summary>
        /// Tests the SafeDeleteFolder Method 
        /// </summary>
        [Test]
        [TestCase(null, false)]
        [TestCase(null, true)]
        [TestCase(TEST_DIR_DNE, false)]
        [TestCase(TEST_DIR_DNE, true)]
        [TestCase(TEST_DIR_NAME, false, true)]
        [TestCase(TEST_DIR_NAME, true, true)]
        [TestCase(TEST_DIR_NAME, true, true, true)]
        [TestCase(TEST_DIR_NAME, false, true, true, false, 1)]
        public void SafeDeleteFolderTest(string folder, bool emptyFolder, 
            bool createFolder = false, bool createFile = false, bool expectedResult = true, int numErrors = 0)
        {
            testObj = new Filer(MockLogger.Object);
            SetupFiles(folder, TEST_FILE_NAME, createFolder, createFile);
            bool result = testObj.SafeDeleteFolder(folder, emptyFolder);
            VerifyDeleteDirError(folder, emptyFolder, numErrors);
            Assert.AreEqual(expectedResult, result);
        }

        /// <summary>
        /// Tests the method for retrieving a file.
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="fileName"></param>
        /// <param name="isResultEmpty"></param>
        /// <param name="createFolder"></param>
        /// <param name="createFile"></param>
        /// <param name="numErrors"></param>
        [Test]
        [TestCase(TEST_DIR_NAME, TEST_FILE_NAME, true)]
        [TestCase(null, null)]
        [TestCase(null, TEST_FILE_NAME)]
        [TestCase(TEST_DIR_NAME, null)]
        [TestCase(TEST_DIR_NAME, TEST_FILE_INVALID)]
        [TestCase(TEST_DIR_INVALID, TEST_FILE_NAME)]
        [TestCase(TEST_DIR_NAME, TEST_FILE_NAME, false, true, true)]
        public void RetrieveFileTest(string folder, string fileName, bool isResultEmpty = true, bool createFolder = false, 
            bool createFile = false, int numErrors = 0)
        {
            testObj = new Filer(MockLogger.Object);
            SetupFiles(folder, TEST_FILE_NAME, createFolder, createFile);
            FileStream fs = testObj.RetrieveFile(folder, fileName);
            VerifyRetrieveFileError(folder, fileName, numErrors);
            Assert.AreEqual(isResultEmpty, fs.IsEmpty());
            if (fs.IsNotEmpty())
            {
                fs.Close();
            }
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
        /// <param name="numErrors"></param>
        [Test]
        [TestCase(TEST_DIR_NAME, TEST_FILE_NAME, false)]
        [TestCase(null, null)]
        [TestCase(null, TEST_FILE_NAME)]
        [TestCase(TEST_DIR_NAME, null)]
        [TestCase(TEST_DIR_NAME, TEST_FILE_INVALID)]
        [TestCase(TEST_DIR_INVALID, TEST_FILE_NAME)]
        public void RetrieveFileForUpdateNoErrorsTest(string folder, string fileName, bool isResultEmpty = true, int numErrors = 0)
        {
            testObj = new Filer(MockLogger.Object);
            FileStream fs = testObj.RetrieveFileForUpdate(folder, fileName);
            VerifyRetrieveFileForUpdateError(folder, fileName, numErrors);
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
