namespace GeneralManagers
{
    //Each feature needs to have three words in the name:
    // - First W: each entry needs the word "Feature"
    // - Second W: to who belongs for example "Building"
    // - Third W: is the specialization for the feature, for example "Barracks" for buildings

    //Each component of the enum need a identification
    //The identification must be the index according to:
    // - First two number: first letter for the second word of the feature
    // - Second two numbers: first letter for the third word of the feature
    // - Third two numbers: second letter for the third word of the feature

    //Index: A:00, B:01,...,Y:25,Z:25
    //Note: the  index -1 is reserved for Invalid
    public enum EFeatureInGame
    {
        Invalid = -1,
        FeatureBuildingArcher = 010017,
        FeatureBuildingBarracks = 010100,
        FeatureBuildingLobby = 011114,
    }
}