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

        public void RuntimeMessageForm(string exceptionMessage)
        {
            Console.Clear();
            Console.SetCursorPosition(RUNTIME_MESSAGE_X, RUNTIME_MESSAGE_Y);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(exceptionMessage);
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(RUNTIME_MESSAGE_X, RUNTIME_MESSAGE_Y + 2);
            Console.WriteLine("PRESS ENTER TO GO BACK...");

            bool pressedEnter = false;

            while (!pressedEnter)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    ConsoleKey key = keyInfo.Key;

                    if (key == ConsoleKey.Enter) pressedEnter = true;
                }
            }
        }
    }
}
