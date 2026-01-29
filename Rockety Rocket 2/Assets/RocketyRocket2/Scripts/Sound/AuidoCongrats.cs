using UnityEngine;

namespace RocketyRocket2
{
    public class AuidoCongrats : MonoBehaviour
    {
        public AudioSource asf;
        void Start()
        {
            SoundManager.SoundManager.PlaySound(SoundManager.SoundValues.SoundType.cUCARACHA, asf, 0.03f);
        }

    }
}
