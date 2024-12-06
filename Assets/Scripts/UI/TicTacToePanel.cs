using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TicTacToePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        EventManager.OnTicTacToeAchieved += EnablePanel;
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    private void OnDisable()
    {
        EventManager.OnTicTacToeAchieved -= EnablePanel;
    }

    private void EnablePanel(uint playerId)
    {
        winText.text = $"Player {playerId} has achieved Tic Tac Toe!";
        panel.SetActive(true);
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
