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
            //Console.WriteLine("Print");

            PrintLeaf(root, "");
        }

        public void PrintLeaf(Leaf curleaf, string branches)
        {
            //Console.WriteLine("PrintLeaf");

            //Adicionar ramo da direita caso este n� n�o seja dos mais � esquerda
            if (branches.Length > 0)
                Console.Write("-");
            
            //Escrever o next hop do atual n�
            if(curleaf.GetNextHop() != -1)
                Console.Write(curleaf.GetNextHop());
            else
                Console.Write(" ");

            //Adicionar ramos da esquerda
            if (curleaf.GetLeft() != null)
                branches += "| ";
            else
                branches += "  ";

            //Verificar se ainda se pode deslocar para a direita
            if (curleaf.GetRight() != null)
                PrintLeaf(curleaf.GetRight(), branches);
            else
            {
                Console.WriteLine(Environment.NewLine + branches);

                branches = branches.Substring(0, branches.Length - 2);

                Console.Write(branches);
            }

            //Verificar se ainda se pode deslocar para a esquerda
            if (curleaf.GetLeft() != null)
                PrintLeaf(curleaf.GetLeft(), branches);

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
                // Verifica que a raiz foi criada. Pode n�o ter acontecido se se estiver a inserir numa �rvore vazia
                // Ou seja, que foi totalmente apagada
                if (root == null)
                    root = new Leaf();

                // Inicializa a vari�vel auxiliar que vai percorrer a �rvore � raiz
                Leaf aux = root;

                // Percorre a �rvore para inserir o prefixo
                for (int i=0; i < prefix.Length; i++)
                {
                    if (prefix[i] == '1')
                    {
                        if(aux.GetRight() == null)
                        {
                            aux.SetRight(new Leaf());
                        }

                        aux = aux.GetRight();
                    }
                    else if (prefix[i] == '0')
                    {
                        if(aux.GetLeft() == null)
                        {
                            aux.SetLeft(new Leaf());
                        }

                        aux = aux.GetLeft();
                    }
                    else
                    {
                        Console.WriteLine("There's an error in the prefix you tried to insert. Please fix it by only using the binary numeral system.");
                        return;
                    }

                }

                aux.SetNextHop(nextHop);

            }

        }
    }
}