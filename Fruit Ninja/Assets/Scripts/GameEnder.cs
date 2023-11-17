using TMPro;
using UnityEngine;

public class GameEnder : MonoBehaviour
{
    // ������ �������� �����
    public Score Score;

    // ������ �������� ������
    public Health Health;

    // ������ ������������� ������� � ����
    public FruitSpawner FruitSpawner;

    // ������ �������� ������
    public GameObject GameScreen;

    // ������ ������ ����� ����
    public GameObject GameEndScreen;

    // ������� � �������� ������
    public TextMeshProUGUI GameEndScoreText;

    private void Start()
    {
        // ��� ������� ���� ���������� ������� �����
        SwitchScreens(true);
    }
    
    private void SwitchScreens(bool isGame)
    {
        // �������� ��� ��������� ������� ����� � ����������� �� �������� isGame
        GameScreen.SetActive(isGame);

        // �������� ��� ��������� ����� ����� ���� � ����������� �� �������� isGame
        GameEndScreen.SetActive(!isGame);
    }

    public void EndGame()
    {
        // ���������� ��������� ������� � ����
        FruitSpawner.Stop();

        // �������� � ������������� �������� ����
        SetGameEndScoreText(Score.GetScore());

        // ������������� �� ����� ����� ����
        SwitchScreens(false);
    }

    public void RestartGame()
    {
        // �������� ������� �����
        Score.Restart();

        // ��������������� ������� ������
        Health.Restart();

        // ������������� ��������� ������� � ����
        FruitSpawner.Restart();

        // ������������ �� ������� �����
        SwitchScreens(true);
    }

    private void SetGameEndScoreText(int value)
    {
        GameEndScoreText.text = $"�� ������� {value} �����!";
    }
}