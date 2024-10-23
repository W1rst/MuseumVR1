using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEditor;

public class ChangeSkyboxOnButton : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private Material[] skyboxMaterials;
    private int currentSkyboxIndex = 0;

    [SerializeField] private Button forwardButton;
    [SerializeField] private Button backwardButton;
    [SerializeField] private Button toggleButton;

    [SerializeField] private GameObject objectToToggle; 
    [SerializeField] private GameObject game;
    [SerializeField] private GameObject cubeGame;
    [SerializeField] private GameObject sprCubeGame;
    [SerializeField] private GameObject panels;
    void Start()
    {

        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i;
            buttons[i].onClick.AddListener(() => ChangeSkybox(buttonIndex));
        }

        forwardButton.onClick.AddListener(NextSkybox);
        backwardButton.onClick.AddListener(PreviousSkybox);
        toggleButton.onClick.AddListener(ToggleObject);
    }
    private void Update()
    {
        Debug.Log(currentSkyboxIndex);
    }
    void ChangeSkybox(int index)
    {
        RenderSettings.skybox = skyboxMaterials[index];

        if (index == 1) 
        { 
            panels.SetActive(true);
        } 
        else if (index != 1)
        {
            panels.SetActive(false);
        }

        if (index == 2)
        {
            game.SetActive(true);
            sprCubeGame.SetActive(true);
        }
        else if(index != 2)
        {
            game.SetActive(false);
            sprCubeGame.SetActive(false);
        }

        if(index == 4)
        {
            cubeGame.SetActive(true);
        } 
        else if(index != 4)
        {
            cubeGame.SetActive(false);
        }

        DynamicGI.UpdateEnvironment();
    }

    void NextSkybox()
    {
        currentSkyboxIndex++;
        if (currentSkyboxIndex >= skyboxMaterials.Length)
        {
            currentSkyboxIndex = 0;
        }

        ChangeSkybox(currentSkyboxIndex);
    }

    void PreviousSkybox()
    {
        currentSkyboxIndex--;
        if (currentSkyboxIndex < 0)
        {
            currentSkyboxIndex = skyboxMaterials.Length - 1;
        }

        ChangeSkybox(currentSkyboxIndex);
    }

    void ToggleObject()
    {
        objectToToggle.SetActive(!objectToToggle.activeSelf);
        
    }
}
