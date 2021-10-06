using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Canteen.Service.EncryptionService
{
	public class EncryptionService : IEncryptionService
	{
		private readonly IConfiguration _config;

		public EncryptionService(IConfiguration config)
		{
			_config = config;
		}

		/// <summary>
		/// Encrypts a string of plain text.
		/// </summary>
		/// <param name="plainText"></param>
		/// <returns>A byte array of the encryptet data.</returns>
		public byte[] Encrypt(string plainText)
		{
			byte[] encrypted;

			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.KeySize = 128;

				// Fetches the encryptionkey from appsettings.json
				aesAlg.Key = Encoding.ASCII.GetBytes(_config["Encryptions:Key"]);

				// Fetches the Initialization vector from appsettings.json
				aesAlg.IV = Encoding.ASCII.GetBytes(_config["Encryptions:IV"]);

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

		/// <summary>
		/// Decrypts a byte array of encryptet data.
		/// </summary>
		/// <param name="cipherText"></param>
		/// <returns>The decryptet data, that has been encryptet.</returns>
		public string Decrypt(byte[] cipherText)
		{
			string plaintext = null;

			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.KeySize = 128;

				// Fetches the encryptionkey from appsettings.json
				aesAlg.Key = Encoding.ASCII.GetBytes(_config["Encryptions:Key"]);

				// Fetches the Initialization vector from appsettings.json
				aesAlg.IV = Encoding.ASCII.GetBytes(_config["Encryptions:IV"]);

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
	}
}
