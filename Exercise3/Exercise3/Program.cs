using System;
using System.Collections.Generic;
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
            Console.WriteLine("Compress/Decompress using the commands 'compress <path> <filetype>' and 'decompress <path>' \n" +
                "examples: 'compress F:\\School\\Assignment\\exercise3.pdf' 'decompress F:\\School\\Assignment\\exercise3.gz .pdf'");
            NewInput();
            
        }

        private static void NewInput()
        {
            Console.WriteLine("\nEnter a command: ");
            string[] command = Console.ReadLine().Split(' ');

            if(command.Length >= 2)
            {
                ProcessCommand(command);
            }
            else
            {
                Console.WriteLine("A problem occurred. You must at least enter a command containing compression-type (compress/decompress) and a path.");
            }

            NewInput();
        }

        private static void ProcessCommand(string[] command)
        {
            var path = command[1];
            if (!File.Exists(path)) Console.WriteLine("Could not find file on specified path.");

            if (command[0].ToLower().StartsWith("compress")) Process(path);

            else if (command[0].ToLower().StartsWith("decompress") && command.Length >= 3)
            {
                var fileExtension = command[2];
                if (fileExtension.StartsWith("."))
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
                Console.WriteLine("You did not enter a valid command. In case of decompression - did you remember to add file-type/extension?");
            }

        }

        private static void Process(string path, string fileExtension = "")
        {
            CompressionMode compressionMode = fileExtension == "" ? CompressionMode.Compress : CompressionMode.Decompress;
            Stopwatch stopWatch = new();
            stopWatch.Start();

            var extension = fileExtension == "" ? ".gz" : fileExtension;
            var newPath = Path.GetDirectoryName(path) + @"\" + Path.GetFileNameWithoutExtension(path) + extension;

            using FileStream fileStream = File.Open(path, FileMode.Open);
            Compress(fileExtension == "" ? CompressionType.Compress : CompressionType.Decompress, fileStream, newPath);

            Console.WriteLine("Successfully " + compressionMode.ToString().ToLower() + $"ed the file in {stopWatch.ElapsedMilliseconds} milliseconds from {new FileInfo(path).Length} bytes to {new FileInfo(newPath).Length} bytes.");
        }

        private static void Compress(CompressionType compressionType, FileStream fileStream, string newPath)
        {
            using FileStream outputFileStream = File.Create(newPath);

            if(compressionType.Equals(CompressionType.Compress))
            {
                using GZipStream processor = new(outputFileStream, (CompressionMode)compressionType);
                fileStream.CopyTo(processor);
            }
            else if (compressionType.Equals(CompressionType.Decompress))
            {
                using GZipStream processor = new(fileStream, (CompressionMode)compressionType);
                processor.CopyTo(outputFileStream);
            } 
        }
    }
}
