using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RocketyRocket2
{
    public class AstronautsCounter : MonoBehaviour
    {

        public GameObject[] Astronauts;

        public GameObject AstronautsGot;

        public bool canFinish;

        [SerializeField] private GameObject father;

        private int TotalAstronauts;

        // Start is called before the first frame update
        void Start()
        {
            if (AstronautsGot != null)
            {
                if (Astronauts.Length == 0 || Astronauts[0].gameObject == null)
                {
                    father.SetActive(false);
                }
                else
                {
                    for (int i = 0; i < Astronauts.Length; i++)
                    {
                        TotalAstronauts += 1;
                        AstronautsGot.GetComponent<TextMeshProUGUI>().text = 0.ToString();
                    }
                }
            }
            gameObject.GetComponent<TextMeshProUGUI>().text = TotalAstronauts.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            if (AstronautsGot != null)
            {
                if (int.Parse(gameObject.GetComponent<TextMeshProUGUI>().text) == int.Parse(AstronautsGot.GetComponent<TextMeshProUGUI>().text))
                {

                    canFinish = true;
                }
            }
        }
    }
}
