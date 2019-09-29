using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ADRC_p1
{
    public class Leaf
    {
        Leaf left;
        Leaf right;
        int nextHop = -1;

        

    }


    public class Tree
    {
        public void PrefixTree()
        {

            // Mudar para a directoria do programa
            string file = Path.Combine(Directory.GetCurrentDirectory(), "tree1.txt");

            var lines = File.ReadLines(file);

            foreach (var line in lines)
            {

            }

        }

        public void PrintTable()
        {
            Console.WriteLine("Print");
        }

        public void LookUp()
        {
            Console.WriteLine("LookUp");
        }

        public void InsertPrefix()
        {

        }

        public void DeletePrefix()
        {

        }

        public void CompressTree()
        {

        }
    }
}