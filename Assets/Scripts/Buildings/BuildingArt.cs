using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings
{
    public class BuildingArt : MonoBehaviour, ILifeCycle
    {
        [SerializeField] private Image _loadProcess = null;
        private Coroutine _processCoroutine;
        private bool _wasInitialized = false;

        public bool Initialization(params object[] parameters)
        {
            _wasInitialized = true;
            _loadProcess.fillAmount = 0;
            return _wasInitialized;
        }

        public bool WasInitialized() => _wasInitialized;

        internal void StartProcess(float timeProcess) 
        {
            if (_processCoroutine != null)
            {
                StopCoroutine(_processCoroutine);
                _processCoroutine = null;
            }
            _processCoroutine = StartCoroutine(ProcessImg(timeProcess));
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