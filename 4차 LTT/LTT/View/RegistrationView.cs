using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    // RegistrationController가 호출하는 View모음
    class RegistrationView
    {
        private string[] registrationInputMessage = { "COURSE ID YOU WANT TO REGISTER :" };
        private string[] registrationDeleteMessage = {"COURSE ID YOU WANT TO DELETE :" };
        private string[] registrationMenuArr = { "1. 수강신청", "2. 수강신청 내역", "3. 수강신청 시간표", "4. 수강과목 삭제" };
        
        public RegistrationMode RegistrationModeSelectForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[SELECT REGISTRATION MODE]");
          
            RegistrationMode selectedMode = (RegistrationMode)MyConsole.GetUserSelection(registrationMenuArr, MyConsole.MENU_STARTX, MyConsole.MENU_STARTY);
            return selectedMode;
        }

        public string RegistrationForm(List<LectureDTO> shoppingList, MemberDTO curMember)
        {
            Console.Clear();

            // 관심과목들을 controller부터 받아서 화면에 보여주고
            CommonView.ShowLectureTable(shoppingList);

            // 담고싶은 과목을 입력 받기 
            Console.Write("등록가능 학점 : " + curMember.GetMaximumCredit() + "  담은 학점 : " + (curMember.GetCurrentRegistrationCredit()) + "  ");
            List<String> inputs = MyConsole.GetUserInputs(registrationInputMessage, Console.CursorLeft, Console.CursorTop);

            return inputs[0];
        }

        // controller에서 특정 학생의 수강신청목록 받아서 CommonView 사용해서 출력해주기
        public void RegistrationResultForm(List<LectureDTO> registrationList, MemberDTO curMember)
        {
            Console.Clear();

            CommonView.ShowLectureTable(registrationList);
            Console.Write("등록가능 학점 : " + curMember.GetMaximumCredit() + "  담은 학점 : " + (curMember.GetCurrentRegistrationCredit()) + "  ");
        }

        public void RegistrationTableForm(List<LectureDTO> registrationList)
        {
            Console.Clear();
            CommonView.ShowTimeTable(registrationList);
        }

        // controller에서 특정 학생의 수강신청목록 받아서 CommonView로 넘겨주고
        // 삭제할 LectureID 받기
        public string RegistrationDeleteForm(List<LectureDTO> registrationList, MemberDTO curMember)
        {
            Console.Clear();
            CommonView.ShowLectureTable(registrationList);

            // view에서 삭제하고 싶은 과목 입력 받기
            Console.Write("등록가능 학점 : " + curMember.GetMaximumCredit() + "  담은 학점 : " + (curMember.GetCurrentRegistrationCredit()) + "  ");
            List<String> deletinginputs = MyConsole.GetUserInputs(registrationDeleteMessage, Console.CursorLeft, Console.CursorTop);
            
            return deletinginputs[0];
        }
    }
}
