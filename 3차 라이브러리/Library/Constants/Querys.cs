using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    static class Querys
    {
        //============= BOOK REPOSITORY QUERYS =============//

        public const string bookDBInitializeQuery = "DELETE FROM bookDB";
        public const string autoIncrementInitializeQuery = "ALTER TABLE bookDB AUTO_INCREMENT = 1";

        // subquery 이용 true false 반환
        public const string getAllBooksQuery = "SELECT * FROM bookDB";
        public const string checkIfBookExistsQuery = "SELECT EXISTS (SELECT TRUE FROM bookDB WHERE id = @BookID)";
        public const string checkIfBookAvailableQuery = "SELECT EXISTS (SELECT TRUE FROM bookDB WHERE id = @BookID AND instock > 0)";
        
        // CRUDs
        public const string addNewBookQuery = "INSERT INTO bookDB (name, author, publisher, price, instock, date, isbn) " +
                "VALUES (@name, @author, @publisher, @price, @inStock, @date, @isbn)";
        public const string deleteBookQuery = "DELETE FROM bookDB WHERE id = @deletingBookID";
        
        public const string updateBookQuery = "UPDATE bookDB SET name = @name, author = @author, publisher = @publisher, " +
                "price = @price, instock = @inStock, date = @date, isbn = @isbn WHERE id = @id";
        public const string  updateBookStockQuery = "UPDATE bookDB SET instock = @updatedStock WHERE id = @bookID";

        //============= HISTORY REPOSITORY QUERYS =============//

        public const string getAllHistoryQuery = "SELECT * FROM historyDB";
        public const string checkIfReturnHistoryAlreadyExistsQuery = "SELECT EXISTS (SELECT TRUE FROM historyDB WHERE borrower_id=@borrowerID AND book_id=@bookID AND returned = TRUE)";

        // CRUDs
        public const string addBorrowHistoryQuery = "INSERT INTO historyDB (borrower_id, book_id, returned) " +
                "VALUES(@borrower_id, @book_id, FALSE) ON DUPLICATE KEY UPDATE returned = FALSE;";

        public const string deleteBorrowHistoryQuery = "DELETE FROM historyDB WHERE borrower_id = @borrowerID AND book_id = @bookID AND returned = FALSE";
        public const string updateToReturnHistoryQuery = "UPDATE historyDB SET returned = TRUE WHERE borrower_id = @borrowerID AND book_id = @bookID";
        
        //============= MEMBER REPOSITORY QUERYS =============//
        
        public const string getMemberByIDQuery = "SELECT * FROM memberDB WHERE id = @requestedMemberID";
        public const string getAllMembersQuery = "SELECT * FROM memberDB";
        public const string checkIfMemberExistsQuery = "SELECT EXISTS (SELECT TRUE FROM memberDB WHERE id = @userID)";
        public const string checkIfValidLoginQuery = "SELECT EXISTS (SELECT TRUE FROM memberDB WHERE id = @userID AND pw = @userPW)";

        // CRUDs
        public const string addNewMemberQuery = "INSERT INTO memberdb (id, pw, name, age, phonenum) VALUES (@id, @pw, @name, @age, @phonenum)";
        public const string deleteQuery = "DELETE FROM memberDB WHERE id = @deletingMemberID";
    }
}
