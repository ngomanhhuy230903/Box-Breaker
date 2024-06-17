using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameoverText;
    public Button restartButton;
    public GameObject sceneTitle;
    private AudioSource playderAudio;
    public AudioClip backgroundSound;
    private float spawnRate = 2f;
    public bool isGameOver = false;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        playderAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnTarget()
    {
        while(!isGameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    public void UpdateScore(int addScore)
    {
        score += addScore;
        scoreText.text = "Score : " + score;
        if(score < 0)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        gameoverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        playderAudio.Pause();
        isGameOver = true;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StarGame(int difficultLevel)
    {
        isGameOver = false;
        score = 0;
        spawnRate /= difficultLevel;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        sceneTitle.gameObject.SetActive(false);
        playderAudio.Play();
    }
}
