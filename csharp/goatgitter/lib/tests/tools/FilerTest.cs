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

        /// <summary>
        /// Tests the Constructor
        /// </summary>
        [Test]
        public void SafeGetFilePathTest()
        {
            testObj = new Filer();
            // Null Folder, null File, Do NOT Create folder
            string result = testObj.SafeGetFilePath(null, null, false);
            Assert.IsNull(result);

            // Null Folder, null File, Do Create folder
            result = testObj.SafeGetFilePath(null, null, true);
            Assert.IsNull(result);

            // Null Folder, NOT null File, Do NOT Create folder
            result = testObj.SafeGetFilePath(null, TEST_FILE_NAME, false);
            Assert.IsNull(result);

            // Null Folder, NOT null File, Do Create folder
            result = testObj.SafeGetFilePath(null, TEST_FILE_NAME, true);
            Assert.IsNull(result);

            // NOT Null Folder, null File, Do NOT Create folder
            result = testObj.SafeGetFilePath(TEST_DIR_NAME, null, false);
            Assert.IsNull(result);

            // NOT Null Folder, null File, Do Create folder
            result = testObj.SafeGetFilePath(TEST_DIR_NAME, null, true);
            Assert.IsNull(result);

            // NOT Null Folder, NOT null File, Do NOT Create folder
            result = testObj.SafeGetFilePath(TEST_DIR_NAME, TEST_FILE_NAME, false);
            Assert.IsNull(result);

            // NOT Null Folder, NOT null File, Do Create folder
            result = testObj.SafeGetFilePath(TEST_DIR_NAME, TEST_FILE_NAME, true);
            Assert.IsNull(result);

            bool deleteResult = testObj.SafeDeleteFolder(TEST_DIR_NAME);
            Assert.IsTrue(deleteResult);
        }

    }
}
