using System;
using System.Collections.Generic;


public class Graph
{
    public int nrClients = 0;
    public int nrProviders = 0;
    public int nrPairs = 0;
    public int nrTotal = 0;

    public const int MAX_NODES = 66000;
    public const int MAX_DIST = 1000;

    public int[] commercialDist = new int[MAX_DIST];
    public int[] shortestDist = new int[MAX_DIST];

    Queue<Node> cliQ = new Queue<Node>();
    Queue<Node> pairQ = new Queue<Node>();
    Queue<Node> provQ = new Queue<Node>();

    Queue<Node> auxProvQ = new Queue<Node>();


    public void ZeroCounters()
    {
        nrClients = 0;
        nrPairs = 0;
        nrProviders = 0;
        nrTotal = 0;
    }


    /*BFS para encontrar os caminhos mais curtos comerciais*/
    public int[] BFS(Node root)
    {
        int[] path = new int[MAX_NODES];

        root.dist = 0;
        root.type = -1;

        ProvSearch(path, root);
        PairSearch(path, root);
        CliSearch(path);

        return path;
    }

    /*BFS para encontrar os caminhos mais curtos gerais*/
    public int[] ShortestBFS(Node root)
    {
        int[] path = new int[MAX_NODES];

        root.dist = 0;

        Queue<Node> queue = new Queue<Node>();

        queue.Enqueue(root);
        root.type = 4;
        while(queue.Count != 0)
        {
            Node cur = queue.Dequeue();

            /*visita todos os vizinhos e mete os não visitados na queue e atualiza as suas distâncias.*/
            foreach (Node p in cur.provider)
            {
                if(p.type == 0)
                {
                    p.type = 4; //make node visited
                    p.dist = cur.dist + 1;
                    queue.Enqueue(p);
                    shortestDist[p.dist]++;
                }
            }

            foreach (Node p in cur.pair)
            {
                if (p.type == 0)
                {
                    p.type = 4; //make node visited
                    p.dist = cur.dist + 1;
                    queue.Enqueue(p);
                    shortestDist[p.dist]++;
                }
            }

            foreach (Node p in cur.customer)
            {
                if (p.type == 0)
                {
                    p.type = 4; //make node visited
                    p.dist = cur.dist + 1;
                    queue.Enqueue(p);
                    shortestDist[p.dist]++;
                }
            }
        }
        
        

        return path;
    }

    /*Encontra os caminhos através de fornecedores na origem (que na realidade vão ser caminhos de clientes) e calcula as distâncias*/
    public void ProvSearch(int[] _path, Node root)
    {
        provQ.Enqueue(root);
        auxProvQ.Enqueue(root);
        
        while(provQ.Count != 0)
        {
            Node cur = provQ.Dequeue();

            foreach( Node p in cur.provider)
            {
                if(p.type == 0)
                {
                    p.type = 3;
                    nrTotal++;
                    nrProviders++;
                    p.dist = cur.dist + 1;
                    provQ.Enqueue(p);
                    _path[p.id] = cur.id;
                    commercialDist[p.dist]++;
                    /*Colocar na lista que vai ser utilizada na procura de pares*/
                    auxProvQ.Enqueue(p);
                }
            }
        }
    }

    /*Encontra os caminhos através de pares e fornecedores*/
    public void PairSearch(int[] _path, Node root)
    {

        while(auxProvQ.Count != 0)
        {
            Node cur = auxProvQ.Dequeue();

            foreach( Node p in cur.pair)
            {
                if(p.type == 0 || p.type == 1)
                {
                    if (p.type == 1)
                    {
                        nrClients--;
                        nrTotal--;
                        commercialDist[p.dist]--;
                    }


                    nrPairs++;
                    nrTotal++;
                    p.type = 2;
                    p.dist = cur.dist + 1;
                    pairQ.Enqueue(p);
                    _path[p.id] = cur.id;
                    commercialDist[p.dist]++;
                }
            }

            foreach(Node p in cur.customer)
            {
                if(p.type == 0)
                {
                    p.type = 1;
                    nrClients++;
                    nrTotal++;
                    p.dist = cur.dist + 1;
                    cliQ.Enqueue(p);
                    _path[p.id] = cur.id;
                    commercialDist[p.dist]++;
                }
            }
        }

        while(pairQ.Count != 0)
        {
            Node cur = pairQ.Dequeue();

            foreach(Node p in cur.customer)
            {
                if(p.type == 0)
                {
                    p.type = 1;
                    nrClients++;
                    nrTotal++;
                    p.dist = cur.dist + 1;
                    cliQ.Enqueue(p);
                    _path[p.id] = cur.id;
                    commercialDist[p.dist]++;
                }
            }
        }
    }

    public void CliSearch(int[] _path)
    {
        while (cliQ.Count != 0)
        {
            Node cur = cliQ.Dequeue();

            //Ignorar nos que eram clientes mas depois foram reescritos para pares
            if(cur.type != 2)
                foreach (Node p in cur.customer)
                {
                    if (p.type == 0)
                    {
                        p.type = 1;
                        nrClients++;
                        nrTotal++;
                        p.dist = cur.dist + 1;
                        cliQ.Enqueue(p);
                        _path[p.id] = cur.id;
                        commercialDist[p.dist]++;
                    }
                }
        }
    }

    public void CumulativeFunction()
    {
        int totalDist = 0;

        float probCommercial = 0;
        float probShortest = 0;

        for (int i = 0; i < MAX_DIST; i++)
        {
            totalDist += commercialDist[i];
        }
        Console.WriteLine("totalDist " + totalDist);
       for(int i = 0; i <MAX_DIST; i++)
       {

            probCommercial +=((float)commercialDist[i] / totalDist);
            probShortest += ((float)shortestDist[i] / totalDist);

          

            if (commercialDist[i] != 0 || shortestDist[i] != 0)
            {
                Console.WriteLine("probCOmmercial " + commercialDist[i]);
                Console.WriteLine("c-" + probCommercial);
                Console.WriteLine("s+" + probShortest);
            }
       }


    }

}

public class Node
{
    public int id;
    public int dist = int.MaxValue;
    public int type = 0; //1 - Cliente 2 - Par   3 - Fornecedor

    public List<Node> provider = new List<Node>();
    public List<Node> customer = new List<Node>();
    public List<Node> pair = new List<Node>();

    public Node(int id)
    {
        this.id = id;
    }

    public void Print()
    {
        Console.WriteLine(id);
    }

}