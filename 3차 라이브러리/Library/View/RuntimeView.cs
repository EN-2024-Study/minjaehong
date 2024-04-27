using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class RuntimeView
    {
        private const int RUNTIME_MESSAGE_X = 40;
        private const int RUNTIME_MESSAGE_Y = 4;

        public RuntimeView() { }

        // 사용자의 행동에 따른 동적으로 나타나는 VIEW 
        public void RuntimeMessageForm(string message)
        {
            Console.Clear();
            Console.SetWindowSize(110, 20);

            Console.SetCursorPosition(RUNTIME_MESSAGE_X, RUNTIME_MESSAGE_Y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;

            MyConsole.WaitForBackSpace();
            Console.SetWindowSize(110, 50);
        }
    }
}
