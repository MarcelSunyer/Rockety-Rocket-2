using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RocketyRocket2
{
    public class TutorialAppear : MonoBehaviour
    {
        [SerializeField] private ShipController shipState;

        [SerializeField] private GameObject cam;

        [SerializeField] private Button startGameplay;

        [SerializeField] private Image fade;

        [SerializeField] private GameObject particleTutorial;

        [SerializeField] private Image shipTutorial;

        [SerializeField] private Image endTutorial;

        [SerializeField] private Image notendGameplay;

        [SerializeField] private ShipTutorial tutorialScript;

        [SerializeField] private SpriteRenderer goal;

        [SerializeField] private SpriteRenderer arrow; 

        void Start()
        {
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
            particleTutorial.gameObject.SetActive(false);

            Tween tween = gameObject.transform.DOMoveY(-200, 2);
            tween.Play();
            yield return tween.WaitForCompletion();

            tween = fade.DOFade(0f,1f);
            tween.Play();

            tween = shipTutorial.DOFade(0f, 1f);
            tween.Play();
           
            tween = endTutorial.DOFade(0f, 1f);
            tween.Play();
            
            tween = notendGameplay.DOFade(0f, 1f);
            tween.Play();
            tutorialScript.enabled = false;
            cam.GetComponent<CameraFollow>().enabled = true;
            yield return new WaitForSeconds(2);
            goal.enabled = false;

            arrow.enabled = false;
            shipState.enabled = true;
            shipState.currentState = ShipController.StateShip.Playing;


        }

    }
}
