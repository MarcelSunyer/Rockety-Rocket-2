using UnityEngine;
using UnityEngine.SceneManagement;

namespace RocketyRocket2
{
    public class MainBootstrap : MonoBehaviour
    {
        public static SaveGameManager SaveManager { get; private set; }

        private void Awake()
        {
            // Asegurar que solo haya una instancia del bootstrap
            var existingBootstrap = FindAnyObjectByType<MainBootstrap>();
            if (existingBootstrap != null && existingBootstrap != this)
            {
                return;
            }

            DontDestroyOnLoad(gameObject);

            // Inicializar SaveGameManager
            if (SaveManager == null)
            {
                SaveManager = new SaveGameManager();
                SaveManager.Load();
            }
        }

        protected void PrepareGame()
        {
            // Aquí puedes agregar otras inicializaciones si las necesitas
            Debug.Log("Game prepared with SaveManager ready");
        }
    }
}