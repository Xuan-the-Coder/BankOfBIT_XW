using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Utility
{
    public static class Encryption
    {
        public static void Encrypt(string unencryptedFileName, string encryptedFileName, string key)
        {
            FileStream PlainTextFileStream = new FileStream(unencryptedFileName, FileMode.Open, FileAccess.Read);

            FileStream EncryptedFileStream = new FileStream(encryptedFileName, FileMode.Create, FileAccess.Write);

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(key);

            ICryptoTransform desEncrypt = des.CreateEncryptor();

            CryptoStream cryptostreamEncr = new CryptoStream(EncryptedFileStream, desEncrypt, CryptoStreamMode.Write);

            byte[] bytearray = new byte[PlainTextFileStream.Length];
            PlainTextFileStream.Read(bytearray, 0, bytearray.Length);
            cryptostreamEncr.Write(bytearray, 0, bytearray.Length);

            cryptostreamEncr.Close();
            PlainTextFileStream.Close();
            EncryptedFileStream.Close();
        }

        public static void Decrypt(string encryptedFileName, string unencryptedFileName, string key)
        {

            StreamWriter swDecrypted = new StreamWriter(unencryptedFileName);
           
            try
            {
                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                DES.Key = ASCIIEncoding.ASCII.GetBytes(key);
                DES.IV = ASCIIEncoding.ASCII.GetBytes(key);

                FileStream fsDecrypt = new FileStream(encryptedFileName, FileMode.Open, FileAccess.Read);

                ICryptoTransform desDecrypt = DES.CreateDecryptor();

                CryptoStream cryptostreamDecr = new CryptoStream(fsDecrypt, desDecrypt, CryptoStreamMode.Read);

                swDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());

                swDecrypted.Flush();

                swDecrypted.Close();

            }
            catch (Exception)
            {
                swDecrypted.Close();
                //throw new Exception("Decrypt error.");
            }
        }
    }
}
