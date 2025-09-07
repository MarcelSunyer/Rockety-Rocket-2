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

        [SerializeField] private Button buttonContinue;
        [SerializeField] private Button buttonTryAgain;
        [SerializeField] private Button buttonMainMenu;

        [SerializeField] private StartEndLevel startEndLevel;

        // Start is called before the first frame update
        void Start()
        {
            buttonContinue.onClick.AddListener(LoadNextLevel);
            buttonTryAgain.onClick.AddListener(LoadLevelWithoutTutorial);
            buttonMainMenu.onClick.AddListener(GoMainMenu);
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

        private IEnumerator LoadNextLevelAnimationClose()
        {
            buttonContinue.enabled = false;
            buttonTryAgain.enabled = false;
            buttonMainMenu.enabled = false;
            startEndLevel.CloseAnim();
            yield return new WaitForSeconds(2);

            if (Galaxy == 1)
            {
                SceneManager.LoadScene("Level_1_1");
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
                SceneManager.LoadScene("Level_5_" + RocketyRocket2Game.Instance.SaveGameManager.Level_Red.ToString() + 1);
            }
        }

        private IEnumerator LoadLevelWithoutTutorialAnimationClose()
        {
            buttonContinue.enabled = false;
            buttonTryAgain.enabled = false;
            buttonMainMenu.enabled = false;
            startEndLevel.CloseAnim();
            yield return new WaitForSeconds(2);

            Scene currentScene = SceneManager.GetActiveScene();

            if (currentScene.name == "Level_1_1_Tutorial")
            {
                SceneManager.LoadScene("Level_1_1");
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
            buttonContinue.enabled = false;
            buttonTryAgain.enabled = false;
            buttonMainMenu.enabled = false;
            startEndLevel.CloseAnim();
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene("MainMenuBootstrap");
        }
    }
}
