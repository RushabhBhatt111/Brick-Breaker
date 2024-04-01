using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ball Ball {  get; private set; }

    public paddle paddle { get; private set; }

    public Brick[] brick { get; private set; }

    public int score = 0;
    public int lives = 3;
    public int level = 1;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += onLevelLoaded;
    }


    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;

        LoadLevel(1);
    }

    private void LoadLevel(int level)
    {
        this.level = level;

        SceneManager.LoadScene("Level" + level);
    }

    private void onLevelLoaded(Scene scene,LoadSceneMode mode)
    {
        this.Ball = FindAnyObjectByType<ball>();
        this.paddle = FindAnyObjectByType<paddle>();
        this.brick = FindObjectsOfType<Brick>();
    }

    public void Hit(Brick brick)
    {
        this.score += brick.points;
        if (Cleared())
        {
            LoadLevel(this.level + 1);
        }
    }

    private bool Cleared()
    {
        for (int i = 0; i < brick.Length; i++)
        {
            if (this.brick[i].gameObject.activeInHierarchy && !this.brick[i].unbreakable)
            {
                return false;
            }
        }
        return true;
    }

    private void Resetlevel()
    {
        this.Ball.ResetBall();
        this.paddle.ResetPaddle();
    }

    private void GameOver()
    {
        NewGame();
    }

    public void Miss()
    {
        this.lives--;
        if(this.lives > 0)
        {
            Resetlevel();
        }
        else
        {
            GameOver();
        }
    }
}
