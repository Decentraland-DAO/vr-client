﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using DCL;
using DCL.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEngine;
using Assert = UnityEngine.Assertions.Assert;

public class MessageQueueHandler_Mock : IMessageQueueHandler
{
    public List<QueuedSceneMessage_Scene> messagesList = new List<QueuedSceneMessage_Scene>();
    public ConcurrentQueue<QueuedSceneMessage_Scene> sceneMessagesPool { get; } = new ConcurrentQueue<QueuedSceneMessage_Scene>();

    private static bool OUTPUT_ASSERT_CODE_ON_CONSOLE = false;
    private int i = 0;

    public void EnqueueSceneMessage(QueuedSceneMessage_Scene message)
    {
        messagesList.Add(message);

        if (OUTPUT_ASSERT_CODE_ON_CONSOLE)
        {
            var output = JsonConvert.SerializeObject(message);
            output = output.Replace(@"""", @"""""");
            i++;
            Debug.Log($"string json{i} = JsonConvert.SerializeObject(queueHandler.messagesList[{i - 1}]);");
            Debug.Log($"string json{i}base = @\"{output}\";");
            Debug.Log($"Assert.AreEqual(json{i}base, json{i});");
        }
    }
}

public class NativeBridgeCommunicationShould
{
    private NativeBridgeCommunication entryPoint;
    private MessageQueueHandler_Mock queueHandler;

    [SetUp]
    public void SetUp()
    {
        queueHandler = new MessageQueueHandler_Mock();
        entryPoint = new NativeBridgeCommunication(queueHandler);

        const int sceneNumber = 1;
        const string tag = "test-tag";
        NativeBridgeCommunication.SetSceneNumber(sceneNumber);
        NativeBridgeCommunication.SetTag(tag);
    }

    [Test]
    public void QueueEntityMessagesCorrectly()
    {
        const string entityId_1 = "1";
        const string entityId_2 = "2";

        NativeBridgeCommunication.SetEntityId(entityId_1);
        NativeBridgeCommunication.CreateEntity();
        NativeBridgeCommunication.SetEntityId(entityId_2);
        NativeBridgeCommunication.CreateEntity();
        NativeBridgeCommunication.SetEntityParent(entityId_1);

        NativeBridgeCommunication.SetEntityId(entityId_1);
        NativeBridgeCommunication.RemoveEntity();
        NativeBridgeCommunication.SetEntityId(entityId_2);
        NativeBridgeCommunication.RemoveEntity();

        string json1 = JsonConvert.SerializeObject(queueHandler.messagesList[0]);
        string json1base = @"{""method"":""CreateEntity"",""payload"":{""entityId"":""1""},""tag"":""test-tag"",""type"":1,""sceneNumber"":1,""message"":null,""isUnreliable"":false}";
        Assert.AreEqual(json1base, json1);

        string json2 = JsonConvert.SerializeObject(queueHandler.messagesList[1]);
        string json2base = @"{""method"":""CreateEntity"",""payload"":{""entityId"":""2""},""tag"":""test-tag"",""type"":1,""sceneNumber"":1,""message"":null,""isUnreliable"":false}";
        Assert.AreEqual(json2base, json2);

        string json3 = JsonConvert.SerializeObject(queueHandler.messagesList[2]);
        string json3base = @"{""method"":""SetEntityParent"",""payload"":{""entityId"":""2"",""parentId"":""1""},""tag"":""test-tag"",""type"":1,""sceneNumber"":1,""message"":null,""isUnreliable"":false}";
        Assert.AreEqual(json3base, json3);

        string json4 = JsonConvert.SerializeObject(queueHandler.messagesList[3]);
        string json4base = @"{""method"":""RemoveEntity"",""payload"":{""entityId"":""1""},""tag"":""test-tag"",""type"":1,""sceneNumber"":1,""message"":null,""isUnreliable"":false}";
        Assert.AreEqual(json4base, json4);

        string json5 = JsonConvert.SerializeObject(queueHandler.messagesList[4]);
        string json5base = @"{""method"":""RemoveEntity"",""payload"":{""entityId"":""2""},""tag"":""test-tag"",""type"":1,""sceneNumber"":1,""message"":null,""isUnreliable"":false}";
        Assert.AreEqual(json5base, json5);
    }

    [Test]
    public void QueueSharedComponentMessagesCorrectly()
    {
        const string entityId = "1";
        const string componentId = "component-1";
        const int componentClass = 1;

        NativeBridgeCommunication.SetEntityId(entityId);
        NativeBridgeCommunication.CreateEntity();
        NativeBridgeCommunication.SharedComponentCreate(componentClass, componentId);
        NativeBridgeCommunication.SharedComponentAttach(componentId, null);
        NativeBridgeCommunication.SharedComponentUpdate(componentId, "{}");
        NativeBridgeCommunication.SharedComponentDispose(componentId);

        string json1 = JsonConvert.SerializeObject(queueHandler.messagesList[0]);
        string json1base = @"{""method"":""CreateEntity"",""payload"":{""entityId"":""1""},""tag"":""test-tag"",""type"":1,""sceneNumber"":1,""message"":null,""isUnreliable"":false}";
        Assert.AreEqual(json1base, json1);

        string json2 = JsonConvert.SerializeObject(queueHandler.messagesList[1]);
        string json2base = @"{""method"":""ComponentCreated"",""payload"":{""id"":""component-1"",""classId"":1,""name"":null},""tag"":""test-tag"",""type"":1,""sceneNumber"":1,""message"":null,""isUnreliable"":false}";
        Assert.AreEqual(json2base, json2);

        string json3 = JsonConvert.SerializeObject(queueHandler.messagesList[2]);
        string json3base = @"{""method"":""AttachEntityComponent"",""payload"":{""entityId"":""1"",""id"":""component-1"",""name"":null},""tag"":""test-tag"",""type"":1,""sceneNumber"":1,""message"":null,""isUnreliable"":false}";
        Assert.AreEqual(json3base, json3);

        string json4 = JsonConvert.SerializeObject(queueHandler.messagesList[3]);
        string json4base = @"{""method"":""ComponentUpdated"",""payload"":{""componentId"":""component-1"",""json"":""{}""},""tag"":""test-tag"",""type"":1,""sceneNumber"":1,""message"":null,""isUnreliable"":false}";
        Assert.AreEqual(json4base, json4);

        string json5 = JsonConvert.SerializeObject(queueHandler.messagesList[4]);
        string json5base = @"{""method"":""ComponentDisposed"",""payload"":{""id"":""component-1""},""tag"":""test-tag"",""type"":1,""sceneNumber"":1,""message"":null,""isUnreliable"":false}";
        Assert.AreEqual(json5base, json5);
    }

    [Test]
    public void QueueEntityComponentCorrectly()
    {
        const string entityId = "1";
        const string componentId = "component-1";
        const int componentClass = 1;

        NativeBridgeCommunication.SetEntityId(entityId);
        NativeBridgeCommunication.CreateEntity();
        NativeBridgeCommunication.EntityComponentCreateOrUpdate(componentClass, componentId);
        NativeBridgeCommunication.EntityComponentDestroy(componentId);

        Assert.AreEqual(3, queueHandler.messagesList.Count);

        string json1base = @"{""method"":""CreateEntity"",""payload"":{""entityId"":""1""},""tag"":""test-tag"",""type"":1,""sceneNumber"":1,""message"":null,""isUnreliable"":false}";
        string json2base = @"{""method"":""UpdateEntityComponent"",""payload"":{""entityId"":""1"",""classId"":1,""json"":""component-1""},""tag"":""test-tag"",""type"":1,""sceneNumber"":1,""message"":null,""isUnreliable"":false}";
        string json3base = @"{""method"":""ComponentRemoved"",""payload"":{""entityId"":""1"",""name"":""component-1""},""tag"":""test-tag"",""type"":1,""sceneNumber"":1,""message"":null,""isUnreliable"":false}";

        string json1 = JsonConvert.SerializeObject(queueHandler.messagesList[0]);
        string json2 = JsonConvert.SerializeObject(queueHandler.messagesList[1]);
        string json3 = JsonConvert.SerializeObject(queueHandler.messagesList[2]);

        Assert.AreEqual(json1base, json1);
        Assert.AreEqual(json2base, json2);
        Assert.AreEqual(json3base, json3);
    }

    [Test]
    public void QueueQueryCorrectly()
    {
        Protocol.QueryPayload payload = new Protocol.QueryPayload();
        payload.queryType = 79014;
        
        payload.raycastPayload = new Protocol.RaycastQueryPayload
        {
            direction = Vector3.right,
            distance = 10,
            id = 66,
            origin = Vector3.zero,
            raycastType = 1
        };
        NativeBridgeCommunication.Query(payload);

        string json1 = JsonConvert.SerializeObject(queueHandler.messagesList[0]);
        string json1base = @"{""method"":""Query"",""payload"":{""queryType"":null,""payload"":{""sceneNumber"":1,""id"":""66"",""raycastType"":1,""ray"":{""origin"":{""x"":0.0,""y"":0.0,""z"":0.0},""direction"":{""x"":1.0,""y"":0.0,""z"":0.0},""distance"":10.0}}},""tag"":""test-tag"",""type"":1,""sceneNumber"":1,""message"":null,""isUnreliable"":false}";
        Assert.AreEqual(json1base, json1);
    }

    [Test]
    public void QueueSceneReadyCorrectly()
    {
        
        NativeBridgeCommunication.SceneReady();

        string json1 = JsonConvert.SerializeObject(queueHandler.messagesList[0]);
        string json1base = @"{""method"":""InitMessagesFinished"",""payload"":{},""tag"":""test-tag"",""type"":1,""sceneNumber"":1,""message"":null,""isUnreliable"":false}";
        Assert.AreEqual(json1base, json1);
    }
}