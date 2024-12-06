
public class PlayerActionHandler
{
    private static Player _currentPlayer;
    
    public PlayerActionHandler()
    {
        EventManager.OnTakeActionFromCurrentPlayer += RemoveActionFromPlayer;
        EventManager.OnPlayerChosen += SetCurrentPlayer;
    }

    public void CleanUpEvents()
    {
        EventManager.OnTakeActionFromCurrentPlayer -= RemoveActionFromPlayer;
        EventManager.OnPlayerChosen -= SetCurrentPlayer;
    }

    private void SetCurrentPlayer(Player player)
    {
        _currentPlayer = player;
    }
    
    public static bool PlayerHasAction()
    {
        return _currentPlayer.ActionCount > 0;
    }
    
    private void RemoveActionFromPlayer()
    {
        _currentPlayer.RemoveAction();
    }
}
