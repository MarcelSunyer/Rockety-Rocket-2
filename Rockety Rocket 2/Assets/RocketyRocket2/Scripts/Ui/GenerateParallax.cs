using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RocketyRocket2
{
    public class GenerateParallax : MonoBehaviour
    {
        public float radius = 1.0f;
        public GameObject star;
        public GameObject[] stars = new GameObject[150];
        [Range(0f, 1f)]
        public float alpha = 0.5f;
        public GameObject parent;

        void Start()
        {
            int count = 0;
            while (count < stars.Length)
            {
                var randomPos = new Vector2(Random.Range(-radius, radius), Random.Range(-radius, radius));

                if (randomPos.magnitude <= radius)
                {

                    var newStar = Instantiate(star, randomPos, Quaternion.identity);
                    stars[count] = newStar;
                    stars[count].gameObject.transform.parent = parent.transform;
                    count++;
                }
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(Vector3.zero, radius);
        }
    }
}
