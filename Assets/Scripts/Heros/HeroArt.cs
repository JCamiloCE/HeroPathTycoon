using UnityEngine;
using Utils.Art;

namespace Heros
{
    public class HeroArt : MonoBehaviour, ILifeCycle
    {
        private bool _wasInitialized = false;
        private SpriteRenderer _spriteRenderer = null;
        private HeroData _heroData = null;
        private SpriteFadeProcessor _fadeProcessor = null;

        public bool WasInitialized() => _wasInitialized;

        public bool Initialization(params object[] parameters)
        {
            if (_wasInitialized)
                return false;

            _heroData = parameters[0] as HeroData;
            CreateSpriteRenderComponent();

            _wasInitialized = true;
            return true;
        }

        public void ActiveCurrentHero() 
        {
            _fadeProcessor.SetAlpha(0f);
            _fadeProcessor.StartFadeIn(2f);
        }

        public void EvolveHero(HeroData heroData)
        {
            _heroData = heroData;
            _spriteRenderer.sprite = _heroData.GetHeroSprite;
        }

        private void CreateSpriteRenderComponent()
        {
            _spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
            _fadeProcessor = gameObject.GetComponent<SpriteFadeProcessor>();
            _fadeProcessor.Initialization(_spriteRenderer);
            _fadeProcessor.SetAlpha(0f);
            _spriteRenderer.sprite = _heroData.GetHeroSprite;
        }
    }
}