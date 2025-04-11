using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings
{
    public class BuildingArt : MonoBehaviour, ILifeCycle
    {
        [SerializeField] private Image _loadProcess = null;
        [SerializeField] private List<GameObject> _lockObjs = null;
        [SerializeField] private Color _lockColor = Color.white;
        private Coroutine _processCoroutine;
        private bool _wasInitialized = false;

        public bool WasInitialized() => _wasInitialized;

        public bool Initialization(params object[] parameters)
        {
            Sprite initialSprite = parameters[0] as Sprite;
            bool isUnlock = (bool)parameters[1];
            SetBuildingSprite(initialSprite, isUnlock);
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
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.white;
            foreach (var obj in _lockObjs)
            {
                obj.SetActive(false);
            }
        }

        private void SetBuildingSprite(Sprite initialSprite, bool isUnlock) 
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = initialSprite;
            if (!isUnlock) 
            {
                spriteRenderer.color = _lockColor;
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