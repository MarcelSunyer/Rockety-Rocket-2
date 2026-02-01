using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
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
            Pause
        }

        public GameObject canvasForcer;

        public BladeFunction[] BladeFunctions;
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

        [Header("DeathStuff")]
        public Image FadeDeath;
        public Button[] DeathButtons;
        public GameObject textDestroyed;
        public bool BlackHoleDeath = false;
        public Deaths counterDeaths;


        [Header("Boost")]
        public float valueBoost;
        [SerializeField] private Slider sliderBoost;
        [SerializeField] private bool activeBoost = true;
        public bool started= false;
        private bool coroutineStarted = false;

        [Header("Astronauts")]
        public Astronaut[] Astronauts;
        public GameObject TextLostaFriend;
        public bool friendDied;
        private bool particelsPlayed;

        private Vector2 pausedVelocity;
        private float pausedAngularVelocity;

        private GameObject safeZone;

        public bool canPressAnyKey = true;

        public GameObject PressAnyKey;
        private bool justResumed = false;

        public AudioSource booOst;
        public AudioSource gamePlayMusic;
        public AudioSource death;
        public AudioSource gameOver;
        public AudioSource astronautdeath;

        public GameObject AudioManager;

        private bool boostSoundPlaying = false;
        private bool isShipDestroyed = false;
       
        void Start()
        {
            if (!GameObject.Find("AudioManager"))
            {
                Instantiate(AudioManager);
            }

            if (RocketyRocket2.RocketyRocket2Game.Instance.SaveGameManager.Music == 1)
            {
            SoundManager.SoundManager.PlaySound(GetRandomAmbience(), gamePlayMusic, 0.15f);
                
            }
            currentState = StateShip.Stop;
            particelsPlayed = false;    
            friendDied = false;
            safeZone = GameObject.Find("End");

           if(!canPressAnyKey)
            {
                PressAnyKey.gameObject.SetActive(false);
                StartCoroutine(WaitForStart());
            }

                boostForce = boostForce / 30000;
            counterDeaths.gameObject.SetActive(true);
        }
        void FixedUpdate()
        {
            if (currentState == StateShip.Pause)
            {
                boost_particle_1.gameObject.SetActive(false);
                boost_particle_2.gameObject.SetActive(false);
                boost_particle_3.gameObject.SetActive(false);
                return;
            }

            switch (currentState)
            {
                case StateShip.Playing:
                    MoveShip();
                    break;
                case StateShip.Stop:
                    StopShip();
                    break;
            }

            if (sliderBoost != null && activeBoost)
            {
                if (sliderBoost.value >= sliderBoost.maxValue)
                {
                    currentState = StateShip.Stop;
                }
            }

            if (boostInput == 0)
            {
                // STOP boost
                if (boostSoundPlaying)
                {
                    StartCoroutine(SoundManager.SoundManager.FadeOut(booOst, 0.4f));
                    boostSoundPlaying = false;
                }

                boost_particle_1.Stop();
                boost_particle_2.Stop();
                boost_particle_3.Stop();
            }
            else
            {
                // START boost
                if (!boostSoundPlaying)
                {
                    if (RocketyRocket2.RocketyRocket2Game.Instance.SaveGameManager.FxSound == 1)
                    {
                        SoundManager.SoundManager.PlaySound(
                            SoundManager.SoundValues.SoundType.Boost,
                            booOst, 0.1f
                        );
                    }

                    boostSoundPlaying = true;
                }

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
            if (Astronauts != null)
            {
                for (int i = 0; i < Astronauts.Length; i++)
                {

                    if (Astronauts[i].IsDeath)
                    {
                        StartCoroutine(AstronautDeath());
                    }
                }
            }
            else
            {

            }
            if (BlackHoleDeath)
            {

                Sequence seq_ = DOTween.Sequence();
                Tween tween = seq_.Join(gameObject.transform.DOScale(new Vector3(0, 0, 0), 1));
                seq_.Join(gameObject.transform.DORotate(new Vector3(0, 0, 630), 1, RotateMode.FastBeyond360));
                seq_.Play();

                tween.WaitForCompletion();
                StopShip();
                StartCoroutine(DestroyShipOnHole());
            }
            if (canPressAnyKey)
            {
                if (!started && (
                        Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) ||
                        (Gamepad.current != null && (
                            Gamepad.current.aButton.IsPressed() ||
                            Gamepad.current.bButton.IsPressed() ||
                            Gamepad.current.xButton.IsPressed() ||
                            Gamepad.current.yButton.IsPressed() ||
                            Gamepad.current.rightTrigger.IsPressed() ||
                            Gamepad.current.leftTrigger.IsPressed() ||
                            Gamepad.current.leftShoulder.IsPressed() ||
                            Gamepad.current.leftStick.IsPressed() ||
                            Gamepad.current.leftStickButton.IsPressed() ||
                            Gamepad.current.rightShoulder.IsPressed() ||
                            Gamepad.current.rightStick.IsPressed() ||
                            Gamepad.current.rightStickButton.IsPressed() ||
                            Gamepad.current.leftStick.IsPressed() ||
                            Gamepad.current.rightStick.IsPressed()))
                ))
                {
                    started = true;
                }
            }

            if (started && !coroutineStarted && PressAnyKey)
            {
                coroutineStarted = true;
                StartCoroutine(StartShip());
            }
        }
        private IEnumerator DestroyShipOnHole()
        {
            yield return new WaitForSeconds(1f);
            ShipDestroyed();
        }
        private void MoveShip()
        {
            canvasForcer.GetComponent<SelctionForcer>().enabled = false;
            float newRotation =
                rigidbody2D.rotation - rotationInput * rotationSpeed * Time.fixedDeltaTime;

            rigidbody2D.MoveRotation(newRotation);

            Vector2 direction = transform.up;
            Vector2 force = direction * boostInput * boostForce;
            rigidbody2D.AddForce(force);

            saveVelocity = rigidbody2D.linearVelocity;
        }




        private void StopShip()
        {
            canvasForcer.GetComponent<SelctionForcer>().enabled = true;
           
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
            rigidbody2D.linearVelocity = Vector2.zero;
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
            StartCoroutine(SoundManager.SoundManager.FadeOut(gamePlayMusic, 0.3f));
            if (RocketyRocket2.RocketyRocket2Game.Instance.SaveGameManager.FxSound == 1)
            {
                SoundManager.SoundManager.PlaySound(SoundManager.SoundValues.SoundType.Death, death, 0.02f);
            }
            if (RocketyRocket2.RocketyRocket2Game.Instance.SaveGameManager.Music == 1)
            {
                SoundManager.SoundManager.PlaySound(SoundManager.SoundValues.SoundType.DeathMusic, gameOver, 0.02f);
            }

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
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Bullet") )
            {
                ShipDestroyed();
            }
            if(collision.CompareTag("End"))
            {

                if (BladeFunctions != null)
                {
                    for (int i = 0; i < BladeFunctions.Length; ++i)
                    {
                        BladeFunctions[i].squareSize = 0;
                    }
                }
            }
            
        }
        private IEnumerator DestroyShip()
        {
            if(isShipDestroyed)
            {
                yield break;
            }

            isShipDestroyed = true;

            counterDeaths.AddDeath();

            if (BladeFunctions != null)
            {
                for (int i = 0; i < BladeFunctions.Length; ++i)
                {
                    BladeFunctions[i].squareSize = Mathf.MoveTowards(BladeFunctions[i].squareSize, 0f, 5f * Time.deltaTime);


                    if (BladeFunctions[i].squareSize < 0.01f)
                    {
                        BladeFunctions[i].squareSize = 0f;
                    }
                }
            }
            sliderBoost.gameObject.SetActive(false);
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
                    DeathButtons[0].Select();
                    DeathButtons[i].gameObject.SetActive(true);
                    yield return new WaitForSeconds(0.75f);
                }

                //Destroy(this.gameObject);
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
                    DeathButtons[0].Select();
                    DeathButtons[i].gameObject.SetActive(true);
                    yield return new WaitForSeconds(0.75f);
                }
                
                Destroy(this.gameObject);
            }

        }

        public IEnumerator StartShip()
        {
            if(canPressAnyKey)
            {
                Tween tween = PressAnyKey.GetComponent<RectTransform>().transform.DOMoveY(-600, 2);
                tween.Play();
                yield return tween.WaitForCompletion();
            }
            Debug.Log("State:Playing");
            if (activeBoost)
            {
                sliderBoost.gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(1);
            PressAnyKey.gameObject.SetActive(false);

            if (BladeFunctions != null)
            {
                for (int i = 0; i < BladeFunctions.Length; ++i)
                {

                    Color color = BladeFunctions[i].lineColor;

                    color.a = Mathf.MoveTowards(color.a, 160f, 1.5f * Time.deltaTime);

                    BladeFunctions[i].lineColor = color;

                }
            }
            yield return new WaitForSeconds(0.5f);
            currentState = StateShip.Playing;
        }

        private IEnumerator AstronautDeath()
        {
            friendDied = true;
            StopShip();
            yield return new WaitForSeconds(3.5f);

            ShipDestroyed();


        }
        public void Pause()
        {
            booOst.Stop();
            gamePlayMusic.Stop();
            if (currentState == StateShip.Pause)
                return;

            canvasForcer.GetComponent<SelctionForcer>().enabled = true;
            currentState = StateShip.Pause;

            // Guardamos estado físico
            pausedVelocity = rigidbody2D.linearVelocity;
            pausedAngularVelocity = rigidbody2D.angularVelocity;

            // Paramos físicas
            rigidbody2D.linearVelocity = Vector2.zero;
            rigidbody2D.angularVelocity = 0f;
            rigidbody2D.simulated = false;

            // Cortamos inputs
            rotationInput = 0f;
            boostInput = 0f;

            // Partículas OFF
            boost_particle_1.gameObject.SetActive(false);
            boost_particle_2.gameObject.SetActive(false);
            boost_particle_3.gameObject.SetActive(false);
        }


        public void Resume()
        {
            if (RocketyRocket2.RocketyRocket2Game.Instance.SaveGameManager.Music == 1)
            {
                SoundManager.SoundManager.PlaySound(GetRandomAmbience(), gamePlayMusic, 0.04f);
            }

            canvasForcer.GetComponent<SelctionForcer>().enabled = false;
            rigidbody2D.simulated = true;

            rigidbody2D.linearVelocity = saveVelocity;

            currentState = StateShip.Playing;

            this.enabled = true;
            boost_particle_1.gameObject.SetActive(true);
            boost_particle_2.gameObject.SetActive(true);
            boost_particle_3.gameObject.SetActive(true);
        }

        private SoundManager.SoundValues.SoundType GetRandomAmbience()
        {
            SoundManager.SoundValues.SoundType[] ambiences =
            {
        SoundManager.SoundValues.SoundType.GamePlay_1,
        SoundManager.SoundValues.SoundType.GamePlay_2,
    };

            return ambiences[Random.Range(0, ambiences.Length)];
    
        }
        private IEnumerator WaitForStart()

        {
            currentState = StateShip.Stop;
            yield return new WaitForSeconds(3);
            currentState = StateShip.Playing;
        }
    }

   

}

