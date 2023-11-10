using UnityEngine;

public class Slicer : MonoBehaviour
{
    public float SliceForce = 65;
    private const float MinSlicingMove = 0.01f;
    private Collider _slicerTrigger;
    private Camera _mainCamera;
    private Vector3 _direction;

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

        Fruit fruit = other.GetComponent<Fruit>();
        if (fruit == null)
        {
            return;
        }

        fruit.Slice(_direction, transform.position, SliceForce);
    }
}
