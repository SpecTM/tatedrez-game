using System.Collections.Generic;
using UnityEngine;

public class Player : IPlayer
{
    private readonly uint _id;
    private readonly int _totalActions = 1;
    private Dictionary<string, Piece> _availablePieces = new();
    private List<Piece> _piecesOnBoard = new();
    public int ActionCount { get; private set; }
    public uint Id => _id;
    private bool _isCurrent;
    
    public Player(uint id, List<PieceDataScriptable> pieceDataList)
    {
        _id = id;
        ActionCount = _totalActions;
        EventManager.OnResetAllActions += ResetActionCount;
        EventManager.OnPieceAddedToBoard += AddPieceOnBoard;

        foreach (var pieceData in pieceDataList)
        {
            _availablePieces.Add(pieceData.PieceName, pieceData.prefab);
        }
    }
    
    public IReadOnlyDictionary<string, Piece> GetAvailablePieces()
    {
        return new Dictionary<string, Piece>(_availablePieces);
    }

    private void AddPieceOnBoard(Piece piece)
    {
        if (_isCurrent)
        {
            _piecesOnBoard.Add(piece);
        }
    }
    
    private void ResetActionCount()
    {
        ActionCount = _totalActions;
    }
    
    public void RemovePiece(Piece piece)
    {
        Debug.Log(piece.PieceName);
        if (_availablePieces.ContainsKey(piece.PieceName))
        {
            _availablePieces.Remove(piece.PieceName);   
        }
    }
    
    public void RemoveAction()
    {
        ActionCount--;

        if (ActionCount <= 0)
        {
            EventManager.TriggerOnNextPlayerShow();
        }
    }

    public void SetCurrent(bool value)
    {
        _isCurrent = value;
    }

    public List<Piece> GetPiecesOnBoard()
    {
        return _piecesOnBoard;
    }
}
