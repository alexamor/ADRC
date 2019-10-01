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

            // Criar o caminho do ficheiro de texto que representa a árvore
            string file = Path.Combine(Directory.GetCurrentDirectory(), "tree1.txt");

            // Abre o ficheiro, lê todas as linhas e guarda a linha num vetor de strings
            var lines = File.ReadLines(file);

            // Cria a raiz da árvore
            root = new Leaf();

            // Lê cada linha do ficheiro e insere cada prefixo na árvore
            foreach (var line in lines)
            {
                // Separa a linha do ficheiro em prefixo e next hop
                string[] node = line.Split(' ');
                if (node[0] == "")
                    break;

                // Insere cada folha na árvore
                InsertLeaf(node[0], Int32.Parse(node[1]));



            }

        }

        public void PrintTable()
        {
            PrintLeaf(root, "", false);
            Console.WriteLine(Environment.NewLine);
        }

        public void PrintLeaf(Leaf curleaf, string branches, bool left)
        {
            //Console.WriteLine("PrintLeaf");

            //Adicionar ramo da direita caso este nó não seja dos mais à esquerda
            if (left)
                Console.Write("-");
            
            //Escrever o next hop do atual nó
            if(curleaf.GetNextHop() != -1)
                Console.Write(curleaf.GetNextHop());
            else
                Console.Write("o");

            //Adicionar ramos da esquerda
            if (curleaf.GetLeft() != null)
                branches += "| ";
            else
                branches += "  ";

            //Verificar se ainda se pode deslocar para a direita
            if (curleaf.GetRight() != null)
            {
                PrintLeaf(curleaf.GetRight(), branches, true);
            }

            //Verificar se ainda se pode deslocar para a esquerda
            if (curleaf.GetLeft() != null)
            {
                Console.WriteLine(Environment.NewLine + branches);

                branches = branches.Substring(0, branches.Length - 2);

                Console.Write(branches);

                PrintLeaf(curleaf.GetLeft(), branches, false);
            }
        }

        public void LookUp()
        {
            Console.WriteLine("Which address do you want to look up?");
            string address = Console.ReadLine();

            if(address.Length > 16)
            {
                Console.WriteLine("The address you provided is not valid.");
            }

            if(root == null)
            {
                Console.WriteLine("There is no tree. Please provide a file with a prefix table or insert prefixes.");
                return;
            }

            Leaf aux = root;
            int nextHop = aux.GetNextHop();

            for(int i = 0; i < address.Length; i++)
            {
                if(address[i] == '0')
                {
                    if (aux.GetLeft() == null)
                    {
                        Console.WriteLine("The next hop is " + nextHop);
                        return;
                    }


                    aux = aux.GetLeft();

                }
                else
                {
                    if(aux.GetRight() == null)
                    {
                        Console.WriteLine("The next hop is " + nextHop);
                        return;
                    }

                    aux = aux.GetRight();
                }


                if (aux.GetNextHop() != -1)
                    nextHop = aux.GetNextHop();

                //Console.WriteLine("NEXT HOP:" + nextHop);
            }

        }

        public void InsertPrefix()
        {
            Console.WriteLine("Please input the prefix and next hop you want to insert in the following format: input nexthop");
            string[] message = Console.ReadLine().Split(' ');
            InsertLeaf(message[0], Int32.Parse(message[1]));

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
                root.SetNextHop(nextHop);
            }
            else
            {
                // Verifica que a raiz foi criada. Pode não ter acontecido se se estiver a inserir numa árvore vazia
                // Ou seja, que foi totalmente apagada
                if (root == null)
                    root = new Leaf();

                // Inicializa a variável auxiliar que vai percorrer a árvore à raiz
                Leaf aux = root;

                // Percorre a árvore para inserir o prefixo
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