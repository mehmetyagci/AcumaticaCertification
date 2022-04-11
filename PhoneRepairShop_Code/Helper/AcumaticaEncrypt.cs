using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.IO.Compression;
using Newtonsoft.Json;

namespace PhoneRepairShop {
    public class AcumaticaEncrypt {
        /// <summary>
        /// Generates new keys
        /// </summary>
        /// <param name="pubKeyFile">File name to output the public key that is embedded in Acumatica DLL</param>
        /// <param name="privKeyFile">File name to output the private key that is embedded in Anterra to decrypt</param>
        public static void GenerateKeys(string pubKeyFile, string privKeyFile) {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(4096);
            // Get the public key (used by Acumatica to encrypt, Acumatica should not have the private key included)
            string publicKey = rsa.ToXmlString(false); // false to get the public key 
            File.WriteAllText(pubKeyFile, publicKey);

            //Key used by Anterra to decrypt the data sent
            string privateKey = rsa.ToXmlString(true); // true to get the private key  
            File.WriteAllText(privKeyFile, privateKey);
        }
        /// <summary>
        /// The public key is used to encrypt, but not to decrypt, the key below is the one we can use
        /// the DecryptData method is here for reference, but shouldn't be embedded into the Acumatica library
        /// </summary>
        private const string publicKey = "<RSAKeyValue><Modulus>tjJU9P8fOi70IjNwH1MfEEZj+RsJ8Uqa49zA9kmJ/UZv90B9z2s+IP72Q6fzLsocLVWQVn+8I6Cum6D/GJ8l3i+tYieoSYR8lQkgdOqfGlGNkfIP6b50w7xGzuXwklVnmCD9hZv+s6wTAnpcd4WtU5au2QGkyTwkwVEHq88CF9S4EOMDghsq0s/WA6pjDxHiKKpVMqwn5JQDGYtIz0Gd5GwVnKPYhzOurmqdAvjZIlAXcSL2iIXFu8Sa+unekx1kGaMKY9fXHlV6IwNAbMQjTKQJa8BucQggaXIgMW0GkYP75sG7gjMlbqfD6tJ57vPa4ZQbM0QE2QmwBxeOef2PUQ4080r1RuGqlIYUwcRPFxx/AQhJe4T8anFtn0+VUnJaROewKhcwF/qxJcVFkyz1U/Bo6JPAQV9DtKKAJgHDg67sty/RQ0VE4Ll0HrYd/RK3tKQ06xbmiU7y1Vgx3hTgiVDI6DYpP6gNVBt6aAxFm8ki6VboGQu8kCiWt8l2pNes+pzIEZiIyRMxm8KbWdW3unNsF3LTM3baC06jkauy7qz+CfrVpgXweitdf9lw+Fl+iGC4j3bGEp0M1wHMHHXiR4ozozMyVJ8PtF0L3FAdlnQFvRbwCqvQQsIkxahlVrRg5xjLNGoJK/DFofYS99nVe6H+1KDvruVzG0329IKMVbk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        public static string EncryptJSON(object ObjectToEncrypt) {
            var text = JsonConvert.SerializeObject(ObjectToEncrypt, Formatting.None);

            byte[] dataToEncrypt = Zip(text);

            // Create a byte array to store the encrypted data in it   
            byte[] encryptedData;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider()) {
                // Set the rsa pulic key   
                rsa.FromXmlString(publicKey);

                // Encrypt the data and store it in the encyptedData Array   
                encryptedData = rsa.Encrypt(dataToEncrypt, false);
            }
            return Convert.ToBase64String(encryptedData);
        }
        #region Compression
        private static byte[] Zip(string str) {
            UnicodeEncoding byteConverter = new UnicodeEncoding();
            byte[] bytes = byteConverter.GetBytes(str);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream()) {
                using (var gs = new GZipStream(mso, CompressionMode.Compress)) {
                    CopyTo(msi, gs);
                }
                return mso.ToArray();
            }
        }
        private static string Unzip(byte[] bytes) {
            UnicodeEncoding byteConverter = new UnicodeEncoding();
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream()) {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress)) {
                    CopyTo(gs, mso);
                }
                return byteConverter.GetString(mso.ToArray());
            }
        }
        private static void CopyTo(Stream src, Stream dest) {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0) {
                dest.Write(bytes, 0, cnt);
            }
        }
        #endregion Compression
    }
}
