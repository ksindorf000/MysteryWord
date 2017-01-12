using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysteryWord
{
    class Program
    {
        static void Main(string[] args)
        {
            //Write contents of WordList into an Array
            string[] contents = File.ReadAllLines(@"..\..\WordList.txt"); //Relative file path
                                                                          //Absolute file path @"C:/namesave.txt"
            //Check array for values
            Console.WriteLine(contents[58]);
            
        }
    }
}
