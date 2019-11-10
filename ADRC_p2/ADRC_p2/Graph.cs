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

        //Inicializar a raiz
        Node cur = root;
        cur.dist = 0;

        addToHeaps(cur, path);

        //Ciclo para percorrer todos os nós vizinhos
        ////Depois mudar a condição
        while(true)
        {
            ////Função de retirar o nó menor de cliHeap
            cur =

            //Adicionar os vizinhos aos respetivos grupos
            addToHeaps(cur, path);


        }
    }

    ///Depois se calhar receber como argumento o tipo visto comportar-se diferente conforme se estamos a lidar com os clientes, pares ou fornecedores
    public void addToHeaps(Node cur, Node[] _path)
    {
        foreach(Node n in cur.provider)
        {
            ////Função de adicionar ordenadamente a provHeap
            
            int dis = cur.dist + 1;

            if(dis < n.dist)
            {
                n.dist = dis;
                _path[n.id] = cur;
            }
        }

        foreach (Node n in cur.pair)
        {
            ////Função de adicionar ordenadamente a parHeap
            
            int dis = cur.dist + 1;
        }

        foreach (Node n in cur.customer)
        {
            ////Função de adicionar ordenadamente a provHeap

            int dis = cur.dist + 1;
        }
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