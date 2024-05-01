using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    // 이름이 이상함
    // APIController? APIHandler? APIManager? API매개해주는놈이라는 이름
    // APIConnector? APIServiceHandler?

    // RequestController??

    // 이거 두 개 밖에 없는데 이걸 클래스로 만들어놔야함??
    // 일단 두 개를 usercontroller랑 managercontroller 안에 둘 수 는 있는데
    // 이 함수들이 핀트가 너무 안맞음

    class RequestController
    {
        private RequestView requestView;
        private NaverAPIService naverAPIService;
        
        public RequestController()
        {
            requestView = new RequestView();
            naverAPIService = new NaverAPIService();
        }

        //=============== SEARCH BOOK BY API (USED FOR USER AND MANAGER BOTH) ===============//


        //=============== API SERVICE FOR USER ONLY ===============//

        // USER가 호출하는 책 요청할때 쓰는 함수
        public bool RequestBookByNaverAPI()
        {
            // 찾을 책 제목이랑 수량 입력받기
            List<string> userInput = requestView.SearchByNaverAPIForm();
            string bookTitle = userInput[0];
            string num = userInput[1];

            // Service 호출해서 검색된 책 받기
            List<BookDTO> searchedBooks = naverAPIService.GetBooksByNaverAPI(bookTitle, num);

            // API로 받은 책들 출력하기
            CommonView.PrintAllBooks(searchedBooks);

            // View로 요청할 책 제목 받기
            List<string> requestedBook = requestView.GetRequestBookTitleFromUserForm();

            string requestedBookTitle = requestedBook[0];

            bool exists = naverAPIService.CheckIfRequestBookExistsInSearchedBooks(requestedBookTitle, searchedBooks);

            if (exists)
            {
                naverAPIService.AddRequestedBook(requestedBookTitle, searchedBooks);
                CommonView.RuntimeMessageForm("BOOK REQUEST SUCCESS!");
                return true;
            }
            else
            {
                CommonView.RuntimeMessageForm("BOOK REQUEST FAIL!");
                return false;
            }
        }

        //=============== API SERVICE FOR MANAGER ONLY ===============//

        public bool SearchBookByNaverAPI()
        {
            // VIEW에서 USER/MANAGER 한테 TITLE 이랑 NUM 입력받게 하기
            List<string> userInput = requestView.SearchByNaverAPIForm();
            // VIEW에서 넘어온 값들 추출
            string bookTitle = userInput[0];
            string num = userInput[1];

            // NAVER API SERVICE 호출해서 해당 조건에 맞는 책 받기
            // 찾아오는 것은 SERVICE한테 위임
            List<BookDTO> searchedBooks = naverAPIService.GetBooksByNaverAPI(bookTitle, num);

            // API로 받은 책들 VIEW로 넘겨서 출력하기
            CommonView.PrintAllBooks(searchedBooks);
            MyConsole.WaitForBackSpace();
            return true;
        }

        // MANAGER가 호출하는 요청된 책을 승인할때 쓰는 함수
        // BookDAO로 requested값만 바꿔주면 됨
        // 그게 승인 처리임
        public void ApplyRequestedBooks()
        {
            
        }
    }
}
