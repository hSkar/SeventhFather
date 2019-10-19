using UnityEngine;
using System.Collections;

public class PortalBehaviour : MonoBehaviour
{
    public PortalBehaviour partner;
    public Camera myCamera;

    public RenderTexture texture;

    //Events

    void Awake()
    {
        //Create the render texture
        texture = new RenderTexture(Screen.width, Screen.height, 1);
        GetComponent<MeshRenderer>().material.mainTexture = texture;

        partner.myCamera.targetTexture = texture;
    }

    void Update()
    {
        RotateCamera();
    }

    //Misc methods

    private void RotateCamera()
    {
        Transform playerCam = Camera.main.transform;
        Transform camTrans = myCamera.transform;
        Transform partnerTrans = partner.transform;

        //Find the position of the camera
        Vector3 pos = partnerTrans.InverseTransformPoint(playerCam.position);
        camTrans.localPosition = new Vector3(-pos.x, pos.y, -pos.z);

        //Find the rotation
        Vector3 euler = Vector3.zero;
        euler.y = SignedAngle(-partnerTrans.forward, playerCam.forward, Vector3.up);
        //TODO: Find the z-rotation

        camTrans.localRotation = Quaternion.Euler(euler);

    }

    private float SignedAngle(Vector3 a, Vector3 b, Vector3 n)
    {
        //Code stolen from DiegoSLTS
        //http://answers.unity3d.com/questions/992289/portal-effect-using-render-textures-how-should-i-m.html

        // angle in [0,180]
        float angle = Vector3.Angle(a, b);
        float sign = Mathf.Sign(Vector3.Dot(n, Vector3.Cross(a, b)));

        // angle in [-179,180]
        float signed_angle = angle * sign;

        while (signed_angle < 0) signed_angle += 360;

        return signed_angle;
    }
}