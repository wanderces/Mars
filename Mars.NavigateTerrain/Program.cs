using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mars.NavigateTerrain
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo cki;
            Robot robot = new Robot();
            
            do
            {
                //0x0
                robot.MatrixInput = Console.ReadLine();
                //F:Front, L:Left, R:Right
                robot.CommandsInput = Console.ReadLine();
                //Print result
                Console.WriteLine(robot.GetResult());

                Console.WriteLine("{0}Press Esc to exit or press any other button to continue...", "\n\r");

                cki = Console.ReadKey(true);

                Console.Clear();

            } while (cki.Key != ConsoleKey.Escape);
            ;
            
        }
        
    }
}

