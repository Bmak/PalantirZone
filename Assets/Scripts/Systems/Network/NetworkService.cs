public enum NetworkRequest
{
    GetManifest,
    GetLogic,
    GetProfile,
    GetNextSeed,
    SyncLogic,

}

public class NetworkService : ILoggable, IInitializable
{
    [Inject] private Config _config;
    [Inject] private MessageController _messageController;
    [Inject] private PlayerRecordDomainController _playerDC;
    [Inject] private PlayerService _playerService;
    [Inject] private LocalizationManager _localizationManager;

    public void Initialize(InstanceInitializedCallback initializedCallback = null)
    {
        if (initializedCallback != null) initializedCallback(this);
    }

}
