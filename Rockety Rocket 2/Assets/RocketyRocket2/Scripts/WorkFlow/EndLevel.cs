using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RocketyRocket2
{
    public class EndLevel : MonoBehaviour
    {
        public int Galaxy;
        public StartEndLevel startEndLevel;

        [SerializeField] private Button buttonContinue;
        [SerializeField] private Button buttonTryAgain;
        [SerializeField] private Button buttonMainMenu;
        [SerializeField] private Button quitButton;



        // Start is called before the first frame update
        void Start()
        {
            if (buttonContinue != null)
                buttonContinue.onClick.AddListener(LoadNextLevel);

            if (buttonTryAgain != null)
                buttonTryAgain.onClick.AddListener(LoadLevelWithoutTutorial);

            if (buttonMainMenu != null)
                buttonMainMenu.onClick.AddListener(GoMainMenu);

            if (quitButton != null)
                quitButton.onClick.AddListener(QuitGame);
        }

        private void LoadNextLevel()
        {
            StartCoroutine(LoadNextLevelAnimationClose());
        }

        private void LoadLevelWithoutTutorial()
        {
            StartCoroutine(LoadLevelWithoutTutorialAnimationClose());
        }

        private void GoMainMenu()
        {
            StartCoroutine(GoMainMenuAnimationClose());
        }

        private void QuitGame()
        {
            StartCoroutine(CloseGame());
        }

        private IEnumerator LoadNextLevelAnimationClose()
        {
            if (buttonContinue != null)
                buttonContinue.enabled = false;

            if (buttonTryAgain != null)
                buttonTryAgain.enabled = false;

            if (buttonMainMenu != null)
                buttonMainMenu.enabled = false;

            if (quitButton != null)
                quitButton.enabled = false;

            startEndLevel.CloseAnim();
            yield return new WaitForSeconds(2);

            if (Galaxy == 1)
            {
                SceneManager.LoadScene("Level_1_" + RocketyRocket2Game.Instance.SaveGameManager.Level_Green.ToString());
            }

            if (Galaxy == 2)
            {
                SceneManager.LoadScene("Level_2_" + RocketyRocket2Game.Instance.SaveGameManager.Level_Blue.ToString());
            }
            if (Galaxy == 3)
            {
                SceneManager.LoadScene("Level_3_" + RocketyRocket2Game.Instance.SaveGameManager.Level_Purple.ToString());
            }
            if (Galaxy == 4)
            {
                SceneManager.LoadScene("Level_4_" + RocketyRocket2Game.Instance.SaveGameManager.Level_Orange.ToString());
            }
            if (Galaxy == 5)
            {
                SceneManager.LoadScene("Level_5_" + RocketyRocket2Game.Instance.SaveGameManager.Level_Red.ToString());
            }
        }

        private IEnumerator LoadLevelWithoutTutorialAnimationClose()
        {
            if(buttonContinue != null)
                buttonContinue.enabled = false;
            
            if(buttonTryAgain != null)
                buttonTryAgain.enabled = false;

            if (buttonMainMenu != null)
                buttonMainMenu.enabled = false;

            if(quitButton != null)
                quitButton.enabled = false;

            startEndLevel.CloseAnim();
            yield return new WaitForSeconds(2);

            Scene currentScene = SceneManager.GetActiveScene();


            //Add scenes with tutorial
            if (currentScene.name == "Level_1_1_Tutorial")
            {
                SceneManager.LoadScene("Level_1_1");
                yield return null;
            }
            if (currentScene.name == "Level_1_4_Tutorial")
            {
                SceneManager.LoadScene("Level_1_4");
                yield return null;
            }
            if (currentScene.name == "Level_2_1_Tutorial")
            {
                SceneManager.LoadScene("Level_2_1");
                yield return null;
            }
            if (currentScene.name == "Level_3_1_Tutorial")
            {
                SceneManager.LoadScene("Level_3_1");
                yield return null;
            }
            if (currentScene.name == "Level_4_1_Tutorial")
            {
                SceneManager.LoadScene("Level_4_1");
                yield return null;
            }
            if (currentScene.name == "Level_5_1_Tutorial")
            {
                SceneManager.LoadScene("Level_5_1");
                yield return null;
            }

            SceneManager.LoadScene(currentScene.name);
        }

        private IEnumerator GoMainMenuAnimationClose()
        {
            if (buttonContinue != null)
                buttonContinue.enabled = false;

            if (buttonTryAgain != null)
                buttonTryAgain.enabled = false;

            if (buttonMainMenu != null)
                buttonMainMenu.enabled = false;

            if (quitButton != null)
                quitButton.enabled = false;

            startEndLevel.CloseAnim();
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("MainMenuBootstrap");
        }

        private IEnumerator CloseGame()
        {
            yield return new WaitForSeconds(2);
            Application.Quit();
        }
    }
}
