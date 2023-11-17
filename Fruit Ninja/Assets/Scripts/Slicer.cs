using UnityEngine;

public class Slicer : MonoBehaviour
{
    public float SliceForce = 65;
    private const float MinSlicingMove = 0.01f;
    private Collider _slicerTrigger;
    private Camera _mainCamera;
    private Vector3 _direction;
    // Скрипт счётчика очков
    public Score Score;
    // Скрипт счётчика жизней
    public Health Health;
    public GameEnder GameEnder;

    void Start()
    {
        Init();
    }

    
    void Update()
    {
        Slicing();
    }

    private void Init()
    {     
        _slicerTrigger = GetComponent<Collider>();       
        _mainCamera = Camera.main;
        SetSlicing(false);
    }

    private void Slicing()
    {
        if (Input.GetMouseButton(0))
        {
            RefreshSlicing();
        }

        if (Input.GetMouseButtonUp(0))
        {
            SetSlicing(false);
        }
    }

    private void SetSlicing(bool value)
    {
        _slicerTrigger.enabled = value;
    }

    private void RefreshSlicing()
    {
        Vector3 targetPosition = GetTargetPosition();
        RefreshDirection(targetPosition);
        MoveSlicer(targetPosition);
        bool isSlicing = CheckMoreThenMinMove(_direction);
        SetSlicing(isSlicing);
    }

    private Vector3 GetTargetPosition()
    {
        Vector3 targetPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0;
        return targetPosition;
    }

    private void RefreshDirection(Vector3 targetPosition)
    {
        _direction = targetPosition - transform.position;
    }

    private void MoveSlicer(Vector3 targetPosition)
    {
        transform.position = targetPosition;
    }

    private bool CheckMoreThenMinMove(Vector3 direction)
    {
        float slicingSpeed = direction.magnitude / Time.deltaTime;
        return slicingSpeed >= MinSlicingMove;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, является ли объект фруктом
        CheckFriut(other);

        // Проверяем, является ли объект бомбой
        CheckBomb(other);
    }

    private void CheckFriut(Collider other)
    {
        // Создаём переменную для фрукта, которого мы коснулись
        Fruit fruit = other.GetComponent<Fruit>();

        // Проверяем, является ли объект фруктом
        // Здесь также можно написать if (!fruit)
        if (fruit == null)
        {
            // Если объект — не фрукт, выходим из метода
            return;
        }

        // Режем фрукт в заданном направлении с учётом позиции курсора и силы разрезания
        fruit.Slice(_direction, transform.position, SliceForce);

        // Получаем одно очко
        Score.AddScore(1);
    }

    private void CheckBomb(Collider other)
    {
        Bomb bomb = other.GetComponent<Bomb>();
        if (bomb == null) // тут можно было написать if(!bomb)
        {
            return;
        }

        Destroy(bomb.gameObject);
        Health.RemoveHealth();
        CheckHealthEnd(Health.GetCurrentHealth());

    }


    private void CheckHealthEnd(int health)
    {
        // Если количество жизней больше нуля
        if (health > 0)
        {
            // Возвращаемся из метода, игра продолжается
            return;
        }

        // Иначе вызываем метод StopGame()
        StopGame();
    }

    private void StopGame()
    {
        GameEnder.EndGame();
    }
}
