using Library.Model;

namespace api.Models.input
{
    public class NotifycsInput
    {
    }
    public class NotifycsListAPMIInput : BaseModel
    {
        /// <summary>
        /// AcID
        /// </summary>
        public string? AcID { get; set; }
    }
}
