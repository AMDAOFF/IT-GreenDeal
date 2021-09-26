﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.EncryptionService
{
	public interface IEncryptionService
	{
		byte[] Encrypt(string plainText, byte[] Key, byte[] IV);
		string Decrypt(byte[] cipherText, byte[] Key, byte[] IV);
		byte[] GetKey();
		byte[] GetIV();
	}
}
