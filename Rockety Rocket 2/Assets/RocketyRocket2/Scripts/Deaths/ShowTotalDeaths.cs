using TMPro;
using UnityEngine;

namespace RocketyRocket2
{
    public class ShowTotalDeaths : MonoBehaviour
    {
        public TextMeshProUGUI text;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

            text.text = RocketyRocket2Game.Instance.SaveGameManager.GobalDeaths.ToString();
        }

        
    }
}
