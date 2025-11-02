using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Photon.ShareLibrary.Utils
{
    public class JX_Ini
    {
        private Dictionary<string, Dictionary<string, string>> mapping = new Dictionary<string, Dictionary<string, string>>(); // section => key => value

        public JX_Ini(string path)
        {
            this.Initialize(System.IO.File.ReadAllText(path));
        }

        public JX_Ini(byte[] buffer)
        {
            this.Initialize(Encoding.GetEncoding(1252).GetString(buffer));
        }

        private void Initialize(string _literalData)
        {
            string[] rowVector = _literalData.Split(new char[] { '\r', '\n' });
            string lastSectionName = string.Empty;

            for (int indexRow = 0; indexRow < rowVector.Length; indexRow++)
            {
                string rowLiteral = rowVector[indexRow];
                rowLiteral = rowLiteral.Trim();

                if (rowLiteral.Length <= 0)
                {
                    continue;
                }

                if (rowLiteral.StartsWith("[") && rowLiteral.EndsWith("]"))
                {
                    rowLiteral = rowLiteral.TrimStart('[');
                    rowLiteral = rowLiteral.TrimEnd(']');

                    this.AddSection(rowLiteral);
                    lastSectionName = rowLiteral;
                    continue;
                }

                string[] pairVector = rowLiteral.Split('=');

                if (pairVector.Length >= 2)
                {
                    this.AddPair(lastSectionName, pairVector);
                    continue;
                }
            }
        }

        private void AddSection(string _name)
        {
            string sectionName = _name.ToLower();

            if (this.mapping.ContainsKey(sectionName) == false)
            {
                this.mapping[sectionName] = new Dictionary<string, string>();
            }
        }

        private void AddPair(string _sectionName, string[] _pairVector)
        {
            string sectionName = _sectionName.ToLower();
            string key = _pairVector[0].Trim().ToLower();
            string value = _pairVector[1].Trim();

            if (this.mapping.ContainsKey(sectionName) == false)
            {
                this.mapping[sectionName] = new Dictionary<string, string>();
            }

            this.mapping[sectionName][key] = value;
        }

        private string GetString(string _sectionName, string _key)
        {
            string section = _sectionName.ToLower();
            string key = _key.ToLower();

            if (this.mapping.ContainsKey(section) == false)
            {
                return string.Empty;
            }

            if (this.mapping[section].ContainsKey(key) == false)
            {
                return string.Empty;
            }

            return this.mapping[section][key];
        }

        private int GetInt(string _sectionName, string _key)
        {
            string value = this.GetString(_sectionName, _key);

            if (value == string.Empty)
            {
                return 0;
            }

            value = Regex.Replace(value, "[^0-9-]", string.Empty);

            return int.Parse(value);
        }

        private byte[] GetBytes(string _sectionName, string _key)
        {
            string value = this.GetString(_sectionName, _key);

            if (value == string.Empty)
            {
                return new byte[0];
            }

            return Encoding.ASCII.GetBytes(value);
        }

        public bool IsEmpty()
        {
            return this.mapping.Count <= 0;
        }

        public bool IsNotEmpty()
        {
            return this.mapping.Count > 0;
        }

        /*  supporting
         *  
         *  string
         *  int
         */

        public Typename Get<Typename>(string _section, string _key)
        {
            System.Type requestType = typeof(Typename);

            if (requestType == typeof(string)) return (Typename)(object)this.GetString(_section, _key);
            if (requestType == typeof(int)) return (Typename)(object)this.GetInt(_section, _key);
            if (requestType == typeof(byte[])) return (Typename)(object)this.GetBytes(_section, _key);

            return (Typename)(object)null;
        }
    }
}
