using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    // ShoppingController가 호출하는 view모음
    class ShoppingView
    {
        public string[] shoppingInputMessage = { "담을 과목 ID :" };

        public ShoppingMode ShoppingModeSelectForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[SELECT SHOPPING MODE]");
            
            ShoppingMode selectedMode = (ShoppingMode)CommonInput.GetUserSelection(Constants.shoppingMenuArr, Constants.MENU_STARTX, Constants.MENU_STARTY);
            return selectedMode;
        }

        public string ShoppingForm(List<LectureDTO> filteredLectures)
        {
            Console.Clear();

            // filetering 된 과목들을 controller부터 받아서 화면에 보여주고
            CommonView.ShowLectureTable(filteredLectures);
            // 담을 과목 입력 받기
            // 바로 밑에 ShoppingInput 띄워줘야함
            int menuStartX = Console.CursorLeft;
            int menuStartY = Console.CursorTop;
            
            List<String> shoppedLecture = CommonInput.GetUserInputs(shoppingInputMessage, menuStartX, menuStartY);

            return shoppedLecture[0];
        }

        // controller에서 특정 학생의 관담목록 받아서 CommonView로 넘겨주기
        public void ShoppingResultForm(List<LectureDTO> shoppingList)
        {
            Console.Clear();

            CommonView.ShowLectureTable(shoppingList);
        }

        // controller에서 특정 학생의 관담목록 받아서 CommonView로 넘겨주기
        public void ShoppingTableForm(List<LectureDTO> shoppingList)
        {
            Console.Clear();

            CommonView.ShowTimeTable(shoppingList);
        }

        // controller에서 특정 학생의 관담목록 받아서 CommonView로 넘겨주기
        public void ShoppingDeleteForm(List<LectureDTO> shoppingList)
        {
            Console.Clear();

            // 이거 띄우고
            CommonView.ShowLectureTable(shoppingList);
            // 바로 밑에 DeleteMenu 띄워줘야함
            int menuStartX = Console.CursorLeft;
            int menuStartY = Console.CursorTop;

            // List<string> input = CommonInput.GetUserInputs(deletionInputMessage, menuStartX, menuStartY);
        }
    }
}
