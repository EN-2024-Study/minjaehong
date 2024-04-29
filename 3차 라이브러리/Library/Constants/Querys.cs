namespace Library
{
    static class Querys
    {
        //============= BOOK REPOSITORY QUERYS =============//

        public const string bookDBInitializeQuery = "DELETE FROM bookDB";
        public const string autoIncrementInitializeQuery = "ALTER TABLE bookDB AUTO_INCREMENT = 1";

        // subquery 이용 true false 반환
        public const string getAllBooksQuery = "SELECT * FROM bookDB";
        public const string getAvailableBooksQuery = "SELECT * FROM bookDB WHERE deleted = FALSE";

        public const string checkIfBookExistsQuery = "SELECT EXISTS (SELECT TRUE FROM bookDB WHERE id = @BookID AND deleted = FALSE)";
        public const string checkIfBookAvailableQuery = "SELECT EXISTS (SELECT TRUE FROM bookDB WHERE id = @BookID AND instock > 0 AND deleted = FALSE)";

        // CRUDs
        public const string addNewBookQuery = "INSERT INTO bookDB (name, author, publisher, price, instock, date, isbn) " +
                "VALUES (@name, @author, @publisher, @price, @inStock, @date, @isbn)";
        
        public const string deleteBookQuery = "UPDATE bookDB SET deleted = TRUE WHERE id = @deletingBookID";

        public const string updateBookQuery = "UPDATE bookDB SET name = @name, author = @author, publisher = @publisher, " +
                "price = @price, instock = @inStock, date = @date, isbn = @isbn WHERE id = @id";
        public const string  updateBookStockQuery = "UPDATE bookDB SET instock = @updatedStock WHERE id = @bookID";

        //============= HISTORY REPOSITORY QUERYS =============//
        public const string historyDBInitializeQuery = "DELETE FROM historyDB";

        public const string getAllHistoryQuery = "SELECT * FROM historyDB";
        public const string getCertainMemberBorrowHistory = "SELECT book_id FROM historyDB WHERE borrower_id = @borrowerID AND returned = FALSE";
        public const string getCertainMemberReturnHistory = "SELECT book_id FROM historyDB WHERE borrower_id = @borrowerID AND returned = TRUE";

        public const string checkIfUserBorrowedQuery = "SELECT EXISTS (SELECT TRUE FROM historyDB WHERE borrower_id = @borrowerID AND book_id = @bookID AND returned = FALSE)";
        public const string checkIfBookIsBorrowedQuery = "SELECT EXISTS (SELECT TRUE FROM historyDB WHERE book_id = @bookID AND returned = FALSE)";
        public const string checkIfReturnHistoryAlreadyExistsQuery = "SELECT EXISTS (SELECT TRUE FROM historyDB WHERE borrower_id = @borrowerID AND book_id = @bookID AND returned = TRUE)";

        // CRUDs
        public const string addBorrowHistoryQuery = "INSERT INTO historyDB (borrower_id, book_id, returned) VALUES(@borrowerID, @bookID, FALSE)";

        public const string deleteBorrowHistoryQuery = "DELETE FROM historyDB WHERE borrower_id = @borrowerID AND book_id = @bookID AND returned = FALSE";
        public const string updateToReturnHistoryQuery = "UPDATE historyDB SET returned = TRUE WHERE borrower_id = @borrowerID AND book_id = @bookID";

        //============= MEMBER REPOSITORY QUERYS =============//

        public const string getMemberByIDQuery = "SELECT * FROM memberDB WHERE BINARY(id) = BINARY @requestedMemberID";
        public const string getAllMembersQuery = "SELECT * FROM memberDB";
        public const string checkIfMemberExistsQuery = "SELECT EXISTS (SELECT TRUE FROM memberDB WHERE BINARY(id) = @userID)";
        public const string checkIfValidLoginQuery = "SELECT EXISTS (SELECT TRUE FROM memberDB WHERE BINARY(id) = @userID AND BINARY(pw) = @userPW)";

        // CRUDs
        public const string addNewMemberQuery = "INSERT INTO memberdb (id, pw, name, age, phonenum) VALUES (@id, @pw, @name, @age, @phonenum)";
        public const string deleteMemberQuery = "DELETE FROM memberDB WHERE BINARY(id) = @deletingMemberID";
        public const string updateMemberQuery = "UPDATE memberDB SET pw = @pw, name = @name, age = @age, phonenum = @phonenum WHERE BINARY(id) = @updatingMemberID";
    }
}
