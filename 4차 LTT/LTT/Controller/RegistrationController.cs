using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    // 3번 수강신청 선택되면 이제부터 얘가 담당임
    class RegistrationController
    {
        RegistrationView registrationView;
        
        LectureModel lectureModel;
        UserModel userModel;

        string curUserID;

        public RegistrationController(string curUserID)
        {
            this.curUserID = curUserID;
            registrationView = new RegistrationView();

            lectureModel = LectureModel.GetInstance();
            userModel = UserModel.GetInstance();
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
                        // UserModel로부터 현재 USER가 관담한 리스트 가져오기
                        List<LectureDTO> curUserShoppingList = userModel.GetUserRegistrationList(curUserID);
                        // View로 보내서 출력
                        CommonView.ShowLectureTable(curUserShoppingList);

                        break;

                    case RegistrationMode.REGST_RESULT:
                        break;

                    case RegistrationMode.REGST_TABLE:
                        break;

                    case RegistrationMode.REGST_DELETE:
                        break;

                    case RegistrationMode.GO_BACK:
                        isRegistrationModeRunning = false;
                        break;
                }
            }
        }
    }
}
