using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    // 2번 관담 눌리면 그때부터는 얘가 담당
    class ShoppingController
    {
        ShoppingView shoppingView;

        LectureModel lectureModel;
        
        public ShoppingController()
        {
            shoppingView = new ShoppingView();

            lectureModel = LectureModel.GetInstance();
        }

        public void Run()
        {
            bool isShoppingModeRunning = true;

            ShoppingMode mode;

            while (isShoppingModeRunning)
            {
                mode = shoppingView.ShoppingModeSelectForm();

                switch (mode)
                {
                    case ShoppingMode.SHOPPING:
                        // view에서 검색필터 받아옴
                        List<String> filters = CommonView.FindLectureForm();
                        // 이걸 model로 보내서 필터링된 강의들 ID 받아옴
                        List<LectureDTO> filteredLectures = lectureModel.GetFilteredLectureResults(filters);
                        // 다시 view로 보내서 강의 출력시키기
                        CommonView.ShowLectureTable(filteredLectures);
                        break;

                    case ShoppingMode.SHOPPING_RESULT:
                        //shoppingView.ShoppingResultForm();
                        break;

                    case ShoppingMode.SHOPPING_TABLE:
                        //shoppingView.ShoppingTableForm();
                        break;

                    case ShoppingMode.SHOPPING_DELETE:
                        //shoppingView.ShoppingDeleteForm();
                        break;

                    case ShoppingMode.GO_BACK:
                        isShoppingModeRunning = false;
                        break;
                }
            }
        }
    }
}
