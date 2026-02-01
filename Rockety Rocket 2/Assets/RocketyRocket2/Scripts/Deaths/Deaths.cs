using TMPro;
using UnityEngine;

namespace RocketyRocket2
{
    public class Deaths : MonoBehaviour
    {
        public TextMeshProUGUI text;

        public bool stopSumar;

        private void Update()
        {
            text.text = RocketyRocket2Game.Instance.SaveGameManager.GobalDeaths.ToString();
        }
        public void AddDeath()
        {
            RocketyRocket2Game.Instance.SaveGameManager.GobalDeaths += 1;
            AddDeathText();
        }

        public void AddDeathText()
        {
            text.text = RocketyRocket2Game.Instance.SaveGameManager.GobalDeaths.ToString();
            RocketyRocket2Game.Instance.SaveGameManager.Save();
        }
    }
}