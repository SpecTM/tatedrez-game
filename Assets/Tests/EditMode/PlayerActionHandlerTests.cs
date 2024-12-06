using System.Collections.Generic;
using NUnit.Framework;

public class PlayerActionHandlerTests
{
    [Test]
    public void PlayerActionHandler_PlayerHasAction_ReturnsTrue()
    {
        PlayerActionHandler playerActionHandler = new PlayerActionHandler();
        var player = new Player(1, new List<PieceDataScriptable>());
        EventManager.TriggerOnPlayerChosen(player);

        Assert.IsTrue(PlayerActionHandler.PlayerHasAction());
    }
}
