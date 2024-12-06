using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerManager 
{
    private List<IPlayer> _players = new();
    private IPlayer _currentPlayer;
    private GameModeManager _gameModeManager;

    public PlayerManager()
    {
        EventManager.OnNextPlayerShow += GetNextPlayer;
    }

    public void Initialize(GameModeManager gameModeManager)
    {
        _gameModeManager = gameModeManager;
    }
    
    public void CleanUpEvents()
    {
        EventManager.OnNextPlayerShow -= GetNextPlayer;
    }
    
    public void CreatePlayers(GameConfig gameConfig, PieceCollectionScriptable pieceCollectionData)
    {
        for (int i = 0; i < gameConfig.numberOfPlayers; i++)
        {
            Player player = new Player((uint)i + 1, pieceCollectionData.pieceCollection);
            _players.Add(player);
        }
        
        ChooseStartingPlayer();
    }

    private void ChooseStartingPlayer()
    {
        int randomIndex = Random.Range(0, _players.Count);
        _currentPlayer = _players[randomIndex];
        _currentPlayer.SetCurrent(true);
        EventManager.TriggerOnFirstPlayerSelected();
        EventManager.TriggerOnPlayerChosen((Player)_currentPlayer);
        EventManager.TriggerOnActionTextChange($"Player {_currentPlayer.Id} Choose your piece");
        Debug.Log($"Starting Player: {_currentPlayer.Id}");
    }

    private void GetNextPlayer()
    {
        _currentPlayer.SetCurrent(false);
        int index = _players.IndexOf(_currentPlayer);
        _currentPlayer = _players[(index + 1) % _players.Count];
        _currentPlayer.SetCurrent(true);
        EventManager.TriggerOnPlayerChosen((Player)_currentPlayer);
        EventManager.TriggerOnActionTextChange($"Player {_currentPlayer.Id} Choose your piece");
        Debug.Log($"Next Player: {_currentPlayer.Id}");
        
        if (_currentPlayer == _players[0])
        {
            EventManager.TriggerOnResetAllActions();
        }

        if (_gameModeManager.GetCurrentGameMode() != GameModes.Dynamic) return;
        
        int validMoves = 0;
        foreach (var piece in _currentPlayer.GetPiecesOnBoard())
        {
            if (piece.HasValidMoves())
            {
                validMoves++;
            }
        }

        if (validMoves <= 0)
        {
            GetNextPlayer();
        }
    }

    public List<IPlayer> GetPlayers()
    {
        return _players;
    }
}
