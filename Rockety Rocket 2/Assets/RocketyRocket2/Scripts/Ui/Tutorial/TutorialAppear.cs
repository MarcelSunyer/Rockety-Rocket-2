using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

namespace RocketyRocket2
{
    public class TutorialAppear : MonoBehaviour
    {


        [SerializeField] private ShipController shipState;

        [SerializeField] private GameObject cam;

        [SerializeField] private Button startGameplay;

        [SerializeField] private GameObject particleTutorial;

        [SerializeField] private Image shipTutorial;

        [SerializeField] private Image endTutorial;

        [SerializeField] private Image notendGameplay;

        [SerializeField] private ShipTutorial tutorialScript;

        [SerializeField] private SpriteRenderer goal;

        [SerializeField] private SpriteRenderer arrow; 

        [SerializeField] private GameObject particles;
        
        //Fade background fadetoblack
        [SerializeField] private Image fade;


        void Start()
        {

            particles.SetActive(false);
           shipState.currentState = ShipController.StateShip.Stop;
           cam.GetComponent<CameraFollow>().enabled = false;


           startGameplay.onClick.AddListener(StartGamePlay);
           startGameplay.Select();
           shipState.enabled = false;

           
        }

        private void StartGamePlay()
        {
            StartCoroutine(HideTutorialAndMove());
        }

        private IEnumerator HideTutorialAndMove()
        {
            if (particleTutorial != null)
            {
                particleTutorial.gameObject.SetActive(false);

            }
            Tween tween = gameObject.transform.DOMoveY(-200, 2);
            tween.Play();
            yield return tween.WaitForCompletion();

            if (tween != null)
            {
                tween = fade.DOFade(0f, 1f);
                tween.Play();
            }

            if (shipTutorial != null)
            {
                tween = shipTutorial.DOFade(0f, 1f);
                tween.Play();
            }

            if (endTutorial != null)
            {
                tween = endTutorial.DOFade(0f, 1f);
                tween.Play();
            }
            if (notendGameplay != null)
            {
                tween = notendGameplay.DOFade(0f, 1f);
                tween.Play();
            }

           
            cam.GetComponent<CameraFollow>().enabled = true;
            yield return new WaitForSeconds(2);

            if (goal != null)
            {
                goal.enabled = false;
            }
            if (arrow != null)
            {
                arrow.enabled = false;
            }
            //Activar particulas de boost al terminar el tutorial
            particles.SetActive(true);
            if(tutorialScript != null)
            { 
                tutorialScript.enabled = false;
            }
           
            shipState.enabled = true;
            shipState.currentState = ShipController.StateShip.Playing;


        }

    }
}
