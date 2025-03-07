using NUnit.Framework;
using System;
using UnityEngine;
using Utils.Pool;

namespace UnitTests.EditModeTests.Utils.Pool
{
    public class UnitTest_PoolController 
    {
        [TestCase(1)]
        [TestCase(4)]
        [TestCase(5)]
        public void SetPoolObject_CorrectSetter_GetCorrectSizeOfPool(int poolSize)
        {
            //Arrange
            IPoolController randomUnity = new PoolControllerImpl();

            //Act
            GameObject gameObject = new GameObject("TestObject");
            randomUnity.SetPoolObject(gameObject, poolSize, false);

            //Assert
            Assert.AreEqual(poolSize, randomUnity.GetCurrentPoolSize());
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-5)]
        public void SetPoolObject_IncorrectSizeOfPool_ReturnError(int poolSize)
        {
            //Arrange
            IPoolController randomUnity = new PoolControllerImpl();

            //Act
            GameObject gameObject = new GameObject("TestObject");
            ArgumentException exception = Assert.Throws<ArgumentException>(() => randomUnity.SetPoolObject(gameObject, poolSize, false));

            //Assert
            Assert.AreEqual(0, randomUnity.GetCurrentPoolSize());
            Assert.That(exception.ParamName, Is.EqualTo("poolSize"));
        }

        [Test]
        public void SetPoolObject_NullGameObject_ReturnError()
        {
            //Arrange
            IPoolController randomUnity = new PoolControllerImpl();

            //Act
            ArgumentException exception = Assert.Throws<ArgumentException>(() => randomUnity.SetPoolObject(null, 1, false));

            //Assert
            Assert.AreEqual(0, randomUnity.GetCurrentPoolSize());
            Assert.That(exception.ParamName, Is.EqualTo("initialPoolObject"));
        }

        [Test]
        public void GetPoolObject_TryToGetLessAvailableObject_ReturnPoolObject()
        {
            //Arrange
            IPoolController randomUnity = new PoolControllerImpl();
            GameObject gameObject = new GameObject("TestObject");
            randomUnity.SetPoolObject(gameObject, 5, false);

            //Act
            PoolObject poolObj = randomUnity.GetPoolObject();

            //Assert
            Assert.IsNotNull(poolObj);
            Assert.IsFalse(poolObj.IsAvailable);
        }

        [Test]
        public void GetPoolObject_TryToGetMoreThanTheAvailableWithNonExpand_ReturnNull()
        {
            //Arrange
            IPoolController randomUnity = new PoolControllerImpl();
            GameObject gameObject = new GameObject("TestObject");
            randomUnity.SetPoolObject(gameObject, 1, false);

            //Act
            PoolObject poolObj = randomUnity.GetPoolObject();
            poolObj = randomUnity.GetPoolObject();

            //Assert
            Assert.IsNull(poolObj);
        }

        [Test]
        public void GetPoolObject_TryToGetMoreThanTheAvailableWithExpand_ReturnPoolObject()
        {
            //Arrange
            IPoolController randomUnity = new PoolControllerImpl();
            GameObject gameObject = new GameObject("TestObject");
            randomUnity.SetPoolObject(gameObject, 1, true);

            //Act
            PoolObject poolObj = randomUnity.GetPoolObject();
            poolObj = randomUnity.GetPoolObject();

            //Assert
            Assert.IsNotNull(poolObj);
            Assert.IsFalse(poolObj.IsAvailable);
        }

        [Test]
        public void ReturnToPool_ReturnCorrectPoolObject_ObjectNowIsAvailable()
        {
            //Arrange
            IPoolController randomUnity = new PoolControllerImpl();
            GameObject gameObject = new GameObject("TestObject");
            randomUnity.SetPoolObject(gameObject, 1, true);
            PoolObject poolObj = randomUnity.GetPoolObject();

            //Act
            randomUnity.ReturnToPool(poolObj);

            //Assert
            Assert.IsTrue(poolObj.IsAvailable);
        }

        [Test]
        public void ReturnToPool_ReturnNullPoolObject_ReturnError()
        {
            //Arrange
            IPoolController randomUnity = new PoolControllerImpl();
            GameObject gameObject = new GameObject("TestObject");

            //Act
            ArgumentException exception = Assert.Throws<ArgumentException>(() => randomUnity.ReturnToPool(null));

            //Assert
            Assert.That(exception.ParamName, Is.EqualTo("newPoolObj"));
        }

        [Test]
        public void ReturnToPool_ReturnPoolObjectThatDoesntBelongToThePool_ReturnError()
        {
            //Arrange
            IPoolController randomUnity = new PoolControllerImpl();
            GameObject gameObject = new GameObject("TestObject");
            randomUnity.SetPoolObject(gameObject, 1, true);
            GameObject gameObject_fake = new GameObject("TestObject_Fake");
            PoolObject poolObj_fake = gameObject_fake.AddComponent<PoolObject>();

            //Act
            ArgumentException exception = Assert.Throws<ArgumentException>(() => randomUnity.ReturnToPool(poolObj_fake));

            //Assert
            Assert.That(exception.ParamName, Is.EqualTo("newPoolObj"));
        }
    }
}