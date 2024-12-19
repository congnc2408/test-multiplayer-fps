using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OCScope : MonoBehaviour
{
    //Aiming
    [Header("Aiming")]
    public Vector3 normalLocalPosition;
    public Vector3 aimingLocalPosition;

    public Quaternion normalLocalQuaternion;
    public Quaternion aimingLocalQuaternion;

    public float aimSmoothing = 10;
    public Camera cameraScope;
    public Image imgCrosshair;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DetermineAim();
    }

    private void DetermineAim()
    {
        //Vector3 target = normalLocalPosition;
        Quaternion target = normalLocalQuaternion;
        if (Input.GetMouseButton(1))
        {
            target = aimingLocalQuaternion;
            // target = aimingLocalPosition;
            cameraScope.fieldOfView = 25;
            imgCrosshair.enabled = false;
        }
        else
        {
            cameraScope.fieldOfView = 60;
            imgCrosshair.enabled = true;
        }
        //Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);
        //transform.localPosition = desiredPosition;
        Quaternion desiredQuaternion = Quaternion.Lerp(transform.localRotation, target, Time.deltaTime * aimSmoothing);
    }
}
