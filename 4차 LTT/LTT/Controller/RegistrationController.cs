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
        
        MemberRepository userModel;

        string curUserID;
        List<LectureDTO> curUserShoppingList;
        List<LectureDTO> curUserRegistrationList;

        public RegistrationController(string curUserID)
        {
            this.curUserID = curUserID;
            registrationView = new RegistrationView();

            userModel = MemberRepository.GetInstance();
        }

        public void Run()
        {
            bool isRegistrationModeRunning = true;

            RegistrationMode mode;

            string lectureID;

            while (isRegistrationModeRunning)
            {
                mode = registrationView.RegistrationModeSelectForm();

                switch (mode)
                {
                    case RegistrationMode.REGST:
                        // 1. model에서 curUserID가 관담한거 가져오기
                        curUserShoppingList = userModel.GetUserShoppingList(curUserID);
                        // 2. view로 보내서 출력하고 수강신청할 과목 받아오기
                        lectureID = registrationView.RegistrationForm(curUserShoppingList);
                        // 3. 진짜 수강신청해주기
                        userModel.AddToUserRegistration(curUserID, lectureID);
                        break;

                    case RegistrationMode.REGST_RESULT:
                        // 1. model에서 curUserID가 진짜로 수강신청한거 가죠오기
                        curUserRegistrationList = userModel.GetUserRegistrationList(curUserID);
                        // 2. view로 보내서 출력해주기
                        CommonView.ShowLectureTable(curUserRegistrationList);
                        break;

                    case RegistrationMode.REGST_TABLE:
                        break;

                    case RegistrationMode.REGST_DELETE:
                        // 1. model에서 curUserID가 수강신청한거 가져오기
                        curUserRegistrationList = userModel.GetUserRegistrationList(curUserID);
                        // 2. view로 보내서 출력하고 삭제할 lectureID 받아오기
                        lectureID = registrationView.RegistrationDeleteForm(curUserRegistrationList);
                        // 3. 수강신청 목록에서 진짜로 삭제하기
                        userModel.RemoveFromUserRegistration(curUserID, lectureID);
                        break;

                    case RegistrationMode.GO_BACK:
                        isRegistrationModeRunning = false;
                        break;
                }
            }
        }
    }
}
