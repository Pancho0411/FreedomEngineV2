using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour {

    Resolution[] resolutions;

    public GameObject FSRSharpness;

    public TMP_Dropdown resolutionDropdown;

    [SerializeField] UniversalRenderPipelineAsset pipelineAsset;

    [SerializeField] Slider ShadowSlider;
    [SerializeField] Slider FSRSharpnessSlider;
    [SerializeField] Toggle HDR_bool;
    [SerializeField] Toggle FSR_bool;
    [SerializeField] Toggle FullscreenBool;
    [SerializeField] Toggle Vsync;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " " + resolutions[i].refreshRateRatio.value.ToString("F2") + "Hz";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        //initialize values to last saved value
        ShadowSlider.value = pipelineAsset.shadowDistance;
        FSRSharpnessSlider.value = pipelineAsset.fsrSharpness;

        if(pipelineAsset.supportsHDR == false)
        {
            HDR_bool.isOn = false;
        }
        else
        {
            HDR_bool.isOn = true;
        }

        if(Screen.fullScreen == false)
        {
            FullscreenBool.isOn = false;
        }
        else
        {
            FullscreenBool.isOn = true;
        }

        if(pipelineAsset.upscalingFilter != UpscalingFilterSelection.FSR)
        {
            FSR_bool.isOn = false;
            FSRSharpness.SetActive(false);
        }
        else
        {
            FSR_bool.isOn = true;
            FSRSharpness.SetActive(true);
        }

        if(QualitySettings.vSyncCount == 0)
        {
            Vsync.isOn = false;
        }
        else
        {
            Vsync.isOn = true;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetVSync(bool VSyncOn)
    {
        if (!VSyncOn)
        {
            QualitySettings.vSyncCount = 0;
        }
        else
        {
            QualitySettings.vSyncCount = 1;
        }
    }

    //Anti-Aliasing
    public void DisableAA()
    {
        pipelineAsset.msaaSampleCount = 1;
    }

    public void AA2X()
    {
        pipelineAsset.msaaSampleCount = 2;
    }

    public void AA4X()
    {
        pipelineAsset.msaaSampleCount = 4;
    }

    public void AA8X()
    {
        pipelineAsset.msaaSampleCount = 8;
    }

    //FSR
    public void enableFSR(bool FSROn)
    {
        if (!FSROn)
        {
            pipelineAsset.upscalingFilter = UpscalingFilterSelection.Point;
            FSRSharpness.SetActive(false);
        }
        else
        {
            pipelineAsset.upscalingFilter = UpscalingFilterSelection.FSR;
            FSRSharpness.SetActive(true);
        }
    }

    //RenderScale

    public void halfRenderScale()
    {
        pipelineAsset.renderScale = 0.5f;
    }

    public void nativeRenderScale()
    {
        pipelineAsset.renderScale = 1f;
    }

    public void upscaleHalf()
    {
        pipelineAsset.renderScale = 1.5f;
    }

    public void upscaleFull()
    {
        pipelineAsset.renderScale = 2f;
    }

    //HDR
    public void toggleHDR(bool HDROn)
    {
        if (!HDROn)
        {
            pipelineAsset.supportsHDR = false;
            pipelineAsset.colorGradingMode = ColorGradingMode.LowDynamicRange;
        }
        else
        {
            pipelineAsset.supportsHDR = true;
            pipelineAsset.colorGradingMode = ColorGradingMode.HighDynamicRange;
        }
    }

    //Shadows
    public void setShadowDistance(float distance)
    {
        pipelineAsset.shadowDistance = ShadowSlider.value;
    }

    public void setFSRSharpness(float fsrSharpness)
    {
        pipelineAsset.fsrSharpness = FSRSharpnessSlider.value;
    }

    //following methods are used for main menu
    public void stopTime()
    {
        Time.timeScale = 0f;
    }

    public void startTime()
    {
        Time.timeScale = 1f;
    }

    //Changing shadow resolution at runtime is not yet supported by Unity
    //public void setShadowResolution256()
    //{
        
    //}
    //public void setShadowResolution512()
    //{

    //}
    //public void setShadowResolution1024()
    //{

    //}
    //public void setShadowResolution2048()
    //{

    //}
    //public void setShadowResolution4096()
    //{

    //}
}
