using System.Collections.Generic;

public class Bishop : Piece
{
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

        // Diagonal movement logic
        validMoves.AddRange(GetMovesInDirection(currentCell, 1, 1, allCells)); // Up-Right
        validMoves.AddRange(GetMovesInDirection(currentCell, -1, -1, allCells)); // Down-Left
        validMoves.AddRange(GetMovesInDirection(currentCell, -1, 1, allCells)); // Up-Left
        validMoves.AddRange(GetMovesInDirection(currentCell, 1, -1, allCells)); // Down-Right

        return validMoves;
    }

    private List<Cell> GetMovesInDirection(Cell startCell, int rowDir, int colDir, List<Cell> allCells)
    {
        List<Cell> moves = new List<Cell>();
        int row = startCell.Row + rowDir;
        int col = startCell.Column + colDir;

        while (true)
        {
            Cell targetCell = allCells.Find(cell => cell.Row == row && cell.Column == col);
            // If the target cell is null (out of bounds) or occupied, stop movement
            if (targetCell == null || targetCell.IsOccupied)
                break;

            moves.Add(targetCell);
            if (targetCell.IsOccupied)
                break;

            row += rowDir;
            col += colDir;
        }
        return moves;
    }
}
