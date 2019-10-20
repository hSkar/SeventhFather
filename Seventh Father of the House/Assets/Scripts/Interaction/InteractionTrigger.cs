using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class InteractionTrigger : MonoBehaviour
{
    public bool playOnce;
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    private bool _hasEntered = false;
    private bool _hasExited = false;

    private void OnTriggerEnter(Collider other)
    {
        if (playOnce && _hasEntered)
            return;

        OnEnter?.Invoke();
        TriggerEnter(other);

        if (playOnce)
            _hasEntered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (playOnce && _hasExited)
            return;

        OnExit?.Invoke();
        TriggerExit(other);

        if (playOnce)
            _hasExited = true;
    }

    protected virtual void TriggerEnter(Collider coll) { }
    protected virtual void TriggerExit(Collider coll) { }
}
