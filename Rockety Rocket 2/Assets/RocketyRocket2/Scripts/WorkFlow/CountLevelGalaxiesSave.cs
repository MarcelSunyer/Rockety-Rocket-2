using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketyRocket2
{
    public class CountLevelGalaxiesSave : MonoBehaviour
    {
        public int countGalaxySaved;
        public int countLevelSavedGreen;
        public int countLevelSavedBlue;
        public int countLevelSavedPurple;
        public int countLevelSavedOrange;
        public int countLevelSavedRed;

        // Update is called once per frame
        void Update()
        {
            countGalaxySaved = RocketyRocket2Game.Instance.SaveGameManager.Galaxy;
            countLevelSavedGreen = RocketyRocket2Game.Instance.SaveGameManager.Level_Green;
            countLevelSavedBlue = RocketyRocket2Game.Instance.SaveGameManager.Level_Blue;
            countLevelSavedPurple = RocketyRocket2Game.Instance.SaveGameManager.Level_Purple;
            countLevelSavedOrange = RocketyRocket2Game.Instance.SaveGameManager.Level_Orange;
            countLevelSavedRed = RocketyRocket2Game.Instance.SaveGameManager.Level_Red;
        }
    }
}
