using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace RocketyRocket2
{
    public class LoadLinks : MonoBehaviour
    {
        [SerializeField] private Button linkedin;
        [SerializeField] private Button Github;
        [SerializeField] private Button Itchio;
        [SerializeField] private Button _MainMenu;
        [SerializeField] private StartEndLevel closeAnim;

        private void Start()
        {
            Github.Select();
            linkedin.onClick.AddListener(LinkToLinkedin);
            Github.onClick.AddListener(LinkToGit);
            Itchio.onClick.AddListener(LinkToItchio);
            _MainMenu.onClick.AddListener(MainMenu);
        }
        private void LinkToLinkedin()
        {
            Application.OpenURL("https://www.linkedin.com/in/marcelsunyer/");
        }
        private void LinkToGit()
        {
            Application.OpenURL("https://github.com/MarcelSunyer");
        }
        private void LinkToItchio()
        {
            Application.OpenURL("https://marcelsunyer.itch.io");
        }
        private void MainMenu()
        {
            StartCoroutine(CloseAnim_End());
            
        }
        private IEnumerator CloseAnim_End()
        {
            closeAnim.CloseAnim();
            yield return new WaitForSeconds(0.7f);
            SceneManager.LoadScene("MainMenuBootstrap");
        }
    }
}
