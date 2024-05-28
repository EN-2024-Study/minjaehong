package constant;

public class Querys {
    public final String bookDBInitializeQuery = "DELETE FROM bookDB";
    public final String autoIncrementInitializeQuery = "ALTER TABLE bookDB AUTO_INCREMENT = 1";
    public final String historyDBInitializeQuery = "DELETE FROM historyDB";
    public final String memberDBInitializeQuery = "DELETE FROM memberDB";
}
