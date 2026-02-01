using DG.Tweening.Core.Easing;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RocketyRocket2
{
    public class MainBootstrap : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
    
            RocketyRocket2Game.Instance.SaveGameManager.Load();
        }

        protected void PrepareGame()
        {

            // Aquí puedes agregar otras inicializaciones si las necesitas
            Debug.Log("Game prepared with SaveManager ready");
        }
    }
}