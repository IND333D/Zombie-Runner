using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpscamera;
    RigidbodyFirstPersonController rb;
    [SerializeField] Vector2 fov;
    [SerializeField] Vector2 mousesens;
    [SerializeField] bool iszoomed = true;

    private void Start()
    {
        rb = GetComponentInParent<RigidbodyFirstPersonController>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (iszoomed == false)
            {
                iszoomed = true;
                fpscamera.fieldOfView = fov.y;
                rb.mouseLook.XSensitivity = mousesens.y;
                rb.mouseLook.YSensitivity = mousesens.y;
            }
            else
            {
                iszoomed = false;
                fpscamera.fieldOfView = fov.x;
                rb.mouseLook.XSensitivity = mousesens.x;
                rb.mouseLook.YSensitivity = mousesens.x;
            }
        }
    }
}
