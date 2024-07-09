using System.Collections.Generic;
using UnityEngine;
using System;
using static MenuStates;
using static BuildingsConsts;
using TMPro;
using UnityEngine.Audio;

public class PauseMenuController : MonoBehaviour
{
    public AudioMixer mixer;
    public TextMeshProUGUI balanceText;
    public TextMeshProUGUI buildingText;
    private PauseMenuModel model;
    public Canvas pauseCanvas;
    public Canvas settingsCanvas;
    public Canvas priceCanvas;
    public Canvas ingameCanvas;
    public Canvas orderCanvas;
    public Canvas buildingCanvas;
    public BuildingIndificator buildingId = 0;

    private float scrollInput;

    public GameObject shopPanelPrefab;
    public Transform shopPanelParent;
    public GameObject marketPanelPrefab;
    public Transform marketPanelParent;

    private List<GameObject> marketPanelInstances = new List<GameObject>();
    private float marketTimer = 0f;
    private float marketInterval = 5f;
    //temp


    private Action<String> _onBuildingIdChange;

    private void Start()
    {
        LoadModels();
        SetupUI();
    }

    

    public void LoadModels()
    {
        model = new();
    }
    public void SetupUI()
    {
        model.Close(pauseCanvas);
        model.Close(settingsCanvas);
        model.Close(priceCanvas);
        model.Close(orderCanvas);
        model.Close(buildingCanvas);
    }

    void UpdateBuildingText(String name)
    {
        buildingText.text = name;
    }
    void ToggleBuildingMenu(bool b)
    {
        if (model.state == MenuState.None)
        {
            OpenMenu(buildingCanvas);
            ChangeState(MenuState.BuildingMenu);
        }
        else if (model.state == MenuState.BuildingMenu)
        {
            CloseMenu(buildingCanvas);
            ChangeState(MenuState.None);
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            switch(model.state)
            {
                case MenuState.None:
                    {
                        PauseGame();
                        OpenMenu(pauseCanvas);
                        ChangeState(MenuState.PauseMenu);
                        break;
                    }
                case MenuState.PauseMenu:
                    {
                        CloseMenu(pauseCanvas);
                        ResumeGame();
                        ChangeState(MenuState.None);
                        break;
                    }
                case MenuState.SettingsMenu:
                    {
                        CloseMenu(settingsCanvas);
                        OpenMenu(pauseCanvas);
                        ChangeState(MenuState.PauseMenu);
                        break;
                    }
                case MenuState.PriceMenu:
                    {
                        CloseMenu(priceCanvas);
                        ResumeGame();
                        ChangeState(MenuState.None);
                        break;
                    }
                case MenuState.BuildingMenu:
                    {
                        CloseMenu(buildingCanvas);
                        ChangeState(MenuState.None);
                        break;
                    }
            }
        }
    }
    #region UI
    
    public void PauseGame()
    {
        model.Pause();
    }

    public void ResumeGame()
    {
        model.Resume();
    }
    public void OpenMenu(Canvas c)
    {
        model.Open(c);
    }
    public void CloseMenu(Canvas c)
    {
        model.Close(c);
    }
    public void LoadScene(string s)
    {
        model.LoadScene(s);
    }
    public void ChangeState(MenuState state)
    {
        model.state = state;
    }
    public void ChangeState(int i)
    {
        if(Enum.IsDefined(typeof(MenuState), i))
        {
            model.state = (MenuState)i;
        }
        else
        {
            UnityEngine.Debug.Log("???????????? ????? ?????????");
        }
    }
    #endregion

    public void SetVolume(float v)
    {
        mixer.SetFloat("Volume", v);
    }



}