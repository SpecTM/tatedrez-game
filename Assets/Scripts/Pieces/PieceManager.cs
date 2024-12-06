using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    [SerializeField] private PieceCollectionScriptable pieceCollectionData;
    [SerializeField] private GameObject basePiece;
    [SerializeField] private Transform piecePanel;

    private Player _currentPlayer;
    private List<Piece> _initialPieces = new();
    private List<Piece> _allPiecesOnBoard = new();
    private GameModeManager _gameModeManager;
    
    private void Awake()
    {
        EventManager.OnPlayerChosen += ShowPlayerPieces;
        EventManager.OnPieceSelected += ManageSelectedPiece;
        EventManager.OnRemoveSelectedPiece += RemoveSelectedPiece;
        EventManager.OnPieceAddedToBoard += OnPieceAddedToBoard;
        EventManager.OnGameModeManagerSet += SetGameModeManager;
        EventManager.OnFirstPlayerSelected += CreatePieces;
    }
    
    private void OnDisable()
    {   
        EventManager.OnPlayerChosen -= ShowPlayerPieces;
        EventManager.OnPieceSelected -= ManageSelectedPiece;
        EventManager.OnRemoveSelectedPiece -= RemoveSelectedPiece;
        EventManager.OnPieceAddedToBoard -= OnPieceAddedToBoard;
        EventManager.OnGameModeManagerSet -= SetGameModeManager;
        EventManager.OnFirstPlayerSelected -= CreatePieces;
    }
    private void CreatePieces()
    {
        foreach (var pieceData in pieceCollectionData.pieceCollection)
        {
            Piece piece = PieceFactory.CreatePiece(basePiece, piecePanel, pieceData, null, _gameModeManager);
            piece.gameObject.SetActive(false);
            _initialPieces.Add(piece);
        }
    }
    
    private void SetGameModeManager(GameModeManager gameModeManager)
    {
        _gameModeManager = gameModeManager;
    }

    private void ShowPlayerPieces(Player player)
    {
        _currentPlayer = player;
        foreach (var piece in _initialPieces)
        {
            if (player.GetAvailablePieces().ContainsKey(piece.PieceName))
            {
                piece.gameObject.SetActive(true);
                piece.ChangePieceSprite(player.Id);
            }
            else
            {
                piece.gameObject.SetActive(false);
            }
        }
    }

    private void ManageSelectedPiece(Piece selectedPiece)
    {
        if (_gameModeManager.GetCurrentGameMode() == GameModes.Normal)
        {
            foreach (var piece in _initialPieces)
            {
                piece.SetSelection(selectedPiece == piece);
            }
        }
        else
        {
            foreach (var piece in _allPiecesOnBoard)
            {
                piece.SetSelection(piece == selectedPiece);
            }
        }

        EventManager.TriggerOnSetSelectedPiece(selectedPiece);  
    }

    private void RemoveSelectedPiece(Piece selectedPiece)
    {
        foreach (var piece in _initialPieces)
        {
            piece.SetSelection(false);
        }
        
        _currentPlayer.RemovePiece(selectedPiece);
    }

    private void OnPieceAddedToBoard(Piece newPiece)
    {
        _allPiecesOnBoard.Add(newPiece);
    }
}
