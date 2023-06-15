using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchManager : MonoBehaviour
{
    public CameraRay cameraRay;
    public Animator doorAnim;
    public DoBt doBt;

    private PlayerInput playerInput;
    //private InputAction getPositionAct;
    private InputAction pressScreenAct;
    private InputAction shootAct;
    private bool withGun;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        //getPositionAct = playerInput.actions["GetPosition"];
        pressScreenAct = playerInput.actions["PressScreen"];
        shootAct = playerInput.actions["Shoot"];
    }

    private void OnEnable()
    {
        pressScreenAct.performed += TapScreen;
        shootAct.performed += PressTrigger;
    }

    private void OnDisable()
    {
        pressScreenAct.performed -= TapScreen;
        shootAct.performed -= PressTrigger;
    }

    private void TapScreen(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        Debug.Log(value);
        Debug.Log(cameraRay.HitState);

        if (cameraRay.HitState == CameraRay.Hitted.Switch)
        {
            //Debug.Log("Door Open");
            doorAnim.SetBool("Open", true);
        }
        if(cameraRay.HitState == CameraRay.Hitted.DoBt)
        {
            //Debug.Log("Take Gun");
            doBt.TakeGun();
            withGun = true;
            doBt.CloseWindow();
        }
         if(withGun == true)
        {
            Debug.Log("Gun Fire!");
        }
    }

    private void PressTrigger(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
        Debug.Log(value);
        if (cameraRay.HitState == CameraRay.Hitted.Switch)
        {
            //Debug.Log("Door Open");
            doorAnim.SetBool("Open", true);
        }
        if(cameraRay.HitState == CameraRay.Hitted.DoBt)
        {
            //Debug.Log("Take Gun");
            doBt.TakeGun();
            doBt.CloseWindow();
        }
        if(withGun == true)
        {
            Debug.Log("Gun Fire!");
        }
    }



}
