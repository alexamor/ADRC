using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ADRC_p1
{
    class Program
    {
        static Tree ptree = new Tree();

        static void Main(string[] args)
        {
            char option;

            ptree.PrefixTree();

            do
            {

                ShowMenu();
                option = Console.ReadKey().KeyChar;
                Console.WriteLine(Environment.NewLine);
                SelectOption(option);

            } while (option != 'q');
        }

        //Mostrar menu das opções
        static void ShowMenu()
        {
            Console.WriteLine("Choose one of the following options:" + Environment.NewLine);
            Console.WriteLine("1 - Print table");
            Console.WriteLine("2 - Look up next-hop");
            Console.WriteLine("3 - Insert prefix");
            Console.WriteLine("4 - Delete prefix");
            Console.WriteLine("5 - Compress tree");
        }

        //Selecionar opção
        static void SelectOption(char option)
        {
            switch(option)
            {
                case '1':
                    ptree.PrintTable();
                    break;
                case '2':
                    ptree.LookUp();
                    break;
                case '3':
                    ptree.InsertPrefix();
                    break;
                case '4':
                    ptree.DeletePrefix();
                    break;
                case '5':
                    ptree.CompressTree();
                    break;
                default:
                    Console.WriteLine("Please choose an option between 1 and 5");
                    break;

            }
        }

    }
}
