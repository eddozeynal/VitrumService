using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPService
{
    public interface ICryptor
    {
        string Encrypt(string strNormalText);
        string Decrypt(string strEncryptedText);
    }
}
