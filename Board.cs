using System.Collections;

namespace mso;

public class Board : IEnumerable<int> {
    private readonly int _size;
    private readonly bool _hasHomePits;
    private readonly int[] _board;

    /// <summary>
    /// Initialize the board
    /// </summary>
    /// <param name="size">The number of playable pits, excluding the home pits</param>
    /// <param name="initialCount">The initial number of stones in each pit</param>
    /// <param name="homePits">Whether home pits are enabled</param>
    public Board(int size, int initialCount, bool homePits) {
        int homePitCount = homePits ? 2 : 0;
        _board = new int[size + homePitCount];
        for (int i = 0; i < size + homePitCount; i++) {
            if (homePits) {
                if (i != size / 2 && i != size + 1) {
                    _board[i] = initialCount;
                }
            } else {
                _board[i] = initialCount;
            }
        }

        _size = size;
        _hasHomePits = homePits;
    }

    public int Size() => _board.Length;

    public int GameplaySize() => Math.Min(_board.Length, _size);

    public bool HasHomePits() => _hasHomePits;
    
    public int this[int idx] {
        get => _board[idx];
        set => _board[idx] = value;
    }
    
    public IEnumerator<int> GetEnumerator() => ((IEnumerable<int>)_board).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}