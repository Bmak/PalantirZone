using System;
using System.Collections.Generic;
using UniRx;
using JsonReader = JsonFx.Json.JsonReader;
using JsonWriter = JsonFx.Json.JsonWriter;
using Random = UnityEngine.Random;

public class PlayerRecordDomainController : ILoggable, ILifecycleAware, IDomainController
{
    [Inject] private MessageController _messageController;

    public void Reset()
    {
    }

}
