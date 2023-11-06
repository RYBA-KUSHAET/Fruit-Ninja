using UnityEngine;

public class FruitSpawner : MonoBehaviour
{

    private float _currentDelay = 0f;

    public float minDelay = 0.2f;
    public float maxDelay = 0.9f;
    public float angleRangeZ = 20f;
    public float lifeTime = 7f;
    public float minForce = 7f;
    public float maxForce = 15f;

    public GameObject FruitPrefab1;
    public GameObject FruitPrefab2;
    public GameObject FruitPrefab3;

    // Start is called before the first frame update
    void Start()
    {
        SetNewDelay();
    }

    private void SetNewDelay()
    {
        _currentDelay = Random.Range(minDelay, maxDelay);
    }

    // Update is called once per frame
    void Update()
    {
        MoveDelay();
    }
    private void MoveDelay()
    {
        _currentDelay -= Time.deltaTime;
        if (_currentDelay < 0 )
        {
            SpawnFruit();
            SetNewDelay();
        }
    }

    private void SpawnFruit()
    {
        Quaternion startRotation = Quaternion.Euler(0f, 0f, Random.Range(-angleRangeZ, angleRangeZ));
        GameObject newFruit = Instantiate(GetRandomFruit(), transform.position, startRotation);
        Destroy(newFruit, lifeTime);
        AddForce(newFruit);
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
    
}
