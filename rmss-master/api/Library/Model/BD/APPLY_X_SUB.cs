using System;
using System.Collections.Generic;

namespace Library.Model.BD
{
    public partial class APPLY_X_SUB
    {
        public string APM_ID { get; set; }

        /// <summary>
        /// 癌症, 1:是; 2:否; 3:不適用
        /// </summary>
        public int CANCER { get; set; }

        /// <summary>
        /// 心血管疾病
        /// </summary>
        public int CARDIO { get; set; }

        /// <summary>
        /// 糖尿病
        /// </summary>
        public int DIABETES { get; set; }

        /// <summary>
        /// 神經退化性疾病
        /// </summary>
        public int NEURO { get; set; }

        /// <summary>
        /// 心理健康與精神疾病
        /// </summary>
        public int MENTAL { get; set; }

        /// <summary>
        /// 產期與生殖健康
        /// </summary>
        public int BIRTH { get; set; }

        /// <summary>
        /// 傳染性疾病
        /// </summary>
        public int INFECTIOUS { get; set; }

        /// <summary>
        /// 耳及乳突之疾病
        /// </summary>
        public int EAR { get; set; }

        /// <summary>
        /// 小兒科
        /// </summary>
        public int PEDIATRICS { get; set; }

        /// <summary>
        /// 長者與老年醫學
        /// </summary>
        public int ELDERLY { get; set; }
    }
}
