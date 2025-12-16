using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketyRocket2
{
    public class TutorialAstronaut : MonoBehaviour
    {
        public GameObject Ship;
        public ParticleSystem particle;
        public float ShipMovement;

        public GameObject Astronaut;

        [SerializeField] private ParticleSystem[] astronautSafe;
        
        private Vector2 InitialPosition;

        private Vector3 InitalScale;


        private void Start()
        {
            InitalScale = Astronaut.GetComponent<RectTransform>().localScale;
            InitialPosition = Ship.GetComponent<RectTransform>().anchoredPosition;
            StartCoroutine(TutorialAstronautShip());
        }
        private IEnumerator TutorialAstronautShip()
        {
            while (true)
            {

                particle.Play();
                Tween tween = Ship.GetComponent<RectTransform>().DOAnchorPosX(ShipMovement, 2.5f);

                tween.Play();
                yield return tween.WaitForCompletion();
                tween = Astronaut.GetComponent<RectTransform>().DOScale(Vector3.zero, 1);
                tween.Play();
                Ship.GetComponent<RectTransform>().anchoredPosition = InitialPosition;
                particle.Stop();
                for (int i = 0; i < astronautSafe.Length; i++)
                {
                    astronautSafe[i].Play();
                }
                yield return tween.WaitForCompletion();

                yield return new WaitForSeconds(0.5f);

                tween = Astronaut.GetComponent<RectTransform>().DOScale(InitalScale, 1);
                tween.Play();
                yield return tween.WaitForCompletion();
            }

        }
    }
}
