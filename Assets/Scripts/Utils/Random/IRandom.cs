using System.Collections.Generic;

namespace Utils.Random
{
    public interface IRandom
    {
        int GetRandomIndexInList<TType>(List<TType> list);
        int GetRandomIntBetween(int minInclusive, int maxExclusive);
        float GetRandomFloatBetween(float minInclusive, float maxExclusive);
    }
}