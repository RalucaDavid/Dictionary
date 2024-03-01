using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Dictionary
{
    class AuthenticationManager
    {
        private Dictionary<string, string> credentials = new Dictionary<string, string>();
        public AuthenticationManager(string filePath)
        {
            LoadCredentials(filePath);
        }
        private void LoadCredentials(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                if (parts.Length == 2)
                {
                    string username = parts[0];
                    string password = parts[1];
                    credentials[username] = password;
                }
            }
        }
        public bool CheckCredentials(string username, string password)
        {
            if (credentials.ContainsKey(username))
            {
                return credentials[username] == password;
            }
            return false;
        }
    }
}
