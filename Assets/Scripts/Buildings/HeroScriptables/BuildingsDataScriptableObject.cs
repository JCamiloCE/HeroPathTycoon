using System.Collections.Generic;
using UnityEngine;
using Enums;

namespace HeroPath.Scripts.Buildings
{
    [CreateAssetMenu(fileName = "BuildingsData", menuName = "ScriptableObjects/BuildingsData", order = 0)]
    public class BuildingsDataScriptableObject : ScriptableObject
    {
        public List<BuildingData> buildingsDataScriptable;

        internal BuildingData GetBuildingDataByBuildingType(EBuildingType buildingType)
        {
            return buildingsDataScriptable.Find(x => x.GetBuildingType == buildingType);
        }

        internal HashSet<EBuildingType> GetAllTypeOfBuildings() 
        {
            HashSet<EBuildingType> buildingsType = new ();
            for (int i = 0; i < buildingsDataScriptable.Count; i++)
            {
                buildingsType.Add(buildingsDataScriptable[i].GetBuildingType);
            }
            return buildingsType;
        }
    }
}