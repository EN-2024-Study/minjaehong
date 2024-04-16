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
        private string[] shoppingInputMessage = { "담을 과목 ID :" };
        private string[] deletionInputMessage = { "삭제할 과목 ID :" };
        private string[] shoppingMenuArr = { "1. 관심과목 검색", "2. 관심과목 내역", "3. 관심과목 시간표", "4. 관심과목 삭제" };
        
        public ShoppingMode ShoppingModeSelectForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[SELECT SHOPPING MODE]");
            
            ShoppingMode selectedMode = (ShoppingMode)CommonInput.GetUserSelection(shoppingMenuArr, Constants.MENU_STARTX, Constants.MENU_STARTY);
            return selectedMode;
        }

        public string ShoppingForm(List<LectureDTO> filteredLectures)
        {
            Console.Clear();

            // filetering 된 과목들을 controller부터 받아서 화면에 보여주고
            CommonView.ShowLectureTable(filteredLectures);

            int inputX = Console.CursorLeft;
            int inputY = Console.CursorTop;

            // view에서 담고싶은 과목 입력 받기
            List<String> inputs = CommonInput.GetUserInputs(shoppingInputMessage, inputX, inputY);

            return inputs[0];
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

        public string ShoppingDeleteForm(List<LectureDTO> shoppingList)
        {
            Console.Clear();
            CommonView.ShowLectureTable(shoppingList);

            int inputX = Console.CursorLeft;
            int inputY = Console.CursorTop;

            // view에서 삭제하고 싶은 과목 입력 받기
            List<String> deletinginputs = CommonInput.GetUserInputs(deletionInputMessage, inputX, inputY);
            return deletinginputs[0];
        }
    }
}
