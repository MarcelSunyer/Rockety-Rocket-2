using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RocketyRocket2
{
    public class MainMenuBootstrap : MainBootstrap
    {
        private void Start()
        {
            PrepareGame();
            SceneManager.LoadScene("MainMenu");
        }

    }
}
