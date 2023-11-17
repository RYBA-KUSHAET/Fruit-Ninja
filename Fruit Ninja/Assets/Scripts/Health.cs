using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int _currentHealth;
    
    private TextMeshProUGUI _healthText;

    public int StartHealth = 3;
    void Start()
    {
        FillComponents();
        SetHealth(StartHealth);
    }

    private void FillComponents()
    {
        _healthText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void RemoveHealth()
    {
        SetHealth(_currentHealth - 1);
    }

    private void SetHealth(int value)
    {
        _currentHealth = value;
        SetHealthText(value);
    }

    private void SetHealthText(int value)
    {
        _healthText.text = value.ToString();
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }
    public void Restart()
    {
        // Восстанавливаем счётчик жизней
        SetHealth(StartHealth);
    }

}
