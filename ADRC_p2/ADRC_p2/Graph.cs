using System;
using System.Collections.Generic;


public class Graph
{
    public const int MAX_NODES = 64000;
	public Graph()
	{

	}


}


public class Node
{
    public int id;

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