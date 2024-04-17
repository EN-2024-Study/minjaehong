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
        
        MemberRepository memberRepository;
        LectureRepository lectureRepository;

        string curUserID;
        List<LectureDTO> curUserShoppingBasket;
        List<LectureDTO> curUserRegistrationList;

        public RegistrationController(string curUserID)
        {
            this.curUserID = curUserID;
            registrationView = new RegistrationView();

            memberRepository = MemberRepository.GetInstance();
            lectureRepository = LectureRepository.GetInstance();
        }

        private void ShoppingRegister()
        {
            // 1. model에서 curUserID가 관담한거 가져오기
            curUserShoppingBasket = memberRepository.GetUserShoppingBasket(curUserID);
            // 2. view로 보내서 출력하고 수강신청할 과목 받아오기
            string lectureID = registrationView.RegistrationForm(curUserShoppingBasket, memberRepository.GetUserInfo(curUserID));
            // 3. 진짜 수강신청해주기
            if (ExceptionHandler.CheckIfValidLectureID(lectureID))
            {
                bool registrationSuccess = memberRepository.AddToUserRegistration(curUserID, lectureID);
                if (registrationSuccess)
                {
                    MyConsole.PrintMessage("수강신청 성공", Console.CursorLeft, Console.CursorTop);
                }
                else
                {
                    MyConsole.PrintMessage("수강신청 실패", Console.CursorLeft, Console.CursorTop);
                }
            }
            else
            {
                MyConsole.PrintMessage("해당 강의는 존재하지 않습니다",Console.CursorLeft, Console.CursorTop);
            }
            MyConsole.WaitForEnterKey();
        }

        private void NormalRegister()
        {
            // 1. view에서 검색필터 받아옴
            List<String> filters = CommonView.FindLectureForm();
            // 2. 검색필터를 model로 보내서 필터링된 강의들 받아오기
            List<LectureDTO> filteredLectures = lectureRepository.GetFilteredLectureResults(filters);
            // 3. view로 보내서 강의 출력하고 수강신청할 과목 받아오기
            string lectureID = registrationView.RegistrationForm2(filteredLectures, memberRepository.GetUserInfo(curUserID));
            // 4. 진짜 수강신청해주기
            if (ExceptionHandler.CheckIfValidLectureID(lectureID))
            {
                bool registrationSuccess = memberRepository.AddToUserRegistration(curUserID, lectureID);
                if (registrationSuccess)
                {
                    MyConsole.PrintMessage("수강신청 성공", Console.CursorLeft, Console.CursorTop);
                }
                else
                {
                    MyConsole.PrintMessage("수강신청 실패", Console.CursorLeft, Console.CursorTop);
                }
            }
            else
            {
                MyConsole.PrintMessage("해당 강의는 존재하지 않습니다", Console.CursorLeft, Console.CursorTop);
            }
            MyConsole.WaitForEnterKey();
        }

        public void GetResult()
        {
            // 1. model에서 curUserID가 진짜로 수강신청한거 가죠오기
            curUserRegistrationList = memberRepository.GetUserRegistrationList(curUserID);
            // 2. view로 보내서 출력해주기
            registrationView.RegistrationResultForm(curUserRegistrationList, memberRepository.GetUserInfo(curUserID));
            
            MyConsole.WaitForEnterKey();
        }

        private void GetTimeTable()
        {

        }

        private void Delete()
        {
            // 1. model에서 curUserID가 수강신청한거 가져오기
            curUserRegistrationList = memberRepository.GetUserRegistrationList(curUserID);
            // 2. view로 보내서 진짜로 수강신청한거 출력하고 삭제할 lectureID 받아오기
            string lectureID = registrationView.RegistrationDeleteForm(curUserRegistrationList, memberRepository.GetUserInfo(curUserID));
            // 3. 수강신청 목록에서 진짜로 삭제하기
            if (ExceptionHandler.CheckIfValidLectureID(lectureID))
            {
                bool deleteRegistrationSuccess = memberRepository.RemoveFromUserRegistration(curUserID, lectureID);
                if (deleteRegistrationSuccess)
                {
                    MyConsole.PrintMessage("수강신청 삭제 성공", Console.CursorLeft, Console.CursorTop);
                }
                else
                {
                    MyConsole.PrintMessage("수강신청 삭제 실패", Console.CursorLeft, Console.CursorTop);
                }
            }
            else
            {
                MyConsole.PrintMessage("해당 강의는 존재하지 않습니다", Console.CursorLeft, Console.CursorTop);
            }
            MyConsole.WaitForEnterKey();
        }

        public void Run()
        {
            bool isRegistrationModeRunning = true;

            RegistrationMode mode;

            while (isRegistrationModeRunning)
            {
                mode = registrationView.RegistrationModeSelectForm();

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
