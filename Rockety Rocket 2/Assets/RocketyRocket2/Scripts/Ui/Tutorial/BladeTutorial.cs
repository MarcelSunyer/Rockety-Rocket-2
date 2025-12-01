using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RocketyRocket2
{
    public class BladeTutorial : MonoBehaviour
    {
        public Image _BladeTutorial;

        public Image _ShipTutorial;

        public float BladeMovement;

        public float BladeCutSpeed;
        private float angle;


        [SerializeField] private ParticleSystem particlesExplosion_1;
        [SerializeField] private ParticleSystem particlesExplosion_2;
        [SerializeField] private ParticleSystem particlesExplosion_3;

        private float originalPosition;
        // Start is called before the first frame update
        void Start()
        {
            originalPosition = _BladeTutorial.GetComponent<RectTransform>().position.x;
            originalPosition = 250;
            StartCoroutine(FBladeTutorial());

        }

        // Update is called once per frame
        void Update()
        {
            angle += BladeCutSpeed /10;

            _BladeTutorial.rectTransform.rotation = Quaternion.Euler(0, 0, angle);
        }

        private IEnumerator FBladeTutorial()
        {
            _ShipTutorial.gameObject.SetActive(true);
            yield return new WaitForSeconds(2);

            Tween tween = _BladeTutorial.GetComponent<RectTransform>().DOAnchorPosX(BladeMovement, 1.2f);
            tween.Play();

            StartCoroutine(ShipDestroyed());

            yield return new WaitForSeconds(3);

            tween = _BladeTutorial.GetComponent<RectTransform>().DOAnchorPosX(originalPosition, 0f);
            tween.Play();

            yield return tween.WaitForCompletion();

            StartCoroutine(FBladeTutorial());
        }


       private IEnumerator ShipDestroyed()
        {
            yield return new WaitForSeconds(0.8f);

            _ShipTutorial.gameObject.SetActive(false);

            particlesExplosion_1.Play();
            particlesExplosion_2.Play();
            particlesExplosion_3.Play();
        }
    }
}
