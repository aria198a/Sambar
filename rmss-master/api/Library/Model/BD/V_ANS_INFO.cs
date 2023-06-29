using System;
using System.Collections.Generic;

namespace Library.Model.BD
{
    public partial class V_ANS_INFO
    {
        public string AM_ID { get; set; }
        public int AM_NO { get; set; }
        public DateTime CREATED_DATETIME { get; set; }
        public int DT_RECORD { get; set; }
        public int DT_SPECIMEN { get; set; }
        public int HE_RECORD { get; set; }
        public int HE_SPECIMEN { get; set; }
        public int AR_RECORD { get; set; }
        public int AR_SPECIMEN { get; set; }
        public int PUBLICINFO { get; set; }
        public int NPUBLICINFO { get; set; }
        public int BUSINESS { get; set; }
        public int EDU { get; set; }
        public int NFOREIGNINFO { get; set; }
        public int FOREIGNINFO { get; set; }
    }
}
