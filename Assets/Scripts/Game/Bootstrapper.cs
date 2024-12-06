using UnityEngine;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private GameConfig gameConfig;
    [SerializeField] private PieceCollectionScriptable pieceCollectionData;
    
    private PlayerManager _playerManager;
    private PlayerActionHandler _playerActionHandler;
    private GameModeManager _gameModeManager;

    private void Start()
    {
        _playerManager = new PlayerManager();
        _playerActionHandler = new PlayerActionHandler();
        
        _gameModeManager = new GameModeManager();
        _playerManager.Initialize(_gameModeManager);
        
        EventManager.TriggerOnGameModeManagerSet(_gameModeManager);
        _playerManager.CreatePlayers(gameConfig, pieceCollectionData);
        _gameModeManager.SetPlayers(_playerManager.GetPlayers());
    }

    private void OnDisable()
    {
        _playerManager?.CleanUpEvents();
        _playerActionHandler?.CleanUpEvents();
        _gameModeManager?.CleanUpEvents();
    }
}
