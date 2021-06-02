using System.Security.Cryptography;
using System.Text;

namespace LaLombriz.Clases
{
    public class Security
    {
        //static readonly string password = "H63XQnCxRZ5p"; //Static key, any change on it will affect the DB
        public Security() { }
        public string encriptar(string str)
        {
            SHA512 sha512 = SHA512Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha512.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}