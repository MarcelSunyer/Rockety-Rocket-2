using RocketyRocket2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheats : MonoBehaviour
{
    [SerializeField] private UnlockedGalaxies unlockedGalaxies;

    [SerializeField] private bool hackActive = false;


    [SerializeField] private PlanetLevelManager[] levels;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            RocketyRocket2Game.Instance.SaveGameManager.Level_Green = 6;

            RocketyRocket2Game.Instance.SaveGameManager.Level_Blue = 0;
            RocketyRocket2Game.Instance.SaveGameManager.Level_Orange = 0;
            RocketyRocket2Game.Instance.SaveGameManager.Level_Purple = 0;
            RocketyRocket2Game.Instance.SaveGameManager.Level_Red = 0;

            UpdateLevelsHack(0);

            unlockedGalaxies.UnlockGalxies();
            hackActive = !hackActive;
            unlockedGalaxies.BlockGalaxies(4);
            UpdateLevelsHack(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            RocketyRocket2Game.Instance.SaveGameManager.Level_Green = 7;
            RocketyRocket2Game.Instance.SaveGameManager.Level_Blue = 0;

            RocketyRocket2Game.Instance.SaveGameManager.Level_Orange = 0;
            RocketyRocket2Game.Instance.SaveGameManager.Level_Purple = 0;
            RocketyRocket2Game.Instance.SaveGameManager.Level_Red = 0;

            UpdateLevelsHack(1);

            unlockedGalaxies.UnlockGalxies();       
            hackActive = !hackActive;
            unlockedGalaxies.BlockGalaxies(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            RocketyRocket2Game.Instance.SaveGameManager.Level_Blue = 7;
            RocketyRocket2Game.Instance.SaveGameManager.Level_Orange = 0;

            RocketyRocket2Game.Instance.SaveGameManager.Level_Purple = 0;
            RocketyRocket2Game.Instance.SaveGameManager.Level_Red = 0;

            UpdateLevelsHack(2);

            unlockedGalaxies.UnlockGalxies();
            hackActive = !hackActive;
            unlockedGalaxies.BlockGalaxies(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            RocketyRocket2Game.Instance.SaveGameManager.Level_Purple = 7;
            RocketyRocket2Game.Instance.SaveGameManager.Level_Orange = 0;

            RocketyRocket2Game.Instance.SaveGameManager.Level_Red = 0;

            UpdateLevelsHack(3);
            
            unlockedGalaxies.UnlockGalxies();
            hackActive = !hackActive;
            unlockedGalaxies.BlockGalaxies(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            RocketyRocket2Game.Instance.SaveGameManager.Level_Orange = 7;
            RocketyRocket2Game.Instance.SaveGameManager.Level_Red = 0;

            unlockedGalaxies.UnlockGalxies();

            levels[4].LevelsOperative = 6;
            UpdateLevelsHack(4);

            hackActive = !hackActive;
        }

    }

    private void UpdateLevelsHack(int i)
    { 
        levels[i].LevelsOperative = 6;
        levels[i].UnblockLevels();
    }

}
