using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class BookDTO
    {
        private int id; // DB에서 알아서 넣어줌

        private string name; // dataFromView[0]
        private string author; // dataFromView[1]
        private string publisher; // dataFromView[2]
        private string price; // dataFromView[3]
        private string inStock; // dataFromView[4]
        
        private string date; // dataFromView[5]
        private string isbn; // dataFromView[6]

        private bool deleted;
        private bool requested;

        public BookDTO() { }

        public BookDTO(string name, string author, string publisher, string price, string inStock, string date, string isbn, bool deleted, bool requested)
        {
            this.name = name;
            this.author = author;
            this.publisher = publisher;
            this.price = price;
            this.inStock = inStock;
            this.date = date;
            this.isbn = isbn;

            this.deleted = deleted; // DEFAULT FALSE
            this.requested = requested; // DEFAULT FALSE
        }

        // view에서 List<string> 으로 책정보가 넘어왔을때
        // BookDTO로 변환시켜주기 위한 생성자
        public BookDTO(List<string> dataFromView)
        {
            this.name = dataFromView[0];
            this.author = dataFromView[1];
            this.publisher = dataFromView[2];
            this.price = dataFromView[3];
            this.inStock = dataFromView[4];
            this.date = dataFromView[5];
            this.isbn = dataFromView[6];
            this.deleted = false;
            this.requested = false;
        }

        public void SetId(int keyId) { this.id = keyId; } // DB에서 알아서 넣어줌
        public void SetName(string name) { this.name = name; }
        public void SetAuthor(string author) { this.author = author; }
        public void SetPublisher(string publisher) { this.publisher = publisher; }
        public void SetPrice(string price) { this.price = price; }
        public void SetInStock(string inStock) { this.inStock = inStock; }
        public void SetDate(string date) { this.date = date; }
        public void SetIsbn(string isbn) { this.isbn = isbn; }
        public void SetDeleted(bool deleted) { this.deleted = deleted; }
        public void SetRequested(bool requested) { this.requested = requested; }

        public int GetId() { return id; }
        public string GetName() { return name; }
        public string GetAuthor() { return author; }
        public string GetPublisher() { return publisher; }
        public string GetPrice() { return price; }
        public string GetInStock() { return inStock; }
        public string GetDate() { return date; }
        public string GetIsbn() { return isbn; }
        public bool GetDeleted() { return deleted; }
        public bool GetRequested() { return requested; }
    }
}