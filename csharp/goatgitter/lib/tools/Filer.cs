using System;
using System.IO;
using goatgitter.lib.extensions;
using static goatgitter.lib.Constants;

namespace goatgitter.lib.tools
{
    /** 
     * Utility class Filer gracefully handles File Input and Output Operations.
     * MIT License
     * Copyright (c) 2022 goatgitter
     * */
    public class Filer : BaseWithLog
    {
       
        /// <summary>
        /// Constructor for the Filer Class.
        /// Filers come equiped with a Notepad to record details it might need later.
        /// </summary>
        public Filer(ILogger notepad = null) : base(notepad: notepad)
        {
            
        }

        /// <summary>
        /// Safely gets a Path object for the params specified.
        /// </summary>
        /// <param name="folder">The directory where the file resides.</param>
        /// <param name="fileName">The name of the file to retrieve.</param>
        /// <param name="createFolder">A boolean that specifies if the folder should automatically be created if it does not exist.</param>
        /// <returns></returns>
        public string SafeGetFilePath(string folder, string fileName, bool createFolder = true)
        {
            string result = null;
            if (folder.IsNotEmpty() && fileName.IsNotEmpty())
            {
                if (!Directory.Exists(folder) && createFolder)
                {
                    try
                    {
                        Directory.CreateDirectory(folder);
                    }
                    catch (Exception exception)
                    {
                        Notepad.LogExceptionWithData(ERR_CREATE_DIR, new object[] { folder }, exception);
                    }
                }
                if (File.Exists(fileName))
                {
                    result = Path.Combine(folder, fileName);
                }                
            }
            return result;
        }

        /// <summary>
        /// Safely gets a Path object for the params specified.
        /// </summary>
        /// <param name="folder">The directory where the file resides.</param>
        /// <param name="emptyFolder">A boolean that specifies if the folder should automatically be emptied.</param>
        /// <returns></returns>
        public bool SafeDeleteFolder(string folder, bool emptyFolder = true)
        {
            bool result = false;
            if (folder.IsNotEmpty())
            {
                if (Directory.Exists(folder))
                {
                    try
                    {
                        Directory.Delete(folder, emptyFolder);
                        result = true;
                    }
                    catch (Exception exception)
                    {
                        Notepad.LogExceptionWithData(ERR_DELETE_DIR, new object[] { folder, emptyFolder }, exception);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Opens a File for writing information.
        /// </summary>
        /// <param name="folder">The directory where the file resides.</param>
        /// <param name="fileName">The name of the file to retrieve for update.</param>
        /// <param name="createFolder">A boolean that specifies if the folder should automatically be created if it does not exist.</param>

        /// <returns>bool result</returns>
        public bool RetrieveFileForUpdate(string folder, string fileName, bool createFolder)
        {
            bool result = false;
            string filePath = SafeGetFilePath(folder, fileName, createFolder);        
            return result;
        }
    }
}
