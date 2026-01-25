using DG.Tweening;
using Unity.VectorGraphics;
using UnityEngine;



namespace RocketyRocket2
{
    public class Congrats : MonoBehaviour
    {
        public int Speed;

        private void Update()
        {
            Tween tween = gameObject.transform
                .DOLocalRotate(new Vector3(0, 0, 3600), Speed, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1);

            tween.Play();
        }
     
    }
}
