using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Exercise3
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Commands:                       - example ");
            Console.WriteLine(@" compress <path>               - compress F:\School\Assignment\exercise3.pdf");
            Console.WriteLine(@" decompress <path> <filetype>  - decompress F:\School\Assignment\exercise3.gz .pdf");
            NewInput();
            
        }

        private static void NewInput()
        {
            Console.WriteLine("\nEnter a command: ");
            string command = Console.ReadLine();
            var path = "";
            try
            {
                path = @command.Split(' ').Skip(1).FirstOrDefault();
                if(!File.Exists(path))
                {
                    Console.WriteLine("Could not find file on specified path.");
                }
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.StackTrace);
            }

            if (command.ToLower().StartsWith("compress")) Process(path);
            else if (command.ToLower().StartsWith("decompress"))
            {
                var fileExtension = @command.Split(' ').Skip(2).FirstOrDefault();
                if (fileExtension != null && fileExtension.StartsWith("."))
                {
                    Process(path, fileExtension);
                }
                else
                {
                    Console.WriteLine("Invalid file-type/extension. Did you remember the (.)?");
                }
            }
            else
            {
                Console.WriteLine("You did not enter a valid command.");
            }

            NewInput();
        }

        private static void Process(string path, string fileExtension = "")
        {
            CompressionMode compressionMode = fileExtension == "" ? CompressionMode.Compress : CompressionMode.Decompress;
            Stopwatch stopWatch = new();
            stopWatch.Start();

            var extension = fileExtension == "" ? ".gz" : fileExtension;
            var newPath = Path.GetDirectoryName(path) + @"\" + Path.GetFileNameWithoutExtension(path) + extension;

            using FileStream fileStream = File.Open(path, FileMode.Open);
            using FileStream outputFileStream = File.Create(newPath);

            if(fileExtension == "")
            {
                using GZipStream processor = new(outputFileStream, compressionMode);
                fileStream.CopyTo(processor);
            }
            else
            {
                using GZipStream processor = new(fileStream, compressionMode);
                processor.CopyTo(outputFileStream);
            }
            stopWatch.Stop();

            Console.WriteLine("Successfully " + compressionMode.ToString().ToLower() + $"ed the file in {stopWatch.ElapsedMilliseconds} milliseconds from {new FileInfo(path).Length} bytes to {new FileInfo(newPath).Length} bytes.");
        }
    }
}
