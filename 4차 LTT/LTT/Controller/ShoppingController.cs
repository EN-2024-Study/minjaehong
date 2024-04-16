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

        public static string[] shoppingInputMessage = { "담을 과목 ID :" };
        public static string[] deletionInputMessage = { "삭제할 과목 ID :" };

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

                        // view에서 검색필터 받아옴
                        List<String> filters = CommonView.FindLectureForm();
                        // 이걸 model로 보내서 필터링된 강의들 ID 받아옴
                        List<LectureDTO> filteredLectures = lectureModel.GetFilteredLectureResults(filters);
                        // 다시 view로 보내서 강의 출력시키기
                        CommonView.ShowLectureTable(filteredLectures);

                        int inputX = Console.CursorLeft;
                        int inputY = Console.CursorTop;

                        // view에서 담고싶은 과목 입력 받기
                        List<String> inputs = CommonInput.GetUserInputs(shoppingInputMessage, inputX, inputY);

                        // 그걸 usermodel에 적용시키기
                        string lectureID = inputs[0];
                        userModel.AddToUserShoppingList(curUserID, lectureID);
                        break;

                    case ShoppingMode.SHOPPING_RESULT:
                        // model에서 user가 관담한거 가져오기
                        curUserShoppingList = userModel.GetUserShoppingList(curUserID);
                        // shoppingView에서 보여주기
                        shoppingView.ShoppingResultForm(curUserShoppingList);
                        Console.ReadLine();
                        break;

                    case ShoppingMode.SHOPPING_TABLE:
                        shoppingView.ShoppingTableForm(curUserShoppingList);
                        break;

                    case ShoppingMode.SHOPPING_DELETE:
                        curUserShoppingList = userModel.GetUserShoppingList(curUserID);
                        CommonView.ShowLectureTable(curUserShoppingList);

                        inputX = Console.CursorLeft;
                        inputY = Console.CursorTop;

                        // view에서 담고싶은 과목 입력 받기
                        List<String> deletinginputs = CommonInput.GetUserInputs(deletionInputMessage, inputX, inputY);

                        // 그걸 usermodel에 적용시키기
                        lectureID = deletinginputs[0];
                        userModel.DeleteUserShoppingList(curUserID, lectureID);
                        break;

                    case ShoppingMode.GO_BACK:
                        isShoppingModeRunning = false;
                        break;
                }
            }
        }
    }
}
