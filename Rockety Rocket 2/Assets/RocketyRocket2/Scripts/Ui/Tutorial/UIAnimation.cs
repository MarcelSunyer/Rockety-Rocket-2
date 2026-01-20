using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace RocketyRocket2
{
    public class UIAnimation : MonoBehaviour
    {
        public Image m_Image;
        public Sprite[] m_SpriteArray;
        public float m_Speed = 1f;

        private int m_IndexSprite;
        private Coroutine m_CoroutineAnim;

        void OnEnable()
        {
            StartAnimation();
        }

        void OnDisable()
        {
            StopAnimation();
        }

        void StartAnimation()
        {
            m_IndexSprite = 0;

            if (m_CoroutineAnim != null)
                StopCoroutine(m_CoroutineAnim);

            m_CoroutineAnim = StartCoroutine(PlayAnimUI());
        }

        void StopAnimation()
        {
            if (m_CoroutineAnim != null)
            {
                StopCoroutine(m_CoroutineAnim);
                m_CoroutineAnim = null;
            }
        }

        IEnumerator PlayAnimUI()
        {
            while (true)
            {
                m_Image.sprite = m_SpriteArray[m_IndexSprite];
                m_IndexSprite = (m_IndexSprite + 1) % m_SpriteArray.Length;

                yield return new WaitForSeconds(m_Speed/10);
            }
        }
    }
}
