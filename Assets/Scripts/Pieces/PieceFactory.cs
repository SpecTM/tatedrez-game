using UnityEngine;

public static class PieceFactory 
{
    public static Piece CreatePiece(GameObject piecePrefab, Transform parent, PieceDataScriptable pieceDataScriptable, Player owner, GameModeManager gameModeManager)
    {
        GameObject pieceObject = Object.Instantiate(piecePrefab, parent);
        Piece piece = pieceObject.GetComponent<Piece>();
        piece.Initialize(pieceDataScriptable, owner, gameModeManager);
        return piece;
    }
}
