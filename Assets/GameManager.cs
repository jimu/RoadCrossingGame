using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

enum GameState { Error, Menu, Play, Paused, Win, Lose};

#pragma warning disable 649,414
[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    int score;
    GameState state;
    [SerializeField] UnityEngine.UI.Text scoreText;
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject winPanel;
    AudioSource audioSource;
    [SerializeField] AudioClip sfxLose, sfxWin, sfxCoin, sfxClick;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        SetScore(0);
        SetGameState(GameState.Play);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int value)
    {
        PlayOneShot(sfxCoin);
        SetScore(score + value);
    }

    void SetScore(int value)
    {
        score = value;
        ShowScore();
    }

    void ShowScore()
    {
        scoreText.text = score.ToString();
    }

    public bool IsPlaying()
    {
        return state == GameState.Play;
    }
    public void Lose()
    {
        PlayOneShot(sfxLose);
        SetGameState(GameState.Lose);
    }
    public void Win()
    {
        SetGameState(GameState.Win);
        PlayOneShot(sfxWin);
    }

    void SetGameState(GameState state)
    {
        this.state = state;

        Time.timeScale = state == GameState.Play ? 1f : 0f;
        losePanel.SetActive(state == GameState.Lose);
        winPanel.SetActive(state == GameState.Win);
    }

    public void PlayOneShot(AudioClip sfx)
    {
        audioSource.PlayOneShot(sfx);
    }

    public void OnRestartButtonPressed()
    {
        PlayOneShot(sfxClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
