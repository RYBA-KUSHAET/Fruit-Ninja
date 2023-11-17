using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    private float _currentDelay = 0f;

    private Collider _spawnZone;

    public float minDelay = 0.2f;
    public float maxDelay = 0.9f;
    public float angleRangeZ = 20f;
    public float lifeTime = 7f;
    public float minForce = 7f;
    public float maxForce = 15f;
    public float BombChance = 0.1f;

    public GameObject FruitPrefab1;
    public GameObject FruitPrefab2;
    public GameObject FruitPrefab3;
    public GameObject BombPrefab;

    private bool _isActive = true;


    // Start is called before the first frame update
    void Start()
    {
        FillComponents();
        SetNewDelay();
    }

    private void FillComponents()
    {
        _spawnZone = GetComponent<Collider>();
    }

    private void SetNewDelay()
    {
        _currentDelay = Random.Range(minDelay, maxDelay);
    }

    public void Stop()
    {
        _isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isActive)
        {
            return;
        }
        MoveDelay();
    }
    private void MoveDelay()
    {
        _currentDelay -= Time.deltaTime;
        if (_currentDelay <= 0 )
        {
            float random = Random.value;
            if (random < BombChance)
            {
                SpawnBomb();
            }
            else
            {
                SpawnFruit();
            }
            SpawnFruit();
            SetNewDelay();
        }
    }

    private void SpawnFruit()
    {
        GameObject fruitPrefab = GetRandomFruit();
        SpawnObject(fruitPrefab);
    }

    private void SpawnBomb()
    {
        SpawnObject(BombPrefab);
    }

    private void SpawnObject(GameObject prefab)
    {
        Vector3 StartPosition = GetRandomSpawnPosition();
        Quaternion startRotation = Quaternion.Euler(0f, 0f, Random.Range(-angleRangeZ, angleRangeZ));
        GameObject newObject = Instantiate(prefab, StartPosition, startRotation);
        Destroy(newObject, lifeTime);
        AddForce(newObject);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 pos;
        pos.x = Random.Range(_spawnZone.bounds.min.x, -_spawnZone.bounds.max.x);
        pos.y = Random.Range(_spawnZone.bounds.min.y, -_spawnZone.bounds.max.y);
        pos.z = Random.Range(_spawnZone.bounds.min.z, -_spawnZone.bounds.max.z);
        return pos;
    }

    private GameObject GetRandomFruit()
    {
        int r = Random.Range(1, 4);
        if(r == 1)
        {
            return FruitPrefab1;
        }
        else if (r == 2)
        {
            return FruitPrefab2;
        }
        else
        {
            return FruitPrefab3;
        }
    }

    private void AddForce(GameObject fruit)
    {
        float force = Random.Range(minForce, maxForce);
        fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);
    }

    public void Restart()
    {
        _isActive = true;
        SetNewDelay();
    }
    
}
