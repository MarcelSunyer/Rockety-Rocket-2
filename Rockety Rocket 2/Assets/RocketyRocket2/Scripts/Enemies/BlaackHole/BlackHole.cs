using UnityEngine;

namespace RocketyRocket2
{
    public class BlackHole : MonoBehaviour
    {
        public float gravityForce = 50f;

        private Rigidbody2D shipRb;
        private bool fauxGravityEnabled;

        void Start()
        {
            gravityForce = gravityForce / 5000;
            shipRb = GameObject.FindGameObjectWithTag("Ship").GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            if (!fauxGravityEnabled) return;

            Vector2 direction = (Vector2)transform.position - shipRb.position;
            float distance = direction.magnitude;

            // Optional safety
            if (distance < 0.1f) return;

            Vector2 force = direction.normalized * gravityForce;
            shipRb.AddForce(force, ForceMode2D.Force);
        }

        void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.CompareTag("Ship"))
                fauxGravityEnabled = true;
        }

        void OnTriggerExit2D(Collider2D coll)
        {
            if (coll.CompareTag("Ship"))
                fauxGravityEnabled = false;
        }
    }
}
