using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatherSevenBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField]
    private float _force = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    [ContextMenu("Knock")]
    public void Knock()
    {
        rb.isKinematic = false;
        rb.AddForce((Vector3.forward + Vector3.up).normalized * _force, ForceMode.Impulse);
        rb.AddTorque(Vector3.forward, ForceMode.Impulse);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Knock();
        }
    }
}
