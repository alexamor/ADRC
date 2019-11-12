using System;
using System.Collections.Generic;


public class Graph
{


    public const int MAX_NODES = 64000;

    Node[] cliHeap = new Node[MAX_NODES];
    Node[] pairHeap = new Node[MAX_NODES];
    Node[] provHeap = new Node[MAX_NODES];

    //localização dos nós nas heaps. 
    //primeiro int: 1 - provider, 2 - par 3 - customer
    //segundo int: localização na heap
    int[,] locationHeap = new int[MAX_NODES,2];

    int cliFree = 0;
    int pairFree = 0;
    int provFree = 0;

    public Graph()
    {

    }

    /*troca dois nós que estejam na heap através dos índices a e b*/
    public void Swap(Node[] heap, int a, int b)
    {
        Node aux = heap[a];
        heap[a] = heap[b];
        heap[b] = aux;

        //atualizar localizações
        locationHeap[aux.id, 1] = b;
        locationHeap[heap[a].id, 1] = a;
    }

    /*insere um nó na heap*/
    public void InsertHeap(Node[] heap, ref int free, Node node)
    {
        if ((free + 1) < MAX_NODES)
        {
            heap[free] = node;
            // atualizar localização do nó na heap
            locationHeap[node.id, 1] = free;
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
    public Node PopHeap(Node[] heap, ref int free)
    {
        /*Troca o menor elemento com o último da heap e repõe a ordem com FixDown*/
        Swap(heap, 0, free - 1);
        FixDown(heap, 0, free - 1);
        return heap[--free];
    }

    //Função que corre o Dijkstra
    //Esta função irá ser executada 3 vezes, uma para os fornecedores, seguida pelos pares e finalizada pelos clientes, sendo indicada a
    //etapa com o stage
    public void Dijkstra(Node[] path, Node root, int size, int stage)
    {
        Node cur;
        
        //Inicialização para o primeiro caso, ou atribuição de uma nova heap
        if (stage == 1)
        {
            path = new Node[size];

            //Inicializar a raiz
            cur = root;
            cur.dist = 0;
            provHeap[0] = cur;
            //AddToHeaps(cur, path);
        }
        /*else
        {
            if (stage == 2)
            {
                cur = PopHeap(pairHeap, ref pairFree);
            }
            else
            {
                cur = PopHeap(cliHeap, ref cliFree);
            }
        }*/
        
        //Ciclo para percorrer todos os nós vizinhos
        while (true)
        {
            ////Função de retirar o nó menor da Heap
            if(stage == 1)
                cur = PopHeap(this.provHeap, ref provFree);
            else if(stage == 2)
                cur = PopHeap(this.pairHeap, ref pairFree);
            else
                cur = PopHeap(this.cliHeap, ref cliFree);

            //Adicionar os vizinhos aos respetivos grupos
            if(stage == 1)
                AddToHeaps(cur, path);
            else
            {
                //No caso dos pares como nao pode haver par de par não ha necessidade de adicionar mais valores a lista de pares apos a stage 1
                AddToCliHeap(cur, path);
            }

            //Se terminou a heap sair
            if ((provFree == 0 && stage == 1) || (pairFree == 0 && stage == 2) || (cliFree == 0 && stage == 3))
                break;
        }

        stage++;

        //Caso ja tenha verificado os caminhos para fornecedores, clientes e pares terminar o dikstra
        if (stage == 4)
            return;
        else
            Dijkstra(path, root, size, stage);
    }

    ///Depois se calhar receber como argumento o tipo visto comportar-se diferente conforme se estamos a lidar com os clientes, pares ou fornecedores
    public void AddToHeaps(Node cur, Node[] _path)
    {
        AddToProvHeap(cur, _path);
        AddToPairHeap(cur, _path);
        AddToCliHeap(cur, _path);
    }

    public void AddToProvHeap(Node cur, Node[] _path)
    {
        //Verificar por cada nó dos fornecedores se algum passou a ter um melhor caminho/ainda nao tinha um caminho
        foreach (Node n in cur.provider)
        {
            int dis = cur.dist + 1;
            bool changed = false;

            //Caso o nó ja esteja noutra heap será preferivel vir para a heap de providers mesmo que o caminho seja mais caro
            changed = CheckForNode(pairHeap, ref pairFree, cur, 2);
            changed = (CheckForNode(cliHeap, ref cliFree, cur, 3) || changed);

            if (dis < n.dist || changed)
            {
                n.dist = dis;
                _path[n.id] = cur;
                locationHeap[n.id, 0] = 1;
                InsertHeap(this.provHeap, ref provFree, n);
            }
        }
    }

    public void AddToPairHeap(Node cur, Node[] _path)
    {
        //Verificar por cada nó dos fornecedores se algum passou a ter um melhor caminho/ainda nao tinha um caminho
        foreach (Node n in cur.pair)
        {
            int dis = cur.dist + 1;
            bool changed = false;

            //Caso o nó ja esteja noutra heap será preferivel vir para a heap de providers mesmo que o caminho seja mais caro
            changed = CheckForNode(cliHeap, ref cliFree, cur, 1);

            if (dis < n.dist || changed)
            {
                n.dist = dis;
                _path[n.id] = cur;
                locationHeap[n.id, 0] = 2;
                InsertHeap(this.pairHeap, ref pairFree, n);
            }
        }
    }

    public void AddToCliHeap(Node cur, Node[] _path)
    {
        //Verificar por cada nó dos fornecedores se algum passou a ter um melhor caminho/ainda nao tinha um caminho
        foreach (Node n in cur.customer)
        {
            int dis = cur.dist + 1;

            if (dis < n.dist)
            {
                n.dist = dis;
                _path[n.id] = cur;
                locationHeap[n.id, 0] = 3;
                InsertHeap(this.cliHeap, ref cliFree, n);
            }
        }
    }

    //Verifica e remove nó da heap enviada se ja tiver o tal nó
    public bool CheckForNode(Node[] heap, ref int heapSize, Node target, int type)
    {

        if (locationHeap[target.id, 0] == type)
        {
            int index = locationHeap[target.id, 1];
            heapSize--;
            Swap(heap, index, heapSize);
            FixDown(heap, index, heapSize);
            return true;
        }
        else
            return false;

        /*int index = -1;

        for(int i = 0; i < heapSize; i++)
        {
            if (heap[i] == target)
            {
                index = i;
                
                return true;
            }
        }

        return false;*/
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