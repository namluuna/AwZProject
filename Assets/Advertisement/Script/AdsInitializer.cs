using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    [SerializeField] protected InterstitialAd interstitialAd;
    [SerializeField] protected List<RewardedAdsButton> rewardedAdsButtons;
    [SerializeField] protected List<BannerAd> bannerAds;

    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
#if UNITY_IOS
            _gameId = _iOSGameId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
            _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }

    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");

        LoadAds();     
    }

    private void LoadAds()
    {
        //Load ads
        interstitialAd.LoadAd();
        foreach (RewardedAdsButton btn in rewardedAdsButtons)
        {
            btn.LoadAd();
        }
        foreach (BannerAd bannerAd in bannerAds)
        {
            bannerAd.LoadBanner();
        }
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}