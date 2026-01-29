using System.Collections;
using UnityEngine;

namespace RocketyRocket2
{
    public class Bullet : MonoBehaviour
    {
        public ParticleSystem Death;
        public int TimeAlive = 5;

        private Rigidbody2D rb;
        private PauseMenu pauseMenu;

        private Vector2 savedVelocity;
        private float countdownRemaining;
        private bool isPaused = false;

        public AudioSource deathSound;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            pauseMenu = GameObject.Find("PauseMenu").GetComponent<PauseMenu>();
        }

        private void OnEnable()
        {
            countdownRemaining = TimeAlive;
            StartCoroutine(CountDownRoutine());
        }

        private void FixedUpdate()
        {
            if (pauseMenu.pauseActive)
            {
                if (!isPaused)
                {
                    // Guardamos velocidad y detenemos la f�sica
                    savedVelocity = rb.linearVelocity;
                    rb.linearVelocity = Vector2.zero;
                    rb.simulated = false;
                    isPaused = true;
                }
            }
            else
            {
                if (isPaused)
                {
                    // Restauramos f�sica
                    rb.simulated = true;
                    rb.linearVelocity = savedVelocity;
                    isPaused = false;
                }
            }
        }

        private IEnumerator CountDownRoutine()
        {
            while (countdownRemaining > 0)
            {
                if (!pauseMenu.pauseActive)
                {
                    countdownRemaining -= Time.deltaTime;
                }
                yield return null;
            }

            Death.Play();
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

            yield return new WaitForSeconds(0.4f);

            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ship") || collision.CompareTag("BlackHole"))
            {
                gameObject.SetActive(false);
            }

            if (collision.CompareTag("Asteroid"))
            {
                StartCoroutine(CountDownImmediate());
            }
        }

        private IEnumerator CountDownImmediate()
        {
            Death.Play();
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.4f);
            gameObject.SetActive(false);
        }
    }
}
