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

        int cliFree = 0;
        int parFree = 0;
        int provFree = 0;

    }

    /*troca dois nós que estejam na heap através dos índices a e b*/
    public void Swap(Node[] heap, int a, int b)
    {
        Node aux = heap[a];
        heap[a] = heap[b];
        heap[b] = aux;
    }

    /*insere um nó na heap*/
    public void InsertHeap(Node[] heap, int free, Node node)
    {
        if ((free + 1) < MAX_NODES)
        {
            heap[free] = node;
            FixUp(heap, free);
            free++;
        }
    }

    /*repõe a ordem na heap a partir dos filhos*/
    public void FixUp(Node[] heap, int index)
    {
        /*Compara o valor do filho com o pai e enquanto o pai for maior, troca o filho com o pai (para pôr o menor à frente)*/
        while ((index > 0) && (heap[(index - 1) / 2].dist > heap[index].dist))
        {
            Swap(heap, (index - 1) / 2, index);
            /*Atualiza o indice para o pai*/
            index = (index - 1) / 2;
        }
    }

    /*repõe a ordem na heap a partir da raiz*/
    public void FixDown(Node[] heap, int index, int N)
    {
        int child;

        /*percorre o ciclo até chegar às folhas*/
        while ((2 * index) < (N - 1))
        {
            /*Atualiza o descendente*/
            child = (2 * index) + 1;

            /*Escolhe o melhor descendente, ou seja, o que tiver menor valor. Caso child == N -1 , só há um descendente*/
            if ((child < (N - 1)) && (heap[child].dist > heap[child + 1].dist))
            {
                child++;
            }

            /*Caso o pai seja menor que o menor filho, condição de heap satisfeita*/
            if (heap[index].dist < heap[child].dist)
            {
                break;
            }

            /*Caso contrário, troca filho com pai e continua a descer a árvore*/
            Swap(heap, child, index);
            index = child;

        }
    }


    /*retira o elemento com maior prioridade, ou seja, o menor, da heap*/
    public Node PopHeap(Node[] heap, int free)
    {
        /*Troca o menor elemento com o último da heap e repõe a ordem com FixDown*/
        Swap(heap, 0, free - 1);
        FixDown(heap, 0, free - 1);
        return heap[--free];
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
        while (true)
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
        foreach (Node n in cur.provider)
        {
            ////Função de adicionar ordenadamente a provHeap

            int dis = cur.dist + 1;

            if (dis < n.dist)
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