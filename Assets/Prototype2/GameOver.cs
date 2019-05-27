using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject gameOver;

    public void GameEnd(int playerID)
    {
        gameOver.SetActive(true);
        GameObject.Find("GameOver").GetComponent<Text>().text = "Player " + playerID.ToString() + " Loses";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
