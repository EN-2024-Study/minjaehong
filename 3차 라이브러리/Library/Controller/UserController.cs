using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class UserController
    {
        private static UserController instance;

        public static UserController GetInstance()
        {
            if (instance == null)
            {
                instance = new UserController();
            }
            return instance;
        }

        //===================== SINGELTON ========================//

        UserView view;
        MemberModel memberModel;
        BookModel bookModel;

        private UserController()
        {
            // 의존관계 주입
            this.view = UserView.GetInstance();
            this.memberModel = MemberModel.GetInstance();
            this.bookModel = BookModel.GetInstance();
        }

        /*
        // 로그인한 ID로 controller 초기화 해주기
        // 매번 새로 로그인할때마다 호출됨
        // curUserID를 알아야 대출 반납을 memberDTO에 적용가능함
        void InitializeUserController(string curUserID)
        {
            this.curUserID = curUserID;
        }
        */

        //======================== RUN ==========================//

        public void run()
        {
            UserMenuState selectedMenu;

            List<string> dataFromView;
            List<BookDTO> retList;
            MiniDTO miniDTO;

            while (true)
            {
                selectedMenu = view.UserMenuForm();

                switch (selectedMenu)
                {
                    case UserMenuState.FIND:
                        
                        // 찾을 책에 대한 정보를 view에서 받아오기
                        dataFromView = view.FindBookForm();
                        // bookModel로 전달해서 매칭된 List<BookDTO> 받아오기
                        retList = bookModel.FindBook(dataFromView);
                        // 다시 view에 전달해서 매칭된 책들 모두 출력해서 보여주기
                        view.PrintSelectedBooksForm(retList);
                        break;

                    case UserMenuState.BORROW:
                        
                        // 빌릴 책에 대한 정보를 view에서 받아오기
                        dataFromView = view.BorrowBookForm();
                        // 책 정보를 MiniDTO에 담아주기
                        miniDTO = new MiniDTO(dataFromView);
                        // bookModel에 누가 빌린거 적용해주기
                        bookModel.UpdateBorrowed(miniDTO);
                        // 성공하면 memberModel로 가서 저장해주기
                        
                        break;

                    case UserMenuState.CHECKBORROW:
                        
                        view.CheckBorrowedForm();
                        break;

                    case UserMenuState.RETURN:

                        view.ReturnForm();
                        break;
                    
                    case UserMenuState.CHECKRETURN:

                        view.CheckReturnedForm();
                        break;
                    
                    case UserMenuState.UPDATEINFO:

                        view.UpdateMyInfoForm();
                        break;
                    
                    case UserMenuState.DELETEMYSELF:

                        view.DeleteMyselfForm();
                        break;
                    
                    case UserMenuState.NAVERSEARCH:
                        break;
                    
                    case UserMenuState.REQUESTED:
                        break;
                }
            }
        }
    }
}
