using System;
using System.IO;

namespace ADRC_p2
{
    class Program
    {


        public const int MAX_NODES = 64000;

        static void Main(string[] args)
        {
            string networkTxt = "network1.txt";
            // Criar o caminho do ficheiro de texto que representa a rede
            string file = Path.Combine(Directory.GetCurrentDirectory(), networkTxt);

            if (file == null)
                Environment.Exit(1);

            // Abre o ficheiro, lê todas as linhas e guarda a linha num vetor de strings
            var lines = File.ReadLines(file);

            // Cria o vetor de nós
            Node[] network = new Node[MAX_NODES];

            // Lê cada linha do ficheiro e insere cada nó adjacente na árvore
            foreach (var line in lines)
            {
                // Separa a linha do ficheiro em prefixo e next hop
                string[] node = line.Split(' ');

                Console.WriteLine(node[0]);

                int firstNode = Int32.Parse(node[0]);

                if (network[firstNode] == null)
                {
                    network[firstNode] = new Node(firstNode);
                }

                int secondNode = Int32.Parse(node[1]);

                if (network[secondNode] == null)
                {
                    network[secondNode] = new Node(secondNode);
                }


                int relationshipType = Int32.Parse(node[2]);

                switch (relationshipType)
                {
                    // fornecedor-cliente
                    case 1:
                        network[firstNode].customer.Add(network[secondNode]);
                        break;
                    // par-par
                    case 2:
                        network[firstNode].pair.Add(network[secondNode]);
                        break;
                    // cliente - fornecedor
                    case 3:
                        network[firstNode].provider.Add(network[secondNode]);
                        break;
                    
                }
                

            }

            /*for (int i = 0; i < MAX_NODES; i++)
            {
                if (network[i] != null)
                {
                    Console.WriteLine("Nó :" + network[i].id);
                    Console.WriteLine("Clientes:");
                    foreach (Node x in network[i].customer){
                        x.Print();
                    }
                    Console.WriteLine("Pares:");
                    foreach (Node x in network[i].pair)
                    {
                        x.Print();
                    }
                    Console.WriteLine("Fornecedores:");
                    foreach (Node x in network[i].provider)
                    {
                        x.Print();
                    }


                }

            }*/
        }
    }
}
