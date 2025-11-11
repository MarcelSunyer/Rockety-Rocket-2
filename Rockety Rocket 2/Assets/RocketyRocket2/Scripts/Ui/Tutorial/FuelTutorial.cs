using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RocketyRocket2
{
    public class FuelTutorial : MonoBehaviour
    {

        [SerializeField] private Image tankGas;

        [SerializeField] private Slider slider;
        [SerializeField] private float lowingValue;
        [SerializeField] private ParticleSystem boost;

        // Start is called before the first frame update
        void Start()
        {
            lowingValue = lowingValue / 5000;

            StartCoroutine(UpdateTankBar());
        }

        private IEnumerator UpdateTankBar()
        {
            while (true)
            {
                slider.value += lowingValue;
                yield return new WaitForEndOfFrame();

                if(slider.value >= 1)
                {
                    boost.Stop();
                    yield return new WaitForSeconds(3);
                    slider.value = 0;
                    boost.Play();
                }

            }
        }


       
    }
}
