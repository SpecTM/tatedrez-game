using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private GameObject blackImagePrefab;
    [SerializeField] private GameObject whiteImagePrefab;
    [SerializeField] private int rows = 3;
    [SerializeField] private int columns = 3;
    
    public List<Cell> Cells { get; private set; }
    private List<Cell> _availableCells = new();
    private Piece _selectedPiece;
    private Player _currentPlayer;
    private Player _winningPlayer;
    private GameModeManager _gameModeManager;
    
    private void Awake()
    {
        Cells = new();
        InstantiateBoard();
        EventManager.OnCellPress += TryPlacePiece;
        EventManager.OnMarkAvailableCell += SetAvailableCells;
        EventManager.OnSetSelectedPiece += OnSelectedPiece;
        EventManager.OnPlayerChosen += SetCurrentPlayer;
        EventManager.OnGameModeManagerSet += SetGameModeManager;
    }

    private void OnDisable()
    {
        EventManager.OnCellPress -= TryPlacePiece;
        EventManager.OnMarkAvailableCell -= SetAvailableCells;
        EventManager.OnSetSelectedPiece -= OnSelectedPiece;
        EventManager.OnPlayerChosen -= SetCurrentPlayer;
        EventManager.OnGameModeManagerSet -= SetGameModeManager;
    }
    
    private void InstantiateBoard()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                GameObject prefab = (row + col) % 2 == 0 ? whiteImagePrefab : blackImagePrefab;
                GameObject instance = Instantiate(prefab, transform);
                instance.transform.SetParent(transform, false);
    
                Cell cell = instance.GetComponent<Cell>();
                cell.Initialize(row, col);
                Cells.Add(cell);
            }
        }
    }

    private void SetCurrentPlayer(Player currentPlayer)
    {
        _currentPlayer = currentPlayer;
    }

    private void SetGameModeManager(GameModeManager gameModeManager)
    {
        _gameModeManager = gameModeManager;
    }

    private void OnSelectedPiece(Piece piece)
    {
        _selectedPiece = piece;
    }
    
    private void TryPlacePiece(Cell cell)
    {
        if (!CanPlacePiece(cell)) return;

        if (_gameModeManager.GetCurrentGameMode() == GameModes.Normal)
        {
            PlaceNewPiece(cell);
        }
        else
        {
            MoveSelectedPiece(cell);
        }

        if (CheckForTicTacToe())
        {
            EventManager.TriggerOnTicTacToeAchieved(_winningPlayer.Id);
        }
    }

    private bool CanPlacePiece(Cell cell)
    {
        return !cell.IsOccupied && PlayerActionHandler.PlayerHasAction() && _selectedPiece != null;
    }

    private void PlaceNewPiece(Cell cell)
    {
        Piece piece = PieceFactory.CreatePiece(_selectedPiece.PieceDataScriptable.prefab.gameObject, cell.transform, 
            _selectedPiece.PieceDataScriptable, _currentPlayer, _gameModeManager);
        piece.SetCell(cell);
        piece.AssignBoard(this);
        EventManager.TriggerOnPieceAddedToBoard(piece);
        EventManager.TriggerOnRemoveSelectedPiece(piece);
        EventManager.TriggerOnTakeActionFromCurrentPlayer();
    }

    private void MoveSelectedPiece(Cell cell)
    {
        foreach (var availableCell in _availableCells)
        {
            if (cell == availableCell)
            {
                _selectedPiece.SetCell(cell);
                EventManager.TriggerOnTakeActionFromCurrentPlayer();
            }
            availableCell.StopSequence();
        }
    }
    
    private bool CheckForTicTacToe()
    {
        // Define winning conditions as sets of indices
        int[][] winningPatterns = {
            new[] { 0, 1, 2 }, // Row 1
            new[] { 3, 4, 5 }, // Row 2
            new[] { 6, 7, 8 }, // Row 3
            new[] { 0, 3, 6 }, // Column 1
            new[] { 1, 4, 7 }, // Column 2
            new[] { 2, 5, 8 }, // Column 3
            new[] { 0, 4, 8 }, // Diagonal 1
            new[] { 2, 4, 6 }  // Diagonal 2
        };

        foreach (var pattern in winningPatterns)
        {
            if (IsWinningLine(pattern))
            {
                return true;
            }
        }

        return false;
    }
    
    private bool IsWinningLine(int[] indices)
    {
        // Check if the cells at the given indices form a winning line
        Cell cell1 = Cells[indices[0]];
        Cell cell2 = Cells[indices[1]];
        Cell cell3 = Cells[indices[2]];

        if (!cell1.IsOccupied || !cell2.IsOccupied || !cell3.IsOccupied) return false;

        Piece piece1 = cell1.PositionedPiece;
        Piece piece2 = cell2.PositionedPiece;
        Piece piece3 = cell3.PositionedPiece;
        _winningPlayer = piece1.Owner;
        
        return piece1.Owner == piece2.Owner && piece2.Owner == piece3.Owner;
    }

    private void SetAvailableCells(List<Cell> availableCells)
    {
        EventManager.TriggerStopCellFeedback();
        foreach (var cell in availableCells)
        {
            cell.StartAvailableFeedback();
        }

        _availableCells = availableCells;
    }
}
