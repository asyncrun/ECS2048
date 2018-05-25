﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Entities;
using UnityEngine;

[AlwaysUpdateSystem]
public class UISystem : ComponentSystem
{
    [Inject] private TextData data;
    protected override void OnUpdate()
    {
        for (int i = 0; i < data.Length; i++)
        {
            int index = data.text[i].Index;
            if (index >= 0)
            {
                var pos = data.position[i].Value;
                pos.z = -1; // it's in front of block
                Bootstrap.GameSettings.BlockTexts[index].transform.position = pos;
                Bootstrap.GameSettings.BlockTexts[index].text = data.block[i].Value.ToString();
            }
        }
        
    }

    public void SetupUI(System.Action playNewGame)
    {
        Bootstrap.GameSettings.PauseButton.onClick.AddListener(TogglePause);
        Parallel.ForEach(Bootstrap.GameSettings.MenuButtons, btn => btn.onClick.AddListener(RestartWorld));
        Parallel.ForEach(Bootstrap.GameSettings.QuitButtons, btn => btn.onClick.AddListener(ExitGame));
        Bootstrap.GameSettings.PlayButton.onClick.AddListener(() => OnNewGame(playNewGame));

        ActiveMenu();
    }

    private void OnNewGame(System.Action callNewGame)
    {
        callNewGame();
        Bootstrap.GameSettings.MenuCanvas.gameObject.SetActive(false);
        Bootstrap.GameSettings.HUDCanvas.gameObject.SetActive(true);
    }

    private void TogglePause()
    {
        Time.timeScale = Time.timeScale < 1 ? 1 : 0;
        Bootstrap.GameSettings.PauseCanvas.SetActive(!Bootstrap.GameSettings.PauseCanvas.activeSelf);
    }

    private void RestartWorld()
    {
        World.Active.SetBehavioursActive(false);
        World.Active.GetExistingManager<EntityManager>().DestroyAllEntities();
        ActiveMenu();
    }

    private void ActiveMenu()
    {
        Bootstrap.GameSettings.HUDCanvas.SetActive(false);
        Bootstrap.GameSettings.PauseCanvas.SetActive(false);
        Bootstrap.GameSettings.GameOverCanvas.SetActive(false);

        Bootstrap.GameSettings.MenuCanvas.SetActive(true);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

}
