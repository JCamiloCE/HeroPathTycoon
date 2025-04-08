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
            IPoolController<PoolObject_Fake> poolController = new PoolControllerImpl<PoolObject_Fake>();

            //Act
            GameObject gameObject = new GameObject("TestObject");
            gameObject.AddComponent<PoolObject_Fake>();
            poolController.SetPoolObject(gameObject, poolSize, false);

            //Assert
            Assert.AreEqual(poolSize, poolController.GetCurrentPoolSize());
        }

        [Test]
        public void SetPoolObject_PoolObjectDoesntHaveTheType_ReturnError()
        {
            //Arrange
            IPoolController<PoolObject_Fake> poolController = new PoolControllerImpl<PoolObject_Fake>();

            //Act
            GameObject gameObject = new GameObject("TestObject");
            ArgumentException exception = Assert.Throws<ArgumentException>(() => poolController.SetPoolObject(gameObject, 1, false));

            //Assert
            Assert.AreEqual(0, poolController.GetCurrentPoolSize());
            Assert.That(exception.ParamName, Is.EqualTo("initialPoolObject"));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-5)]
        public void SetPoolObject_IncorrectSizeOfPool_ReturnError(int poolSize)
        {
            //Arrange
            IPoolController<PoolObject_Fake> poolController = new PoolControllerImpl<PoolObject_Fake>();

            //Act
            GameObject gameObject = new GameObject("TestObject");
            ArgumentException exception = Assert.Throws<ArgumentException>(() => poolController.SetPoolObject(gameObject, poolSize, false));

            //Assert
            Assert.AreEqual(0, poolController.GetCurrentPoolSize());
            Assert.That(exception.ParamName, Is.EqualTo("poolSize"));
        }

        [Test]
        public void SetPoolObject_NullGameObject_ReturnError()
        {
            //Arrange
            IPoolController<PoolObject_Fake> poolController = new PoolControllerImpl<PoolObject_Fake>();

            //Act
            ArgumentException exception = Assert.Throws<ArgumentException>(() => poolController.SetPoolObject(null, 1, false));

            //Assert
            Assert.AreEqual(0, poolController.GetCurrentPoolSize());
            Assert.That(exception.ParamName, Is.EqualTo("initialPoolObject"));
        }

        [Test]
        public void GetPoolObject_TryToGetLessAvailableObject_ReturnPoolObject()
        {
            //Arrange
            IPoolController<PoolObject_Fake> poolController = new PoolControllerImpl<PoolObject_Fake>();
            GameObject gameObject = new GameObject("TestObject");
            gameObject.AddComponent<PoolObject_Fake>();
            poolController.SetPoolObject(gameObject, 5, false);

            //Act
            PoolObject_Fake poolObj = poolController.GetPoolObject();

            //Assert
            Assert.IsNotNull(poolObj);
            Assert.IsFalse(poolObj.IsAvailable);
        }

        [Test]
        public void GetPoolObject_TryToGetMoreThanTheAvailableWithNonExpand_ReturnErrorAndNull()
        {
            //Arrange
            IPoolController<PoolObject_Fake> poolController = new PoolControllerImpl<PoolObject_Fake>();
            GameObject gameObject = new GameObject("TestObject");
            gameObject.AddComponent<PoolObject_Fake>();
            poolController.SetPoolObject(gameObject, 1, false);

            //Act
            poolController.GetPoolObject();
            PoolObject_Fake poolObj = null;
            NullReferenceException exception = Assert.Throws<NullReferenceException>(() => poolObj = poolController.GetPoolObject());

            //Assert
            Assert.IsNull(poolObj);
            Assert.That(exception.Message, Is.EqualTo("PoolObject wasnt found"));
        }

        [Test]
        public void GetPoolObject_TryToGetMoreThanTheAvailableWithExpand_ReturnPoolObject()
        {
            //Arrange
            IPoolController<PoolObject_Fake> poolController = new PoolControllerImpl<PoolObject_Fake>();
            GameObject gameObject = new GameObject("TestObject");
            gameObject.AddComponent<PoolObject_Fake>();
            poolController.SetPoolObject(gameObject, 1, true);

            //Act
            poolController.GetPoolObject();
            PoolObject_Fake poolObj = poolController.GetPoolObject();

            //Assert
            Assert.IsNotNull(poolObj);
            Assert.IsFalse(poolObj.IsAvailable);
        }

        [Test]
        public void ReturnToPool_ReturnCorrectPoolObject_ObjectNowIsAvailable()
        {
            //Arrange
            IPoolController<PoolObject_Fake> poolController = new PoolControllerImpl<PoolObject_Fake>();
            GameObject gameObject = new GameObject("TestObject");
            gameObject.AddComponent<PoolObject_Fake>();
            poolController.SetPoolObject(gameObject, 1, true);
            PoolObject_Fake poolObj = poolController.GetPoolObject();

            //Act
            poolController.ReturnToPool(poolObj);

            //Assert
            Assert.IsTrue(poolObj.IsAvailable);
        }

        [Test]
        public void ReturnToPool_ReturnNullPoolObject_ReturnError()
        {
            //Arrange
            IPoolController<PoolObject_Fake> poolController = new PoolControllerImpl<PoolObject_Fake>();

            //Act
            ArgumentException exception = Assert.Throws<ArgumentException>(() => poolController.ReturnToPool(null));

            //Assert
            Assert.That(exception.ParamName, Is.EqualTo("newPoolObj"));
        }

        [Test]
        public void ReturnToPool_ReturnPoolObjectThatDoesntBelongToThePool_ReturnError()
        {
            //Arrange
            IPoolController<PoolObject_Fake> poolController = new PoolControllerImpl<PoolObject_Fake>();
            GameObject gameObject = new GameObject("TestObject");
            gameObject.AddComponent<PoolObject_Fake>();
            poolController.SetPoolObject(gameObject, 1, true);
            GameObject gameObject_fake = new GameObject("TestObject_Fake");
            PoolObject_Fake poolObj_fake = gameObject_fake.AddComponent<PoolObject_Fake>();

            //Act
            ArgumentException exception = Assert.Throws<ArgumentException>(() => poolController.ReturnToPool(poolObj_fake));

            //Assert
            Assert.That(exception.ParamName, Is.EqualTo("newPoolObj"));
        }

        [Test]
        public void ReturnToPool_ReturnCorrectPoolObjectWithResettableObject_ObjectWasReset()
        {
            //Arrange
            IPoolController<PoolObject_Fake> poolController = new PoolControllerImpl<PoolObject_Fake>();
            GameObject gameObject = new GameObject("TestObject");
            gameObject.AddComponent<PoolObject_Fake>();
            poolController.SetPoolObject(gameObject, 1, true);
            PoolObject_Fake poolObj = poolController.GetPoolObject();

            //Act
            poolController.ReturnToPool(poolObj);

            //Assert
            Assert.IsTrue(poolObj.wasReset);
        }


        //====================================================
        //====================================================
        public class PoolObject_Fake : MonoBehaviour, IPoolResettable
        {
            public bool IsAvailable => GetComponent<PoolObject>().IsAvailable;

            public bool wasReset = false;

            void IPoolResettable.ResetPoolObject()
            {
                wasReset = true;
            }
        }
    }
}