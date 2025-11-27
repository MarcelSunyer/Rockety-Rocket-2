using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RocketyRocket2
{
    public class GalaxyShaderManager : MonoBehaviour
    {

        public GameObject[] shaders;

        [SerializeField] private Button start;
        [SerializeField] private Button back;
        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < shaders.Length; i++)
            {
                shaders[i].SetActive(false);
            }
            start.onClick.AddListener(ShaderActive);
            back.onClick.AddListener(ShaderDisabled);
        }

        private void ShaderActive()
        {
            for (int i = 0; i < shaders.Length; i++)
            {
                shaders[i].SetActive(true);
            }
        }

        private void ShaderDisabled()
        {
            for (int i = 0; i < shaders.Length; i++)
            {
                shaders[i].SetActive(false);
            }
        }


    }
}
