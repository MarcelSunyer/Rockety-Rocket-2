using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace RocketyRocket2
{
    public class MenuManager : MonoBehaviour
    {
        //Canvas parameters
        public RectTransform canvasFather;
        private float screenWidth;
        private float screenHeight;

        //Start parameters
        public Button GoToGalaxyScreen;
        public Button BackStart;

        public Button Skins;
        public Button BackSkin;

        public Button Settings;
        public Button BackSettings;

        //Galxies Parameters
        public Button MilkyWay;
        public Button MilkyWayBack;

        public Button SagDeg;
        public Button SagDegBack;

        public Button LMC;
        public Button LMCBack;

        public Button SMC;
        public Button SMCBack;

        public Button Messier32;
        public Button Messier32Back;

        [SerializeField] private Image fade;
        public AudioSource AmbienceSound;
        public bool playingSound;

        private void Start()
        {
            if (RocketyRocket2.RocketyRocket2Game.Instance.SaveGameManager.Music == 1)
                SoundManager.SoundManager.PlaySound(
                GetRandomAmbience(),
                AmbienceSound
                );

            if (canvasFather != null)
            {
                screenWidth = canvasFather.rect.width;
                screenHeight = canvasFather.rect.height;
            }
            //Start
            if (GoToGalaxyScreen != null)
                GoToGalaxyScreen.onClick.AddListener(StartButton);
            if (BackStart != null)
                BackStart.onClick.AddListener(StartToMain);

            //Skin
            if (Skins != null)
                Skins.onClick.AddListener(SkinButton);
            if (BackSkin != null)
                BackSkin.onClick.AddListener(SkinToMain);

            //Settings
            if (Settings != null)
                Settings.onClick.AddListener(SettingsButton);
            if (BackSettings != null)
                BackSettings.onClick.AddListener(SettingsToMain);

            //MilkyWay
            if (MilkyWay != null)
                MilkyWay.onClick.AddListener(GoMilkyWay);
            if (MilkyWayBack != null)
                MilkyWayBack.onClick.AddListener(MilkyWayToGalaxy);

            //SagDeg
            if (SagDeg != null)
                SagDeg.onClick.AddListener(GoSagDeg);
            if (SagDegBack != null)
                SagDegBack.onClick.AddListener(SagDegToGalaxy);

            //LMC
            if (LMC != null)
                LMC.onClick.AddListener(GoLMC);
            if (LMCBack != null)
                LMCBack.onClick.AddListener(LMCToGalaxy);

            //SMC
            if (SMC != null)
                SMC.onClick.AddListener(GoSMC);
            if (SMCBack != null)
                SMCBack.onClick.AddListener(SMCToGalaxy);

            //Messier32
            if (Messier32 != null)
                Messier32.onClick.AddListener(GoMessier32);
            if (Messier32Back != null)
                Messier32Back.onClick.AddListener(Messier32ToGalaxy);

            if (fade != null)
                StartCoroutine(FadeIn());
        }

        private IEnumerator FadeIn()
        {
            Tween tween = fade.DOFade(0, 1f);
            tween.Play();
            yield return tween.WaitForCompletion();

            fade.gameObject.SetActive(false);
        }
        public void StartButton()
        {
            Vector2 targetPos = new Vector2(0, -screenHeight * 0.020f);

            Tween tween = canvasFather.DOAnchorPos(targetPos, 1.34f);
            tween.Play();

            BackStart.Select();
        }

        public void SkinButton()
        {
            Vector2 targetPos = new Vector2(screenWidth * 0.018f, 0);

            Tween tween = canvasFather.DOAnchorPos(targetPos, 1.34f);
            tween.Play();

            BackSkin.Select();

        }
        public void SettingsButton()
        {
            Vector2 targetPos = new Vector2(-screenWidth * 0.019f, 0);
            Tween tween = canvasFather.DOAnchorPos(targetPos, 1.34f);
            tween.Play();
            BackSettings.Select();
        }
        public void Quit()
        {
            RocketyRocket2Game.Instance.SaveGameManager.Save();
            Application.Quit();

        }

        public void StartToMain()
        {
            Tween tween = canvasFather.DOAnchorPos(Vector2.zero, 1.34f);
            tween.Play();
            GoToGalaxyScreen.Select();
        }
        public void SkinToMain()
        {
            Tween tween = canvasFather.DOAnchorPos(Vector2.zero, 1.34f);
            tween.Play();
            Skins.Select();
        }
        public void SettingsToMain()
        {
            Tween tween = canvasFather.DOAnchorPos(Vector2.zero, 1.34f);
            tween.Play();
            Settings.Select();
        }

        public void GoMilkyWay()
        {
            Vector2 targetPos = new Vector2(screenWidth * 0.030f, -screenHeight * 0.040f);

            Tween tween = canvasFather.DOAnchorPos(targetPos, 1.34f);
            tween.Play();

            MilkyWayBack.Select();
        }
        public void MilkyWayToGalaxy()
        {
            Vector2 targetPos = new Vector2(0, -screenHeight * 0.020f);
            Tween tween = canvasFather.DOAnchorPos(targetPos, 1.34f);
            tween.Play();
            MilkyWay.Select();
        }

        public void GoSagDeg()
        {
            Vector2 targetPos = new Vector2(0, -screenHeight * 0.052f);
            Tween tween = canvasFather.DOAnchorPos(targetPos, 1.34f);
            tween.Play();
            SagDegBack.Select();
        }
        public void SagDegToGalaxy()
        {
            Vector2 targetPos = new Vector2(0, -screenHeight * 0.020f);
            Tween tween = canvasFather.DOAnchorPos(targetPos, 1.34f);
            tween.Play();
            SagDeg.Select();
        }

        public void GoLMC()
        {
            Vector2 targetPos = new Vector2(-screenWidth * 0.030f, -screenHeight * 0.040f);
            Tween tween = canvasFather.DOAnchorPos(targetPos, 1.34f);
            tween.Play();
            LMCBack.Select();
        }
        public void LMCToGalaxy()
        {
            Vector2 targetPos = new Vector2(0, -screenHeight * 0.020f);
            Tween tween = canvasFather.DOAnchorPos(targetPos, 1.34f);
            tween.Play();
            LMC.Select();
        }

        public void GoSMC()
        {
            Vector2 targetPos = new Vector2(screenWidth * 0.0355f, -screenHeight * 0.0175f);
            Tween tween = canvasFather.DOAnchorPos(targetPos, 1.34f);
            tween.Play();
            SMCBack.Select();
        }
        public void SMCToGalaxy()
        {
            Vector2 targetPos = new Vector2(0, -screenHeight * 0.020f);
            Tween tween = canvasFather.DOAnchorPos(targetPos, 1.34f);
            tween.Play();
            SMC.Select();
        }

        public void GoMessier32()
        {
            Vector2 targetPos = new Vector2(-screenWidth * 0.0355f, -screenHeight * 0.0175f);
            Tween tween = canvasFather.DOAnchorPos(targetPos, 1.34f);
            tween.Play();
            Messier32Back.Select();
        }
        public void Messier32ToGalaxy()
        {
            Vector2 targetPos = new Vector2(0, -screenHeight * 0.020f);
            Tween tween = canvasFather.DOAnchorPos(targetPos, 1.34f);
            tween.Play();
            Messier32.Select();
        }

        private void Update()
        {
            if (RocketyRocket2.RocketyRocket2Game.Instance.SaveGameManager.Music == 0 && !playingSound)
            {
                AmbienceSound.Stop();
                playingSound = true;
                return;
            }
            if (RocketyRocket2.RocketyRocket2Game.Instance.SaveGameManager.Music == 1 && playingSound)
            {
                playingSound = false;
                SoundManager.SoundManager.PlaySound(
                GetRandomAmbience(),
                AmbienceSound,0.05f
                );
            }
        }
        private SoundManager.SoundValues.SoundType GetRandomAmbience()
        {
            SoundManager.SoundValues.SoundType[] ambiences =
            {
        SoundManager.SoundValues.SoundType.Ambience_1,
        SoundManager.SoundValues.SoundType.Ambience_2,
        SoundManager.SoundValues.SoundType.Ambience_3,
        SoundManager.SoundValues.SoundType.Ambience_4,
        SoundManager.SoundValues.SoundType.Ambience_5,
    };

            return ambiences[Random.Range(0, ambiences.Length)];
        }
    }
}
