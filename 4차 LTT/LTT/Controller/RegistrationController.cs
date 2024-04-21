using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    // 3번 수강신청 선택되면 호출되는 controller
    class RegistrationController
    {
        RegistrationView registrationView;
        
        MemberService memberService;
        LectureService lectureService;

        string curUserID;

        List<LectureDTO> curUserShoppingBasket;
        List<LectureDTO> curUserRegistrationList;

        public RegistrationController(string curUserID)
        {
            this.curUserID = curUserID;

            registrationView = new RegistrationView();

            memberService = MemberService.GetInstance();
            lectureService = LectureService.GetInstance();
        }

        // 진짜로 수강신청 처리해주는 함수
        private void StartRegisterTransaction(string lectureID)
        {
            if (ExceptionHandler.CheckIfValidLectureID(lectureID))
            { 
                bool registrationSuccess = memberService.AddToUserRegistration(curUserID, lectureID);

                if (registrationSuccess) MyConsole.PrintMessage("수강신청 성공", Console.CursorLeft, Console.CursorTop); 
                else MyConsole.PrintMessage("수강신청 실패", Console.CursorLeft, Console.CursorTop);
            }
            else
            {
                MyConsole.PrintMessage("해당 강의는 존재하지 않습니다", Console.CursorLeft, Console.CursorTop);
            }
        }

        // 진짜로 수강신청 삭제해주는 함수
        private void StartDeleteTransaction(string lectureID)
        {
            if (ExceptionHandler.CheckIfValidLectureID(lectureID))
            {
                bool deleteRegistrationSuccess = memberService.RemoveFromUserRegistration(curUserID, lectureID);
                if (deleteRegistrationSuccess) MyConsole.PrintMessage("수강신청 삭제 성공", Console.CursorLeft, Console.CursorTop);
                else MyConsole.PrintMessage("수강신청 삭제 실패", Console.CursorLeft, Console.CursorTop);
            }
            else
            {
                MyConsole.PrintMessage("해당 강의는 존재하지 않습니다", Console.CursorLeft, Console.CursorTop);
            }
        }

        private void ShoppingRegister()
        {
            MemberDTO curUser = memberService.GetCurUserInfo(curUserID);

            // 1. model에서 curUserID가 관담한거 가져오기
            curUserShoppingBasket = memberService.GetCurUserShoppingBasket(curUserID);

            // 관담한거 없을때 예외처리 
            if (curUserShoppingBasket.Count() == 0) { CommonView.NoResultForm(); }
            else
            {
                // 2. view로 보내서 관담한거 출력하고 수강신청할 과목 받아오기
                string lectureID = registrationView.RegistrationForm(curUserShoppingBasket, memberService.GetCurUserInfo(curUserID));
                // 3. 수강신청 함수 호출
                StartRegisterTransaction(lectureID);
            }

            MyConsole.WaitForEnterKey();
        }

        private void NormalRegister()
        {
            // 1. view에서 검색필터 받아옴
            List<String> filters = CommonView.FindLectureForm();
            // 2. 검색필터를 model로 보내서 필터링된 강의들 받아오기
            List<LectureDTO> filteredLectures = lectureService.GetFilteredLectureResults(filters);

            // 검색된거 없을때 예외처리
            if (filteredLectures.Count() == 0) { CommonView.NoResultForm(); }
            else
            {
                // 3. view로 보내서 강의 출력하고 수강신청할 과목 받아오기
                string lectureID = registrationView.RegistrationForm2(filteredLectures, memberService.GetCurUserInfo(curUserID));
                // 4. 수강신청 함수 호출
                StartRegisterTransaction(lectureID);
            }

            MyConsole.WaitForEnterKey();
        }

        public void GetResult()
        {
            // 1. model에서 curUserID가 진짜로 수강신청한거 가죠오기
            curUserRegistrationList = memberService.GetCurUserRegistrationList(curUserID);
            // 2. view로 보내서 출력해주기
            registrationView.RegistrationResultForm(curUserRegistrationList, memberService.GetCurUserInfo(curUserID));
            
            MyConsole.WaitForEnterKey();
        }

        private void GetTimeTable()
        {

        }

        private void Delete()
        {
            // 1. model에서 curUserID가 수강신청한거 가져오기
            curUserRegistrationList = memberService.GetCurUserRegistrationList(curUserID);

            if (curUserRegistrationList.Count() == 0) { CommonView.NoResultForm(); }
            else
            {
                // 2. view로 보내서 진짜로 수강신청한거 출력하고 삭제할 lectureID 받아오기
                string lectureID = registrationView.RegistrationDeleteForm(curUserRegistrationList, memberService.GetCurUserInfo(curUserID));
                // 3. 수강신청 삭제 함수 호출
                StartDeleteTransaction(lectureID);
            }
            
            MyConsole.WaitForEnterKey();
        }

        public void Run()
        {
            bool isRegistrationModeRunning = true;

            RegistrationMode mode;

            while (isRegistrationModeRunning)
            {
                mode = registrationView.RegistrationModeSelectForm(curUserID);

                switch (mode)
                {
                    case RegistrationMode.NORMAL_REGISTER:
                        NormalRegister();
                        break;

                    case RegistrationMode.SHOPPING_REGISTER:
                        ShoppingRegister();
                        break;

                    case RegistrationMode.REGISTER_RESULT:
                        GetResult();
                        break;

                    case RegistrationMode.REGISTER_TABLE:
                        GetTimeTable();
                        break;

                    case RegistrationMode.REGISTER_DELETE:
                        Delete();
                        break;

                    case RegistrationMode.GO_BACK:
                        isRegistrationModeRunning = false;
                        break;
                }
            }
        }
    }
}
