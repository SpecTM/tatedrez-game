using UnityEngine;

public interface IPiece 
{
    PieceDataScriptable PieceDataScriptable { get; }
    void Initialize(PieceDataScriptable pieceDataScriptable, Player owner, GameModeManager gameModeManager);
}
