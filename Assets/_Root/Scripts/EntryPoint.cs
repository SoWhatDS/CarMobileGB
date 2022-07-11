using Profile;
using UnityEngine;
using Services.Analytics;
using Services.Ads.UnityAds;
using Services.IAP;

internal class EntryPoint : MonoBehaviour
{
    private const float SpeedCar = 15f;
    private const GameState InitialState = GameState.Start;

    [SerializeField] private Transform _placeForUi;
    [SerializeField] private UnityAdsService _adsService;
    [SerializeField] private AnalyticsManager _analytics;
    [SerializeField] private IAPService _iapService;

    private MainController _mainController;


    private void Start()
    {        
        var profilePlayer = new ProfilePlayer(SpeedCar, InitialState);      
        _mainController = new MainController(_placeForUi, profilePlayer,_analytics,_adsService,_iapService);

        _analytics.SendMainMenuOpened();

        if (_adsService.IsInitialized) OnAdsInitialized();
        else _adsService.Initialized.AddListener(OnAdsInitialized);

    }

    private void OnDestroy()
    {
        _mainController.Dispose();
        _adsService.Initialized.RemoveListener(OnAdsInitialized);
        
    }

    private void OnAdsInitialized() => _adsService.InterstitialPlayer.Play();    
}
