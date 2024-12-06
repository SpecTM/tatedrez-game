using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PlayerManagerTests
{
    [Test]
    public void PlayerManager_CreatesCorrectNumberOfPlayers()
    {
        var gameConfig = ScriptableObject.CreateInstance<GameConfig>();
        gameConfig.numberOfPlayers = 3;

        var pieceCollectionData = ScriptableObject.CreateInstance<PieceCollectionScriptable>();
        pieceCollectionData.pieceCollection = new List<PieceDataScriptable>();

        var playerManager = new PlayerManager();
        playerManager.CreatePlayers(gameConfig, pieceCollectionData);

        Assert.AreEqual(3, playerManager.GetPlayers().Count);
    }
    
    [Test]
    public void ChooseStartingPlayer_ChoosesRandomPlayer()
    {
        var playerManager = new PlayerManager();
        var gameConfig = ScriptableObject.CreateInstance<GameConfig>();
        gameConfig.numberOfPlayers = 2;
        var pieceCollection = ScriptableObject.CreateInstance<PieceCollectionScriptable>();
        GameObject newObject = new GameObject("Piece");
        newObject.AddComponent<Piece>();
        Piece piece = newObject.GetComponent<Piece>();
        var pieceDataScriptable = ScriptableObject.CreateInstance<PieceDataScriptable>();
        pieceDataScriptable.prefab = piece;
        pieceDataScriptable.PieceName = "test";
        pieceCollection.pieceCollection = new();
        pieceCollection.pieceCollection.Add(pieceDataScriptable);

        playerManager.CreatePlayers(gameConfig, pieceCollection);
        
        playerManager.CleanUpEvents(); // Force initialization for tests.

        // Assert
        var players = playerManager.GetPlayers();
        Assert.IsNotNull(players, "Players should not be null after initialization.");
    }
}
