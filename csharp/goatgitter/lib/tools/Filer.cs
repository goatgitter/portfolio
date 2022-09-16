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
            Notepad.Started();
        }

        public string GetAppPathFolder(string folder)
        {
            string dirPath = null;
            if (folder.IsNotEmpty())
            {
                dirPath = Path.Combine(AppPath, folder);
            }                
            return dirPath;
        }

        /// <summary>
        /// Safely gets a Path object for the params specified.
        /// </summary>
        /// <param name="folder">The directory where the file resides.</param>
        /// <param name="fileName">The name of the file to retrieve.</param>
        /// <param name="createFolder">A boolean that specifies if the folder should automatically be created if it does not exist.</param>
        /// <param name="createFile">A boolean that specifies if the file should automatically be created if it does not exist.</param>
        /// <returns>A String for the Combined File Path.</returns>
        public string SafeGetFilePath(string folder, string fileName, bool createFolder = true, bool createFile = true)
        {
            string result = null;
            if (folder.IsNotEmpty() && fileName.IsNotEmpty())
            {
                string dirPath = GetAppPathFolder(folder);
                bool dirExists = Directory.Exists(dirPath);
                if (!dirExists && createFolder)
                {
                    bool dirCreated = SafeCreateDirectory(dirPath);
                    dirExists = dirCreated;
                }
                try
                {
                    string filePath = Path.Combine(dirPath, fileName);
                    bool fileExists = File.Exists(filePath);
                    if (fileExists)
                    {
                        result = filePath;
                    }
                    else if (!fileExists && dirExists && createFile)
                    {
                        bool fileCreated = SafeCreateFile(filePath);
                        result = fileCreated ? filePath : null;
                    }               
                }
                catch (Exception exception)
                {
                    Notepad.LogExceptionWithData(ERR_GET_PATH, new object[] { folder, fileName }, exception);
                }
            }
            return result;
        }

        /// <summary>
        /// Safely creates a Directory.
        /// </summary>
        /// <param name="folder">The directory to be created.</param>
        /// <returns></returns>
        public bool SafeCreateDirectory(string folder)
        {
            bool result = false;
            if (folder.IsNotEmpty())
            {
                string dirPath = GetAppPathFolder(folder);
                if (!Directory.Exists(dirPath))
                {
                    try
                    {
                        Directory.CreateDirectory(dirPath);
                        result = true;
                    }
                    catch (Exception exception)
                    {
                        Notepad.LogExceptionWithData(ERR_CREATE_DIR, new object[] { dirPath }, exception);
                    }
                }
                else
                {
                    // Directory already exists.
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Safely creates a file.
        /// </summary>
        /// <param name="filePath">The file path where the file will be created.</param>
        /// <returns></returns>
        public bool SafeCreateFile(string filePath)
        {
            bool result = false;
            if (filePath.IsValidFileName() && filePath.IsValidDirName())
            {
                if (!File.Exists(filePath))
                {
                    try
                    {
                        FileStream fs = File.Create(filePath);
                        fs.Close();
                        result = true;
                    }
                    catch (Exception exception)
                    {
                        Notepad.LogExceptionWithData(ERR_CREATE_FILE, new object[] { filePath }, exception);
                    }
                }
                else
                {
                    // File already exists.
                    result = true;
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
                string dirPath = GetAppPathFolder(folder);
                if (Directory.Exists(dirPath))
                {
                    try
                    {
                        Directory.Delete(dirPath, emptyFolder);
                        result = true;
                    }
                    catch (Exception exception)
                    {
                        Notepad.LogExceptionWithData(ERR_DELETE_DIR, new object[] { dirPath, emptyFolder }, exception);
                    }
                }
                else
                {
                    // No Directory to delete
                    result = true;
                }
            }
            else
            {
                // No Directory to delete
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Creates an empty a File.
        /// </summary>
        /// <param name="folder">The directory where the file resides.</param>
        /// <param name="fileName">The name of the file to create for write.</param>
        
        /// <returns>bool result</returns>
        public bool CreateFileForWrite(string folder, string fileName)
        {
            bool result = false;
            string filePath = SafeGetFilePath(folder, fileName, true, true);
            result = filePath.IsNotEmpty();
            return result;
        }

        /// <summary>
        /// Opens a File for writing information.
        /// </summary>
        /// <param name="folder">The directory where the file resides.</param>
        /// <param name="fileName">The name of the file to retrieve for update.</param>
        /// <returns>bool result</returns>
        public bool RetrieveFileForUpdate(string folder, string fileName)
        {
            bool result = false;
            string filePath = SafeGetFilePath(folder, fileName, true, true);
            result = filePath.IsNotEmpty();
            return result;
        }
    }
}
