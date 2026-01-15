using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RocketyRocket2
{
    public class BlackHoleTutorial : MonoBehaviour
    {
        public TextMeshProUGUI[] Letters;

        public TextMeshProUGUI Tutorial;

        public TextMeshProUGUI Skip;

        public GameObject PositionToEnd;


        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(TutorialStart());
            
        }

        private IEnumerator TutorialStart()
        {
            yield return new WaitForSeconds(2.5f);
            for (int i = 0; i < Letters.Length; i++)
            {
                RectTransform rt = Letters[i].rectTransform;

                Sequence seq = DOTween.Sequence();

                Tween tween = seq.Join(rt.DOScale(Vector3.zero, 3f));
                tween.Play();
                tween = seq.Join(rt.DORotate(new Vector3(0, 0, 360), 2f, RotateMode.FastBeyond360));
                tween.Play();
                tween = seq.Join(rt.DOMove(PositionToEnd.transform.position, 3f));
                tween.Play();

                yield return new WaitForSeconds(0.3f);
            }
            yield return new WaitForSeconds(0.7f);

            Sequence _seq = DOTween.Sequence();
            Tween _tween = _seq.Join(Tutorial.transform.DOScale(Vector3.zero, 3f)).
                Join(Tutorial.transform.DORotate(new Vector3(0, 0, 360), 2f, RotateMode.FastBeyond360)).
                Join(Tutorial.transform.DOMove(PositionToEnd.transform.position, 3f));
           
            _seq.Play();

            yield return new WaitForSeconds(0.3f);
            Sequence seq_ = DOTween.Sequence();
            _tween = seq_.Join(Skip.transform.DOScale(Vector3.zero, 3f)).
                Join(Skip.transform.DORotate(new Vector3(0, 0, 360), 2f, RotateMode.FastBeyond360)).
                Join(Skip.transform.DOMove(PositionToEnd.transform.position, 3f));

            _tween.Play();

            yield return new WaitForSeconds(3f);
        }
    }
}
