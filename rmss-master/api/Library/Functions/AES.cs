using Library.Interface;
using System;
using System.IO;
using System.Text;

namespace Library.Functions
{
    public class AES : IEncryption
    {
        /// <summary>
        /// 
        /// </summary>
        public string Key { get => "b9eQ5YMdPmfHuDxJYz/Xp820tM1CFQd84HeDEI5PE+Y="; }
        /// <summary>
        /// 
        /// </summary>
        public string IV { get => "o747+vZcy1/UR0fWfcEtgT=="; }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="decrypt"></param>
        /// <returns></returns>
        public string Decryption(string decrypt)
        {
            string ret = string.Empty;

            try
            {
                byte[] inputByteArray = Convert.FromBase64String(decrypt);

                using (System.Security.Cryptography.AesCryptoServiceProvider csp = new System.Security.Cryptography.AesCryptoServiceProvider())
                {
                    byte[] rgbKey = Convert.FromBase64String(Key);
                    byte[] rgbIV = Convert.FromBase64String(IV);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (System.Security.Cryptography.CryptoStream cs = new System.Security.Cryptography.CryptoStream(ms, csp.CreateDecryptor(rgbKey, rgbIV),
                            System.Security.Cryptography.CryptoStreamMode.Write))
                        {
                            cs.Write(inputByteArray, 0, inputByteArray.Length);
                            cs.FlushFinalBlock();
                            ret = Encoding.UTF8.GetString(ms.ToArray());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new NLog().LogDetail("Functions", "AES", "Decryption", ex.Message);
            }

            return ret;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="encrypt"></param>
        /// <returns></returns>
        public string Encryption(string encrypt)
        {
            string ret = string.Empty;

            try
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encrypt);

                using (System.Security.Cryptography.AesCryptoServiceProvider csp =
                    new System.Security.Cryptography.AesCryptoServiceProvider())
                {
                    byte[] rgbKey = Convert.FromBase64String(Key);
                    byte[] rgbIV = Convert.FromBase64String(IV);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (System.Security.Cryptography.CryptoStream cs =
                            new System.Security.Cryptography.CryptoStream(ms, csp.CreateEncryptor(rgbKey, rgbIV),
                            System.Security.Cryptography.CryptoStreamMode.Write))
                        {
                            cs.Write(inputByteArray, 0, inputByteArray.Length);
                            cs.FlushFinalBlock();
                            ret = Convert.ToBase64String(ms.ToArray());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new NLog().LogDetail("Functions", "AES", "Encryption", ex.Message);
            }

            return ret;
        }
    }
}
