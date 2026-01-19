using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketyRocket2
{
    public class Bullet : MonoBehaviour
    {
        public ParticleSystem Death;
        public int TimeAlive;
        private void Start()
        {
            StartCoroutine(CountDown(TimeAlive));
        }

        private IEnumerator CountDown(float TimeAlive)
        {
            yield return new WaitForSeconds(TimeAlive);
            Death.Play();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.4f);
            gameObject.SetActive(false);
        }
        // Start is called before the first frame update
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.collider.CompareTag("Ship"))
            {
                gameObject.SetActive(false);
            }
            if (collision.collider.CompareTag("BlackHole"))
            {
                gameObject.SetActive(false);
            }

            if (collision.collider.CompareTag("Asteroid"))
            {
                StartCoroutine(CountDown(0));
            }
        }



    }
}
