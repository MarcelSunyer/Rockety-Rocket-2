using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RocketyRocket2
{
    public class PlanetLevelManager : MonoBehaviour
    {
        public int LevelsOperative = 1;
        public Color BlockedLevel;
        public Color UnblockedLevel;
        public Color UnblockedText;

        public int GalaxyNum;

        [SerializeField] private Button[] levels;
        [SerializeField] private Button updateLevels;

        private bool canBePlayed;


        private void Start()
        {
            if (RocketyRocket2Game.Instance != null)
            {
                if (GalaxyNum == 1)
                    LevelsOperative = RocketyRocket2Game.Instance.SaveGameManager.Level_Green;
                if (GalaxyNum == 2)
                    LevelsOperative = RocketyRocket2Game.Instance.SaveGameManager.Level_Blue;
                if (GalaxyNum == 3)
                    LevelsOperative = RocketyRocket2Game.Instance.SaveGameManager.Level_Purple;
                if (GalaxyNum == 4)
                    LevelsOperative = RocketyRocket2Game.Instance.SaveGameManager.Level_Orange;
                if (GalaxyNum == 5)
                    LevelsOperative = RocketyRocket2Game.Instance.SaveGameManager.Level_Red;
            }
            updateLevels.onClick.AddListener(BlockLevels);
        }
        public void BlockLevels()
        {
            for (int i = levels.Length - 1; i >= LevelsOperative; --i)
            {
                levels[i].image.color = BlockedLevel;
                levels[i].GetComponentInChildren<TextMeshProUGUI>().color = BlockedLevel;
                levels[i].enabled = false;
                levels[i].interactable = false;
            }

        }
        public void UnblockLevels()
        {
            for (int i = 0; i < LevelsOperative; ++i)
            {
                levels[i].image.color = UnblockedLevel;
                levels[i].GetComponentInChildren<TextMeshProUGUI>().color = UnblockedText;    
                levels[i].enabled = true;
                levels[i].interactable = true;
            }
        }


    }
}
