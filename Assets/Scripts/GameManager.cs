using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int scorePlayer1 = 0;
    public int scorePlayer2 = 0;

    public int pointsToIncreaseSpeed = 2;

    public float speedIncrement = 0.1f;

    public int maxScore = 2;

    private KeyCode inputKeySpace = KeyCode.Space;

    private bool startGame = false;

    private bool timerActive = false;

    private float timer = 0f;




    public Ball ball;
    public Text score;

    private void Update()
    {


        if (Input.GetKey(inputKeySpace) && !timerActive)
        {
            startGame = true;
        }

        if (timerActive)
        {
            startGame = false;

            timer += Time.deltaTime;

            if (timer >= 2f)
            {
                Debug.Log("2 segundos se passaram!");

                startGame = true;
                timer = 0f;
                timerActive = false;
            }
        }

        if (!startGame)
            return;

        ball.Move();

        float screenLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        float screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;

        if (ball.transform.position.x + 0.25f < screenLeft)
        {
            AddScore(2);
            ball.ResetPosition();
        }

        else if (ball.transform.position.x - 0.25f > screenRight)
        {
            AddScore(1);
            ball.ResetPosition();

        }

        RestartScene();

    }

    private void AddScore(int player)
    {

        if (player == 1)
            scorePlayer1++;
        if (player == 2)
            scorePlayer2++;

        if ((scorePlayer1 + scorePlayer2) % pointsToIncreaseSpeed == 0)
            ball.speed += speedIncrement;

        score.text = $"{scorePlayer1} X {scorePlayer2}";

        timerActive = true;

    }

    private void RestartScene()
    {
        if (scorePlayer1 + scorePlayer2 >= maxScore)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            startGame = false;

        }



    }

}
