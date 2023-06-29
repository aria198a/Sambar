namespace Library.Interface
{
    interface IEncryption
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="encrypt"></param>
        /// <returns></returns>
        string Encryption(string encrypt);
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="decrypt"></param>
        /// <returns></returns>
        string Decryption(string decrypt);
    }
}
