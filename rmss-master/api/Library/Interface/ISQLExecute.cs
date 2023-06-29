using System.Data;

namespace Library.Interface
{
    public interface ISQLExecute
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        int Insert();
        /// <summary>
        /// 編輯
        /// </summary>
        /// <returns></returns>
        int Update();
        /// <summary>
        /// 刪除
        /// </summary>
        /// <returns></returns>
        int Delete();
        /// <summary>
        /// 查詢
        /// </summary>
        /// <returns></returns>
        DataTable Select();
    }
}
