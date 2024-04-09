﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class BookModel
    {
        private static BookModel instance;
        
        private BookModel()
        {
            keyID = 0;
            bookDB = new Dictionary<int, BookDTO>();
        }

        public static BookModel GetInstance()
        {
            if (instance == null)
            {
                instance = new BookModel();
            }
            return instance;
        }

        //===================== SINGELTON ========================//

        // Model에 BookDTO들 저장할 Dictionary 있음
        Dictionary<int, BookDTO> bookDB;
        // Dictionary Key값으로 사용할거임
        private int keyID;

        // BookDTO 조회 - view에서 제목 작가 넘어온거
        // retList controller로 보내주면 다시 view로 넘겨서 print 해줘야함
        public List<BookDTO> FindBook(List<string> dataFromView)
        {
            string bookName = dataFromView[0];
            string author = dataFromView[1];

            List<BookDTO> retList = new List<BookDTO>();

            // resultMatched는 검색결과에 맞는 것인지 확인해주는 변수
            // 2가 되면 매칭하는 책이라는 뜻임
            int resultMatched = 0;

            foreach(int curKey in bookDB.Keys)
            {
                resultMatched = 0;

                // 아무것도 입력없었으면 검색결과 일치 처리
                if (bookName == "") resultMatched++;
                else
                {
                    if (bookDB[curKey].GetName() == bookName) resultMatched++;
                }

                // 아무것도 입력없었으면 검색결과 일치 처리
                if (author == "") resultMatched++;
                else
                {
                    if (bookDB[curKey].GetAuthor() == author) resultMatched++;
                }

                // bookName author 둘 다 매칭되었으면 List에 추가
                if (resultMatched == 2)
                {
                    retList.Add(bookDB[curKey]);
                }
            }

            return retList;
        }

        public void AddNewBook(BookDTO newBook)
        {
            // Model에서는 ID만 설정해주고 저장해줌
            newBook.SetId(keyID);
            bookDB.Add(keyID, newBook);

            // 다음 ID를 위해
            keyID++;
        }

        public bool DeleteBook(int deletingBookID)
        {
            bookDB.Remove(deletingBookID);
            return true;
        }

        public bool UpdateBook(BookDTO updatingBook)
        {
            int updatingBookID = updatingBook.GetId();

            if (updatingBook.GetName() != "") bookDB[updatingBookID].SetName(updatingBook.GetName());
            if (updatingBook.GetAuthor() != "") bookDB[updatingBookID].SetAuthor(updatingBook.GetAuthor());
            if (updatingBook.GetPublisher() != "") bookDB[updatingBookID].SetPublisher(updatingBook.GetPublisher());
            if (updatingBook.GetPrice() != "") bookDB[updatingBookID].SetPrice(updatingBook.GetPrice());
            if (updatingBook.GetQuantity() != "") bookDB[updatingBookID].SetQuantity(updatingBook.GetQuantity());

            return true;
        }

        public List<BookDTO> GetAllBooks()
        {
            return bookDB.Values.ToList();
        }
    }
}