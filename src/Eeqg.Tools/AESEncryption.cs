using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EEQG.Tools
{
    public class AESEncryption
    {

        //默认密钥向量   
        private static byte[] _key1 = { 0x21, 0x43, 0x65, 0x87, 0x09, 0xBA, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        private static string keys = "dongbinhuiasxiny";//密钥,128位     
        /// <summary>  
        /// AES加密算法  
        /// </summary>  
        /// <param name="plainText">明文字符串</param>  
        /// <param name="strKey">密钥</param>  
        /// <returns>返回加密后的密文字节数组</returns>  
        public static byte[] AESEncryptStrToByte(string plainText)
        {
            //分组加密算法  
            //SymmetricAlgorithm des = Rijndael .Create () ;                
            byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);//得到需要加密的字节数组   
            return AESEncrypt(inputByteArray);
        }
        /// <summary>  
        /// AES加密算法  
        /// </summary>  
        /// <param name="plainText">明文字符串</param>  
        /// <param name="strKey">密钥</param>  
        /// <returns>返回加密后的密文字节数组</returns>  
        public static string AESEncryptStr(string plainText)
        {
            byte[] cipherBytes = AESEncryptStrToByte(plainText);
            return Convert.ToBase64String(cipherBytes);//Encoding.UTF8.GetString();
        }
        /// <summary>  
        /// AES加密算法  
        /// </summary>  
        /// <param name="plainText">明文字符串</param>  
        /// <param name="strKey">密钥</param>  
        /// <returns>返回加密后的密文字节数组</returns>  
        public static byte[] AESEncrypt(byte[] inputByteArray)
        {
            //分组加密算法  
            SymmetricAlgorithm des = Rijndael.Create();
            //设置密钥及密钥向量  
            des.Key = Encoding.UTF8.GetBytes(keys);
            des.IV = _key1;

            //MemoryStream ms = new MemoryStream();
            //CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            //cs.Write(inputByteArray, 0, inputByteArray.Length);
            //cs.FlushFinalBlock();
            //byte[] cipherBytes = ms.ToArray();//得到加密后的字节数组  
            //cs.Close();
            //ms.Close();
            //return cipherBytes;

            ICryptoTransform cTransform = des.CreateEncryptor();
            return cTransform.TransformFinalBlock(inputByteArray, 0, inputByteArray.Length);
        }

        /// <summary>  
        /// AES加密算法  
        /// </summary>  
        /// <param name="plainText">明文字符串</param>  
        /// <param name="strKey">密钥</param>  
        /// <returns>返回加密后的密文字节数组</returns>  
        public static byte[] AESDecryptStrToByte(string plainText)
        {
            //分组加密算法  

            byte[] inputByteArray = Convert.FromBase64String(plainText); //Encoding.UTF8.GetBytes(plainText);//得到需要加密的字节数组   
            return AESDecrypt(inputByteArray);
        }
        /// <summary>  
        /// AES加密算法  
        /// </summary>  
        /// <param name="plainText">明文字符串</param>  
        /// <param name="strKey">密钥</param>  
        /// <returns>返回加密后的密文字节数组</returns>  
        public static string AESDecryptStr(string plainText)
        {
            byte[] cipherBytes = AESDecryptStrToByte(plainText);
            return Encoding.UTF8.GetString(cipherBytes);
        }
        /// <summary>  
        /// AES解密  
        /// </summary>  
        /// <param name="cipherText">密文字节数组</param>  
        /// <param name="strKey">密钥</param>  
        /// <returns>返回解密后的字符串</returns>  
        public static byte[] AESDecrypt(byte[] cipherText)
        {
            try
            {
                SymmetricAlgorithm des = Rijndael.Create();
                //设置密钥及密钥向量  
                des.Key = Encoding.UTF8.GetBytes(keys);
                des.IV = _key1;
                //des.BlockSize = 128;
                //des.Mode = CipherMode.ECB;

                //byte[] decryptBytes = new byte[cipherText.Length];
                //MemoryStream ms = new MemoryStream(cipherText);
                //CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read);
                //cs.Read(decryptBytes, 0, decryptBytes.Length);
                //cs.Close();
                //ms.Close();
                //return decryptBytes;


                ICryptoTransform cTransform = des.CreateDecryptor();
                return cTransform.TransformFinalBlock(cipherText, 0, cipherText.Length);
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }




    }
}
