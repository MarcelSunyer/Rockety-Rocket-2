using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RocketyRocket2
{
    public class GamePlayControll : MonoBehaviour
    {
        public Slider Restart;
        public GameObject Bar;
        public bool clicked = false;
        void Update()
        {
            RestartLevel();
        }

        private void RestartLevel()
        {
            if (Input.GetKey(KeyCode.R))
            {
                Bar.SetActive(true);
                Restart.value += 2;
            }
            else
            {
                if (Restart.value != 0)
                {
                    Restart.value -= 1;
                }
                else
                {
                    Bar.SetActive(false);
                }
            }
            if (Restart.value == 100)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
