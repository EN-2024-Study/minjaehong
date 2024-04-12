using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    static class InputHandler
    {
        private const int INPUT_STARTX = 60;
        private const int INPUT_STARTY = 8;

        // 정보입력 창에 쓰임
        // 원하는 위치부터 ReadLine 해주는 함수
        // 커서는 각 메뉴의 ":" 바로 옆부터 시작함
        // enter 치면 바로 거기부터 시작
        // 입력 값들을 List로 반환해줌
        public static List<string> GetUserInputs(int rows, ExceptionState[] exceptionArr)
        {
            Console.CursorVisible = true;

            string input="";
            ExceptionState exceptionState;

            List<string> retList = new List<string>();
            
            for (int i = 0; i < rows; i++)
            {
                exceptionState = exceptionArr[i];
                
                bool isCorrectInput = false;

                // USER가 제대로 된 INPUT을 입력했을때까지 계속함
                while (!isCorrectInput)
                {
                    Console.SetCursorPosition(INPUT_STARTX, INPUT_STARTY + i);
                    EraseUserInput(INPUT_STARTX, INPUT_STARTY + i);
                    input = Console.ReadLine();

                    switch (exceptionState)
                    {
                        case ExceptionState.INT_ONLY:
                            isCorrectInput = ExceptionHandler.CheckIfIntOnlyInput(input);
                            break;

                        case ExceptionState.ENGLISH_ONLY:
                            isCorrectInput = ExceptionHandler.CheckIfEnglishOnlyInput(input);
                            break;

                        case ExceptionState.ENGLISH_INT_ONLY:
                            isCorrectInput = ExceptionHandler.CheckIfEnglistAndIntOnlyInput(input);
                            break;

                        case ExceptionState.PHONENUM_ONLY:
                            isCorrectInput = ExceptionHandler.CheckIfPhoneNumInput(input);
                            break;

                        case ExceptionState.ISBN_ONLY:
                            break;
                        case ExceptionState.DATE_ONLY:
                            break;
                    }
                }

                retList.Add(input); // ENTER찍으면 하나씩 list에 저장
            }
            Console.CursorVisible = false;
            return retList;
        }

        // 지우개 함수
        // 지우고 CursorPosition 제자리로 돌려놓음
        public static void EraseUserInput(int eraseStartX, int eraseStartY)
        {
            Console.SetCursorPosition(eraseStartX, eraseStartY);
            string eraser = "                         ";
            Console.WriteLine(eraser);
            Console.SetCursorPosition(eraseStartX, eraseStartY);
        }

        // 메뉴 선택에 쓰임
        // spacebar 누르면 user가 select한 menu의 idx번호 return
        // 이거 모듈로 연산때문에 0부터 시작하는거임
        // 그래서 enum들 모두 0부터 시작해야함
        // 여기서는 int로 return하는데 controller에서는 enum으로 받기 때문에 enum 형변환해야함
        public static int GetUserSelection(string[] menuArr)
        {
            int beforeSel = 0;
            int curSel = 0;

            int menuNum = menuArr.Length;

            bool pressedSpaceBar = false;

            MyConsole.RenderMenu(menuArr, beforeSel, curSel);

            // SPACEBAR 누를때까지 대기
            while (!pressedSpaceBar)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    ConsoleKey key = keyInfo.Key;

                    if (key == ConsoleKey.DownArrow)
                    {
                        beforeSel = curSel;
                        curSel = (curSel + 1) % menuNum;
                    }
                    else if (key == ConsoleKey.UpArrow)
                    {
                        beforeSel = curSel;
                        curSel = (curSel + menuNum - 1) % menuNum;
                    }
                    // 화면이동일때
                    else if (key == ConsoleKey.Spacebar)
                    {
                        pressedSpaceBar = true;
                    }
                    else if (key == ConsoleKey.Backspace)
                    {
                        return -1;
                    }
                    MyConsole.RenderMenu(menuArr, beforeSel, curSel);
                }
            }

            return curSel;
        }
    }
}
