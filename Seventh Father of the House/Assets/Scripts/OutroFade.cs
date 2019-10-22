using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class OutroFade : MonoBehaviour
{
    [SerializeField]
    private float _fadeOutDuration;

    [ContextMenu("FadeOut")]
    public void FadeOut()
    {
        SteamVR_Fade.Start(Color.black, 5f);
    }
}
