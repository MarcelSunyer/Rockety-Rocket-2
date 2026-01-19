using UnityEngine;

namespace RocketyRocket2
{
    public class DeadBlackHole : MonoBehaviour
    {

        private ShipController ship;

        public void Start()
        {
            ship = GameObject.Find("Ship").GetComponent<ShipController>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Ship"))
            {
                ship.BlackHoleDeath = true;
            }

        }
    }
}
