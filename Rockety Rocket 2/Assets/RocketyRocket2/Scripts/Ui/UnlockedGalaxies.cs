using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RocketyRocket2
{
    public class UnlockedGalaxies : MonoBehaviour
    {
        public Button[] Galaxies;
        public Button UpdateStateGalaxies;

        public Color color;

        public int galaxiesLocked = 0;

        private void Start()
        {
            UpdateStateGalaxies.onClick.AddListener(UnlockGalxies);
        }

        public void UnlockGalxies()
        {
            if(RocketyRocket2Game.Instance.SaveGameManager.Level_Green ==7)
            {
                galaxiesLocked = 1;
            }
            if (RocketyRocket2Game.Instance.SaveGameManager.Level_Blue == 7)
            {
                galaxiesLocked = 2;
            }
            if (RocketyRocket2Game.Instance.SaveGameManager.Level_Purple == 7)
            {
                galaxiesLocked = 3;
            }
            if (RocketyRocket2Game.Instance.SaveGameManager.Level_Orange == 7)
            {
                galaxiesLocked = 4;
            }



            for (int i = Galaxies.Length - 1; i >= galaxiesLocked; --i)
            {
                Galaxies[i].image.color = Color.black;
                Galaxies[i].GetComponentInChildren<SpriteRenderer>().color = Color.black;

                Galaxies[i].interactable = false;

            }

            for (int i = 0; i <= galaxiesLocked; ++i)
            {
                Galaxies[i].image.color = color;
                Galaxies[i].GetComponentInChildren<SpriteRenderer>().color = Color.white;

                Galaxies[i].interactable = true;

            }
        }

    }
}
