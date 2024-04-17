using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    static class RuntimeView
    {   
        public static void PrintMessage(string message, int messageStartX, int messageStartY)
        {
            Console.Write("뒤로가기 : ESC | 계속담기 : ENTER");
        }
    }
}
