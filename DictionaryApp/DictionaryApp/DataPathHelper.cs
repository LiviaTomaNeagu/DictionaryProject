using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApp
{
    internal class DataPathHelper
    {
        public static string GetDataFilePath(string fileName)
        {
            // Get the full path of the currently executing assembly (including the exe filename)
            string exePath = Assembly.GetExecutingAssembly().Location;

            // Get the directory of the executable
            string exeDirectory = Path.GetDirectoryName(exePath);

            // Construct the path to the Data directory by navigating up from the exe directory
            // Adjust the number of ".." based on your actual directory structure
            string dataDirectoryPath = Path.Combine(exeDirectory, "..", "..", "..", "Data");

            // Ensure the Data directory exists
            Directory.CreateDirectory(dataDirectoryPath); // Safe to call if it already exists

            // Combine the Data directory path with the filename to get the full path to the JSON file
            string filePath = Path.Combine(dataDirectoryPath, fileName);

            return filePath;
        }
    }
}
