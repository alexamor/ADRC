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

        public Leaf(int nextHop)
        {
            this.nextHop = nextHop;
        }

        public int GetNextHop()
        {
            return nextHop;
        }

        public void SetNextHop(int nextHop)
        {
            this.nextHop = nextHop;
        }

        public Leaf GetLeft()
        {
            return left;
        }
        public Leaf GetRight()
        {
            return right;
        }
        public void SetLeft(Leaf left)
        {
            this.left = left;
        }
        public void SetRight(Leaf right)
        {
            this.right = right;
        }


    }


    public class Tree
    {
        Leaf root;
        public void PrefixTree()
        {

            // Criar o caminho do ficheiro de texto que representa a árvore
            string file = Path.Combine(Directory.GetCurrentDirectory(), "tree1.txt");

            // Abre o ficheiro, lê todas as linhas e guarda 
            var lines = File.ReadLines(file);

            foreach (var line in lines)
            {
                string[] node = line.Split(' ');
                if (node[0] == "e")
                {
                    Console.WriteLine("default found!");
                    root = new Leaf(Int32.Parse(node[1]));
                    Console.WriteLine("next hop:" + root.GetNextHop());
                }
                else
                {
                    InsertLeaf(node[0], Int32.Parse(node[1]));
                }

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

        public void InsertLeaf(string prefix, int nextHop)
        {

        }
    }
}