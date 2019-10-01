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

                // Insere cada folha na �rvore
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

            //Adicionar ramo da direita caso este n� n�o seja dos mais � esquerda
            if (left)
                Console.Write("-");
            
            //Escrever o next hop do atual n�
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
            Console.WriteLine("Prefix to delete:");

            //caso o delete se propague at� aqui � porque a root em si tamb�m ser� eliminada
            if (DeleteLeaf(root, Console.ReadLine()) == true)
                root = null;
        }

        //Retorna positivo caso seja uma folha (sem filhos) para avisar para apagar os n�s sem next hop acima
        public bool DeleteLeaf(Leaf curleaf, string prefix)
        {
            bool delete = false;

            //Verificar para onde seguir s� caso ainda n�o ter atingindo o destino
            if (prefix.Length > 0)
            {
                char next = prefix[0];

                prefix = prefix.Substring(1);
            
                if(next == '0')
                {
                    delete = DeleteLeaf(curleaf.GetLeft(), prefix);
                    
                    if(delete)
                        curleaf.SetLeft(null);

                }
                else if(next == '1')
                {
                    delete = DeleteLeaf(curleaf.GetRight(), prefix);
                    
                    if(delete)
                        curleaf.SetRight(null);
                }

                //Cancelar delete progressivo caso se tenha atingido um n� a meio com next hop
                if (curleaf.GetNextHop() != -1 || curleaf.GetLeft() != null || curleaf.GetRight() != null)
                    delete = false;
            }
            else
            {
                //S� pedir para apagar n�s sem next hop anteriores caso esta seja uma folha
                if(curleaf.GetLeft() == null && curleaf.GetRight() == null)
                {
                    delete = true;
                }
                else
                {
                    curleaf.SetNextHop(-1);
                }
            }

            return delete;    
        }

        public void CompressTree()
        {
            ORTCIter(root, -1);
        }

        public List<int> ORTCIter(Leaf curLeaf, int nextHop)
        {
            List<int> leftVal, rightVal; 
            
            //Atualizar valor do next hop a propagar
            if (curLeaf.GetNextHop() != -1)
                nextHop = curLeaf.GetNextHop();

            //Verificar se nos encontramos num n� s� com um filho e adicionar um caso seja esse o caso
            if(curLeaf.GetLeft() == null && curLeaf.GetRight() != null)
            {
                curLeaf.SetLeft(new Leaf(nextHop));
            }
            else if(curLeaf.GetLeft() != null && curLeaf.GetRight() == null)
            {
                curLeaf.SetRight(new Leaf(nextHop));
            }

            if (curLeaf.GetLeft() != null && curLeaf.GetRight() != null)
            {
                List<int> intersect = new List<int>();
                
                leftVal = ORTCIter(curLeaf.GetLeft(), nextHop);
                rightVal = ORTCIter(curLeaf.GetRight(), nextHop);

                //caso haja interse�ao, manter a interse�ao, caso nao manter a uni�o
                intersect = leftVal.Intersect<int>(rightVal).ToList<int>();

                if (intersect.Count == 0)
                    intersect = leftVal.Union<int>(rightVal).ToList<int>();

                //Verificar se toda a lista contenha o mesmo nexthop, caso sim pode-se truncar por aqui
                //facilmente verificavel caso so haja 2 valores e ambos sejam iguais
                if(intersect.Count == 2 && intersect[0] == intersect[1])
                {
                    curLeaf.SetNextHop(intersect[0]);
                    curLeaf.SetLeft(null);
                    curLeaf.SetRight(null);
                }

                return intersect;
            }
            else
            {
                List<int> newList = new List<int>();

                newList.Add(nextHop);

                return newList;
            }

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

        public bool DeleteDefault(Leaf curLeaf, int newDefault)
        {
            bool delete;
            // Inicializar a vari�vel que vai percorrer a �rvore � raiz
            if (curLeaf.GetNextHop() == newDefault)
                return true;

            if(curLeaf.GetLeft() != null)
            {
                delete = DeleteDefault(curLeaf.GetLeft(), newDefault);
                // O garbage collector vai apagar os n�s seguintes ao p�r a ra�z de cada sub-�rvore a null
                if(delete)
                    curLeaf.SetLeft(null);
            }
            
            if(curLeaf.GetRight() != null)
            {
                delete = DeleteDefault(curLeaf.GetRight(), newDefault);
                if (delete)
                    curLeaf.SetRight(null);
                        
            }

            return false;


            
        }
    }
}