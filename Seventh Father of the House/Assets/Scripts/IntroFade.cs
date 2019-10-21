using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
public class IntroFade : MonoBehaviour
{
    private IEnumerator Start()
    {
        SteamVR_Fade.Start(new Color(0, 0, 0, 1), 0);
        yield return new WaitForEndOfFrame();
        SteamVR_Fade.Start(new Color(0, 0, 0, 0), 5f);
    }
}
