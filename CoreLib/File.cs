using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CoreLib
{
    public class File
    {

        public static string readAll(string filePath)
        {

            string result = "";

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(filePath))
                {
                    // Read the stream to a string, and write the string to the console.
                    result = sr.ReadToEnd();
                    // Console.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }


            return result;


        }


        public static void moveFile(string path, string bakPath)
        {


            if (System.IO.File.Exists(bakPath))
            {
                System.IO.File.Delete(bakPath);
            }

            // Move the file.
            System.IO.File.Move(path, bakPath);

        }

        public static string[] getFiles(string path, string fileType)
        {

            String[] FileCollection;

            String FilePath = path;



            FileCollection = Directory.GetFiles(FilePath, fileType);

            // FileCollection = Directory.GetFiles(FilePath, "*.txt");

            return FileCollection;

            /*
           
            for (int i = 0; i < FileCollection.Length; i++)

            {

                theFileInfo = new FileInfo(FileCollection[i]);

              

            }
            */


        }
    }
}
