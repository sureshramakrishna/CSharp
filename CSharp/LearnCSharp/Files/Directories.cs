using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Directories
{
    class Program
    {
        static void Main(string[] args)
        {
            Directories();
        }
        static void Directories()
        {
            string path = "D:\\Directory";
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            string[] files = Directory.GetFiles(path); //Returns all files in the path, does not return sub directories, output has full path.
            string[] subDirectories = Directory.GetDirectories(path); //Returns all sub directories inside path given, output has full path to subdirectories.
            string currentDirectory = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(path);
            string[] logicalDrives = Directory.GetLogicalDrives(); //Returns all logical drives.
            DirectoryInfo parent = Directory.GetParent(path); //Gets parent directory.
            Directory.Move(sourceDirName: path, destDirName: "C:\\Temp"); //Moves directory or file from source to destination
            Directory.Delete(path, recursive: true); //Deletes specified directory. If directory has files or folders inside, then recursive parameter has to be set to true or else it errors out saying directory is not empty.
        }
        static void Directory_Info()
        {
            string path = "D:\\Directory";
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                FileInfo[] files = di.GetFiles(path);
                DirectoryInfo[] subDirectories = di.GetDirectories(path);
                DirectoryInfo parent = di.Parent; //Gets parent directory.
                di.MoveTo(destDirName: "C:\\Temp"); //Moves directory to destination
                di.Delete(recursive: true);
            }
        }
    }
}
