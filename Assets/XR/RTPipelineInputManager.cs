using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class RTPipelineInputManager : MonoBehaviour
{
    // Oculus
    [Header("XR Origin")]
    [SerializeField] GameObject XROriginGO;
    [SerializeField] RayTracingRenderPipelineAsset rayTracingRenderPipelineAsset;

    [Header("Right Controller")]
    [SerializeField] private InputActionReference rightAPressed;
    [SerializeField] private InputActionReference rightBPressed;
    [SerializeField] private InputActionReference rightTriggerPressed;
    [SerializeField] private InputActionReference rightGridPressed;

    [Header("Left Controller")]
    [SerializeField] private InputActionReference leftXPressed;
    [SerializeField] private InputActionReference leftYPressed;
    [SerializeField] private InputActionReference leftTriggerPressed;
    [SerializeField] private InputActionReference leftGridPressed;

    [Header("Material")]
    public Material foxMat;
    public Material planeMat;

    float fox_metallic;
    float fox_roughness;
    float plane_metallic;
    float plane_roughness;

    float perTimeMatChange;

    void Start()
    {
        fox_metallic = 0f;
        fox_roughness = 1f;
        plane_roughness = 0.05f;
        plane_metallic = 0.35f;

        foxMat.SetFloat("_Metallic", fox_metallic);
        foxMat.SetFloat("_Roughness", fox_roughness);
        planeMat.SetFloat("_Metallic", plane_metallic);
        planeMat.SetFloat("_Roughness", plane_roughness);

        perTimeMatChange = 0.05f;

        leftTriggerPressed.action.started += ChangeFoxMetallic;
        leftGridPressed.action.started += ChangeFoxRoughness;
        rightTriggerPressed.action.started += ChangePlaneMetallic;
        rightGridPressed.action.started += ChangePlaneRoughness;
    }



    // Update is called once per frame
    void Update()
    {
        // Switch direct/indirect
        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            if (rayTracingRenderPipelineAsset.RtRenderSetting.EnableIndirect == true)
                rayTracingRenderPipelineAsset.EnableIndirect = false;
            else
                rayTracingRenderPipelineAsset.EnableIndirect = true;
        }

        // Switch render mod
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            if (rayTracingRenderPipelineAsset.RtRenderSetting.renderMode == RayTracingRenderPipelineAsset.RTRenderSetting.RTRenderMode.MIS)
                rayTracingRenderPipelineAsset.RenderMode = RayTracingRenderPipelineAsset.RTRenderSetting.RTRenderMode.VSAT;
            else
                rayTracingRenderPipelineAsset.RenderMode = RayTracingRenderPipelineAsset.RTRenderSetting.RTRenderMode.MIS;
        }

        // Switch animation
        if (Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            SceneManager.isPlayAnimation = SceneManager.isPlayAnimation ? false : true;
        }

        // enable denoiser
        //if ((Input.GetKeyDown(KeyCode.JoystickButton4) || Input.GetKeyDown(KeyCode.F8))
        //   && rayTracingRenderPipelineAsset.RenderMode == RayTracingRenderPipelineAsset.RTRenderSetting.RTRenderMode.MIS)
        //{
        //    RayTracingRenderPipelineAsset.EnableDenoise = RayTracingRenderPipelineAsset.EnableDenoise ? false : true;
        //    plane_metallic = 0.8f;
        //    planeMat.SetFloat("_Metallic", plane_metallic);
        //}


        // material property
        // left trigger
        if (leftTriggerPressed.action.IsPressed())
        {
            if (fox_metallic < 1 - perTimeMatChange)
            {
                fox_metallic += perTimeMatChange;
                foxMat.SetFloat("_Metallic", fox_metallic);
            }
            else
            {
                fox_metallic = 0.0f;
                foxMat.SetFloat("_Metallic", fox_metallic);
            }
        }

        // right trigger
        if (rightTriggerPressed.action.IsPressed())
        {
            if (plane_metallic < 1 - perTimeMatChange)
            {
                plane_metallic += perTimeMatChange;
                planeMat.SetFloat("_Metallic", plane_metallic);
            }
            else
            {
                plane_metallic = 0.0f;
                planeMat.SetFloat("_Metallic", plane_metallic);
            }
        }

        if (leftGridPressed.action.IsPressed())
        {
            if (fox_roughness < 1 - perTimeMatChange)
            {
                fox_roughness += perTimeMatChange;
                foxMat.SetFloat("_Roughness", fox_roughness);
            }
            else
            {
                fox_roughness = 0.0f;
                foxMat.SetFloat("_Roughness", fox_roughness);
            }
        }


        if (rightGridPressed.action.IsPressed())
        {
            if (plane_roughness < 1 - perTimeMatChange)
            {
                plane_roughness += perTimeMatChange;
                planeMat.SetFloat("_Roughness", plane_roughness);
            }
            else
            {
                plane_roughness = 0.0f;
                planeMat.SetFloat("_Roughness", plane_roughness);
            }
        }
    }

    void ChangeFoxMetallic(InputAction.CallbackContext context)
    {
        if (fox_metallic < 1 - perTimeMatChange)
        {
            fox_metallic += perTimeMatChange;
            foxMat.SetFloat("_Metallic", fox_metallic);
        }
        else
        {
            fox_metallic = 0.0f;
            foxMat.SetFloat("_Metallic", fox_metallic);
        }
    }

    void ChangePlaneMetallic(InputAction.CallbackContext context)
    {
        if (plane_metallic < 1 - perTimeMatChange)
        {
            plane_metallic += perTimeMatChange;
            planeMat.SetFloat("_Metallic", plane_metallic);
        }
        else
        {
            plane_metallic = 0.0f;
            planeMat.SetFloat("_Metallic", plane_metallic);
        }
    }

    void ChangeFoxRoughness(InputAction.CallbackContext context)
    {
        Debug.Log(fox_roughness);
        if (fox_roughness < 1 - perTimeMatChange)
        {
            fox_roughness += perTimeMatChange;
            foxMat.SetFloat("_Roughness", fox_roughness);
        }
        else
        {
            fox_roughness = 0.0f;
            foxMat.SetFloat("_Roughness", fox_roughness);
        }
    }

    void ChangePlaneRoughness(InputAction.CallbackContext context)
    {
        if (plane_roughness < 1 - perTimeMatChange)
        {
            plane_roughness += perTimeMatChange;
            planeMat.SetFloat("_Roughness", plane_roughness);
        }
        else
        {
            plane_roughness = 0.0f;
            planeMat.SetFloat("_Roughness", plane_roughness);
        }
    }
}
