using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RocketyRocket2
{
    public class SafeZone : MonoBehaviour
    {
        public int GalaxyNum = 1;
        public int PlanetNum = 1;

        public UnityEngine.Color idle;
        public UnityEngine.Color idleColor;

        public float lerpSpeed = 1;
        public bool startUpdateColor = false;

        public ShipController shipController;

        private SpriteRenderer sprite;

        [SerializeField] private GameObject endLevelUI;
        void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
            if (shipController == null)
            {
                shipController = GameObject.Find("Ship").GetComponent<ShipController>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (startUpdateColor)
            {
                StartCoroutine(UpdateColor());
            }

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ship"))
            {

                startUpdateColor = true;

                shipController.rigidbody2D.drag = 5f;
                shipController.boostInput = 0;

                shipController.boost_particle_1.Stop();
                shipController.boost_particle_2.Stop();
                shipController.boost_particle_3.Stop();

               

                Destroy(shipController);

                if (GalaxyNum == 1)
                    RocketyRocket2Game.Instance.SaveGameManager.Level_Green += 1;
                    RocketyRocket2Game.Instance.SaveGameManager.Save();

                if (GalaxyNum == 2)
                    RocketyRocket2Game.Instance.SaveGameManager.Level_Blue = PlanetNum + 1;
                    RocketyRocket2Game.Instance.SaveGameManager.Save();

                if (GalaxyNum == 3)
                    RocketyRocket2Game.Instance.SaveGameManager.Level_Purple = PlanetNum + 1;
                    RocketyRocket2Game.Instance.SaveGameManager.Save();

                if (GalaxyNum == 4)
                    RocketyRocket2Game.Instance.SaveGameManager.Level_Orange = PlanetNum + 1;
                    RocketyRocket2Game.Instance.SaveGameManager.Save();

                if (GalaxyNum == 5)
                    RocketyRocket2Game.Instance.SaveGameManager.Level_Red = PlanetNum + 1;
                    RocketyRocket2Game.Instance.SaveGameManager.Save();

                StartCoroutine(NextLevelAppear());

            }
        }
        private IEnumerator NextLevelAppear()
        {
            yield return new WaitForSeconds(1);
            endLevelUI.gameObject.SetActive(true);
            shipController.boost_particle_1.gameObject.SetActive(false);
            shipController.boost_particle_2.gameObject.SetActive(false);
            shipController.boost_particle_3.gameObject.SetActive(false);
        }
        private IEnumerator UpdateColor()
        {
            UnityEngine.Color lerpedColor = UnityEngine.Color.white;
            float currentTime = 0;

            while (this.enabled)
            {
                lerpedColor = UnityEngine.Color.Lerp(idle, idleColor, Mathf.PingPong(currentTime += (Time.deltaTime * lerpSpeed / 1), 1));

                sprite.color = lerpedColor;

                yield return new WaitForSeconds(0);
            }

            sprite.color = idle;
        }
    }
}
