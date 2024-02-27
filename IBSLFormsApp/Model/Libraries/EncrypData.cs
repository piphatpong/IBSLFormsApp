using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using IBSLFormsApp.Helpers;

namespace IBSLFormsApp.Model.Libraries
{
    internal class EncrypData
    {
        public static string EncryptData(string value, string centralCerPath)
        {
            /*************** Log Test ***************/
            //DebugLog debl = new DebugLog();
            if (value != null)
            {
                //debl.debuglog("Encryp:" + value + " key: "+centralCerPath);
                // Load the certificate into an X509Certificate object.
                X509Certificate cert = new X509Certificate(centralCerPath);
                RSA rsa = RSA.Create();
                rsa.ImportRSAPublicKey(cert.GetPublicKey(), out _);
                try
                {
                    byte[] data = BaseHelper.ConvertStringToByte(value);
                    byte[] encryptedData = rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);
                    string encryptedText = Convert.ToBase64String(encryptedData);

                    return encryptedText;
                }
                catch (Exception ex)
                {
                    throw ex;
                    Console.WriteLine(ex.ToString());
                    value = ex.Message;
                }
            }
            else { return value; }
        }
    }
}
