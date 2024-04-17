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
        private string[] shoppingInputMessage = { "COURSE ID YOU WANT TO PUT IN YOUR BASKET :" };
        private string[] deletionInputMessage = { "COURSE ID YOU WANT TO DELETE FROM YOUR BASKET :" };
        private string[] shoppingMenuArr = { "1. 관심과목 검색", "2. 관심과목 내역", "3. 관심과목 시간표", "4. 관심과목 삭제" };
        
        public ShoppingMode ShoppingModeSelectForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[SELECT SHOPPING MODE]");
            
            ShoppingMode selectedMode = (ShoppingMode)MyConsole.GetUserSelection(shoppingMenuArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY);
            return selectedMode;
        }

        public string ShoppingForm(List<LectureDTO> filteredLectures, MemberDTO curMember)
        {
            Console.Clear();

            // filetering 된 과목들을 controller부터 받아서 화면에 보여주기
            CommonView.ShowLectureTable(filteredLectures);

            // view에서 담고싶은 과목 입력 받기
            Console.Write("등록가능 학점 : " + curMember.GetMaximumCredit() + "  장바구니에 담은 학점 : "+(curMember.GetCurrentShoppingCredit())+ "  ");
            List<String> inputs = MyConsole.GetUserInputs(shoppingInputMessage, Console.CursorLeft, Console.CursorTop);

            return inputs[0];
        }

        // controller에서 특정 학생의 관담목록 받아서 CommonView 사용해서 출력해주기
        public void ShoppingResultForm(List<LectureDTO> shoppingList, MemberDTO curMember)
        {
            Console.Clear();
            CommonView.ShowLectureTable(shoppingList);
            Console.Write("등록가능 학점 : " + curMember.GetMaximumCredit() + "  장바구니에 담은 학점 : " + (curMember.GetCurrentShoppingCredit()) + "  ");
        }

        public void ShoppingTableForm(List<LectureDTO> shoppingList)
        {
            Console.Clear();
            CommonView.ShowTimeTable(shoppingList);
        }

        // controller에서 특정 학생의 관담목록 받아서 CommonView로 넘겨주고
        // 삭제할 LectureID 받기
        public string ShoppingDeleteForm(List<LectureDTO> shoppingList, MemberDTO curMember)
        {
            Console.Clear();
            CommonView.ShowLectureTable(shoppingList);

            // view에서 삭제하고 싶은 과목 입력 받기
            Console.Write("등록가능 학점 : " + curMember.GetMaximumCredit() + "  장바구니에 담은 학점 : " + (curMember.GetCurrentShoppingCredit()) + "  ");
            List<String> deletinginputs = MyConsole.GetUserInputs(deletionInputMessage, Console.CursorLeft, Console.CursorTop);

            return deletinginputs[0];
        }
    }
}
