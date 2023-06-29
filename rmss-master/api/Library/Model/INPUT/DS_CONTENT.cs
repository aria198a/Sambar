using Library.Model.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.INPUT
{

    public class DS_CONTENT
    {
    }

    public class DS_CONTENT_ADD : BaseModel
    {
        public List<OUT_DS_CONTENT> ContAnsList { get; set; }

        public List<ANS_CONTENT_OTHER_LIST> ContAnsOtherList { get; set; }

    }

    public class ANS_CONTENT_OTHER_LIST
    {
        /// <summary>
        /// 儲存格名稱
        /// </summary>
        public string name { get; set; }

        public List<ANS_CONTENT_OTHER> list { get; set; }
    }

    public class ANS_CONTENT_OTHER
    {
        public string dcId { get; set; }

        public int ANS { get; set; }

        public List<string> sub { get; set; }

    }
}
