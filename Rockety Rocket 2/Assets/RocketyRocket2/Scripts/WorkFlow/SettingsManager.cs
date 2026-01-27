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
     
        }

        private void ChangeFullSceran()
        {
            Screen.fullScreen = !Screen.fullScreen;
        }
    }
}
