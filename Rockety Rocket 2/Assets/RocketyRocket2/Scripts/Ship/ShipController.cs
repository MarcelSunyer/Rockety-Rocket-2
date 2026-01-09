using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
namespace RocketyRocket2
{
    public class ShipController : MonoBehaviour
    {
        public enum StateShip
        {
            Stop,
            Playing,
            WithoutControls
        }
        public StateShip currentState = StateShip.Playing;
        private Vector2 saveForce = Vector2.zero;
        private Vector2 saveVelocity = Vector2.zero;
        private bool stopToPlay = false;

        public ParticleSystem boost_particle_1;
        public ParticleSystem boost_particle_2;
        public ParticleSystem boost_particle_3;

        public Rigidbody2D rigidbody2D;

        public float rotationSpeed = 200f;
        public float boostForce = 5f;

        private float rotationInput;
        public float boostInput;

        public ParticleSystem destroy_particle_1;
        public ParticleSystem destroy_particle_2;
        public ParticleSystem destroy_particle_3;

        public GameObject[] skins;

        [Header("TimeToStart")]
        public float TimeToStart;

        [Header("DeathStuff")]
        public Image FadeDeath;
        public Button[] DeathButtons;
        public GameObject textDestroyed;

        [Header("Boost")]
        public float valueBoost;
        [SerializeField] private Slider sliderBoost;
        [SerializeField] private bool activeBoost = true;

        [Header("Astronauts")]
        public Astronaut[] Astronauts;
        public GameObject TextLostaFriend;
        public bool friendDied;
        private bool particelsPlayed;


        private GameObject safeZone;

        void Start()
        {
            particelsPlayed = false;    
            friendDied = false;
            safeZone = GameObject.Find("End");

            if (skins != null)
            {
                currentState = StateShip.Stop;
                StartCoroutine(WaitToStart());

                for (int i = 0; i < skins.Length; i++)
                {
                    if (skins[i].name == "Skin_" + RocketyRocket2Game.Instance.SaveGameManager.Skin)
                    {
                        skins[i].SetActive(true);
                    }
                    else
                    {
                        skins[i].SetActive(false);
                    }
                }
            }
            if (activeBoost)
            {
                sliderBoost.gameObject.SetActive(true);
            }
            boostForce = boostForce / 30000;
        }
        void FixedUpdate()
        {
            switch (currentState)
            {
                case StateShip.Stop:
                    StopShip();
                    break;
                case StateShip.Playing:
                    MoveShip();
                    break;
                case StateShip.WithoutControls:
                    break;
            }

            if (sliderBoost != null && activeBoost)
            {
                if (sliderBoost.value >= sliderBoost.maxValue)
                {
                    currentState = StateShip.Stop;
                }
            }
            //Slider value reversed

            if (boostInput == 0)
            {
                boost_particle_1.Stop();
                boost_particle_2.Stop();
                boost_particle_3.Stop();
            }
            else
            {
                if (sliderBoost != null && activeBoost)
                {
                    sliderBoost.value += valueBoost;
                }
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                safeZone.GetComponent<BoxCollider2D>().enabled = true;
                safeZone.transform.position = gameObject.transform.position;
            }
            for (int i = 0; i < Astronauts.Length; i++)
            {
                if (Astronauts[i].IsDeath)
                {
                    StartCoroutine(AstronautDeath());
                }
            }
        }

        private void MoveShip()
        {
            if (stopToPlay)
            {
                boost_particle_1.gameObject.SetActive(true);
                boost_particle_2.gameObject.SetActive(true);
                boost_particle_3.gameObject.SetActive(true);
                rigidbody2D.velocity = saveVelocity;
                stopToPlay = false;
            }
            rigidbody2D.rotation -= rotationInput * rotationSpeed * Time.fixedDeltaTime;

            Vector2 direction = transform.up;
            saveForce = direction * boostInput * boostForce;
            rigidbody2D.AddForce(saveForce);
            saveVelocity = rigidbody2D.velocity;

        }

        private void StopShip()
        {
            rotationInput = 0;

            stopToPlay = true;
            boostInput = 0;
            if (sliderBoost != null && activeBoost || friendDied)
            {
                if (sliderBoost.value >= sliderBoost.maxValue || friendDied)
                {
                    StartCoroutine(StopParticles());
                    if (rigidbody2D.rotation == 0)
                    {
                        rigidbody2D.rotation = 0 * Time.fixedDeltaTime;
                        return;
                    }

                    if (rigidbody2D.rotation < 0)
                    {
                        rigidbody2D.rotation -= rotationSpeed * Time.fixedDeltaTime;
                        return;
                    }

                    if (rigidbody2D.rotation > 0)
                    {
                        rigidbody2D.rotation -= -rotationSpeed * Time.fixedDeltaTime;
                        return;
                    }


                }
            }
            rigidbody2D.AddForce(Vector2.zero);
            rigidbody2D.velocity = Vector2.zero;
        }

        public IEnumerator StopParticles()
        {
            yield return new WaitForSeconds(2);

            boost_particle_1.gameObject.SetActive(false);
            boost_particle_2.gameObject.SetActive(false);
            boost_particle_3.gameObject.SetActive(false);
        }
        public void Rotation(InputAction.CallbackContext context)
        {
            rotationInput = context.ReadValue<Vector2>().x;
        }

        public void Boost(InputAction.CallbackContext context)
        {
            boostInput = context.ReadValue<float>();
            if (boostInput > 0)
            {
                boost_particle_1.Play();
                boost_particle_2.Play();
                boost_particle_3.Play();
            }
            else
            {
                boost_particle_1.Stop();
                boost_particle_2.Stop();
                boost_particle_3.Stop();
            }
        }


        public void ShipDestroyed()
        {
            for (int i = 0; i < skins.Length; i++)
            {

                skins[i].SetActive(false);

            }
            if (!particelsPlayed)
            {
                destroy_particle_1.Play();
                destroy_particle_2.Play();
                destroy_particle_3.Play();
                particelsPlayed = true;
            }

            boost_particle_1.Stop();
            boost_particle_2.Stop();
            boost_particle_3.Stop();

            boost_particle_1.gameObject.SetActive(false);
            boost_particle_2.gameObject.SetActive(false);
            boost_particle_3.gameObject.SetActive(false);


            gameObject.GetComponent<Collider2D>().enabled = false;
            
            StartCoroutine(DestroyShip());
            
         
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Asteroid") || collision.gameObject.CompareTag("Turret"))
            {
                ShipDestroyed();
            }
        }
        private IEnumerator DestroyShip()
        {
            if (!friendDied)
            {
                currentState = StateShip.Stop;
                yield return new WaitForSeconds(1);
                if (FadeDeath)
                    FadeDeath.gameObject.SetActive(true);
                Tween tween = FadeDeath.DOFade(0.8f, 1);
                tween.Play();

                yield return tween.WaitForCompletion();

                textDestroyed.SetActive(true);

                yield return new WaitForSeconds(1f);


                for (int i = 0; i < DeathButtons.Length; ++i)
                {
                    DeathButtons[i].gameObject.SetActive(true);
                    yield return new WaitForSeconds(0.75f);
                }

                Destroy(this.gameObject);
            }
            else
            {
                if (FadeDeath)
                    FadeDeath.gameObject.SetActive(true);
                Tween tween = FadeDeath.DOFade(0.8f, 1);
                tween.Play();

                yield return tween.WaitForCompletion();
                if(TextLostaFriend != null)
                    TextLostaFriend.SetActive(true);

                yield return new WaitForSeconds(1f);

                for (int i = 0; i < DeathButtons.Length; ++i)
                {
                    DeathButtons[i].gameObject.SetActive(true);
                    yield return new WaitForSeconds(0.75f);
                }
                
                Destroy(this.gameObject);
            }
        }

        private IEnumerator WaitToStart()
        {
            yield return new WaitForSeconds(TimeToStart);
            currentState = StateShip.Playing;
        }

        private IEnumerator AstronautDeath()
        {
            friendDied = true;
            StopShip();
            yield return new WaitForSeconds(3.5f);

            ShipDestroyed();


        }
    }
}

