using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionButton : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private List<Resolution> filteredResolution;

    private float currentRefreshRate;
    private int resolutionIndex = 0;
    
    void Start()
    {
        resolutions = Screen.resolutions;
        filteredResolution = new List<Resolution>();
        resolutionDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for(int i = 0; i < resolutions.Length; i++)
        {
            if(resolutions[i].refreshRate == currentRefreshRate)
            {
                filteredResolution.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for(int i = 0; i < resolutions.Length; i++)
        {
            string resolutionOption = filteredResolution[i].width + "x" + filteredResolution[i].height + " " + filteredResolution[i].refreshRate + "Hz";
            options.Add(resolutionOption);
            if(filteredResolution[i].width == Screen.width && filteredResolution[i].height == Screen.height)
            {
                resolutionIndex = i;
            }
        }
        resolutionDropdown.value = resolutionIndex;
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.AddOptions(options);



    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolution[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

}
