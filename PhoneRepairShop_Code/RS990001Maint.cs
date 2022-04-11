using System;
using PX.Data;
using System;
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;

namespace PhoneRepairShop {
    
    public class RS990001Maint : PXGraph<RS990001Maint> {
   
        [PXVirtualDAC]
        [PXPreview(typeof(PXFilter<ANBIMenu>))]
        public PXFilter<ANBIMenu> MasterView;
        public IEnumerable masterView() {

            PXTrace.WriteInformation("Processing Anterra Menu creation ");

            #region Encryption
            var authPayload = new {
                ValidUntil = DateTimeOffset.UtcNow.AddMinutes(5).ToUnixTimeSeconds(),
                ClientId = "A02F8FA3-B6FF-2446-5BE9-5DE174B9D79C@AVADEK",
                UserEMail = "anterra@avadek.com"
            };
            var encryptedQS = AcumaticaEncrypt.EncryptJSON(authPayload);
            string sampleURL = $"https://qa.anterracloudbi.com/#/?auth={System.Net.WebUtility.UrlEncode(encryptedQS)}";

            //Example showing decrypted data for Alex's implementation
            #endregion Encryption

            Guid guid = Guid.NewGuid();

            var anterraMenu = AnterraMenu.LoadFromURIDelete("https://anterra-customer-landing.s3.us-east-1.amazonaws.com/MenuSample.json");
            var mockMenu = anterraMenu.ToMenu("https://qa.anterracloudbi.com/#/", guid.ToString());
            var mockMenuHTML = mockMenu.ToHTML(true);

            ANBIMenu anbiMenu = new ANBIMenu() { Id = "1", AntTok = guid.ToString(), MenuHTML = mockMenuHTML };

            //int iCachedData = 0;
            //foreach (var row in MasterView.Cache.Cached) {
            //    iCachedData++;
            //    yield return anbiMenu;
            //}

            //if (iCachedData == 0) {
                anbiMenu = MasterView.Insert(anbiMenu);
                MasterView.Cache.SetStatus(anbiMenu, PXEntryStatus.Held);
                yield return anbiMenu;
            //}
        }
    }
}
