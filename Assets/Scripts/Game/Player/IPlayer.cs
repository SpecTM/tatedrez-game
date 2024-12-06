using System.Collections.Generic;

public interface IPlayer 
{
    uint Id { get; }
    int ActionCount { get; }
    void RemoveAction();
    void RemovePiece(Piece piece);
    void SetCurrent(bool value);
    List<Piece> GetPiecesOnBoard();
    IReadOnlyDictionary<string, Piece> GetAvailablePieces();
}
