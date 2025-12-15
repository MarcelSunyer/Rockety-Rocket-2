using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketyRocket2
{
    public class Astronaut : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] particleSystems;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Ship"))
            {

                StartCoroutine(AstrnautCollected());

            }
        }
        private IEnumerator AstrnautCollected()
        {
            while (gameObject.transform.localScale.x >= 0)
            {
                gameObject.transform.localScale -= new Vector3(0.06f, 0.06f, 0f);
                gameObject.transform.Rotate(0,0,50);
                yield return new WaitForSeconds(0.055f);
            }
            for (int i = 0; i < particleSystems.Length; i++)
            {
                particleSystems[i].Play();
            }
            yield return new WaitForSeconds(2f);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            
            yield return new WaitForSeconds(2f);

            gameObject.SetActive(false);
        }
    }
}
