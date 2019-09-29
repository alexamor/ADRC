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

        public Leaf()
        {

        }
        
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

            // Criar o caminho do ficheiro de texto que representa a �rvore
            string file = Path.Combine(Directory.GetCurrentDirectory(), "tree1.txt");

            // Abre o ficheiro, l� todas as linhas e guarda a linha num vetor de strings
            var lines = File.ReadLines(file);

            // Cria a raiz da �rvore
            root = new Leaf();

            // L� cada linha do ficheiro e insere cada prefixo na �rvore
            foreach (var line in lines)
            {
                // Separa a linha do ficheiro em prefixo e next hop
                string[] node = line.Split(' ');
                if (node[0] == "")
                    break;
                Console.WriteLine(node[0]);
                // Insere cada folha na �rvore
                InsertLeaf(node[0], Int32.Parse(node[1]));


                Console.WriteLine("Did: " + node[0]);

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
            // Caso o prefixo seja o default, insere na raiz
            if (prefix[0] == 'e')
            {
                Console.WriteLine("default found!");
                root.SetNextHop(nextHop);
                Console.WriteLine("next hop:" + root.GetNextHop());
            }
            else
            {
                
            }

        }
    }
}