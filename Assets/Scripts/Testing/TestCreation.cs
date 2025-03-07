using Heros;
using System.Collections;
using System.Collections.Generic;
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

