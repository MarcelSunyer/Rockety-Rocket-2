using TMPro;
using UnityEngine;

namespace RocketyRocket2
{
    public class Deaths : MonoBehaviour
    {
        public TextMeshProUGUI text;

        void Start()
        {
            // Acceder a través del Bootstrap
            text.text = MainBootstrap.SaveManager.LevelsDeath.ToString();
        }

        public void AddDeath()
        {
            MainBootstrap.SaveManager.LevelsDeath += 1;
            MainBootstrap.SaveManager.GobalDeaths += 1;
            AddDeathText();
        }

        public void AddDeathText()
        {
            text.text = MainBootstrap.SaveManager.LevelsDeath.ToString();
            MainBootstrap.SaveManager.Save();
        }
    }
}