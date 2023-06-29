namespace api.Models.output
{
    public class SensorThings
    {
        /// <summary>
        /// IOT數量 (@iot.count)
        /// </summary>
        public int iotcount { get; set; }

        /// <summary>
        /// @iot.nextLink
        /// </summary>
        public string iotnextLink { get; set; }

        /// <summary>
        /// Datastream 資料合集
        /// </summary>
        public List<Value> value { get; set; }

        /// <summary>
        /// 資料生產時間
        /// </summary>
        //private DateTime DataDateTime { get; set; }
    }

    /// <summary>
    /// 一組 Observation 之集合（數據流）
    /// </summary>
    /// (+): Observations
    public class Value
    {
        /// <summary>
        /// 相應 Datastream 屬性的簡短描述
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// @iot.id
        /// </summary>
        public int iotid { get; set; }

        /// <summary>
        /// 提供 Datastream 屬性一個描述性的標籤
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Observation 的類型（具有唯一的結果類型），服務使用該類型來對觀察值進行編碼
        /// </summary>
        public string observationType { get; set; }

        /// <summary>
        /// 此Datastream的所有 FeaturesOfInterest 的涵蓋範圍
        /// </summary>
        public Observedarea observedArea { get; set; }

        /// <summary>
        /// 此 Datastream 的所有觀測結果之觀測時間區間
        /// </summary>
        public string phenomenonTime { get; set; }

        /// <summary>
        /// 此 Datastream 的所有觀測結果之結果時間區間
        /// </summary>
        public string resultTime { get; set; }

        /// <summary>
        /// @iot.selfLink
        /// </summary>
        public string iotselfLink { get; set; }

        /// <summary>
        /// 此 Datastream 之觀測結果所帶有的單位
        /// </summary>
        public Unitofmeasurement unitOfMeasurement { get; set; }

        /// <summary>
        /// 物理世界的物件，也可能是資訊世界（虛擬的物體），可被識別和整合進入通訊網路
        /// </summary>
        public Thing Thing { get; set; }

        /// <summary>
        /// Observations@iot.count
        /// </summary>
        public int Observationsiotcount { get; set; }

        /// <summary>
        /// Observations@iot.nextLink
        /// </summary>
        public string ObservationsiotnextLink { get; set; }

        /// <summary>
        /// 針對特定現象產生評估結果的量測行為
        /// </summary>
        public Observation[] Observations { get; set; }

        /// <summary>
        /// Thing@iot.navigationLink
        /// </summary>
        public string ThingiotnavigationLink { get; set; }

        /// <summary>
        /// Sensor@iot.navigationLink
        /// </summary>
        public string SensoriotnavigationLink { get; set; }

        /// <summary>
        /// ObservedProperty@iot.navigationLink
        /// </summary>
        public string ObservedPropertyiotnavigationLink { get; set; }

        /// <summary>
        /// Observations@iot.navigationLink
        /// </summary>
        public string ObservationsiotnavigationLink { get; set; }
    }

    /// <summary>
    /// 此 Datastream 的所有FeaturesOfInterest的涵蓋範圍
    /// </summary>
    public class Observedarea
    {
        /// <summary>
        /// 類型
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// 座標點位
        /// </summary>
        public float[] coordinates { get; set; }
    }

    /// <summary>
    /// 觀測結果之單位
    /// </summary>
    public class Unitofmeasurement
    {
        /// <summary>
        /// 描述該空氣品質現象之觀測結果的單位之可識別描述性標籤
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 描述該空氣品質現象之觀測結果的單位之符號的文本形式
        /// </summary>
        public string symbol { get; set; }

        /// <summary>
        /// 描述該空氣品質現象之觀測結果的單位之定義URI
        /// </summary>
        public string definition { get; set; }
    }

    /// <summary>
    /// 物理世界的物件，也可能是資訊世界（虛擬的物體），可被識別和整合進入通訊網路
    /// </summary>
    public class Thing
    {
        //站點描述 例:空氣品質測站-基隆
        /// <summary>
        /// 相應 Thing 的簡短描述
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// @iot.id
        /// </summary>
        public int iotid { get; set; }

        /// <summary>
        /// 提供 Thing 物件一個描述性的標籤
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 包裝使用者註釋的 key-value 的 JSON 物件
        /// </summary>
        public Properties properties { get; set; }

        /// <summary>
        /// @iot.selfLink
        /// </summary>
        public string iotselfLink { get; set; }
    }

    /// <summary>
    /// 包裝使用者註釋的 key-value 的JSON物件
    /// </summary>
    /// (+): isDisplay, Description
    /// (-): landmark, stationName, englishName, areaName, stationType, deviceType, sensorType, status
    public class Properties
    {
        /// <summary>
        /// 空氣品質測站所在之縣市
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 空氣品質測站所在位置類型，如社區、交通區
        /// </summary>
        public string areaType { get; set; }

        /// <summary>
        /// 空氣品質測站所在之移動狀態。若未填寫，預設為固定站
        /// </summary>
        public string isMobile { get; set; }

        /// <summary>
        /// 空氣品質測站所在之鄉鎮
        /// </summary>
        public string township { get; set; }

        /// <summary>
        /// 空氣品質測站之來源，如設置及管理單位
        /// </summary>
        public string authority { get; set; }

        /// <summary>
        /// 未標記
        /// </summary>
        public string isDisplay { get; set; }

        /// <summary>
        /// 空氣品質測站所在之室內/外狀態。若未填寫，預設為室外
        /// </summary>
        public string isOutdoor { get; set; }

        /// <summary>
        /// 空氣品質測站代碼
        /// </summary>
        public string stationID { get; set; }

        /// <summary>
        /// 未標記
        /// </summary>
        public string locationId { get; set; }

        /// <summary>
        /// 未標記
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 空氣品質測站位置描述
        /// </summary>
        public string areaDescription { get; set; }
    }

    /// <summary>
    /// 一個針對特定現象產生評估結果的量測行為
    /// </summary>
    /// (-): resultQuality, validTime, parameters, Datastream, FeatureOfInterest
    public class Observation
    {
        /// <summary>
        /// @iot.id
        /// </summary>
        public int iotid { get; set; }

        /// <summary>
        /// 觀測發生的時刻或週期
        /// </summary>
        public DateTime phenomenonTime { get; set; }

        /// <summary>
        /// 對ObservedProperty 觀測所得之估計值
        /// </summary>
        public float result { get; set; }

        /// <summary>
        /// 觀測結果已經產生的時間
        /// </summary>
        public DateTime resultTime { get; set; }

        /// <summary>
        /// @iot.selfLink
        /// </summary>
        public string iotselfLink { get; set; }
    }
}