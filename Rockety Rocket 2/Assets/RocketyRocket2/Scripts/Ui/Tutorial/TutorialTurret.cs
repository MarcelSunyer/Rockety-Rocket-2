using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

namespace RocketyRocket2
{
    public class TutorialTurret : MonoBehaviour
    {

        public Image Ship;
        public Image Turret;
        public Image Bullet;

        public ParticleSystem ParticleShoot;
        public ParticleSystem[] ExplosionParticle; 

        private Vector2 InitialPositionBullet;


        private void Start()
        {
            InitialPositionBullet = Bullet.GetComponent<RectTransform>().anchoredPosition;
            StartCoroutine(TutorialShip());
        }
        private IEnumerator TutorialShip()
        {
            while (true)
            {
                ParticleShoot.Play();                                      //(Velocity, Duration)
                Tween tween = Bullet.GetComponent<RectTransform>().DOAnchorPosX(370, 1f);

                tween.Play();
                yield return tween.WaitForCompletion();
                Ship.enabled = false;
                for (int i = 0; i < ExplosionParticle.Length; i++)
                {
                    ExplosionParticle[i].Play();
                }
                Bullet.GetComponent<RectTransform>().anchoredPosition = InitialPositionBullet;

                yield return new WaitForSeconds(2f);
                Ship.enabled = true;
            }
        }
    }
}
