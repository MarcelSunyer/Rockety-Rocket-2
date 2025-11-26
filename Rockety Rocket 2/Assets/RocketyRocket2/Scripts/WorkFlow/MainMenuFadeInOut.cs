using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RocketyRocket2
{
    public class MainMenuFadeInOut : MonoBehaviour
    {

        public Button[] ButtonsToFadeOut;

        [SerializeField] private GameObject upImage;
        [SerializeField] private GameObject downImage;

        [SerializeField] private float fadeTimeInOut;
        private void Start()
        {
            StartCoroutine(OpenAnim());

            for (int i = 0; i < ButtonsToFadeOut.Length; ++i)
            {
                ButtonsToFadeOut[i].onClick.AddListener(CloseAnim);
            }

        }

        private IEnumerator Test()
        {
            OpenAnim();
            yield return new WaitForSeconds(4);
            CloseAnim();
        }

        public IEnumerator OpenAnim()
        {
            Tween upTween = upImage.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 2500), fadeTimeInOut);
            Tween downTween = downImage.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -2500), fadeTimeInOut);
            upTween.Play();
            downTween.Play();
            yield return new WaitForSeconds(5);

        }
        public void CloseAnim()
        {
            Tween upTween = upImage.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -500), fadeTimeInOut / 1.5f);
            Tween downTween = downImage.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 500), fadeTimeInOut / 1.5f);
            upTween.Play();
            downTween.Play();
        }

    }
}


