using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RocketyRocket2
{
    public class StartEndLevel : MonoBehaviour
    {
        [SerializeField] private GameObject upImage;
        [SerializeField] private GameObject downImage;

        [SerializeField] private bool isTutorial;
        [SerializeField] private CameraFollow cameraFollow;

        [SerializeField] private SpriteRenderer arrow;
        [SerializeField] private SpriteRenderer goal;

        [SerializeField] private float fadeTimeInOut;
        private void Start()
        {
            if(arrow != null)
                arrow.enabled = false;

            if(goal != null)
                goal.enabled = false;

            StartCoroutine(OpenAnim());
        }

        private IEnumerator Test()
        {
            OpenAnim();
            yield return new WaitForSeconds(4);
            CloseAnim();
        }

        public IEnumerator OpenAnim()
        {
            Tween upTween = upImage.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0,2500), fadeTimeInOut);
            Tween downTween = downImage.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -2500), fadeTimeInOut);
            upTween.Play();
            downTween.Play();

            if(!isTutorial)
            {
                yield return new WaitForSeconds(2);
                if (cameraFollow != null)
                    cameraFollow.enabled = true;
            }
            yield return new WaitForSeconds(5);



        }
        public void CloseAnim()
        {
            Tween upTween =   upImage.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, -500), fadeTimeInOut / 1.5f);
            Tween downTween = downImage.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 500), fadeTimeInOut / 1.5f);
            upTween.Play();
            downTween.Play();
        }
    }
}
