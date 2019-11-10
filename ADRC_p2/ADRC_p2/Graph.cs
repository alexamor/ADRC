using System;
using System.Collections.Generic;


public class Graph
{
    public const int MAX_NODES = 64000;
	public Graph()
	{
        Node[] cliHeap = new Node[MAX_NODES];
        Node[] parHeap = new Node[MAX_NODES];
        Node[] provHeap = new Node[MAX_NODES];
    }

    public void Disktra(Node root, int size)
    {
        //vetor com os caminhos para a root
        Node[] path = new Node[size];

        Node cur = root;

        //Ciclo para percorrer todos os nós vizinhos
        ////Depois mudar a condição
        while(true)
    }
}


public class Node
{
    public int id;
    public int dist = int.MaxValue;

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