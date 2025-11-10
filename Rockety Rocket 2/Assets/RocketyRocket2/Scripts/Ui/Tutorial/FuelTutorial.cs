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
        [SerializeField] private float LowingValue;

        // Start is called before the first frame update
        void Start()
        {
            LowingValue = LowingValue / 1000;

            StartCoroutine(UpdateTankBar());
        }

        private IEnumerator UpdateTankBar()
        {
            while (true)
            {
                slider.value += LowingValue;
                yield return new WaitForEndOfFrame();

                if(slider.value <= 1)
                {
                    yield return new WaitForSeconds(1);
                    slider.value = 0;
                }

            }
        }


       
    }
}
