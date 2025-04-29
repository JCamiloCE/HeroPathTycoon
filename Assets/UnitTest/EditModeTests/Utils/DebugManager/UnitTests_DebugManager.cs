using JCC.Utils.DebugManager;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class UnitTests_DebugManager
{
    [TearDown]
    public void TearDown() 
    {
        DebugManager.Debug__ResetDebugManager();
    }

    [Test]
    public void Initialization_NoCallMethodToInitialize_InitializationIsFalse() 
    {
        //Arrange

        //Act

        //Assert
        Assert.IsFalse(DebugManager.IsInitialized);
    }

    [Test]
    public void Initialization_CallMethodToInitialize_InitializationIsTrue()
    {
        //Arrange
        IDebug FakeIDebug = new Fake_IDebug();

        //Act
        DebugManager.Initialization(FakeIDebug, EDebugScope.All);

        //Assert
        Assert.IsTrue(DebugManager.IsInitialized);
    }

    [Test]
    public void Initialization_CallMethodToInitializeWithNullIDebug_InitializationIsFalseAndError()
    {
        //Arrange

        //Act
        DebugManager.Initialization(null, EDebugScope.All);

        //Assert
        Assert.IsFalse(DebugManager.IsInitialized);
        LogAssert.Expect(LogType.Error, "DebugManager::Initialization -> IDebug was null");
    }

    [Test]
    public void LogVerbose_CallMethodSeveralTimesWithMessages_CorrectNumbersOfMessage()
    {
        //Arrange
        Fake_IDebug FakeIDebug = new Fake_IDebug();

        //Act
        DebugManager.Initialization(FakeIDebug, EDebugScope.All);
        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
                DebugManager.LogVerbose("Message " + i);
            if (i == 1)
                DebugManager.LogWarning("Message " + i);
            if (i == 2)
                DebugManager.LogError("Message " + i);
        }

        //Assert
        Assert.AreEqual(3, FakeIDebug.generalMessages.Count);
        Assert.AreEqual(1, FakeIDebug.verboseMessages.Count);
        Assert.AreEqual(1, FakeIDebug.warningMessages.Count);
        Assert.AreEqual(1, FakeIDebug.errorMessages.Count);
        Assert.Contains("Message 0", FakeIDebug.verboseMessages);
        Assert.Contains("Message 1", FakeIDebug.warningMessages);
        Assert.Contains("Message 2", FakeIDebug.errorMessages);
    }

    [TestCase(EDebugScope.WarningAndError)]
    [TestCase(EDebugScope.NoLogs)]
    [TestCase(EDebugScope.Error)]
    public void LogVerbose_CallMethodWithHihgerScope_CorrectNumbersOfMessage(EDebugScope scope)
    {
        //Arrange
        Fake_IDebug FakeIDebug = new Fake_IDebug();

        //Act
        DebugManager.Initialization(FakeIDebug, scope);
        for (int i = 0; i < 3; i++)
        {
            DebugManager.LogVerbose("Message " + i);
        }

        //Assert
        Assert.AreEqual(0, FakeIDebug.verboseMessages.Count);
    }
}
