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
        private string quantity; // dataFromView[4]

        public BookDTO()
        {

        }

        public BookDTO(string name, string author, string publisher, string price, string quantity)
        {
            this.name = name;
            this.author = author;
            this.publisher = publisher;
            this.price = price;
            this.quantity = quantity;
        }

        public BookDTO(List<string> dataFromView)
        {
            this.name = dataFromView[0];
            this.author = dataFromView[1];
            this.publisher = dataFromView[2];
            this.price = dataFromView[3];
            this.quantity = dataFromView[4];
        }

        public void SetId(int keyId) { this.id = keyId; } // DB에서 알아서 넣어줌
        public void SetName(string name) { this.name = name; }
        public void SetAuthor(string author) { this.author = author; }
        public void SetPrice(string price) { this.price = price; }
        public void SetQuantity(string quantity) { this.quantity = quantity; }

        public int GetId() { return id; }

        public string GetName()
        {
            return name;        }

        public string GetAuthor()
        {
            return author ;
        }

        public string GetPrice()
        {
            return price;
        }

        public string GetQuantity()
        {
            return quantity;
        }
    }
}
