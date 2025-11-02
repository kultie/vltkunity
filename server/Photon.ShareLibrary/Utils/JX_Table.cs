using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Photon.ShareLibrary.Utils
{
    public class JX_Table
    {
        private int rowTotal;
        private string[] rowLiteralVector;
        private Dictionary<int, Dictionary<int, string>> rowKeyCacheValue; // row index => header index => value as string
        private Dictionary<string, int> headerKeyIndex; // header column key => index
        private Dictionary<int, string> headerIndexKey; // header column index => key

        public JX_Table()
        {

        }

        public JX_Table(string path)
        {
            rowLiteralVector = System.IO.File.ReadAllLines(path);
            Initialize();
        }

        private void Initialize()
        {
            this.rowTotal = this.rowLiteralVector.Length;
            this.rowKeyCacheValue = new Dictionary<int, Dictionary<int, string>>();
            this.headerKeyIndex = new Dictionary<string, int>();
            this.headerIndexKey = new Dictionary<int, string>();

            if (this.rowTotal <= 0)
            {
                return;
            }

            string[] headerKeyVector = rowLiteralVector[0].Split('\t');

            for (int index = 0; index < headerKeyVector.Length; index++)
            {
                string headerKey = headerKeyVector[index];

                if (index == headerKeyVector.Length - 1)
                {
                    headerKey = JX_Table.RemoveSpecialSymbol(headerKey);
                }

                this.headerKeyIndex[headerKey] = index;
                this.headerIndexKey[index] = headerKey;
            }
        }

        private static string RemoveSpecialSymbol(string _string)
        {
            string result = _string;
            char[] removeCharList = { '\r', '\n', '\t' };
            int removePosition;

            while ((removePosition = result.IndexOfAny(removeCharList)) != -1)
            {
                result = result.Remove(removePosition, 1);
            }

            return result;
        }

        public string GetString(int _headerIndex, int _rowIndex)
        {
            if (this.rowTotal <= 0
                || this.rowTotal <= _rowIndex)
            {
                return string.Empty;
            }

            if (_rowIndex <= 0)
            {
                if (this.headerIndexKey.ContainsKey(_headerIndex))
                {
                    return headerIndexKey[_headerIndex];
                }
                else
                {
                    return string.Empty;
                }
            }

            if ((this.rowKeyCacheValue.ContainsKey(_rowIndex)) == false)
            {
                string[] rowSplited = this.rowLiteralVector[_rowIndex].Split('\t');

                this.rowLiteralVector[_rowIndex] = string.Empty;
                this.rowKeyCacheValue[_rowIndex] = new Dictionary<int, string>();

                int indexer = 0;
                foreach (var indexHeaderPair in this.headerKeyIndex)
                {
                    if (indexHeaderPair.Value >= rowSplited.Length)
                    {
                        this.rowKeyCacheValue[_rowIndex][indexHeaderPair.Value] = string.Empty;
                        continue;
                    }

                    string value = rowSplited[indexHeaderPair.Value];

                    if (indexer == rowSplited.Length - 1)
                    {
                        value = JX_Table.RemoveSpecialSymbol(value);
                    }

                    this.rowKeyCacheValue[_rowIndex][indexHeaderPair.Value] = value;
                    indexer++;
                }
            }

            if (this.rowKeyCacheValue[_rowIndex].ContainsKey(_headerIndex) == false)
            {
                return string.Empty;
            }

            return this.rowKeyCacheValue[_rowIndex][_headerIndex];
        }

        public string GetString(string _headerKey, int _rowIndex)
        {
            if (this.headerKeyIndex.ContainsKey(_headerKey) == false)
            {
                return string.Empty;
            }

            return this.GetString(this.headerKeyIndex[_headerKey], _rowIndex);
        }

        public int GetInt(string _headerKey, int _rowIndex)
        {
            string value = this.GetString(_headerKey, _rowIndex);

            if (value == string.Empty)
            {
                return -1;
            }

            value = Regex.Replace(value, "[^0-9-]", string.Empty);

            if (value == string.Empty)
            {
                return -1;
            }

            return int.Parse(value);
        }

        public int GetInt(int _columnIndex, int _rowIndex, int _default)
        {
            string value = this.GetString(_columnIndex, _rowIndex);

            if (value == string.Empty)
            {
                return _default;
            }

            value = Regex.Replace(value, "[^0-9-]", string.Empty);

            if (value == string.Empty)
            {
                return _default;
            }

            return int.Parse(value);
        }

        public float GetFloat(int _columnIndex, int _rowIndex, float _default)
        {
            string value = this.GetString(_columnIndex, _rowIndex);

            if (value == string.Empty)
            {
                return _default;
            }

            if (float.TryParse(value, out float result))
            {
                return result;
            }

            return _default;
        }
        
        public bool GetBool(int _columnIndex, int _rowIndex, bool _default)
        {
            string value = this.GetString(_columnIndex, _rowIndex);

            if (value == string.Empty)
            {
                return _default;
            }

            return value != "0";
        }

        public int HeaderCount
        {
            get { return this.headerKeyIndex.Count; }
        }

        public int RowCount
        {
            get { return this.rowTotal; }
        }

        public bool IsEmpty()
        {
            return this.rowTotal <= 0;
        }

        public bool IsNotEmpty()
        {
            return this.rowTotal > 0;
        }

        public int FindRowIndex(string _headerKey, string _data)
        {
            int result = -1;

            for (int rowIndex = 0; rowIndex < this.rowTotal; rowIndex++)
            {
                if (this.GetString(_headerKey, rowIndex).CompareTo(_data) == 0)
                {
                    result = rowIndex;
                    break;
                }
            }

            return result;
        }

        public List<string> GetHeaderKeyList()
        {
            List<string> result = new List<string>();

            foreach (KeyValuePair<string, int> pairIndex in this.headerKeyIndex)
            {
                result.Add(pairIndex.Key);
            }

            return result;
        }

        public Dictionary<string, int> GetHeaderKeyIndexPair()
        {
            return this.headerKeyIndex;
        }

        public int GetHeaderIndex(string _headerKey)
        {
            if (this.headerKeyIndex.ContainsKey(_headerKey))
            {
                return this.headerKeyIndex[_headerKey];
            }

            return -1;
        }

        public string GetHeaderKey(int _headerIndex)
        {
            if (this.headerIndexKey.ContainsKey(_headerIndex))
            {
                return this.headerIndexKey[_headerIndex];
            }

            return string.Empty;
        }

        /*  supporting
         *  
         *  string
         *  int
         */

        public Typename Get<Typename>(string _headerKey, int _rowIndex)
        {
            Type requestType = typeof(Typename);

            if (requestType == typeof(string)) return (Typename)(object)this.GetString(_headerKey, _rowIndex);
            if (requestType == typeof(int)) return (Typename)(object)this.GetInt(_headerKey, _rowIndex);

            return (Typename)(object)null;
        }

        public Typename Get<Typename>(int _columnIndex, int _rowIndex)
        {
            Type requestType = typeof(Typename);

            if (requestType == typeof(string)) return (Typename)(object)this.GetString(_columnIndex, _rowIndex);
            if (requestType == typeof(int)) return (Typename)(object)this.GetInt(_columnIndex, _rowIndex, -1);
            if (requestType == typeof(float)) return (Typename)(object)this.GetFloat(_columnIndex, _rowIndex, 0f);
            if (requestType == typeof(bool)) return (Typename)(object)this.GetBool(_columnIndex, _rowIndex, false);

            return (Typename)(object)null;
        }

        public Typename Get<Typename>(int _columnIndex, int _rowIndex, Typename _default)
        {
            Type requestType = typeof(Typename);

            if (requestType == typeof(int)) return (Typename)(object)this.GetInt(_columnIndex, _rowIndex, (int)(object)_default);
            if (requestType == typeof(float)) return (Typename)(object)this.GetFloat(_columnIndex, _rowIndex, (float)(object)_default);
            if (requestType == typeof(bool)) return (Typename)(object)this.GetBool(_columnIndex, _rowIndex, (bool)(object)_default);

            return (Typename)(object)null;
        }
    }
}
