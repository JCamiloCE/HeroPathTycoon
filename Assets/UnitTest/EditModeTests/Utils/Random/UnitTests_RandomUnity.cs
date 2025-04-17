using System;
using System.Collections.Generic;
using NUnit.Framework;
using JCC.Utils.Random;

namespace UnitTests.EditModeTests.Utils.Random
{ 
    public class UnitTests_RandomUnity
    {
        [Test]
        public void GetRandomIndexInList_NullList_ArgumentNullException()
        {
            //Arrange
            IRandom randomUnity = new RandomUnity();
            List<int> list = new List<int>();
            list = null;

            //Act
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => randomUnity.GetRandomIndexInList(list));

            //Assert
            Assert.That(exception.ParamName, Is.EqualTo("The list is null"));
        }

        [Test]
        public void GetRandomIndexInList_EmptyList_ReturnIndexZero()
        {
            //Arrange
            IRandom randomUnity = new RandomUnity();
            List<int> list = new List<int>();
            int expected_result = 0;

            //Act
            int result = randomUnity.GetRandomIndexInList(list);

            //Assert
            Assert.AreEqual(expected_result,result);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void GetRandomIndexInList_FilledList_ReturnIndexBetweenZeroAndMaxValue(int numberOfItems)
        {
            //Arrange
            IRandom randomUnity = new RandomUnity();
            List<int> list = new List<int>();
            for (int i = 0; i < numberOfItems; i++)
            {
                list.Add(0);
            }
            int minValue = 0;

            //Act
            int result = randomUnity.GetRandomIndexInList(list);

            //Assert
            Assert.That(result, Is.LessThan(numberOfItems));
            Assert.That(result, Is.GreaterThanOrEqualTo(minValue));
        }

        [TestCase(1,0)]
        [TestCase(1,1)]
        public void GetRandomIntBetween_MinEqualOrHigherThanMax_ReturnArgumentException(int minInclusive, int maxExclusive) 
        {
            //Arrange
            IRandom randomUnity = new RandomUnity();

            //Act
            ArgumentException exception = Assert.Throws<ArgumentException>(() => randomUnity.GetRandomIntBetween(minInclusive, maxExclusive));

            //Assert
            Assert.That(exception.Message, Is.EqualTo("minInclusive should be less than maxExclusive"));
        }

        [TestCase(0,10)]
        [TestCase(1,2)]
        [TestCase(-5,0)]
        public void GetRandomIntBetween_MinLessThanMax_ReturnNumberBetween(int minInclusive, int maxExclusive)
        {
            //Arrange
            IRandom randomUnity = new RandomUnity();

            //Act
            int result = randomUnity.GetRandomIntBetween(minInclusive, maxExclusive);

            //Assert
            Assert.That(result, Is.LessThan(maxExclusive));
            Assert.That(result, Is.GreaterThanOrEqualTo(minInclusive));
        }

        [TestCase(1f, 0f)]
        [TestCase(1f, -1f)]
        public void GetRandomFloatBetween_MinEqualOrHigherThanMax_ReturnArgumentException(float minInclusive, float maxInclusive)
        {
            //Arrange
            IRandom randomUnity = new RandomUnity();

            //Act
            ArgumentException exception = Assert.Throws<ArgumentException>(() => randomUnity.GetRandomFloatBetween(minInclusive, maxInclusive));

            //Assert
            Assert.That(exception.Message, Is.EqualTo("minInclusive should be less or equal than maxInclusive"));
        }

        [TestCase(0f, 10f)]
        [TestCase(1f, 1f)]
        [TestCase(1f, 2f)]
        [TestCase(-5f, 0f)]
        public void GetRandomFloatBetween_MinLessThanMax_ReturnNumberBetween(float minInclusive, float maxInclusive)
        {
            //Arrange
            IRandom randomUnity = new RandomUnity();

            //Act
            float result = randomUnity.GetRandomFloatBetween(minInclusive, maxInclusive);

            //Assert
            Assert.That(result, Is.LessThanOrEqualTo(maxInclusive));
            Assert.That(result, Is.GreaterThanOrEqualTo(minInclusive));
        }
    }
}