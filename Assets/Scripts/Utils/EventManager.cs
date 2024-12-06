using System;
using System.Collections.Generic;

public static class EventManager //In a bigger project we would likely have different event managers
{
    public static event Action<Piece> OnPieceSelected;
    public static event Action<Piece> OnRemoveSelectedPiece;
    public static event Action<Player> OnPlayerChosen;
    public static event Action OnNextPlayerShow;
    public static event Action OnEnterDynamicMode;
    public static event Action OnTakeActionFromCurrentPlayer;
    public static event Action OnResetAllActions;
    public static event Action<Cell> OnCellPress;
    public static event Action<List<Cell>> OnMarkAvailableCell;
    public static event Action<Piece> OnSetSelectedPiece;
    public static event Action OnStopCellFeedback;
    public static event Action<Piece> OnPieceAddedToBoard;
    public static event Action<string> OnActionTextChange;
    public static event Action<GameModeManager> OnGameModeManagerSet;
    public static event Action OnFirstPlayerSelected;
    public static event Action<uint> OnTicTacToeAchieved;

    public static void TriggerOnPieceSelected(Piece piece) => OnPieceSelected?.Invoke(piece);
    public static void TriggerOnRemoveSelectedPiece(Piece piece) => OnRemoveSelectedPiece?.Invoke(piece);
    public static void TriggerOnPlayerChosen(Player player) => OnPlayerChosen?.Invoke(player);
    public static void TriggerOnNextPlayerShow() => OnNextPlayerShow?.Invoke();
    public static void TriggerOnTakeActionFromCurrentPlayer() => OnTakeActionFromCurrentPlayer?.Invoke();
    public static void TriggerOnResetAllActions() => OnResetAllActions?.Invoke();
    public static void TriggerOnCellPress(Cell cell) => OnCellPress?.Invoke(cell);
    public static void TriggerOnMarkAvailableCell(List<Cell> availableCells) => OnMarkAvailableCell?.Invoke(availableCells);
    public static void TriggerOnEnterDynamicMode() => OnEnterDynamicMode?.Invoke();
    public static void TriggerOnSetSelectedPiece(Piece piece) => OnSetSelectedPiece?.Invoke(piece);
    public static void TriggerStopCellFeedback() => OnStopCellFeedback?.Invoke();
    public static void TriggerOnPieceAddedToBoard(Piece piece) => OnPieceAddedToBoard?.Invoke(piece);
    public static void TriggerOnActionTextChange(string text) => OnActionTextChange?.Invoke(text);
    public static void TriggerOnGameModeManagerSet(GameModeManager gameModeManager) => OnGameModeManagerSet?.Invoke(gameModeManager);
    public static void TriggerOnFirstPlayerSelected() => OnFirstPlayerSelected?.Invoke();
    public static void TriggerOnTicTacToeAchieved(uint value) => OnTicTacToeAchieved?.Invoke(value);
}
