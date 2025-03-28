using EvenSystemCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;

public class EventManagerUnitTests
{
    [Test]
    public void AddListener_NullListener_ArgumentNullException()
    {
        //Arrange
        Listener_Fake listener_Fake = null;

        //Act
        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => EventManager.AddListener(listener_Fake));

        //Assert
        Assert.That(exception.ParamName, Is.EqualTo("new_listener"));

        //End
        EventManager.ClearListener();
    }

    [Test]
    public void AddListener_FilledListener_NoErrorAndNoCalledEvent()
    {
        //Arrange
        Listener_Fake listener_Fake = new();

        //Act
        EventManager.AddListener(listener_Fake);

        //Assert
        Assert.That(listener_Fake.OnEventCalledCount, Is.EqualTo(0));

        //End
        EventManager.ClearListener();
    }

    [TestCase(5)]
    [TestCase(-1)]
    [TestCase(20)]
    public void TriggerEvent_CallOneEvent_ReceivedArgumentsAndOneCalledCount(int expectedNumber)
    {
        //Arrange
        Listener_Fake listener_Fake = new();
        EventManager.AddListener(listener_Fake);

        //Act
        EventManager.TriggerEvent<Event_Fake>(expectedNumber);

        //Assert
        Assert.That(listener_Fake.OnEventCalledCount, Is.EqualTo(1));
        Assert.AreEqual(expectedNumber, listener_Fake.numberReceived[0]);

        //End
        EventManager.ClearListener();
    }

    [TestCase(5, 3)]
    [TestCase(-1, 2)]
    [TestCase(20, 4)]
    public void TriggerEvent_CallMoreThanOneEvent_ReceivedArgumentsAndTheNumberCalledCount(int expectedNumber, int numberOfCalls)
    {
        //Arrange
        Listener_Fake listener_Fake = new();
        EventManager.AddListener(listener_Fake);

        //Act
        for (int i = 0; i < numberOfCalls; i++)
        {
            EventManager.TriggerEvent<Event_Fake>(expectedNumber);
        }

        //Assert
        Assert.AreEqual(numberOfCalls, listener_Fake.OnEventCalledCount);
        for (int i = 0; i < numberOfCalls; i++)
        {
            Assert.AreEqual(expectedNumber, listener_Fake.numberReceived[i]);
        }

        //End
        EventManager.ClearListener();
    }

    [TestCase(1, 2, 3)]
    [TestCase(-1, -2, -3)]
    [TestCase(-5, 0, 5)]
    public void TriggerEvent_CallMoreThanOneEventWithDifferentValue_ReceivedArguments(int expectedFirstNumber, int expectedSecondNumber, int expectedThirdNumber)
    {
        //Arrange
        Listener_Fake listener_Fake = new();
        EventManager.AddListener(listener_Fake);

        //Act
        EventManager.TriggerEvent<Event_Fake>(expectedFirstNumber);
        EventManager.TriggerEvent<Event_Fake>(expectedSecondNumber);
        EventManager.TriggerEvent<Event_Fake>(expectedThirdNumber);

        //Assert
        Assert.AreEqual(expectedFirstNumber, listener_Fake.numberReceived[0]);
        Assert.AreEqual(expectedSecondNumber, listener_Fake.numberReceived[1]);
        Assert.AreEqual(expectedThirdNumber, listener_Fake.numberReceived[2]);

        //End
        EventManager.ClearListener();
    }

    private class Event_Fake : EventBase
    {
        public int number;

        public override void SetParameters(params object[] parameters)
        {
            number = (int)parameters[0];
        }
    }

    private class Listener_Fake : IEventListener<Event_Fake> 
    {
        public Event_Fake dataReceived;
        public List<int> numberReceived = new ();
        public int OnEventCalledCount;

        void IEventListener<Event_Fake>.OnEvent(Event_Fake event_data)
        {
            dataReceived = event_data;
            numberReceived.Add(event_data.number);
            OnEventCalledCount++;
        }
    }
}
