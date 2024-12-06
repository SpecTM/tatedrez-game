using UnityEngine;

[CreateAssetMenu(fileName = "New Piece", menuName = "Tatedrez/Piece")]
public class PieceDataScriptable : ScriptableObject
{
    public string PieceName;
    public Sprite whiteSprite;
    public Sprite blackSprite;
    public Piece prefab;
}

