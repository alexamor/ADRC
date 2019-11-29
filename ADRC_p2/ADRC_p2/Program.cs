using System;
using System.IO;

namespace ADRC_p2
{
    class Program
    {


        public const int MAX_NODES = 66000;

        static void Main(string[] args)
        {

            if (args.Length < 1)
            {
                Console.WriteLine("Please provide a txt file as an argument.");
                Environment.Exit(1);
            }


            // Criar o caminho do ficheiro de texto que representa a rede
            string file = Path.Combine(Directory.GetCurrentDirectory(), args[0]);

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

                //Console.WriteLine(node[0]);

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


            Graph graph = new Graph();

            //Console.WriteLine("Id da raiz?");
            /*int id = Int32.Parse(Console.ReadLine());

            Node root = network[id];

            int[] path = graph.BFS(root);*/
            int[] path;

            for (int i = 0; i < MAX_NODES; i++)
            {
                if (network[i] != null)
                {
                    path = graph.BFS(network[i]);

                    for (int j = 0; j < MAX_NODES; j++)
                        if (network[j] != null)
                            network[j].type = 0;
                    /*Console.WriteLine("Nó :" + network[i].id);
                    Console.WriteLine("Tipo de rota: ");
                    if(network[i].type == 3)
                    {
                        Console.WriteLine("Cliente");
                    }
                    else if(network[i].type == 2)
                    {
                        Console.WriteLine("Par");
                    }
                    else if(network[i].type == 1)
                    {
                        Console.WriteLine("Fornecedor");
                    }
                    Console.WriteLine(network[i].dist);
                    Console.WriteLine();*/
                    /*Console.WriteLine("Clientes:");
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
                    }*/


                    //Zerar valor dos contadores de clientes, pares e fornecedores
                    //graph.ZeroCounters();

                    graph.ShortestBFS(network[i]);

                    for (int j = 0; j < MAX_NODES; j++)
                        if (network[j] != null)
                            network[j].type = 0;
                }

            }

            Console.WriteLine("Clients: " + (float)graph.nrClients / (float)graph.nrTotal);
            Console.WriteLine("Pairs: " + (float)graph.nrPairs / (float)graph.nrTotal);
            Console.WriteLine("Providers: " + (float)graph.nrProviders / (float)graph.nrTotal);

            graph.CumulativeFunction();

            Console.WriteLine("DONE!");
        }
    }
}
