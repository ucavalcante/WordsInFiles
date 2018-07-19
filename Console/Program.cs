using System;
using System.Diagnostics;
using System.IO;
using System.Text;

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
            Stopwatch stopwatch = new Stopwatch();

            if (!String.IsNullOrEmpty(inFile))
            {
                stopwatch.Start();
                StringBuilder sb = new StringBuilder();
                FileInfo fileInfoIn = new FileInfo(inFile);
                if (fileInfoIn.Exists)
                {
                    using (StreamReader sr = new StreamReader(fileInfoIn.FullName)){
                        var line = "";
                        while ((line =  sr.ReadLine())!= null)
                        {
                            if (!String.IsNullOrEmpty(line) && !String.IsNullOrEmpty(directoryForRead))
                            {
                                Console.WriteLine($">Searched word:{line}");
                                DirectoryInfo directoryInfoDirectoryForRead = new DirectoryInfo(directoryForRead);
                                if (directoryInfoDirectoryForRead.Exists)
                                {
                                    foreach (var files in directoryInfoDirectoryForRead.GetFiles())
                                    {
                                        Console.WriteLine($">>File for search:{files.Name}");
                                        var lineNumber = 0;
                                        using (StreamReader fr = new StreamReader(files.FullName)){
                                            var lineInFile = "";
                                            while ((lineInFile = fr.ReadLine()) != null)
                                            {
                                                lineNumber++;
                                                if (!String.IsNullOrEmpty(lineInFile))
                                                {
                                                    if (lineInFile.Contains(line))
                                                    {
                                                        var msg = $"[File:{files.Name}][Word:\"{line}\"][Line Number:{lineNumber}]{Environment.NewLine}{lineInFile}";
                                                        Console.WriteLine(msg);
                                                        sb.AppendLine(msg);
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
                stopwatch.Stop();  
                sb.AppendLine($"End all files and words in:{stopwatch.Elapsed}");
                using(StreamWriter sw = new StreamWriter(outFile)){
                    sw.Write(sb);
                }
            }
            
        }
    }
}