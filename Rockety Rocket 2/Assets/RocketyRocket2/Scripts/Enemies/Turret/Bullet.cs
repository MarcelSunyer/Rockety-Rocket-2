using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketyRocket2
{
    public class Bullet : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Ship"))
            {
                this.gameObject.GetComponent<ParticleSystem>().Stop();
                Destroy(this.gameObject);
            }
        }
        
    }
}
