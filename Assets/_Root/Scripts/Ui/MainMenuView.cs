using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Ui
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Button _buttonSettings;
        [SerializeField] private Button _buttonAdvertisement;
        [SerializeField] private Button _buttonBuy;

        public void Init(UnityAction startGame, UnityAction settings, UnityAction advertisement, UnityAction iapBuy)
        {
            _buttonStart.onClick.AddListener(startGame);
            _buttonSettings.onClick.AddListener(settings);
            _buttonAdvertisement.onClick.AddListener(advertisement);
            _buttonBuy.onClick.AddListener(iapBuy);

        }                            

        public void OnDestroy()
        {
            _buttonStart.onClick.RemoveAllListeners();
            _buttonSettings.onClick.RemoveAllListeners();
            _buttonAdvertisement.onClick.RemoveAllListeners();
            _buttonBuy.onClick.RemoveAllListeners();
        }                  
    }
}
