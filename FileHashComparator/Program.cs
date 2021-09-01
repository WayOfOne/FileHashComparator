using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace FileHashComparator
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var dir1 = new DirectoryInfo(@"C:\Users\D_Jok\Desktop\test2");
            var dir2 = new DirectoryInfo(@"C:\Users\D_Jok\Desktop\test");

            var list1 = dir1.GetFiles("*", SearchOption.AllDirectories);
            var list2 = dir2.GetFiles("*", SearchOption.AllDirectories);

            var count = 0;
            foreach (var files in list1.Zip(list2, (f1, f2) => Tuple.Create(f1, f2)))
            {
                var equal = Equals(files.Item1, files.Item2);
                if (!equal)
                {
                    count++;
                    Console.WriteLine($"{files.Item1.FullName} and {files.Item2.FullName} are not identical");
                }
                else
                {
                    Console.WriteLine($"{files.Item1.FullName} and {files.Item2.FullName} are identical");
                }
            }

            if (count == 0)
            {
                Console.WriteLine("All files were identical");
            }
        }
        
        public static bool Equals(FileInfo f1, FileInfo f2)
        {
            byte[] f1Hash;
            byte[] f2Hash;
            using var md5 = MD5.Create();
            using (var stream = File.OpenRead(f1.FullName))
            {
                f1Hash = md5.ComputeHash(stream);
            }
            using (var stream = File.OpenRead(f2.FullName))
            {
                f2Hash  = md5.ComputeHash(stream);
            }

            return f1Hash.SequenceEqual(f2Hash);
            
        }
    }
    
}