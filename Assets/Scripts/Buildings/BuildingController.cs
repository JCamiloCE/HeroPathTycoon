using UnityEngine;

namespace Buildings
{
    public class BuildingController : ILifeCycle
    {
        private BuildingData _buildingData = null;
        private bool _wasInitialized = false;

        public bool WasInitialized() => _wasInitialized;

        public bool Initialization(params object[] parameters)
        {
            _buildingData = parameters[0] as BuildingData;
            Vector3 buildingPosition = (Vector3)parameters[1];
            CreateBuilding(buildingPosition);
            _wasInitialized = true;
            return true;
        }

        private void CreateBuilding(Vector3 initialPosition) 
        {
            GameObject buildingGameObject = new GameObject("Building");
            buildingGameObject.transform.position = initialPosition;
            SpriteRenderer spriteRenderer = buildingGameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = _buildingData.GetBuildingInitialSprite;
        }
    }
}