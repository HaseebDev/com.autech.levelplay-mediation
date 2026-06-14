#if LEVELPLAY_INSTALLED
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.LevelPlay;
using Autech.LevelPlay;

/// <summary>
/// Example UI for the Autech.LevelPlay <see cref="AdsManager"/>: rewarded,
/// interstitial, banner toggle, Remove-Ads toggle, and the privacy-options form.
/// Mirrors the Autech AdMob <c>AdsExampleUI</c>. (LevelPlay has no App-Open or
/// Rewarded-Interstitial formats, so those demos are intentionally omitted.)
/// </summary>
public class AdsExampleUI : MonoBehaviour
{
    [Header("Buttons")]
    public Button showRewardedBtn;
    public Button showInterstitialBtn;
    public Button toggleBannerBtn;
    public Button toggleRemoveAdsBtn;
    public Button privacyOptionsBtn;

    [Header("Debug Logging")]
    public Text debugLogText;

    private string textLog = "LEVELPLAY DEBUG LOG:";

    public delegate void DebugEvent(string msg);
    public static event DebugEvent OnDebugLog;

    private void Start()
    {
        if (showRewardedBtn != null) showRewardedBtn.onClick.AddListener(CallRewarded);
        if (showInterstitialBtn != null) showInterstitialBtn.onClick.AddListener(CallInterstitial);
        if (toggleBannerBtn != null) toggleBannerBtn.onClick.AddListener(ToggleBanner);
        if (toggleRemoveAdsBtn != null) toggleRemoveAdsBtn.onClick.AddListener(ToggleRemoveAds);
        if (privacyOptionsBtn != null) privacyOptionsBtn.onClick.AddListener(ShowPrivacyOptions);

        InvokeRepeating(nameof(UpdateButtonStates), 0f, 1f);
    }

    private void OnEnable() => OnDebugLog += HandleDebugLog;
    private void OnDisable() => OnDebugLog -= HandleDebugLog;

    private void HandleDebugLog(string msg)
    {
        textLog += "\n" + msg;
        if (debugLogText != null) debugLogText.text = textLog;
    }

    private void UpdateButtonStates()
    {
        var ads = AdsManager.Instance;
        if (showRewardedBtn != null) showRewardedBtn.interactable = ads.IsRewardedReady();
        if (showInterstitialBtn != null) showInterstitialBtn.interactable = ads.IsInterstitialReady();
        if (toggleBannerBtn != null) toggleBannerBtn.interactable = ads.IsBannerLoaded();
        if (toggleRemoveAdsBtn != null)
        {
            var img = toggleRemoveAdsBtn.GetComponent<Image>();
            if (img != null) img.color = ads.RemoveAds ? Color.red : Color.green;
        }
        if (privacyOptionsBtn != null)
            privacyOptionsBtn.interactable = ads.ShouldShowPrivacyOptionsButton();
    }

    public void CallInterstitial()
    {
        LogTest("Showing interstitial...");
        AdsManager.Instance.ShowInterstitial(
            onSuccess: () => LogTest("Interstitial closed."),
            onFailure: () => LogTest("Interstitial failed / not ready."));
    }

    public void CallRewarded()
    {
        LogTest("Showing rewarded...");
        AdsManager.Instance.ShowRewarded(
            onRewarded: OnReward,
            onSuccess: () => LogTest("Rewarded closed."),
            onFailure: () => LogTest("Rewarded failed / not ready."));
    }

    private void OnReward(LevelPlayReward reward)
    {
        LogTest(reward != null ? $"Reward earned: {reward.Name} x{reward.Amount}" : "Reward earned.");
    }

    public void ToggleBanner()
    {
        bool visible = AdsManager.Instance.IsBannerVisible();
        AdsManager.Instance.ShowBanner(!visible);
        LogTest($"Banner -> {(!visible ? "shown" : "hidden")}");
    }

    public void ToggleRemoveAds()
    {
        bool next = !AdsManager.Instance.RemoveAds;
        AdsManager.Instance.RemoveAds = next;
        LogTest($"RemoveAds -> {next}");
    }

    public void ShowPrivacyOptions()
    {
        LogTest("Opening privacy options...");
        AdsManager.Instance.ShowPrivacyOptionsForm();
    }

    public void ClearLog()
    {
        textLog = "LEVELPLAY DEBUG LOG:";
        if (debugLogText != null) debugLogText.text = textLog;
    }

    private void LogTest(string message)
    {
        OnDebugLog?.Invoke(message);
        Debug.Log($"[Autech.LevelPlay.Example] {message}");
    }
}
#endif // LEVELPLAY_INSTALLED
