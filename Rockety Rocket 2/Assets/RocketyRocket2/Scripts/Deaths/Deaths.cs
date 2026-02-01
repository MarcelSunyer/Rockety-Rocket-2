using TMPro;
using UnityEngine;

namespace RocketyRocket2
{
    public class Deaths : MonoBehaviour
    {
        public TextMeshProUGUI text;


        private void Update()
        {
            text.text = MainBootstrap.SaveManager.GobalDeaths.ToString();
        }
        public void AddDeath()
        {
            MainBootstrap.SaveManager.GobalDeaths += 1;
            AddDeathText();
        }

        public void AddDeathText()
        {
            text.text = MainBootstrap.SaveManager.GobalDeaths.ToString();
            MainBootstrap.SaveManager.Save();
        }
    }
}