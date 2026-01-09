using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RocketyRocket2
{
    public class Astronaut : MonoBehaviour
    {
        public GameObject AstronautGot;
        [SerializeField] private ParticleSystem[] particleSystems;
        [SerializeField] private ParticleSystem[] death;
        public bool IsDeath;

        private GameObject astronautDied;
        

        private void Start()
        {
            IsDeath = false;
            astronautDied = GameObject.Find("AstronautDestroyed");
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Ship"))
            {
                var tmp = AstronautGot.GetComponent<TextMeshProUGUI>();

                if (int.TryParse(tmp.text, out int value))
                {
                    tmp.text = (value + 1).ToString();
                }

                StartCoroutine(AstrnautCollected());


            }
            if (collision.gameObject.CompareTag("Asteroid"))
            {
                StartCoroutine(Die());

            }

        }
        private IEnumerator Die()
        {
            IsDeath = true;
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            while (gameObject.transform.localScale.x >= 0)
            {
                gameObject.transform.localScale -= new Vector3(0.06f, 0.06f, 0f);
                gameObject.transform.Rotate(0, 0, 50);
                yield return new WaitForSeconds(0.055f);
            }
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            for (int i = 0; i < death.Length; i++)
            {
                death[i].Play();
            }


            yield return new WaitForSeconds(2f);

            Destroy(gameObject);
        }
        private IEnumerator AstrnautCollected()
        {
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            
            while (gameObject.transform.localScale.x >= 0)
            {
                gameObject.transform.localScale -= new Vector3(0.06f, 0.06f, 0f);
                gameObject.transform.Rotate(0,0,50);
                yield return new WaitForSeconds(0.035f);
            }
            

            for (int i = 0; i < particleSystems.Length; i++)
            {
                particleSystems[i].Play();
            }
            yield return new WaitForSeconds(2f);

            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            
            
            yield return new WaitForSeconds(2f);

            gameObject.SetActive(false);
        }
    }
}
