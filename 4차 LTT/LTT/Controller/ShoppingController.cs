using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTT
{
    // 2번 관심과목 담기 담당
    class ShoppingController
    {
        ShoppingView shoppingView;

        LectureService lectureService;
        MemberService memberService;

        List<LectureDTO> curUserShoppingBasket;

        string curUserID;

        public ShoppingController(string curUserID)
        {
            this.curUserID = curUserID;

            shoppingView = new ShoppingView();

            lectureService = LectureService.GetInstance();
            memberService = MemberService.GetInstance();
        }

        private void Shop()
        {
            // 1. view에서 검색필터 받아옴
            List<String> filters = CommonView.FindLectureForm();

            // 2. 이걸 model로 보내서 필터링된 강의들 받아오기
            List<LectureDTO> filteredLectures = lectureService.GetFilteredLectureResults(filters);

            // 보여줄게 있을때만
            if (filteredLectures.Count != 0)
            {
                // 3. 필터링된 강의들을 view로 보내서 출력시키고 관담할 과목 받아오기
                string lectureID = shoppingView.ShoppingForm(filteredLectures, memberService.GetCurUserInfo(curUserID));
                // 4. 관담할 과목을 진짜로 관담하기
                // 유효한 lectureID이고 + 아직 담지 않은 과목이면
                if (ExceptionHandler.CheckIfValidLectureID(lectureID))
                {
                    bool shoppingSuccess = memberService.AddToUserShoppingBasket(curUserID, lectureID);
                    if (shoppingSuccess)
                    {
                        MyConsole.PrintMessage("관심과목 담기 성공", Console.CursorLeft, Console.CursorTop);
                    }
                    else
                    {
                        MyConsole.PrintMessage("관심과목 담기 실패", Console.CursorLeft, Console.CursorTop);
                    }
                }
            }
            else
            {
                CommonView.NoResultForm();
            }

            MyConsole.WaitForEnterKey();
        }

        private void GetResult()
        {
            // 1. model에서 user가 관담한거 가져오기
            curUserShoppingBasket = memberService.GetUserShoppingBasket(curUserID);

            // 보여줄거 없을때 예외처리
            if (curUserShoppingBasket.Count() == 0) CommonView.NoResultForm();
            else
            {
                // 2. view로 보내서 관담한거 출력하기
                shoppingView.ShoppingResultForm(curUserShoppingBasket, memberService.GetCurUserInfo(curUserID));
            }
            MyConsole.WaitForEnterKey();
        }

        private void GetTimeTable()
        {
            shoppingView.ShoppingTableForm(curUserShoppingBasket);
        }

        private void Delete()
        {
            // 1. model에서 curUserID가 관담한거 가져오기
            curUserShoppingBasket = memberService.GetUserShoppingBasket(curUserID);

            // 보여줄게 없을때 예외처리
            if (curUserShoppingBasket.Count() == 0) CommonView.NoResultForm();
            else
            {
                // 2. view로 보내서 출력하고 삭제할 lectureID 받아오기
                string lectureID = shoppingView.ShoppingDeleteForm(curUserShoppingBasket, memberService.GetCurUserInfo(curUserID));
                // 3. 관담목록에서 진짜로 삭제하기
                if (ExceptionHandler.CheckIfValidLectureID(lectureID))
                {
                    memberService.RemoveFromUserShoppingBasket(curUserID, lectureID);
                }
            }

            MyConsole.WaitForEnterKey();
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
                        Shop();
                        break;

                    case ShoppingMode.SHOPPING_RESULT:
                        GetResult();
                        break;

                    case ShoppingMode.SHOPPING_TABLE:
                        GetTimeTable();
                        break;

                    case ShoppingMode.SHOPPING_DELETE:
                        Delete();
                        break;

                    case ShoppingMode.GO_BACK:
                        isShoppingModeRunning = false;
                        break;
                }

            }
        }
    }
}
