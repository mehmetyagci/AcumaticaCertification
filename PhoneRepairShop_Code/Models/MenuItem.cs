using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneRepairShop {
    public class MenuItem {
        public string Title { get; set; }
        public string Tooltip { get; set; }
        public List<MenuItem> Children { get; set; } = null;
        public string URI { get; set; }
        public int TotalItems {
            get {
                int ret = 0;
                if (Children != null) {
                    ret += Children.Count;
                    foreach (var c in Children)
                        ret += c.TotalItems;
                }
                return ret;
            }
        }
    }
}
