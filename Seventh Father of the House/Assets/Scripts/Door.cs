using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public MeshRenderer rend;
    public Collider coll;


    private void OnValidate()
    {
        if (rend == null) rend = GetComponent<MeshRenderer>();
        if (coll == null) coll = GetComponent<Collider>();
    }

    public void ToggleDoor(bool enable)
    {
        rend.enabled = enable;
        coll.enabled = enable;
    }
}
