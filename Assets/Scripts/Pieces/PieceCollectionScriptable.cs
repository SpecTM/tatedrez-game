using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Piece Collection", menuName = "Tatedrez/Piece Collection")]
public class PieceCollectionScriptable : ScriptableObject
{
    public List<PieceDataScriptable> pieceCollection;
}
