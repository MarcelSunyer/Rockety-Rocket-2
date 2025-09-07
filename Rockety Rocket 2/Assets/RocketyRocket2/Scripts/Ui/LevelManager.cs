using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RocketyRocket2
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Button[] Levels;
        [SerializeField] private int galaxy = 1;

        [SerializeField] private Image fade;

        private void Start()
        {
            for (int i = 0; i < Levels.Length; i++)
            {
                string levelName = Levels[i].name + "_" + galaxy.ToString();
                if (i == 0)
                {
                    levelName = Levels[i].name + "_" + galaxy.ToString() + "_Tutorial";
                }
                
                Levels[i].onClick.AddListener(()=> LoadLevel(levelName));
            }
        }

        private void LoadLevel(string levelName)
        {
            StartCoroutine(FadeOut(levelName));
        }

        private IEnumerator FadeOut(string levelName)
        {
            fade.gameObject.SetActive(true);

            Tween tween = fade.DOFade(1,1);
            tween.Play();

            yield return tween.WaitForCompletion();

            SceneManager.LoadScene(levelName);
        }
    }
}
