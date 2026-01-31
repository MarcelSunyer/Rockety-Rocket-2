using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RocketyRocket2
{
    public class SafeZone : MonoBehaviour
    {
        //Keep Updateing Levels
        public int GalaxyNum = 1;
        public int PlanetNum = 1;

        public UnityEngine.Color idle;
        public UnityEngine.Color idleColor;

        public float lerpSpeed = 1;
        public bool startUpdateColor = false;

        public ShipController shipController;

        private SpriteRenderer sprite;

        [SerializeField] private GameObject endLevelUI;

        [SerializeField] private AstronautsCounter canPass;

        public AudioSource musicNormal;

        public AudioSource WinMusic;
        private bool played = false;
        void Start()
        {
            musicNormal = GameObject.Find("GameMusic").GetComponent<AudioSource>();
            WinMusic = GameObject.Find("WinMusic")?.GetComponent<AudioSource>();

            sprite = GetComponent<SpriteRenderer>();
            if (shipController == null)
            {
                shipController = GameObject.Find("Ship").GetComponent<ShipController>();
            }
            if (canPass != null)
            {
                if (canPass.Astronauts[0] != null)
                {
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (startUpdateColor)
            {
                StartCoroutine(UpdateColor());
            }

            if(canPass != null)
            {
                if (canPass.canFinish && !played)
                {
                    played = true;
                    if (RocketyRocket2.RocketyRocket2Game.Instance.SaveGameManager.FxSound == 1)
                    {
                        SoundManager.SoundManager.PlaySound(SoundManager.SoundValues.SoundType.allAstronauts_Safe, shipController.astronautdeath, 0.04f);
                    }
                    gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }

            }
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ship"))
            {

                //musicNormal.gameObject.SetActive(false);
                StartCoroutine(SoundManager.SoundManager.FadeOut(musicNormal, 1.5f));
                if (RocketyRocket2.RocketyRocket2Game.Instance.SaveGameManager.Music == 1)
                {
                    SoundManager.SoundManager.PlaySound(SoundManager.SoundValues.SoundType.WinMusic, WinMusic, 0.015f);
                }
                shipController.booOst.Stop();
                
                startUpdateColor = true;

                shipController.rigidbody2D.linearDamping = 5f;
                shipController.boostInput = 0;

                shipController.boost_particle_1.Stop();
                shipController.boost_particle_2.Stop();
                shipController.boost_particle_3.Stop();

                Destroy(shipController);

                if (GalaxyNum == 1)
                {
                    RocketyRocket2Game.Instance.SaveGameManager.Level_Green = PlanetNum + 1;
                    if (RocketyRocket2Game.Instance.SaveGameManager.Level_Green == 7 && RocketyRocket2Game.Instance.SaveGameManager.Level_Blue ==0)
                    {
                        RocketyRocket2Game.Instance.SaveGameManager.Galaxy = 2;
                        RocketyRocket2Game.Instance.SaveGameManager.Level_Blue = 1;
                    }
                    RocketyRocket2Game.Instance.SaveGameManager.Save();
                }
                if (GalaxyNum == 2)
                {
                    RocketyRocket2Game.Instance.SaveGameManager.Level_Blue = PlanetNum + 1;
                    if (RocketyRocket2Game.Instance.SaveGameManager.Level_Blue == 7 && RocketyRocket2Game.Instance.SaveGameManager.Level_Purple == 0)
                    {
                        RocketyRocket2Game.Instance.SaveGameManager.Galaxy = 3;
                        RocketyRocket2Game.Instance.SaveGameManager.Level_Purple = 1;
                    }
                    RocketyRocket2Game.Instance.SaveGameManager.Save();
                }
                if (GalaxyNum == 3)
                {
                    RocketyRocket2Game.Instance.SaveGameManager.Level_Purple = PlanetNum + 1;
                    if (RocketyRocket2Game.Instance.SaveGameManager.Level_Purple == 7 && RocketyRocket2Game.Instance.SaveGameManager.Level_Orange == 0)
                    {
                        RocketyRocket2Game.Instance.SaveGameManager.Galaxy = 4;
                        RocketyRocket2Game.Instance.SaveGameManager.Level_Orange = 1;
                    }
                    RocketyRocket2Game.Instance.SaveGameManager.Save();
                }
                if (GalaxyNum == 4)
                {
                    RocketyRocket2Game.Instance.SaveGameManager.Level_Orange = PlanetNum + 1;
                    if (RocketyRocket2Game.Instance.SaveGameManager.Level_Orange == 7 && RocketyRocket2Game.Instance.SaveGameManager.Level_Red == 0)
                    {
                        RocketyRocket2Game.Instance.SaveGameManager.Galaxy = 5;
                        RocketyRocket2Game.Instance.SaveGameManager.Level_Red = 1;

                    }
                    RocketyRocket2Game.Instance.SaveGameManager.Save();
                }
                if (GalaxyNum == 5)
                {
                    RocketyRocket2Game.Instance.SaveGameManager.Level_Red = PlanetNum + 1;
                    RocketyRocket2Game.Instance.SaveGameManager.Save();

                }
                StartCoroutine(NextLevelAppear());

            }
        }
        private IEnumerator NextLevelAppear()
        {
            yield return new WaitForSeconds(1);
            if (endLevelUI != null)
            {
                endLevelUI.gameObject.SetActive(true);
            }
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
