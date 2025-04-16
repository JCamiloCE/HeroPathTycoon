using UnityEngine;

namespace HeroPath.Scripts.Enums
{
    public static class EnumConverter
    {
        public static EBuildingType GetBuildingByFeature(EFeatureInGame feature)
        {
            switch (feature)
            {
                case EFeatureInGame.FeatureBuildingArcher:
                    return EBuildingType.Archery;

                case EFeatureInGame.FeatureBuildingBarracks:
                    return EBuildingType.Barracks;

                case EFeatureInGame.FeatureBuildingLobby:
                    return EBuildingType.Lobby;
            }

            Debug.LogError("Unsuppor Feature type: " + feature);
            return EBuildingType.Invalid;
        }

        public static EFeatureInGame GetFeatureByBuilding(EBuildingType buildingType) 
        {
            switch (buildingType)
            {
                case EBuildingType.Archery:
                    return EFeatureInGame.FeatureBuildingArcher;

                case EBuildingType.Barracks:
                    return EFeatureInGame.FeatureBuildingBarracks;

                case EBuildingType.Lobby:
                    return EFeatureInGame.FeatureBuildingLobby;
            }

            Debug.LogError("Unsuppor Building type: " + buildingType);
            return EFeatureInGame.Invalid;
        }

        public static EHeroFamily GetHeroFamilyByBuilding(EBuildingType buildingType) 
        {
            switch (buildingType)
            {
                case EBuildingType.Archery:
                    return EHeroFamily.Archer;

                case EBuildingType.Barracks:
                    return EHeroFamily.Warrior;

                case EBuildingType.Lobby:
                    return EHeroFamily.Candidate;
            }

            Debug.LogError("Unsuppor Building type: " + buildingType);
            return EHeroFamily.Invalid;
        }
    }
}