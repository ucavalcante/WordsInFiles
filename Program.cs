using System;
using System.IO;

namespace WordsInFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            string inFile = "";
            string directoryForRead = "";
            string outFile = "";

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-h":
                    Console.WriteLine("Help menu");
                    break;
                    case "-i":
                    inFile = args[i+1];
                    i++;
                    break;
                    case "-d":
                    directoryForRead = args[i+1];
                    i++;
                    break;
                    case "-o":
                    outFile = args[i+1];
                    i++;
                    break;
                    default:
                    Console.WriteLine("-h for help");
                    break;
                }
            }

            var usrFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            if (inFile.Contains('~'))
            {
                inFile = inFile.Replace("~",usrFolder);
            }
            if (directoryForRead.Contains('~'))
            {
                directoryForRead = directoryForRead.Replace("~",usrFolder);
            }
            if (outFile.Contains('~'))
            {
                outFile = outFile.Replace("~", usrFolder);
            }

            Console.WriteLine($"-i {inFile} -d {directoryForRead} -o {outFile}");




            if (!String.IsNullOrEmpty(inFile))
            {
                FileInfo fileInfoIn = new FileInfo(inFile);
                if (fileInfoIn.Exists)
                {
                    using (StreamReader sr = new StreamReader(fileInfoIn.FullName)){
                        while (sr.Peek() > 0)
                        {
                            var line =  sr.ReadLine();
                            if (!String.IsNullOrEmpty(line) && !String.IsNullOrEmpty(directoryForRead))
                            {
                                DirectoryInfo directoryInfoDirectoryForRead = new DirectoryInfo(directoryForRead);
                                if (directoryInfoDirectoryForRead.Exists)
                                {
                                    foreach (var files in directoryInfoDirectoryForRead.GetFiles())
                                    {
                                        var lineNumber = 0;
                                        using (StreamReader fr = new StreamReader(files.FullName)){
                                            while (sr.Peek() > 0)
                                            {
                                                lineNumber++;
                                                var lineInFile = fr.ReadLine();
                                                if (!String.IsNullOrEmpty(lineInFile) && lineInFile.Contains(line))
                                                {
                                                    Console.WriteLine($"File:{files.Name}|Line Number:{lineNumber}{Environment.NewLine}{lineInFile}");
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }    
            }
            
        }
    }
}