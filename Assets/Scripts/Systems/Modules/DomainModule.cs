public class DomainModule : Module
{
    override protected void Configure()
    {
        
        Bind<AuthenticationDomainController>();
        Bind<PlayerRecordDomainController>();
		Bind<LocalizationDomainController>();

/*
		Bind<AuthenticationDomainController>();
		Bind<DeviceInfoDomainController>();
        Bind<SettingsDomainController>();
		Bind<MetaDataDomainController>();
		Bind<GameDataDomainController>();
		Bind<GameData>().In( Scope.PROTOTYPE );
*/
    }
}

