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

        [SerializeField] private float second;
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

        private IEnumerator OpenAnim()
        {
            Tween upTween = upImage.transform.DOMoveY(2000, second);
            Tween downTween = downImage.transform.DOMoveY(-900, second);
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

            Tween upTween = upImage.transform.DOMoveY(400, second);
            Tween downTween = downImage.transform.DOMoveY(200, second);
            upTween.Play();
            downTween.Play();

        }
    }
}
