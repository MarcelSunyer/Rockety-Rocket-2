using UnityEngine;
using UnityEngine.UI;

namespace RocketyRocket2
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField] private Button fullScrean;
        [SerializeField] private Button fxAudio;
        [SerializeField] private Button musicAudio;

        private void Start()
        {
            fullScrean.onClick.AddListener(ChangeFullSceran);
            fxAudio.onClick.AddListener(ChangeAudio);
            musicAudio.onClick.AddListener(ChangeMusic);
        }

        private void ChangeFullSceran()
        {
            Screen.fullScreen = !Screen.fullScreen;
        }

        private void ChangeAudio()
        {
            RocketyRocket2.RocketyRocket2Game.Instance.SaveGameManager.FxSound =
                RocketyRocket2.RocketyRocket2Game.Instance.SaveGameManager.FxSound == 1 ? 0 : 1;
        }
        private void ChangeMusic()
        {
            RocketyRocket2.RocketyRocket2Game.Instance.SaveGameManager.Music =
               RocketyRocket2.RocketyRocket2Game.Instance.SaveGameManager.Music == 1 ? 0 : 1;
        }
    }
}
