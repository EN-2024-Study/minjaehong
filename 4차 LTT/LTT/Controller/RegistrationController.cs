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

        public RegistrationController()
        {
            registrationView = new RegistrationView();
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
