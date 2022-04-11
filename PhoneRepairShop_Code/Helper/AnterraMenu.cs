using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace PhoneRepairShop {
    public class AnterraMenu {
        /// <summary>
        /// First request for getting cookie from authURI
        /// Then, calling menuURI service
        /// </summary>
        /// <param name="authURI"></param>
        /// <param name="menuURI"></param>
        /// <returns></returns>
        public static AnterraMenu LoadFromURI(string authURI, string menuURI) {
            CookieWebClient wc = new CookieWebClient();
            var authResult = wc.DownloadString(authURI);
            string JSON = wc.DownloadString(menuURI);
            return LoadFromJSON(JSON);
        }

        public static AnterraMenu LoadFromURIDelete(string URI) {
            var wc = new System.Net.WebClient();
            string JSON = wc.DownloadString(URI);
            return LoadFromJSON(JSON);
        }

        public static AnterraMenu LoadFromJSON(string JSON) {
            AnterraMenu ret = new AnterraMenu { MenuItems = JsonConvert.DeserializeObject<List<AnterraMenuItem>>(JSON) };
            return ret;
        }
        public List<AnterraMenuItem> MenuItems { get; set; } = new List<AnterraMenuItem>();

        /// <summary>
        /// https://qa.anterracloudbi.com/?anttok=qweqwe#/https://widgets.qa.anterracloudbi.com/?anttok=qweqwe
        /// </summary>
        /// <param name="URIprefix"></param>
        /// <param name="anterraToken"></param>
        /// <returns></returns>
        public Menu ToMenu(string URIprefix, string anterraToken) {
            Menu ret = new Menu();
            var orderedItems = MenuItems.OrderBy(x => x.groupSort).ThenBy(x => x.subGroupSort).ThenBy(x => x.itemSort);
            foreach (var i in orderedItems) {
                var group = ret.MenuItems.FirstOrDefault(x => x.Title == i.groupName);
                if (group == null) {
                    group = new MenuItem { Title = i.groupName, Tooltip = i.groupDescription, Children = new List<MenuItem>() };
                    ret.MenuItems.Add(group);
                }
                var subGroup = group.Children.FirstOrDefault(x => x.Title == i.subGroupName);
                if (subGroup == null) {
                    subGroup = new MenuItem { Title = i.subGroupName, Tooltip = i.subGroupDescription, Children = new List<MenuItem>() };
                    group.Children.Add(subGroup);
                }
                var tempURI = i.itemUrl.Contains("://widgets")
                    ? $"https://widgets.qa.anterracloudbi.com/?anttok={anterraToken}"
                    : $"{URIprefix}?{Constants.AnterraTokenParamName}={anterraToken}#/{i.itemUrl}?hideMenu=1"; // &{Constants.AnterraTokenParamName}={anterraToken}"
                subGroup.Children.Add(new MenuItem {
                    Title = i.itemName,
                    Tooltip = i.itemDescription,
                    URI = tempURI
                    //URI = $"{URIprefix}?{Constants.AnterraTokenParamName}={anterraToken}#/{i.itemUrl}?hideMenu=1&{Constants.AnterraTokenParamName}={anterraToken}"
                });
            }
            return ret;
        }
    }
}
