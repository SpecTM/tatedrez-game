using System.Collections.Generic;
using UnityEngine;

public enum GameModes
{
    None,
    Normal,
    Dynamic
}

public class GameModeManager
{
    private List<IPlayer> _players;
    private GameModes _currentGameMode = GameModes.Normal;

    public void SetPlayers(List<IPlayer> players)
    {
        EventManager.OnPlayerChosen += CheckForDynamicMode;
        _players = players;
    }
    
    public void CleanUpEvents()
    {
        EventManager.OnPlayerChosen -= CheckForDynamicMode;
    }

    public GameModes GetCurrentGameMode()
    {
        return _currentGameMode;
    }
    
    private void CheckForDynamicMode(IPlayer chosenPlayer)
    {
        if (_currentGameMode != GameModes.Dynamic)
        {
            int playersWithoutPieces = 0;
        
            foreach (var player in _players)
            {
                if (player.GetAvailablePieces().Count > 0) continue;
                playersWithoutPieces++;

                if (playersWithoutPieces >= _players.Count)
                {
                    Debug.Log("Entering Dynamic Mode");
                    EventManager.TriggerOnEnterDynamicMode();
                    _currentGameMode = GameModes.Dynamic;
                
                }
            }   
        }
    }
}
