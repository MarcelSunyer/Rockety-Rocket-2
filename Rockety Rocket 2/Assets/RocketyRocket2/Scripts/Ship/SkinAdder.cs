using UnityEngine;

namespace RocketyRocket2
{
    public class SkinAdder : MonoBehaviour
    {
        public GameObject[] skins;
        void Start()
        {
            for (int i = 0; i < skins.Length; i++)
            {
                if (skins[i].name == "Skin_" + RocketyRocket2Game.Instance.SaveGameManager.Skin)
                {
                    skins[i].SetActive(true);
                }
                else
                {
                    skins[i].SetActive(false);
                }
            }
        }

      
    }
}
