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

        string curUserID;
        List<LectureDTO> curUserShoppingBasket;
        List<LectureDTO> curUserRegistrationList;

        public RegistrationController(string curUserID)
        {
            this.curUserID = curUserID;
            registrationView = new RegistrationView();

            memberRepository = MemberRepository.GetInstance();
        }

        private void Register()
        {
            // 1. model에서 curUserID가 관담한거 가져오기
            curUserShoppingBasket = memberRepository.GetUserShoppingBasket(curUserID);
            // 2. view로 보내서 출력하고 수강신청할 과목 받아오기
            string lectureID = registrationView.RegistrationForm(curUserShoppingBasket);
            // 3. 진짜 수강신청해주기
            memberRepository.AddToUserRegistration(curUserID, lectureID);
        }

        private void GetResult()
        {
            // 1. model에서 curUserID가 진짜로 수강신청한거 가죠오기
            curUserRegistrationList = memberRepository.GetUserRegistrationList(curUserID);
            // 2. view로 보내서 출력해주기
            CommonView.ShowLectureTable(curUserRegistrationList);
        }

        private void GetTimeTable()
        {

        }

        private void Delete()
        {
            // 1. model에서 curUserID가 수강신청한거 가져오기
            curUserRegistrationList = memberRepository.GetUserRegistrationList(curUserID);
            // 2. view로 보내서 출력하고 삭제할 lectureID 받아오기
            string lectureID = registrationView.RegistrationDeleteForm(curUserRegistrationList);
            // 3. 수강신청 목록에서 진짜로 삭제하기
            memberRepository.RemoveFromUserRegistration(curUserID, lectureID);
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
                    case RegistrationMode.REGST:
                        Register();
                        break;

                    case RegistrationMode.REGST_RESULT:
                        GetResult();
                        break;

                    case RegistrationMode.REGST_TABLE:
                        GetTimeTable();
                        break;

                    case RegistrationMode.REGST_DELETE:
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
