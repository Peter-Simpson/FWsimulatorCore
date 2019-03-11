using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;

namespace ASCOMCore
{
    /// <summary>
    /// Write a log file in a standard location in the standard ASCOM format
    /// </summary>
    /// <remarks>
    /// By default log files will be created in a standard location with templated file names where yyyy, dd, hh, mm ss and xxx change according to the run time year month day hour minute second and second fraction
    ///   Windows -     Location: Documents\ASCOM\Logs yyyy-mm-dd   File name format: ASCOM.LogFileName.hhmm.ssxxx.txt
    ///   Linux & OSX - Location: /var/log/ascom/logsyyyy-mm-dd     File name format: ascom.logfilename.hhmm.ssxxx.txt
    ///   
    /// Each line has the format: FUNCTION MESSAGE where FUNCTION is formatted to a specified fixed width, ensuring that the MESSAGE elements on all lines start in the same place 
    /// 
    /// </remarks>
    public class TraceLogger
    {
        const int FUNCTION_WIDTH = 25; // Default width of the Function part of the line

        private StreamWriter logStreamWriter; // Log file StreamWriter
        string logName = ""; // Name of the log as supplied by the user

        /// <summary>
        /// Enum of possible platforms
        /// </summary>
        private enum Platform
        {
            Windows,
            Linux,
            OSX,
            Unknown
        }

        #region Initialiser

        /// <summary>
        /// Class initialiser
        /// </summary>
        /// <param name="LogName">Name of the log file</param>
        public TraceLogger(string LogName)
        {
            logName = LogName; // Save the log name
            Enabled = false; // Initialise the log in the disabled state 
        }

        #endregion

        #region Public interface

        /// <summary>
        /// File path to which the log file should be written
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Enable or disable the trace logger
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Add a message to the log file
        /// </summary>
        /// <param name="Function">Name of the unit generating the message</param>
        /// <param name="Message">Log message (can contain {X} placeholders for parameters)</param>
        /// <param name="Parameters">Optional comma separated list of parameters to be placed at the placeholder markers in the Message text.</param>
        public void LogMessage(string Function, string Message, params object[] Parameters)
        {
            if (logStreamWriter == null) CreateLogFile();
            LogMsgFormatter(Function, Message, Parameters);
        }

        #endregion

        #region Support code
        /// <summary>
        /// Format a message and write it to the log file
        /// </summary>
        /// <param name="Function">Name of the unit generating the log message</param>
        /// <param name="Message">Log message (can contain {X} placeholders for parameters)</param>
        /// <param name="Parameters">Optional comma separated list of parameters to be placed at the placeholder markers in the Message text.</param>
        private void LogMsgFormatter(string Function, string Message, params object[] Variables)
        {
            string message = string.Format("{0} {1} {2}", DateTime.Now.ToString("HH:mm:ss.fff"), (Function + new string(' ', FUNCTION_WIDTH)).Substring(0, FUNCTION_WIDTH), string.Format(Message, Variables));
            if (!(logStreamWriter == null))
            {
                logStreamWriter.WriteLine(message); // Update log file including a newline terminator
                logStreamWriter.Flush();
            }
        }

        /// <summary>
        /// Create the log file using the correct format for the current OS Platform
        /// </summary>
        private void CreateLogFile()
        {
            const string TRACE_LOGGER_FILE_NAME_DATE_FORMAT = "yyyy-MM-dd"; // Common format for file name
            string filePath;

            if (string.IsNullOrEmpty(FilePath)) // User has not supplied their own path so use the default path
            {
                // Create the base path for the log file, which depends on the OS Platform
                switch (CurrentPlatform())
                {
                    case Platform.Windows:
                        {
                            FilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\ASCOM\Logs ";
                        }
                        break;

                    case Platform.Linux:
                        {
                            FilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                        }
                        break;

                    case Platform.OSX:
                        {
                            FilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                        }
                        break;

                    default: // Anything else is unknown and unsupported
                        {
                            throw new Exception("Unknown OSPlatform - cannot automatically determine correct file path.");
                        }
                }
                filePath = FilePath + Path.DirectorySeparatorChar.ToString() + DateTime.Now.ToString(TRACE_LOGGER_FILE_NAME_DATE_FORMAT); // Append the current date time string to form the full ASCOM log file path
            }
            else // User has supplied their own path so use that
            {
                filePath = FilePath;
            }
            Directory.CreateDirectory(filePath); //Create the directory if it doesn't exist (otherwise the stream writer creation below will fail!))

            // Create the log file name using the correct directory separator (\ or /) in mixed case for Windows or lower case for all other OSs
            string fullFileName = string.Format(@"{0}{1}ASCOM.{2}.{3}.txt", filePath, Path.DirectorySeparatorChar.ToString(), logName, DateTime.Now.ToString("HHmm.ssfff"));
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) fullFileName = fullFileName.ToLower();

            // Create the log file stream writer
            logStreamWriter = new StreamWriter(fullFileName);
            logStreamWriter.AutoFlush = true;
            logStreamWriter.WriteLine(string.Format(@"File path: {0}, Directory separator: {1}, Log name: {2}, DateTime: {3}", filePath, Path.DirectorySeparatorChar.ToString(), logName, DateTime.Now.ToString("HHmm.ssfff")));
            logStreamWriter.Flush();
        }

        /// <summary>
        /// Return a flag showing the current OS Platform
        /// </summary>
        /// <returns>Platform enum value</returns>
        private Platform CurrentPlatform()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return Platform.Windows;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return Platform.Linux;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) return Platform.OSX;
            return Platform.Unknown; // If it's not recognised, indicate this
        }

        #endregion
    }
}
