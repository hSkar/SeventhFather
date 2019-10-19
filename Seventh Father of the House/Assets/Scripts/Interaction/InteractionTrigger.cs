using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class InteractionTrigger : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    private void OnTriggerEnter(Collider other)
    {
        OnEnter?.Invoke();
        TriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnExit?.Invoke();
        TriggerExit(other);
    }

    protected virtual void TriggerEnter(Collider coll) { }
    protected virtual void TriggerExit(Collider coll) { }
}
