using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PhoneRepairShop {
    public class Menu {
        public Menu() { }
        public Menu(int MaxCols) {
            this.MaxCols = MaxCols;
        }
        public static Menu GetSampleMenu(int MaxTopLevel = 18, int MaxChildLevel = 8, int MaxLeafLevel = 8) {
            var id = 0;
            Random rnd = new Random();
            Menu ret = new Menu();
            int topItems = rnd.Next(3, MaxTopLevel);
            for (int i = 0; i < topItems; i++) {
                var mi = new MenuItem {
                    Title = $"Top Level {i + 1}",
                    Tooltip = $"Tooltip for Top Level Item",
                    Children = new List<MenuItem>()
                };
                var numItems = rnd.Next(1, MaxChildLevel);
                for (var c = 0; c < numItems; c++) {
                    var childItem = new MenuItem {
                        Title = $"Child Level {c + 1}",
                        Tooltip = $"Tooltip for Child Level Item",
                        Children = new List<MenuItem>()
                    };
                    var numLeaf = rnd.Next(1, MaxLeafLevel);
                    for (var l = 0; l < numLeaf; l++) {
                        id++;
                        var leafItem = new MenuItem {
                            Title = $"Leaf Level {l + 1}",
                            Tooltip = $"Tooltip for Leaf Level Item",
                            URI = $"https://qa.anterracloudbi.com/$/?id={id}"
                        };
                        childItem.Children.Add(leafItem);
                    }
                    mi.Children.Add(childItem);
                }
                ret.MenuItems.Add(mi);
            }
            return ret;
        }
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
        public int MaxCols { get; set; } = 3;
        public int TotalItems {
            get {
                return MenuItems.Sum(x => x.TotalItems);
            }
        }
        private int ItemsPerCol {
            get {
                var ret = Convert.ToInt32(Math.Ceiling((double)MenuItems.Count / MaxCols));
                if (ret < 1)
                    return 1;
                return ret;
            }
        }
        public override string ToString() {
            return ToHTML();
        }
        public string ToHTML(bool IncludeHead = false) {
            StringBuilder sbHTML = new StringBuilder();
            if (IncludeHead) {
                sbHTML.Append("<html>\n");
                sbHTML.Append("<head>\n<title>Anterra Menu</title>\n");
                sbHTML.Append("</head>\n<body>\n");
            }

            sbHTML.Append("<style>\n");
            sbHTML.Append(".anterraMenuTable { font-size: 9pt; font-family: Arial, sans-serif; width: 100%; }\n");
            sbHTML.Append(".anterraMenuCol { vertical-align: top; }\n");
            sbHTML.Append(".anterraNoLink { text-decoration: none; }\n");
            sbHTML.Append("</style>\n");

            int curCol = 0;
            int curItem = -1;
            bool keepGoing = true;
            sbHTML.Append("<table cellpadding='5' cellspacing='0' class=\"anterraMenuTable\">\n\t<tr>");
            while (keepGoing) {
                curCol++;
                sbHTML.Append("\t\t<td class=\"anterraMenuCol\">\n");
                for (int ci = 0; ci < ItemsPerCol; ci++) {
                    curItem++;
                    if (curItem > MenuItems.Count - 1) {
                        keepGoing = false;
                        break;
                    }
                    var m = MenuItems[curItem];
                    sbHTML.Append($"\t\t\t<h1>");
                    addMenuItem(sbHTML, m);
                    sbHTML.Append("</h1>\n");
                    if (m.Children != null) {
                        sbHTML.Append("\t\t\t<ul>\n");
                        foreach (var c in m.Children) {
                            sbHTML.Append($"\t\t\t\t<li>");
                            addMenuItem(sbHTML, c);
                            sbHTML.Append("</li>\n");
                            if (c.Children != null) {
                                sbHTML.Append("\t\t\t\t\t<ul>\n");
                                foreach (var l in c.Children) {
                                    sbHTML.Append($"\t\t\t\t\t<li>");
                                    addMenuItem(sbHTML, l);
                                    sbHTML.Append("</li>\n");
                                }
                                sbHTML.Append("\t\t\t\t\t</ul>\n");
                            }
                        }
                        sbHTML.Append("\t\t\t</ul>\n");
                    }
                }
                sbHTML.Append("\t\t</td>\n");
            }
            sbHTML.Append("\t</tr>\n</table>");
            if (IncludeHead)
                sbHTML.Append("\n</body>\n</html>");
            return sbHTML.ToString();

            void addMenuItem(StringBuilder paramSbHTML, MenuItem m) {
                paramSbHTML.Append($"<a");
                if (m.URI != null)
                    paramSbHTML.Append($" href=\"{m.URI}\"");
                else
                    paramSbHTML.Append(" class=\"anterraNoLink\"");
                if (m.Tooltip != null)
                    paramSbHTML.Append($" title=\"{m.Tooltip}\"");
                paramSbHTML.Append($">{m.Title}</a>");
            }
        }
        public void SaveHTML(string FileName, bool IncludeHead = false) {
            File.WriteAllText(FileName, this.ToHTML(IncludeHead));
        }

    }
}
