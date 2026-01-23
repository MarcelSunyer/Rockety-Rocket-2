using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace RocketyRocket2
{
    public class SeeLevelTutorial : MonoBehaviour
    {
        public ShipController ship;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            StartCoroutine(TutorialSeeMap()); 
        }
        private void Update()
        {
            ship.boostInput = 0;
        }
        private IEnumerator TutorialSeeMap()
        {
            yield return new WaitForSeconds(3.5f);
            Tween tween = gameObject.GetComponent<RectTransform>().DOScale(new Vector3(2,2,2),2);
            tween.Play();
            yield return new WaitForSeconds(4f);
            tween = gameObject.GetComponent<RectTransform>().DOScale(new Vector3(1, 1, 1), 0.3f);
            tween.Play();
            StartCoroutine(TutorialSeeMap());

            
        }

    }
}
