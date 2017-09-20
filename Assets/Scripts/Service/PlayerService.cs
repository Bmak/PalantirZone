using System;
using JsonFx.Json;
using UnityEngine;

public class PlayerService : ILoggable
{
    [Inject] private LocalPrefs _localPrefs;
    [Inject] private PlayerRecordDomainController _playerDC;
    [Inject] private NetworkService _networkService;
    [Inject] private MessageController _messageController;
    [Inject] private ViewProvider _viewProvider;
    [Inject] private LocalizationManager _localizationManager;

    /**********************************************/
    /******************INITIALIZATION**************/
    /**********************************************/

}
