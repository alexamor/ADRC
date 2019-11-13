using System;
using System.Collections.Generic;


public class Graph
{


    public const int MAX_NODES = 64000;

    Queue<Node> cliQ = new Queue<Node>();
    Queue<Node> pairQ = new Queue<Node>();
    Queue<Node> provQ = new Queue<Node>();


    public void BFS(Node root)
    {
        int[] path = new int[MAX_NODES];

        root.dist = 0;

        ProvSearch(path, root);
        PairSearch(path, root);
        CliSearch(path, root);
    }

    public void ProvSearch(int[] _path, Node root)
    {
        provQ.Enqueue(root);
        
        while(provQ.Count != 0)
        {
            Node cur = provQ.Dequeue();

            foreach( Node p in cur.provider)
            {
                if(p.type == 0)
                {
                    p.type = 3;
                    p.dist = cur.dist + 1;
                    provQ.Enqueue(p);
                    _path[p.id] = cur.id;
                }
            }
        }
    }

    public void PairSearch(int[] _path, Node root)
    {

    }

    public void CliSearch(int[] _path, Node root)
    {

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