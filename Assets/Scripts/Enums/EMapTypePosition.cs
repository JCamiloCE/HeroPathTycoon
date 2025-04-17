namespace HeroPath.Scripts.Enums
{
    //Each component of the enum need a identification
    //The identification must be the index of the first three letters
    //Index: A:00, B:01,...,Y:25,Z:25
    //Note: the  index -1 is reserved for Invalid and the index 0 for None
    public enum EMapTypePosition
    {
        Invalid = -1,
        None = 0,
        ForArt = 051417,
        ToStartQueue = 191418,
        ToWait = 191422, 
    }
}
