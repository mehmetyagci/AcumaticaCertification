using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneRepairShop {
    public static class RepairComplexity {
        public const string Low = "L";
        public const string Medium = "M";
        public const string High = "H";
    }

    public static class RepairItemTypeConstants {
        public const string Battery = "BT";
        public const string Screen = "SR";
        public const string ScreenCover = "SC";
        public const string BackCover = "BC";
        public const string Motherboard = "MB";
    }

    public static class Constants {
        public const string ProjectName = "AnterraBI";
        // @"https://auth.dev.anterracloudbi.com/";    Dev 
        // @"https://auth.qa.anterracloudbi.com/";     Test 
        // @"https://auth.stage.anterracloudbi.com/";  Stage
        // @"https://auth.anterracloudbi.com/";        Prod
        public const string AnterraAuthURL = @"https://auth.qa.anterracloudbi.com/";
        public const string AnterraMenuServiceURL = @"https://qa.anterracloudbi.com/api/menu"; //"https://anterra-customer-landing.s3.us-east-1.amazonaws.com/MenuSample.json";
        public const string AnterraMenuURL = @"https://qa.anterracloudbi.com/";  //  @"https://qa.anterracloudbi.com/#/";
        public const string AnterraTokenParamName = "anttok";
        public const string AcumaticaTokenParamName = "acutok";

        //Constants for the repair item types
       

    }

   
}
