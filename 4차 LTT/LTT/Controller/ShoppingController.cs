using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    // 2번 관담 눌리면 그때부터는 얘가 담당
    // MainController에서 curUserShoppingList 참조값 넘겨준거로 모두 작업
    // 따로 UserModel에서 가져올 필요없게
    class ShoppingController
    {
        ShoppingView shoppingView;

        LectureModel lectureModel;
        UserModel userModel;

        List<LectureDTO> curUserShoppingList;

        string curUserID;


        // ShoppingController가 또 userModel을 참조해서 받아오지않고
        // MainController에서 넘겨준 curUserShoppingList를 사용
        public ShoppingController(string curUserID)
        {
            this.curUserID = curUserID;

            shoppingView = new ShoppingView();

            lectureModel = LectureModel.GetInstance();
            userModel = UserModel.GetInstance();    
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

                        // 1. view에서 검색필터 받아옴
                        List<String> filters = CommonView.FindLectureForm();
                        // 2. 이걸 model로 보내서 필터링된 강의들 받아오기
                        List<LectureDTO> filteredLectures = lectureModel.GetFilteredLectureResults(filters);
                        // 3. 필터링된 강의들을 view로 보내서 출력시키고 관담할 과목 받아오기
                        string lectureID = shoppingView.ShoppingForm(filteredLectures);
                        // 4. 관담할 과목을 진짜로 관담하기
                        userModel.AddToUserShoppingBasket(curUserID, lectureID);
                        break;

                    case ShoppingMode.SHOPPING_RESULT:
                        // 1. model에서 user가 관담한거 가져오기
                        curUserShoppingList = userModel.GetUserShoppingList(curUserID);
                        // 2. view로 보내서 관담한거 출력하기
                        shoppingView.ShoppingResultForm(curUserShoppingList);
                        Console.ReadLine();
                        break;

                    case ShoppingMode.SHOPPING_TABLE:
                        shoppingView.ShoppingTableForm(curUserShoppingList);
                        break;

                    case ShoppingMode.SHOPPING_DELETE:
                        // 1. model에서 curUserID가 관담한거 가져오기
                        curUserShoppingList = userModel.GetUserShoppingList(curUserID);
                        // 2. view로 보내서 출력하고 삭제할 lectureID 받아오기
                        lectureID = shoppingView.ShoppingDeleteForm(curUserShoppingList);
                        // 3. 관담목록에서 진짜로 삭제하기
                        userModel.RemoveFromUserShoppingBasket(curUserID, lectureID);
                        break;

                    case ShoppingMode.GO_BACK:
                        isShoppingModeRunning = false;
                        break;
                }
            }
        }
    }
}
