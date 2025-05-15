using JCC.Utils.LifeCycle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HeroPath.Scripts.Buildings
{
    public class BuildingArt : MonoBehaviour, ILifeCycle
    {
        [SerializeField] private Image _loadProcess = null;
        [SerializeField] private SpriteRenderer _mainBuildingArt = null;
        [SerializeField] private SpriteRenderer _bannerBuildingArt = null;
        [SerializeField] private List<GameObject> _lockObjs = null;
        [SerializeField] private Color _lockColor = Color.white;
        private Coroutine _processCoroutine;
        private bool _wasInitialized = false;

        public bool WasInitialized() => _wasInitialized;

        public bool Initialization(params object[] parameters)
        {
            Sprite mainSprite = parameters[0] as Sprite;
            Sprite bannerSprite = parameters[1] as Sprite;
            bool isUnlock = (bool)parameters[2];
            SetBuildingSprite(mainSprite, bannerSprite, isUnlock);
            _loadProcess.fillAmount = 0;
            _wasInitialized = true;
            return _wasInitialized;
        }

        internal void StartProcess(float timeProcess) 
        {
            if (_processCoroutine != null)
            {
                StopCoroutine(_processCoroutine);
                _processCoroutine = null;
            }
            _processCoroutine = StartCoroutine(ProcessImg(timeProcess));
        }

        internal void UnlockBuilding() 
        {
            _mainBuildingArt.color = Color.white;
            _bannerBuildingArt.color = Color.white;
            foreach (var obj in _lockObjs)
            {
                obj.SetActive(false);
            }
        }

        private void SetBuildingSprite(Sprite initialSprite, Sprite bannerSprite, bool isUnlock) 
        {
            _mainBuildingArt.sprite = initialSprite;
            _bannerBuildingArt.sprite = bannerSprite;
            if (!isUnlock) 
            {
                _mainBuildingArt.color = _lockColor;
                _bannerBuildingArt.color = _lockColor;
                foreach (var obj in _lockObjs) 
                {
                    obj.SetActive(true);
                }
            }
        }

        private IEnumerator ProcessImg(float timeProcess)
        {
            float timeElapsed = 0f;

            while (timeElapsed < timeProcess)
            {
                float fillAmount = Mathf.Lerp(0f, 1f, timeElapsed / timeProcess);
                _loadProcess.fillAmount = fillAmount;
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            _loadProcess.fillAmount = 0;
        }
    }
}