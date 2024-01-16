namespace mso.reader;

public interface IReader {
    public int ReadInt(string description, int lb, int? ub, Func<int, bool>? validator);
}