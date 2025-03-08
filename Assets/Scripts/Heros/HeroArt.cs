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
            _fadeProcessor.SetAlpha(0f);
            _fadeProcessor.StartFadeIn(2f);
        }

        public void EvolveHero(Sprite heroSprite)
        {
            _spriteRenderer.sprite = heroSprite;
        }

        public void SetNewHeroData(Sprite heroSprite)
        {
            _spriteRenderer.sprite = heroSprite;
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