using PX.Data;
using System;

namespace PhoneRepairShop {
    [Serializable()]
    //[PXCacheName("Anterra Menu")]
    public class ANBIMenu : IBqlTable {

        [PXDBString(50, IsUnicode = true, IsKey = true)]
        [PXUIField(DisplayName = "Id")]
        public virtual string Id { get; set; }
        public abstract class id : IBqlField { }

        [PXDBString(1000, IsUnicode = true)]
        [PXUIField(DisplayName = "AcuTok")]
        public virtual string AcuTok { get; set; }
        public abstract class acuTok : IBqlField { }

        [PXDBString(100, IsUnicode = true)]
        [PXUIField(DisplayName = "AntTok")]
        public virtual string AntTok { get; set; }
        public abstract class antTok : IBqlField { }

        #region MenuHTML
        [PXUIField(DisplayName = "Menu HTML")]
        [PXDBString(int.MaxValue, IsUnicode = true)]
        public virtual string MenuHTML { get; set; }
        public abstract class menuHTML : IBqlField { }
        #endregion MenuHTML
    }
}
