using System.Collections.Generic;

namespace GoogleMobileAds.Ump.Api
{
    public static class UMPConsent
    {
        public static bool DebugMode = false;
        public static void TryShowConsentForm(bool isUnderage, System.Action<ConsentStatus> callback)
        {
            var request = new ConsentRequestParameters {TagForUnderAgeOfConsent = isUnderage};
            if (DebugMode)
            {
                request.ConsentDebugSettings = new ConsentDebugSettings()
                {
                    DebugGeography = DebugGeography.EEA,
                    TestDeviceHashedIds = new List<string> {"09D1689B-4D16-4852-8E44-69416D7AC01A"}
                };
            }

            ConsentInformation.Update(request, error =>
            {
                if (error != null)
                {
                    callback(ConsentInformation.ConsentStatus);
                    return;
                }
                ConsentForm.LoadAndShowConsentFormIfRequired(_ =>
                {
                    callback(ConsentInformation.ConsentStatus);
                });
            });
        }
        public static void OpenPrivacyOptions()
        {
            ConsentForm.ShowPrivacyOptionsForm(_=>{});
        }
    }
}