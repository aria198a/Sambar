using System;
using System.Collections.Generic;

namespace Library.Model.BD
{
    public partial class V_APPLY_INFO
    {
        public string APM_ID { get; set; }
        public string APM_NO { get; set; }
        public string NAME { get; set; }
        public int TPLACE { get; set; }
        public string USERID { get; set; }
        public int? DT_RECORD { get; set; }
        public int? DT_SPECIMEN { get; set; }
        public int? HE_RECORD { get; set; }
        public int? HE_SPECIMEN { get; set; }
        public int? AR_RECORD { get; set; }
        public int? AR_SPECIMEN { get; set; }
        public int? PUBLICINFO { get; set; }
        public int? NPUBLICINFO { get; set; }
        public int? BUSINESS { get; set; }
        public int? EDU { get; set; }
        public int? FOREIGNINFO { get; set; }
        public int? NFOREIGNINFO { get; set; }
        public string X_SUB { get; set; }
        public string Y_SUB { get; set; }
    }
}
