using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static RocketyRocket2.ShipController;

namespace RocketyRocket2
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject TutorialIsActive;

        [SerializeField] private StartEndLevel SEL_isTutorial;

        [SerializeField] private GameObject pauseMenu;

        [SerializeField] private ShipController shipController;

        public bool pauseActive = false;

        [SerializeField] private Button resumePause;

        [SerializeField] private Button restartLevel;

        [SerializeField] private Button returnToMainMenu;


        [SerializeField] private BladeFunction[] bladeStop;



        // Start is called before the first frame update
        void Start()
        {
            pauseMenu.SetActive(false);

            resumePause.onClick.AddListener(PauseDisable);

            restartLevel.onClick.AddListener(RestartLevel);

            returnToMainMenu.onClick.AddListener(ReturnMainMenu);
        }

        // Update is called once per frame
        void Update()
        {

            if(TutorialIsActive == null)
            {
                if (!pauseActive && Input.GetKeyDown(KeyCode.Escape) || Gamepad.current.startButton.isPressed)
                {
                    resumePause.Select();
                    PauseActive();
                    return;
                }

            } else if(!pauseActive && Input.GetKeyDown(KeyCode.Escape) || Gamepad.current.startButton.isPressed && !TutorialIsActive )
            {
                PauseActive();
                return;
            }

        }

        private void PauseActive()
        {
                                resumePause.Select();
            for (int i = 0; i < bladeStop.Length; i++)
            {
                if (bladeStop[i] != null)
                    bladeStop[i].BladeCanMove = false;
            }

            shipController.Pause();

            pauseMenu.SetActive(true);
            pauseActive = true;

            resumePause.Select();
            if (resumePause != null)
                resumePause.gameObject.SetActive(true);

            if(returnToMainMenu != null)
                returnToMainMenu.gameObject.SetActive(true);

            if (restartLevel != null)
                restartLevel.gameObject.SetActive(true);
            shipController.enabled = false;
        }

        private void PauseDisable()
        {
            for (int i = 0; i < bladeStop.Length; i++)
            {
                if (bladeStop[i] != null)
                    bladeStop[i].BladeCanMove = true;
            }

            shipController.Resume();
            pauseMenu.SetActive(false);

            if (resumePause != null)
                resumePause.gameObject.SetActive(false);

            if (returnToMainMenu != null)
                returnToMainMenu.gameObject.SetActive(false);

            if (restartLevel != null)
                restartLevel.gameObject.SetActive(false);

            pauseActive = false;
        }

        private void RestartLevel()
        {
            if(SEL_isTutorial.isTutorial)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        private void ReturnMainMenu()
        {
            SceneManager.LoadScene("MainMenuBootstrap");
        }
    }
}
