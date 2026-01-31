using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RocketyRocket2
{
    public class EndLevel : MonoBehaviour
    {
        public bool NextLevelIsTutorial;
        
        public int Galaxy;

        public bool IsNextLevelAnotherGalxy;

        public bool IsEnd = false;
        public StartEndLevel startEndLevel;

        [SerializeField] private Button buttonContinue;
        [SerializeField] private Button buttonTryAgain;
        [SerializeField] private Button buttonMainMenu;
        [SerializeField] private Button quitButton;

        private ShipController shipController;
        private SafeZone zone;

        // Start is called before the first frame update
        void Start()
        {
            zone = GameObject.Find("End").GetComponent<SafeZone>();
            shipController = GameObject.Find("Ship").GetComponent<ShipController>();
            if (buttonContinue != null)
            {
                buttonContinue.onClick.AddListener(LoadNextLevel);
                buttonContinue.Select();
            }
            
            if (buttonTryAgain != null)
            {
                buttonTryAgain.onClick.AddListener(LoadLevelWithoutTutorial);
                if(buttonContinue == null)
                    buttonTryAgain.Select();

            }
            if (buttonMainMenu != null)
                buttonMainMenu.onClick.AddListener(GoMainMenu);

            if (quitButton != null)
                quitButton.onClick.AddListener(QuitGame);
        }

        private void LoadNextLevel()
        {
            RocketyRocket2Game.Instance.SaveGameManager.LevelsDeath = 0;
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
            MainBootstrap.SaveManager.LevelsDeath = 0;
            StartCoroutine(CloseGame());
        }

        private IEnumerator LoadNextLevelAnimationClose()
        {
            MainBootstrap.SaveManager.LevelsDeath = 0;
            StartCoroutine(SoundManager.SoundManager.FadeOut(zone.WinMusic, 1.5f));
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

            if(IsEnd)
            {
                SceneManager.LoadScene("Congrations");
            }
            if (IsNextLevelAnotherGalxy)
            {
                switch (Galaxy)
                {
                    case 1:
                        if (NextLevelIsTutorial)
                        {
                            SceneManager.LoadScene("Level_2_1_Tutorial");
                        }
                        else
                        {
                            SceneManager.LoadScene("Level_2_1");
                        }
                        break;
                    case 2:
                        if (NextLevelIsTutorial)
                        {
                            SceneManager.LoadScene("Level_3_1_Tutorial");
                        }
                        else
                        {
                            SceneManager.LoadScene("Level_3_1");
                        }
                        break;
                    case 3:
                        if (NextLevelIsTutorial)
                        {
                            SceneManager.LoadScene("Level_4_1_Tutorial");
                        }
                        else
                        {
                            SceneManager.LoadScene("Level_4_1");
                        }
                        break;
                    case 4:
                        if (NextLevelIsTutorial)
                        {
                            SceneManager.LoadScene("Level_5_1_Tutorial");
                        }
                        else
                        {
                            SceneManager.LoadScene("Level_5_1" );
                        }
                        break;
                }


            }
            else
            {

                if (Galaxy == 1)
                {
                    if (NextLevelIsTutorial)
                    {
                        SceneManager.LoadScene("Level_1_" + RocketyRocket2Game.Instance.SaveGameManager.Level_Green.ToString() + "_Tutorial");
                    }
                    else
                    {
                        SceneManager.LoadScene("Level_1_" + RocketyRocket2Game.Instance.SaveGameManager.Level_Green.ToString());
                    }
                }

                if (Galaxy == 2)
                {
                    if (NextLevelIsTutorial)
                    {
                        SceneManager.LoadScene("Level_2_" + RocketyRocket2Game.Instance.SaveGameManager.Level_Blue.ToString() + "_Tutorial");
                    }
                    else
                    {
                        SceneManager.LoadScene("Level_2_" + RocketyRocket2Game.Instance.SaveGameManager.Level_Blue.ToString());
                    }
                }
                if (Galaxy == 3)
                {
                    if (NextLevelIsTutorial)
                    {
                        SceneManager.LoadScene("Level_3_" + RocketyRocket2Game.Instance.SaveGameManager.Level_Purple.ToString() + "_Tutorial");
                    }
                    else
                    {
                        SceneManager.LoadScene("Level_3_" + RocketyRocket2Game.Instance.SaveGameManager.Level_Purple.ToString());
                    }
                }
                if (Galaxy == 4)
                {
                    if (NextLevelIsTutorial)
                    {
                        SceneManager.LoadScene("Level_4_" + RocketyRocket2Game.Instance.SaveGameManager.Level_Orange.ToString() + "_Tutorial");
                    }
                    else
                    {
                        SceneManager.LoadScene("Level_4_" + RocketyRocket2Game.Instance.SaveGameManager.Level_Orange.ToString());
                    }
                }
                if (Galaxy == 5)
                {
                    if (NextLevelIsTutorial)
                    {
                        SceneManager.LoadScene("Level_5_" + RocketyRocket2Game.Instance.SaveGameManager.Level_Red.ToString() + "_Tutorial");
                    }
                    else
                    {
                        SceneManager.LoadScene("Level_5_" + RocketyRocket2Game.Instance.SaveGameManager.Level_Red.ToString());
                    }
                }
            }
        }

        private IEnumerator LoadLevelWithoutTutorialAnimationClose()
        {

            if (buttonContinue != null)
            {
                buttonContinue.enabled = false;
                    StartCoroutine(SoundManager.SoundManager.FadeOut(zone.WinMusic, 1.5f));
            }
            else
            {
                StartCoroutine(SoundManager.SoundManager.FadeOut(shipController.gameOver, 1.5f));
            }

            if (buttonTryAgain != null)

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
            {
                buttonContinue.enabled = false;
                StartCoroutine(SoundManager.SoundManager.FadeOut(zone.WinMusic, 1.5f));
            }
            else
            {
                StartCoroutine(SoundManager.SoundManager.FadeOut(shipController.gameOver,1.5f));
            }

            if (buttonTryAgain != null)
            {
                buttonTryAgain.enabled = false;
            }
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
            if (buttonContinue != null)
            {
                buttonContinue.enabled = false;
                StartCoroutine(SoundManager.SoundManager.FadeOut(zone.WinMusic, 1.5f));
            }
            else
            {
                StartCoroutine(SoundManager.SoundManager.FadeOut(shipController.gameOver, 1.5f));
            }
            yield return new WaitForSeconds(2);
            Application.Quit();
        }
    }
}
