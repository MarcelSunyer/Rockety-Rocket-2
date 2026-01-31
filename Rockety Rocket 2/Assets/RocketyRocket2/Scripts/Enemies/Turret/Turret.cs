using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace RocketyRocket2
{
    public class Turret : MonoBehaviour
    {
        public GameObject Bullet;

        public int SpeedBullet;
        public float SecondsToShoot;

        public AudioSource shoot;

        private GameObject Ship;

        public bool Range;

        private GameObject pauseMenu;
        void Start()
        {
            Ship = GameObject.Find("Ship");
            StartCoroutine(ShootBullet());
            pauseMenu = GameObject.Find("PauseMenu");
        }

        void FixedUpdate()
        {
            if(pauseMenu.GetComponent<PauseMenu>().pauseActive)
            {
                return;
            }
            if (Range)
            {
                Vector2 direction = Ship.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                transform.rotation = Quaternion.Euler(0f, 0f, angle + (-90));
            }

        }

        private IEnumerator ShootBullet()
        {
            while (true)
            {
                yield return new WaitForSeconds(SecondsToShoot);
                if (Range)
                {
                    if (RocketyRocket2.RocketyRocket2Game.Instance.SaveGameManager.FxSound == 1)
                    {
                        SoundManager.SoundManager.PlaySound(SoundManager.SoundValues.SoundType.TurretShoot, shoot, 0.0015f);
                    }
                    GameObject bullet = Instantiate(Bullet);

                    // Spawn position in world space
                    bullet.transform.position = gameObject.transform.position;
                    bullet.transform.rotation = transform.rotation;

                    // Get rigidbody
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                    // Calculate direction ONCE
                    Vector2 direction = (Ship.transform.position - gameObject.transform.position).normalized;

                    // LOCK movement
                    rb.linearVelocity = direction * SpeedBullet;
                }
            }
               
            
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ship"))
            {
                Range = true;
                Vector2 direction = Ship.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Tween tween = transform.DORotateQuaternion(
                    Quaternion.Euler(0f, 0f, angle - 90f),
                    0.2f
                );
                tween.Play();
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Ship"))
            {
                Range = false;
            }
        }
    }
}
