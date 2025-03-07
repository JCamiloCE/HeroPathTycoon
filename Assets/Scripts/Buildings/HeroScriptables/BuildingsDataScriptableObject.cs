using System.Collections.Generic;
using UnityEngine;

namespace Buildings
{
    [CreateAssetMenu(fileName = "BuildingsData", menuName = "ScriptableObjects/BuildingsData", order = 0)]
    public class BuildingsDataScriptableObject : ScriptableObject
    {
        public List<BuildingData> buildingsDataScriptable;

        internal BuildingData GetBuildingDataByBuildingType(EBuildingType buildingType)
        {
            return buildingsDataScriptable.Find(x => x.GetBuildingType == buildingType);
        }
    }
}