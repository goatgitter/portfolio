using goatgitter.lib.tools;
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

        /// <summary>
        /// Tests the Constructor
        /// </summary>
        [Test]
        public void SafeGetFilePathTest()
        {
            testObj = new Filer(MockLogger.Object);
            string folder = null;
            string fileName = null;

            // Null Folder, null File, Do NOT Create folder, Do NOT Create File
            string result = testObj.SafeGetFilePath(folder, fileName, false, false);
            VerifyCreateDirNoErrorNullResult(result, folder);

            // Null Folder, null File, Do NOT Create folder, Do Create File
            result = testObj.SafeGetFilePath(folder, fileName, false, true);
            VerifyCreateDirNoErrorNullResult(result, folder);

            // Null Folder, null File, Do Create folder, Do NOT Create File
            result = testObj.SafeGetFilePath(folder, fileName, true, false);
            VerifyCreateDirNoErrorNullResult(result, folder);

            // Null Folder, null File, Do Create folder, Do Create File
            result = testObj.SafeGetFilePath(folder, fileName, true, true);
            VerifyCreateDirNoErrorNullResult(result, folder);

            // Null Folder, NOT null File, Do NOT Create folder, Do NOT Create File
            fileName = TEST_FILE_NAME;
            result = testObj.SafeGetFilePath(folder, fileName, false, false);
            VerifyCreateDirNoErrorNullResult(result, folder);

            // Null Folder, NOT null File, Do NOT Create folder, Do Create File
            result = testObj.SafeGetFilePath(folder, fileName, false, true);
            VerifyCreateDirNoErrorNullResult(result, folder);

            // Null Folder, NOT null File, Do Create folder, Do NOT Create File
            result = testObj.SafeGetFilePath(folder, fileName, true, false);
            VerifyCreateDirNoErrorNullResult(result, folder);

            // Null Folder, NOT null File, Do Create folder, Do Create File
            result = testObj.SafeGetFilePath(folder, fileName, true, true);
            VerifyCreateDirNoErrorNullResult(result, folder);

            // NOT Null Folder, null File, Do NOT Create folder, Do NOT Create File
            folder = TEST_DIR_NAME;
            fileName = null;
            result = testObj.SafeGetFilePath(folder, fileName, false, false);
            VerifyCreateDirNoErrorNullResult(result, folder);

            // NOT Null Folder, null File, Do NOT Create folder, Do Create File
            result = testObj.SafeGetFilePath(folder, fileName, false, true);
            VerifyCreateDirNoErrorNullResult(result, folder);

            // NOT Null Folder, null File, Do Create folder, Do NOT Create File
            result = testObj.SafeGetFilePath(folder, fileName, true, false);
            VerifyCreateDirNoErrorNullResult(result, folder);

            // NOT Null Folder, null File, Do Create folder, Do Create File
            result = testObj.SafeGetFilePath(folder, fileName, true, true);
            VerifyCreateDirNoErrorNullResult(result, folder);

            // NOT Null Folder, NOT null File, Do NOT Create folder, Do NOT Create File
            fileName = TEST_FILE_NAME;
            result = testObj.SafeGetFilePath(folder, fileName, false, false);
            VerifyCreateDirNoErrorNullResult(result, folder);

            // NOT Null Folder, NOT null File, Do Create folder, Do NOT Create File
            result = testObj.SafeGetFilePath(folder, fileName, true, false);
            VerifyCreateDirNoErrorNullResult(result, folder);

            // NOT Null Folder, NOT null File, Do NOT Create folder, Do Create File
            result = testObj.SafeGetFilePath(folder, fileName, false, true);
            VerifyCreateDirNoErrorNullResult(result, folder);

            // NOT Null Folder, NOT null File, Do Create folder, Do Create File
            result = testObj.SafeGetFilePath(folder, fileName, true, true);
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
        /// Tests the SafeDeleteDirectory Method 
        /// </summary>
        [Test]
        public void SafeDeleteFolderTest()
        {
            testObj = new Filer(MockLogger.Object);
            string folder = null;
            bool emptyFolder = false;

            // Null Folder, Do NOT empty folder
            bool result = testObj.SafeDeleteFolder(folder, emptyFolder);
            VerifyDeleteDirNoErrorTrueResult(result, folder, emptyFolder);

            // Null Folder, Do empty folder
            emptyFolder = true;
            result = testObj.SafeDeleteFolder(folder, emptyFolder);
            VerifyDeleteDirNoErrorTrueResult(result, folder, emptyFolder);

            // Folder does NOT exist, Do NOT empty folder
            folder = TEST_DIR_DNE;
            emptyFolder = false;
            result = testObj.SafeDeleteFolder(folder, emptyFolder);
            VerifyDeleteDirNoErrorTrueResult(result, folder, emptyFolder);

            // Folder does NOT exist, Do empty folder
            emptyFolder = true;
            result = testObj.SafeDeleteFolder(folder, emptyFolder);
            VerifyDeleteDirNoErrorTrueResult(result, folder, emptyFolder);

            // Folder does exist, and is empty, Do NOT empty folder
            folder = TEST_DIR_NAME;
            bool createDirResult = testObj.SafeCreateDirectory(folder);
            VerifyCreateDirNoErrorTrueResult(createDirResult, folder);
            emptyFolder = false;

            result = testObj.SafeDeleteFolder(folder, emptyFolder);
            VerifyDeleteDirNoErrorTrueResult(result, folder, emptyFolder);

            // Folder does exist, and is empty, Do empty folder
            createDirResult = testObj.SafeCreateDirectory(folder);
            VerifyCreateDirNoErrorTrueResult(createDirResult, folder);
            emptyFolder = true;

            result = testObj.SafeDeleteFolder(folder, emptyFolder);
            VerifyDeleteDirNoErrorTrueResult(result, folder, emptyFolder);

            // Folder does exist, and is NOT empty, Do NOT empty folder
            createDirResult = testObj.SafeCreateDirectory(folder);
            VerifyCreateDirNoErrorTrueResult(createDirResult, folder);
            emptyFolder = false;
            result = testObj.SafeDeleteFolder(folder, emptyFolder);
            VerifyDeleteDirNoErrorTrueResult(result, folder, emptyFolder);

            // Folder does exist, and is NOT empty, Do empty folder
            createDirResult = testObj.SafeCreateDirectory(folder);
            VerifyCreateDirNoErrorTrueResult(createDirResult, folder);
            emptyFolder = true;
            result = testObj.SafeDeleteFolder(folder, emptyFolder);
            VerifyDeleteDirNoErrorTrueResult(result, folder, emptyFolder);

        }

    }
}
