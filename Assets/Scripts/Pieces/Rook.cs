using System.Collections.Generic;
public class Rook : Piece
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

        // Horizontal and Vertical movement logic
        validMoves.AddRange(GetMovesInDirection(currentCell, 1, 0, allCells)); // Right
        validMoves.AddRange(GetMovesInDirection(currentCell, -1, 0, allCells)); // Left
        validMoves.AddRange(GetMovesInDirection(currentCell, 0, 1, allCells)); // Up
        validMoves.AddRange(GetMovesInDirection(currentCell, 0, -1, allCells)); // Down

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
            
            if (targetCell == null || targetCell.IsOccupied)
                break;
            
            moves.Add(targetCell);

            row += rowDir;
            col += colDir;
        }

        return moves;
    }
}
