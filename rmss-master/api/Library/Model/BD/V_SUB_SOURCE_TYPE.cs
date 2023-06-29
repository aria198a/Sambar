using System;
using System.Collections.Generic;

namespace Library.Model.BD
{
    public partial class V_SUB_SOURCE_TYPE
    {
        public string AC_ID { get; set; }
        public string AM_ID { get; set; }
        public string AC_USERID { get; set; }
        public string AM_NO { get; set; }
        public string ANS_TYPE { get; set; }
        public string ANS_SOURCECN { get; set; }
        public string ANS_TYPECN { get; set; }
        public int AC_ANS { get; set; }
        public string SUB { get; set; }
        public int? PUBLICINFO { get; set; }
        public int? NPUBLICINFO { get; set; }
        public int? BUSINESS { get; set; }
        public int? EDU { get; set; }
        public int? NFOREIGNINFO { get; set; }
        public int? FOREIGNINFO { get; set; }
        public string PUBLICINFO_SUB { get; set; }
        public string NPUBLICINFO_SUB { get; set; }
        public string BUSINESS_SUB { get; set; }
        public string EDU_SUB { get; set; }
        public string NFOREIGNINFO_SUB { get; set; }
        public string FOREIGNINFO_SUB { get; set; }
        public bool APM_IS_READED { get; set; }
        // public int APPLY_STATUS { get; set; }

        public DateTime? CREATED_DATETIME { get; set; }
    }

    public partial class V_SUB_SOURCE_TYPE_STATUS: V_SUB_SOURCE_TYPE
    {
      
         public int APPLY_STATUS { get; set; }

       
    }
}
