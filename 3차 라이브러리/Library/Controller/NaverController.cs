using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class NaverController
    {
        private NaverView naverView;
        private NaverService naverService;
        
        public NaverController()
        {
            naverView = new NaverView();
            naverService = new NaverService();
        }

        // USER가 호출하는 책 요청할때 쓰는 함수
        public bool RequestBookByNaverAPI()
        {
            // 찾을 책 제목이랑 수량 입력받기
            List<string> userInput = naverView.SearchByNaverAPIForm();
            string bookTitle = userInput[0];
            string num = userInput[1];

            // Service 호출해서 검색된 책 받기
            List<BookDTO> searchedBooks = naverService.GetBooksByNaverAPI(bookTitle, num);

            // API로 받은 책들 출력하기
            CommonView.PrintAllBooks(searchedBooks);

            // View로 요청할 책 제목 받기
            List<string> requestedBook = naverView.GetRequestBookTitleFromUser();

            string requestedBookTitle = requestedBook[0];
            if(naverService.CheckIfRequestBookExistsInSearchedBooks(requestedBookTitle, searchedBooks))
            {
                naverService.AddRequestedBook(requestedBookTitle, searchedBooks);
                CommonView.RuntimeMessageForm("BOOK REQUEST SUCCESS!");
                return true;
            }
            else
            {
                CommonView.RuntimeMessageForm("BOOK REQUEST FAIL!");
                return false;
            }
        }

        // MANAGER가 호출하는 요청된 책 추가할때 쓰는 함수
        public void AddRequestedBooksFromUsers()
        {

        }
    }
}
