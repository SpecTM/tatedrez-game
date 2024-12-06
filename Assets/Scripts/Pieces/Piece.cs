using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class Piece : MonoBehaviour, IPiece
{
   [SerializeField] private Image image;
   private Button _button;
   private string _pieceName;
   private bool _removeTouchListener;
   protected Board _board;
   protected Cell _cell;
   protected bool _isSelected;
   public PieceDataScriptable PieceDataScriptable { get; private set; }
   public Player Owner { get; private set; }
   private Player _currentPlayer;
   private GameModeManager _gameModeManager;
   public string PieceName => _pieceName;
   
   private void OnDestroy()
   {
      EventManager.OnEnterDynamicMode -= SetRemoveListener;
      EventManager.OnPlayerChosen -= SetCurrentPlayer;
      _button.onClick.RemoveAllListeners();
   }

   public void Initialize(PieceDataScriptable pieceDataScriptable, Player owner, GameModeManager gameModeManager)
   {
      EventManager.OnEnterDynamicMode += AddTouchListener;
      EventManager.OnEnterDynamicMode += SetRemoveListener;
      EventManager.OnPlayerChosen += SetCurrentPlayer;
      
      _button = GetComponent<Button>();
      AddTouchListener();
      _removeTouchListener = true;
      
      _gameModeManager = gameModeManager;
      PieceDataScriptable = pieceDataScriptable;
      _pieceName = PieceDataScriptable.PieceName;
      Owner = owner;

      if (Owner != null)
      {
         ChangePieceSprite(Owner.Id);  
      }
   }

   private void SetCurrentPlayer(Player player)
   {
      _currentPlayer = player;
   }
   
   public void AssignBoard(Board board)
   {
      _board = board;
   }

   public void ChangePieceSprite(uint playerId)
   {
      image.sprite = playerId == 1 ? PieceDataScriptable.whiteSprite : PieceDataScriptable.blackSprite;
   }

   private void OnPieceTouch()
   {
      if (_gameModeManager.GetCurrentGameMode() == GameModes.Dynamic)
      {
         if (_currentPlayer == Owner)
         {
            SelectPiece();
         }  
      }
      else
      {
         SelectPiece();
      }
   }

   protected virtual void SelectPiece()
   {
      if (_isSelected) return;
      Debug.Log("Piece press: " + _pieceName);
      EventManager.TriggerOnPieceSelected(this);
   }

   private void RemoveTouchListener()
   {
      if (_removeTouchListener)
      {
         _button.onClick.RemoveListener(OnPieceTouch);  
      }
   }

   private void AddTouchListener()
   {
      _button.onClick.AddListener(OnPieceTouch);
   }

   public void SetCell(Cell cell)
   {
      if (_cell != null)
      {
         _cell.SetPiece(null);
      }
      
      _cell = cell;
      cell.SetPiece(this);
      transform.SetParent(cell.transform);
      transform.position = cell.transform.position;
      ScaleDown();
      RemoveTouchListener();  
   }

   public void SetSelection(bool isSelected)
   {
      _isSelected = isSelected;
      AnimationHelper.Scale(transform ,_isSelected ? 1.3f : 1f, 0.15f, Ease.InQuad);
   }
   
   private void ScaleDown()
   {
      transform.DOScale(1f, 0.15f);
   }

   private void SetRemoveListener()
   {
      _removeTouchListener = false;
   }

   public bool HasValidMoves()
   {
      List<Cell> validCells = GetValidMoves(this, _cell, _board.Cells);

      return validCells.Count > 0;
   }

   protected virtual List<Cell> GetValidMoves(Piece piece, Cell currentCell, List<Cell> allCells)
   {
      return null;
   }
}
  