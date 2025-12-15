using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketyRocket2
{
    public class ShipTutorial : MonoBehaviour
    {
        public GameObject Ship;
        public ParticleSystem particle;
        public float ShipMovement;

        public GameObject Astronaut;
        public ParticleSystem AstronautParticles;

        private Vector2 InitialPosition;
        

        private void Start()
        {
            InitialPosition = Ship.GetComponent<RectTransform>().anchoredPosition;
            StartCoroutine(TutorialShip());
        }
        private IEnumerator TutorialShip()
        {
            while (true)
            {

                particle.Play();
                Tween tween = Ship.GetComponent<RectTransform>().DOAnchorPosX(ShipMovement, 3.5f);

                tween.Play();
                yield return tween.WaitForCompletion();
                Ship.GetComponent<RectTransform>().anchoredPosition = InitialPosition;
                particle.Stop();
                Astronaut.SetActive(false);
                AstronautParticles.Play();

                yield return new WaitForSeconds(1.5f);
            }
        }
    }
}
