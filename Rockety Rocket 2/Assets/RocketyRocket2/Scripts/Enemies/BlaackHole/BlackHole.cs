using UnityEngine;

namespace RocketyRocket2
{
    public class BlackHole : MonoBehaviour
    {
        public float gravityForce = 50f;

        private Rigidbody2D shipRb;
        private bool fauxGravityEnabled;
        private bool BulletGravityEnabled;

        private Rigidbody2D bullet;



        void Start()
        {
            gravityForce = gravityForce / 5000;
            shipRb = GameObject.FindGameObjectWithTag("Ship").GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            if (fauxGravityEnabled && shipRb != null)
            {
                Vector2 dir = (Vector2)transform.position - shipRb.position;
                float dist = dir.magnitude;

                if (dist >= 0.1f)
                    shipRb.AddForce(dir.normalized * gravityForce);
            }

            if (BulletGravityEnabled && bullet != null)
            {
                Vector2 dir = (Vector2)transform.position - bullet.position;
                float dist = dir.magnitude;

                if (dist >= 0.1f)
                    bullet.AddForce(dir.normalized * (gravityForce * 50000));
            }
        }


        void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.CompareTag("Ship"))
                fauxGravityEnabled = true;

            if (coll.CompareTag("Bullet"))
            {
                bullet = coll.attachedRigidbody;
                BulletGravityEnabled = bullet != null;
            }
        }

        void OnTriggerExit2D(Collider2D coll)
        {
            if (coll.CompareTag("Ship"))
                fauxGravityEnabled = false;

            if (coll.CompareTag("Bullet"))
            {
                BulletGravityEnabled = false;
                bullet = null;
            }
        }
    }
}
