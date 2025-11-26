using log4net.Core;
using RocketyRocket2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheats : MonoBehaviour
{
    public Button CheatsButton;

    [SerializeField] private UnlockedGalaxies unlockedGalaxies;

    [SerializeField] private PlanetLevelManager[] levels;


    private Vector2 buttonPosition;
    // Start is called before the first frame update
    void Start()
    {
        CheatsButton.onClick.AddListener(OpenCheats);
        buttonPosition = CheatsButton.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheatsButton.transform.position = buttonPosition;
    }

    private void OpenCheats()
    {
        levels[0].LevelsOperative = 6;
        RocketyRocket2Game.Instance.SaveGameManager.Level_Green = 7;

        RocketyRocket2Game.Instance.SaveGameManager.Level_Blue = 7;

        RocketyRocket2Game.Instance.SaveGameManager.Level_Orange = 7;

        RocketyRocket2Game.Instance.SaveGameManager.Level_Purple = 7;

        RocketyRocket2Game.Instance.SaveGameManager.Level_Red = 7;

        unlockedGalaxies.UnlockGalxies();

        for (int i = 0; i < levels.Length; ++i)
        {
            levels[i].LevelsOperative = 6;

            levels[i].UpdateLevels();
        }

    }
}
