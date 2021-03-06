using Profile;
using Services.Ads.UnityAds;
using Services.IAP;
using Tool;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Ui
{
    internal class MainMenuController : BaseController
    {
        private readonly ResourcePath _resourcePath = new ResourcePath("Prefabs/MainMenu");
        private readonly ProfilePlayer _profilePlayer;
        private readonly MainMenuView _view;
        private UnityAdsService _adsService;
        private IAPService _iapService;

        public MainMenuController(Transform placeForUi, ProfilePlayer profilePlayer,UnityAdsService adsService, IAPService iapService)
        {
            _profilePlayer = profilePlayer;
            _view = LoadView(placeForUi);
            _view.Init(StartGame,SettingsMenu,OnAdvertismentRewarded,IAPPurchase);
            _adsService = adsService;
            _iapService = iapService;
        }


        private MainMenuView LoadView(Transform placeForUi)
        {
            GameObject prefab = ResourcesLoader.LoadPrefab(_resourcePath);
            GameObject objectView = Object.Instantiate(prefab, placeForUi, false);
            AddGameObject(objectView);

            return objectView.GetComponent<MainMenuView>();
        }

        private void StartGame() =>
            _profilePlayer.CurrentState.Value = GameState.Game;

        private void SettingsMenu() =>
            _profilePlayer.CurrentState.Value = GameState.Settings;

        private void OnAdvertismentRewarded() 
        {
            if (_adsService.IsInitialized) OnAdsInitialized();
            else _adsService.Initialized.AddListener(OnAdsInitialized);
        }         

        private void OnAdsInitialized() => _adsService.RewardedPlayer.Play();

        private void IAPPurchase()
        {
            if (_iapService.IsInitialized) OnIapInitialized();
            else _iapService.Initialized.AddListener(OnIapInitialized);
        }
    
        private void OnIapInitialized() => _iapService.Buy("product_1");


    }
}
