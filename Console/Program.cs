using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace WordsInFiles
{
    class Program
    {
        private static string inFile = "";
        private static string directoryForRead = "";
        private static string outFile = "";

        static void Main(string[] args)
        {
            ComandDriver(args);

            Stopwatch stopwatch = new Stopwatch();

            if (!String.IsNullOrEmpty(inFile))
            {
                stopwatch.Start();
                StringBuilder sb = StartReading();
                stopwatch.Stop();
                sb.AppendLine($"End all files and words in:{stopwatch.Elapsed}");
                using (StreamWriter sw = new StreamWriter(outFile))
                {
                    sw.Write(sb);
                }
            }
            Console.WriteLine("Finish.");
        }

        private static StringBuilder StartReading()
        {
            StringBuilder sb = new StringBuilder();
            FileInfo fileInfoIn = new FileInfo(inFile);
            if (fileInfoIn.Exists)
            {
                using (StreamReader sr = new StreamReader(fileInfoIn.FullName))
                {
                    var line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (!String.IsNullOrEmpty(line) && !String.IsNullOrEmpty(directoryForRead))
                        {
                            Console.WriteLine($">Searched word:{line}");
                            DirectoryInfo directoryInfoDirectoryForRead = new DirectoryInfo(directoryForRead);
                            if (directoryInfoDirectoryForRead.Exists)
                            {
                                foreach (var files in directoryInfoDirectoryForRead.GetFiles())
                                {
                                    ProcessFile(sb, line, files);
                                }
                            }
                        }
                    }
                }
            }

            return sb;
        }

        private static void ProcessFile(StringBuilder sb, string line, FileInfo files)
        {
            Console.WriteLine($">>File for search:{files.Name}");
            var lineNumber = 0;
            using (StreamReader fr = new StreamReader(files.FullName))
            {
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

        private static void PathAdjustments()
        {
            var usrFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            if (inFile.Contains('~'))
            {
                inFile = inFile.Replace("~", usrFolder);
            }
            if (directoryForRead.Contains('~'))
            {
                directoryForRead = directoryForRead.Replace("~", usrFolder);
            }
            if (outFile.Contains('~'))
            {
                outFile = outFile.Replace("~", usrFolder);
            }

            Console.WriteLine($"-i {inFile} -d {directoryForRead} -o {outFile}");
        }

        private static void ComandDriver(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-h":
                        Console.WriteLine("Help menu");
                        break;
                    case "-i":
                        inFile = args[i + 1];
                        i++;
                        break;
                    case "-d":
                        directoryForRead = args[i + 1];
                        i++;
                        break;
                    case "-o":
                        outFile = args[i + 1];
                        i++;
                        break;
                    default:
                        Console.WriteLine("-h for help");
                        break;
                }
            }
            PathAdjustments();
        }
    }
}