using System.Collections.Generic;
public class Knight : Piece
{
    private readonly int[] _rowOffsets = { 2, 2, -2, -2, 1, 1, -1, -1 };
    private readonly int[] _colOffsets = { 1, -1, 1, -1, 2, -2, 2, -2 };

    protected override void SelectPiece()
    {
        if (!_isSelected)
        {
            EventManager.TriggerOnMarkAvailableCell(GetValidMoves(this, _cell, _board.Cells));   
        }
        base.SelectPiece();
    }

    protected override List<Cell> GetValidMoves(Piece piece, Cell currentCell, List<Cell> allCells)
    {
        List<Cell> validMoves = new List<Cell>();
        int currentRow = currentCell.Row;
        int currentCol = currentCell.Column;

        for (int i = 0; i < _rowOffsets.Length; i++)
        {
            int newRow = currentRow + _rowOffsets[i];
            int newCol = currentCol + _colOffsets[i];

            // Check bounds and add valid cells
            Cell targetCell = allCells.Find(cell => cell.Row == newRow && cell.Column == newCol);
            if (targetCell != null && !targetCell.IsOccupied)
            {
                validMoves.Add(targetCell);
            }
        }
        return validMoves;
    }
}
