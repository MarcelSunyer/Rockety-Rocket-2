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
        public ShipController ship;
        public AudioSource collected;
        private void Start()
        {
            
            IsDeath = false;
            astronautDied = GameObject.Find("AstronautDestroyed");
            AstronautGot = GameObject.Find("AstronautsGot");
            collected = GameObject.Find("Collected").GetComponent<AudioSource>();
            
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
            if (collision.gameObject.CompareTag("Asteroid") || collision.gameObject.CompareTag("Bullet"))
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
                yield return new WaitForSeconds(0.035f);
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

            if (RocketyRocket2.RocketyRocket2Game.Instance.SaveGameManager.FxSound == 1)
            {
                SoundManager.SoundManager.PlaySound(SoundManager.SoundValues.SoundType.Collected, collected, 0.005f);
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
