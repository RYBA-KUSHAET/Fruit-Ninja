using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{

    private int _score;

    private TextMeshProUGUI _scoreText;

    void Start()
    {
        FillComponents ();
        SetScore (0);
    }

    private void FillComponents()
    {
        _scoreText = GetComponentInChildren<TextMeshProUGUI> ();
    }

    public void AddScore(int value)
    {
        SetScore(_score + value);
    }

    private void SetScore(int value)
    {
        _score = value;
        SetScoreText(value);
    }

    private void SetScoreText(int value)
    {
        _scoreText.text = "Очки: " + value;
    }

    public int GetScore()
    {
        return _score;
    }

    public void Restart()
    {
        SetScore(0);
    }

}
