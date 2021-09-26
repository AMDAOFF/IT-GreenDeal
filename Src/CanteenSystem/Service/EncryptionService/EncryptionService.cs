using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Service.EncryptionService
{
	public class EncryptionService : IEncryptionService
	{
		private readonly IConfiguration _config;

		public EncryptionService(IConfiguration config)
		{
			_config = config;
		}

		public byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
		{
			byte[] encrypted;

			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.KeySize = 128;
				aesAlg.Key = Key;
				aesAlg.IV = IV;

				ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

				using (MemoryStream msEncrypt = new())
				{
					using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter swEncrypt = new(csEncrypt))
						{
							swEncrypt.Write(plainText);
						}
					}
					encrypted = msEncrypt.ToArray();
				}
			}

			return encrypted;
		}

		public string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
		{
			string plaintext = null;

			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.KeySize = 128;
				aesAlg.Key = Key;
				aesAlg.IV = IV;
				aesAlg.Padding = PaddingMode.None;

				ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

				using (MemoryStream msDecrypt = new(cipherText))
				{
					using (CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader srDecrypt = new(csDecrypt))
						{
							plaintext = srDecrypt.ReadToEnd();
						}
					}
				}
			}

			return plaintext;
		}

		public byte[] GetKey()
		{
			return Encoding.ASCII.GetBytes(_config["Encryptions:Key"]);
		}

		public byte[] GetIV()
		{
			return Encoding.ASCII.GetBytes(_config["Encryptions:IV"]);
		}
	}
}
