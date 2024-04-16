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
        private string[] registrationInputMessage = { "수강신청할 과목 ID :" };
        private string[] registrationDeleteMessage = { "수강신청 삭제할 과목 ID :" };
        private string[] registrationMenuArr = { "1. 수강신청", "2. 수강신청 내역", "3. 수강신청 시간표", "4. 수강과목 삭제" };
        
        public RegistrationMode RegistrationModeSelectForm()
        {
            Console.Clear();

            MyConsole.PrintHeader("[SELECT REGISTRATION MODE]");
          
            RegistrationMode selectedMode = (RegistrationMode)CommonInput.GetUserSelection(registrationMenuArr, Constants.MENU_STARTX, Constants.MENU_STARTY);
            return selectedMode;
        }

        public string RegistrationForm(List<LectureDTO> shoppingList)
        {
            Console.Clear();

            // 관심과목들을 controller부터 받아서 화면에 보여주고
            CommonView.ShowLectureTable(shoppingList);

            int inputX = Console.CursorLeft;
            int inputY = Console.CursorTop;

            // 담고싶은 과목을 입력 받고 
            List<String> inputs = CommonInput.GetUserInputs(registrationInputMessage, inputX, inputY);

            // 다시 controller한테 보내주기
            return inputs[0];
        }

        public void RegistrationResultForm(List<LectureDTO> registrationList)
        {
            Console.Clear();

            // controller에서 받은 현재 수강신청한 과목 보여주기
            CommonView.ShowLectureTable(registrationList);
        }

        public void RegistrationTableForm()
        {

        }

        public string RegistrationDeleteForm(List<LectureDTO> registrationList)
        {
            Console.Clear();
            CommonView.ShowLectureTable(registrationList);

            int inputX = Console.CursorLeft;
            int inputY = Console.CursorTop;

            // view에서 삭제하고 싶은 과목 입력 받기
            List<String> deletinginputs = CommonInput.GetUserInputs(registrationDeleteMessage, inputX, inputY);
            return deletinginputs[0];
        }
    }
}
