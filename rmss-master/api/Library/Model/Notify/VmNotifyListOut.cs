using Library.Model.Apply;
using Library.Model.BD;

namespace Library.Model.Notify;

public class VmNotifyListOut
{
    /// <summary>
    /// 日期
    /// </summary>
    public string DateInfo { get; set; }
    
    /// <summary>
    /// 計畫名稱
    /// </summary>
    public string PlanName { get; set; }
    
    /// <summary>
    /// 類型 1:申請審核 2: 申請回覆
    /// </summary>
    public int TYPE { get; set; }
    
    /// <summary>
    /// ApmID
    /// </summary>
    public string ApmID { get; set; }
    
    /// <summary>
    /// AcID
    /// </summary>
    public string AcID { get; set; }

    /// <summary>
    /// 申請審核是否已閱讀
    /// </summary>
    public bool isRead { get; set; }

    /// <summary>
    /// 英文計畫名稱
    /// </summary>
    public string EnglishName { get; set; }

    /// <summary>
    /// 研究機構/執行單位(院校/科部系所)
    /// </summary>
    public string ResearchinsTitute { get; set; }

    /// <summary>
    /// 委託單位/藥廠
    /// </summary>
    public string Requester { get; set; }

    /// <summary>
    /// 經費來源
    /// </summary>
    public string Funding { get; set; }

    /// <summary>
    /// 主要主持人(姓名)
    /// </summary>
    public string HostName { get; set; }

    /// <summary>
    /// 主要主持人(職稱)
    /// </summary>
    public string HostJobTtitle { get; set; }

    /// <summary>
    /// 協同主持人(姓名)
    /// </summary>
    public string ViceHostName { get; set; }

    /// <summary>
    /// 協同主持人(職稱)
    /// </summary>
    public string ViceHostJobTtitle { get; set; }

    /// <summary>
    /// 聯絡人
    /// </summary>
    public string ContactPerson { get; set; }

    /// <summary>
    /// 上班時間聯絡電話
    /// </summary>
    public string ContactNumber { get; set; }

    /// <summary>
    /// 研究目的
    /// </summary>
    public string Purpose { get; set; }

    /// <summary>
    /// 計畫編號
    /// </summary>
    public string apmNo { get; set; }

    /// <summary>
    /// 計畫類型
    /// </summary>
    public string planType { get; set; }

    /// <summary>
    /// 申請資料來源
    /// </summary>
    public string applySource { get; set; }

    /// <summary>
    /// 申請資料類型
    /// </summary>
    public string applyType { get; set; }

    /// <summary>
    /// 申請日期
    /// </summary>
    public DateTime applyDate { get; set; }

    /// <summary>
    /// 偏好設定
    /// </summary>
    public string preference { get; set; }
}

public class VmNotifyListOut2
{
    /// <summary>
    /// 計畫名稱
    /// </summary>
    public string PlanName { get; set; }

    /// <summary>
    /// ApmID
    /// </summary>
    public string ApmID { get; set; }

    public List<VmApplyMatchInfoOut> MatchList { get; set; }
}

public class ACIDList
{
    public string acId { get; set; }

    public string apmId { get; set; }
}