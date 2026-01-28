using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RocketyRocket2
{
    public class Sounds : MonoBehaviour
{
        private GameObject lastSelected;
        public AudioSource soundSource;
        void Update()
        {
            EventSystem es = EventSystem.current;
            if (es == null) return;

            // ---- MOVE (navegación entre botones) ----
            if (es.currentSelectedGameObject != lastSelected)
            {
                if (es.currentSelectedGameObject != null &&
                    es.currentSelectedGameObject.GetComponent<Button>() != null)
                {
                    if (RocketyRocket2.RocketyRocket2Game.Instance.SaveGameManager.FxSound == 1)
                    {
                        SoundManager.SoundManager.PlaySound(SoundManager.SoundValues.SoundType.UiMovement, soundSource);
                    }
                }
                lastSelected = es.currentSelectedGameObject;
            }

            // ---- CLICK (mouse / enter / controller) ----
            if (IsSubmitPressed())
            {
                if (es.currentSelectedGameObject != null &&
                    es.currentSelectedGameObject.GetComponent<Button>() != null)
                {
                    if (RocketyRocket2.RocketyRocket2Game.Instance.SaveGameManager.FxSound == 1)
                    {
                        SoundManager.SoundManager.PlaySound(SoundManager.SoundValues.SoundType.UiSound, soundSource);
                    }
                }
            }
        }

        bool IsSubmitPressed()
        {
            // Mouse
            if (Input.GetMouseButtonDown(0))
                return true;

            // Teclado
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
                return true;

            // Controller (A / X)
            if (Input.GetKeyDown(KeyCode.JoystickButton0))
                return true;

            return false;
        }
    }
}
