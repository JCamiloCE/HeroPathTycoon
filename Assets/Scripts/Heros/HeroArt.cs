using UnityEngine;
using Utils.Art;

namespace Heros
{
    public class HeroArt : MonoBehaviour, ILifeCycle
    {
        private bool _wasInitialized = false;
        private SpriteRenderer _spriteRenderer = null;
        private SpriteFadeProcessor _fadeProcessor = null;

        public bool WasInitialized() => _wasInitialized;

        public bool Initialization(params object[] parameters)
        {
            if (_wasInitialized)
                return false;

            Sprite heroSprite = parameters[0] as Sprite;
            CreateSpriteRenderComponent(heroSprite);

            _wasInitialized = true;
            return true;
        }

        public void ActiveCurrentHero() 
        {
            StartFadeIn(1f, true);
        }

        public void EvolveHero(Sprite heroSprite)
        {
            _spriteRenderer.sprite = heroSprite;
        }

        public void SetNewHeroData(Sprite heroSprite)
        {
            _spriteRenderer.sprite = heroSprite;
        }

        internal void StartFadeOut(float time, bool overrideFade) 
        {
            if (!overrideFade && _fadeProcessor.IsProcessFade())
                return;
            _fadeProcessor.SetAlpha(1f);
            _fadeProcessor.StartFadeOut(time);
        }

        internal void StartFadeIn(float time, bool overrideFade)
        {
            if (!overrideFade && _fadeProcessor.IsProcessFade())
                return;
            _fadeProcessor.SetAlpha(0f);
            _fadeProcessor.StartFadeIn(time);
        }

        private void CreateSpriteRenderComponent(Sprite heroSprite)
        {
            _spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
            _fadeProcessor = gameObject.GetComponent<SpriteFadeProcessor>();
            _fadeProcessor.Initialization(_spriteRenderer);
            _fadeProcessor.SetAlpha(0f);
            _spriteRenderer.sprite = heroSprite;
        }
    }
}