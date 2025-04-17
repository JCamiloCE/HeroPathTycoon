using HeroPath.Scripts.Heros;
using UnityEngine;

namespace Testing 
{
    public class TestCreation : MonoBehaviour
    {
        [SerializeField] private HerosManager _heroManager;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.C)) 
            {
                _heroManager.SpawnNewHero();
            }
        }
    }
}

