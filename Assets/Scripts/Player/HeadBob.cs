using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBob : MonoBehaviour
{
    public float bobFrequency = 1.5f;
    public float bobHeight = 0.05f;
    public float bobSwayAngle = 0.5f;
    public float bobSideMovement = 0.05f;
    public float bobSpeedMultiplier = 1f;
    public Transform cameraTransform;
    public Transform cameraHolder;

    private float timer = 0;
    private Vector3 initialCameraPosition;

    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        initialCameraPosition = cameraTransform.localPosition;
    }

    void Update()
    {
        if (Time.timeScale == 0)
            return;

        float waveslice = Mathf.Sin(timer);
        timer += bobFrequency * bobSpeedMultiplier * Time.deltaTime;

        if (timer > Mathf.PI * 2)
        {
            timer -= Mathf.PI * 2;
        }

        if (Mathf.Abs(waveslice) > 0)
        {
            float translateChange = waveslice * bobHeight;
            float totalAxes = Mathf.Abs(waveslice);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);

            float swayAngle = waveslice * bobSwayAngle;
            float sideMovement = waveslice * bobSideMovement;

            Vector3 newCameraPosition = initialCameraPosition;
            newCameraPosition.y += translateChange;
            newCameraPosition.x += sideMovement;

            cameraTransform.localPosition = newCameraPosition;
            cameraTransform.localRotation = Quaternion.Euler(new Vector3(cameraTransform.localRotation.eulerAngles.x, cameraTransform.localRotation.eulerAngles.y, swayAngle));
        }
        else
        {
            cameraTransform.localPosition = initialCameraPosition;
        }
    }
}
