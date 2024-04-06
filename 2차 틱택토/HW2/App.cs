using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class App
    {
        // ctrl handler
        protected static void ctrlHandler(Object sender, ConsoleCancelEventArgs args)
        {
            args.Cancel = true;
        }

        // 콘솔 버퍼 키우기 및 콘솔 창 환경설정
        private static void InitializeConsole()
        {
            

            int width = Console.LargestWindowWidth/2;
            int height = Console.LargestWindowHeight/2;

            Console.SetWindowPosition(0, 0);
            Console.SetWindowSize(213, 62); // set the size of the console window
            //Console.SetBufferSize(100, 80); // set the size of the console buffer

            Console.Title = "틱택토틱택토";
            Console.CancelKeyPress += new ConsoleCancelEventHandler(ctrlHandler);
            Console.CursorVisible = false;
        }

        public static void Main()
        {
            InitializeConsole();
            
            // 프로그램객체 생성
            Program p = new Program();
            p.Run();
        }
    }
}
