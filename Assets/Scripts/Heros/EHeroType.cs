namespace Heros
{
    //Each component of the enum need a identification
    //The identification must be the index of the first three letters
    //Index: A:00, B:01,...,Y:25,Z:25
    //Note: the  index -1 is reserved for Invalid and the index 0 for None
    public enum EHeroFamily
    {
        Invalid = -1,
        None = 0,
        Archer = 001702,
        Candidate = 020013,
        Warrior = 220017,
    }
}