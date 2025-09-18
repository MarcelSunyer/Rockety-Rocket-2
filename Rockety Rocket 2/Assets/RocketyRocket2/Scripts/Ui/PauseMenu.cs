using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static RocketyRocket2.ShipController;

namespace RocketyRocket2
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;

        [SerializeField] private ShipController shipController;

        [SerializeField] private bool pauseActive = false;

        // Start is called before the first frame update
        void Start()
        {
            pauseMenu.SetActive(false);

        }

        // Update is called once per frame
        void Update()

        {
            if (!pauseActive && Input.GetKeyDown(KeyCode.Escape))
            {
                PauseActive();
                return;
            }


            if (pauseActive && Input.GetKeyDown(KeyCode.Escape))
            {
                PauseDisable();
                return;
            }

        }

        private void PauseActive()
        {
            shipController.currentState = StateShip.Stop;
            pauseMenu.SetActive(true);
            pauseActive = true;
        }

        private void PauseDisable()
        {
            shipController.currentState = StateShip.Playing;
            pauseMenu.SetActive(false);
            pauseActive = false;
        }


    }
}
