using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    // MainController에서 ShoppingController RegistrationController 호출하면서 분기
    // 1번 강의조회는 간단한 작업이므로 그냥 너가 처리하자
    class MainController
    {
        MainView mainView;

        ShoppingController shoppingController;
        RegistrationController registrationController;

        LectureModel lectureModel;

        public MainController()
        {
            mainView = new MainView();

            shoppingController = new ShoppingController();
            registrationController = new RegistrationController();

            lectureModel = LectureModel.GetInstance();
        }

        public void Run()
        {
            bool isProgramRunning = true;
            MainMode mode;

            while (isProgramRunning)
            {
                mode = mainView.MainModeSelectForm();

                switch (mode)
                {
                    case MainMode.SEARCH_MODE:
                        // view에서 검색필터 받아옴
                        List<String> filters = CommonView.FindLectureForm();
                        // 이걸 model로 보내서 필터링된 강의들 ID 받아옴
                        List<LectureDTO> filteredLectures = lectureModel.GetFilteredLectureResults(filters);
                        // 다시 view로 보내서 강의 출력시키기
                        CommonView.ShowLectureTable(filteredLectures);
                        break;

                    case MainMode.SHOPPING_MODE:
                        shoppingController.Run();
                        break;

                    case MainMode.REGST_MODE:
                        registrationController.Run();
                        break;

                    case MainMode.REGST_RESULT_MODE:
                        break;
                }
            }
        }
    }
}
