using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneRepairShop {
    public class AnterraMenuItem {
        public string groupName { get; set; }
        public string groupDescription { get; set; }
        public object groupIconPath { get; set; }
        public object groupIconBackgroundPosition { get; set; }
        public object groupColor { get; set; }
        public int groupSort { get; set; }
        public string subGroupName { get; set; }
        public string subGroupDescription { get; set; }
        public string subGroupIconPath { get; set; }
        public object subGroupIconBackgroundPosition { get; set; }
        public int subGroupSort { get; set; }
        public int itemId { get; set; }
        public string itemName { get; set; }
        public string itemDescription { get; set; }
        public string itemIconPath { get; set; }
        public object itemIconBackgroundPosition { get; set; }
        public string itemUrl { get; set; }
        public string itemType { get; set; }
        public int itemSort { get; set; }
        public int biComponentId { get; set; }
        public bool isDefault { get; set; }
        public bool isFavorite { get; set; }
        public int businessAreaId { get; set; }
    }
}
