using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    public Camera CameraObject;

    public float CameraRotateXDeg = 10f;
    public float CameraRotateYDeg = 10f;
    public float SmoothingTime = 10f;
    public float FOVAnimationSpeed = 5f;

    private bool fovAnimating = false;
    private Quaternion target;

    public void AnimateFOV()
    {
        StartCoroutine(FOVAnimation());
    }

    IEnumerator FOVAnimation()
    {
        fovAnimating = true;
        while (true)
        {
            CameraObject.fieldOfView += FOVAnimationSpeed * Time.deltaTime;
            yield return null;
        }
    }
    
    private void Update()
    {
        if (!fovAnimating)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector2 mousePercentPos = new Vector2(Mathf.Clamp01(mousePos.x / Screen.width), Mathf.Clamp01(mousePos.y / Screen.height));
            mousePercentPos.x = mousePercentPos.x - 0.5f;

            target = Quaternion.Euler(-mousePercentPos.y * CameraRotateXDeg, mousePercentPos.x * CameraRotateYDeg, 0);
        }
        else
            target = Quaternion.identity;
        
        CameraObject.transform.localRotation = Quaternion.Slerp(CameraObject.transform.localRotation, target, Time.deltaTime * SmoothingTime);

    }
}
