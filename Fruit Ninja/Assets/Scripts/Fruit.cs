using UnityEngine;

public class Fruit : MonoBehaviour
{

    private Rigidbody _mainRigidBody;
    private Collider _sliceTrigger;

    public GameObject Whole;
    public GameObject Sliced;

    public Rigidbody TopPartRigidbody;
    public Rigidbody BottomPartRigidbody;

    void Start()
    {
        FillComponents();
    }

    private void FillComponents()
    {
        _mainRigidBody = GetComponent<Rigidbody>();
        _sliceTrigger = GetComponent<Collider>();  
    }

    public void Slice(Vector3 diraction, Vector3 position, float force)
    {
        SetSlice();
        RotateBySliceDiraction(diraction);
        AddForce(TopPartRigidbody, diraction, position, force);
        AddForce(BottomPartRigidbody, diraction, position, force);
    }

    private void SetSlice()
    {
        Whole.SetActive(false);
        Sliced.SetActive(true);
        _sliceTrigger.enabled = false;
    }

    private void RotateBySliceDiraction(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void AddForce(Rigidbody sliceRigidbody, Vector3 direction, Vector3 position, float force)
    {
        sliceRigidbody.velocity = _mainRigidBody.velocity;
        sliceRigidbody.angularVelocity = _mainRigidBody.angularVelocity;

        sliceRigidbody.AddForceAtPosition(direction * force, position);
    }
}   