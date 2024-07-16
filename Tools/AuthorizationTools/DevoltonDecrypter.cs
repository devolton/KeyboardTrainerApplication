using Encrypter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectKeyboardApplication.Tools.AuthorizationTools
{
    public sealed class DevoltonDecrypter:BaseDevoltonEncrypter
    {
        private static DevoltonDecrypter _instance;
        private DevoltonDecrypter():base()
        {
            
        }
        public static DevoltonDecrypter Instance()
        {
            _instance ??= new DevoltonDecrypter();
            return _instance;
        }
        private string DecryptCeasar(string str)
        {
            List<string> strItems = new List<string>();
            foreach (var item in GetSliced(str, Math.Abs(_key)))
            {
                strItems.Add(item);
            }
            int index;
            int remainderIndex = (_key >= 0) ? 0 : 1;
            foreach (var oneStr in strItems)
            {
                index = strItems.IndexOf(oneStr);
                for (int i = 0; i < oneStr.Length; i++)
                {
                    if (index % 2 == remainderIndex)
                        _sb.Append((char)(oneStr[i] - _key));

                    else
                        _sb.Append((char)(oneStr[i] + _key));
                }

            }

            string result = _sb.ToString();
            _sb.Clear();
            return result;

        }
        private string DecryptMakiavelli(string str)
        {
            int replaceIndex = (_key >= 0) ? Math.Abs(_key) % 7 : Math.Abs(_key) % 7 + 2;
            var sb = new StringBuilder(str);
            int counter = 0;
            for (int i = 0; i < str.Length / 2; i++)
            {
                if (counter == replaceIndex)
                {
                    char firstReplaceValue = str[i];
                    char secondReplaceValue = str[str.Length - 1 - i];
                    sb[i] = secondReplaceValue;
                    sb[str.Length - 1 - i] = firstReplaceValue;
                    counter = 0;
                }
                else
                    counter++;
            }
            return sb.ToString();
        }
        private string DecryptSeneca(string str)
        {
            _sb = new StringBuilder();
            var mostShowChar = GetTheMostFrequentlyOccurringCharacter(str);
            var leastShowChar = GetLeastFrequentlyOccurringCharacter(str);
            var mostShowCharCount = str.Count(symbol => symbol.Equals(mostShowChar));
            var leastShowCharCount = str.Count(symbol => symbol.Equals(leastShowChar));
            int manipulationValue = mostShowCharCount + leastShowCharCount;
            int startIndex = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (i % manipulationValue == 0 && i != 0)
                {
                    string currentSubstr = str.Substring(startIndex, i - startIndex + 1);
                    int middleIndex = currentSubstr.Length / 2;
                    if (currentSubstr.Length % 2 == 1) middleIndex++;
                    string firstSubstrPart = currentSubstr.Substring(0, middleIndex);
                    string secondSubstrPart = currentSubstr.Substring(middleIndex);
                    _sb.Append(secondSubstrPart);
                    _sb.Append(firstSubstrPart);
                    startIndex = i + 1;

                }
                else if (i == str.Length - 1)
                {
                    _sb.Append(str.Substring(startIndex));
                }
            }

            string result = _sb.ToString();
            _sb.Clear();
            return result;
        }
        private string DecryptComod(string str)
        {
            var sliceSize = (_isStrKey) ? GetDeliveder() : GetDeliveder() + 1;
            if (sliceSize == 0) sliceSize = 4;
            var strList = GetSliced(str, sliceSize).ToArray();

            for (int i = 0; i < strList.Length; i++)
            {
                if (i % 2 == 0)
                {
                    foreach (var c in strList[i].Reverse())
                        _sb.Append(c);


                }
                else
                    _sb.Append(strList[i]);

            }
            string result = _sb.ToString();
            _sb.Clear();

            return result;
        }
        private string DecryptAvreliy(string str)
        {

            StringBuilder currentSb = new StringBuilder();

            string[] byteArr;

            foreach (var oneByte in GetSliced(str, 32))
            {
                byteArr = GetBrokenIntPerByte(oneByte, false);
                BitArray secondBlock = FromBinaryStringToBitArray(byteArr[1]);
                BitArray thirdBlock = FromBinaryStringToBitArray(byteArr[2]);
                var xorSecondResult = secondBlock.Xor(_xorBitArrayForSecondBlock);
                var xorThirdResult = thirdBlock.Xor(_xorBitArrayForThirdBlock);
                currentSb.Append(byteArr[0]);
                currentSb.Append(FromBitArrayToBinaryString(xorSecondResult));
                currentSb.Append(FromBitArrayToBinaryString(xorThirdResult));
                currentSb.Append(byteArr[3]);
                _sb.Append((char)(Convert.ToInt32(currentSb.ToString(), 2)));
                currentSb.Clear();

            }
            string result = _sb.ToString();
            _sb.Clear();
            return result;
        }
        private string DecryptCicero(string str)
        {

            foreach (var oneChar in GetSliced(str, 32))
            {
                _sb.Append(ReplaceBits(oneChar, false));

            }
            string result = _sb.ToString();
            _sb.Clear();
            return result;
        }

        private string DecryptEpictetus(string str)
        {

            foreach (var oneCharBytes in GetSliced(str, 32))
            {
                var byteArr = GetBrokenIntPerByte(oneCharBytes, false);
                for (int i = 0; i < byteArr.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        string inversionPart = (i == 0) ? byteArr[i].Substring(4, 4) : byteArr[i].Substring(0, 4);
                        string staticPart = (i == 0) ? byteArr[i].Substring(0, 4) : byteArr[i].Substring(4, 4);
                        BitArray beforeInversionBitsArr = FromBinaryStringToBitArray(inversionPart);
                        BitArray afterInvertionBitsArr = beforeInversionBitsArr.Not();
                        string afterInversionStr = FromBitArrayToBinaryString(afterInvertionBitsArr);
                        if (i == 0)
                        {
                            _sb.Append(staticPart);
                            _sb.Append(afterInversionStr);
                        }
                        else
                        {
                            _sb.Append(afterInversionStr);
                            _sb.Append(staticPart);
                        }
                    }
                    else
                        _sb.Append(byteArr[i]);
                }

            }
            string result = _sb.ToString();
            _sb.Clear();
            return result;
        }

        private string DecryptMaximus(string str)
        {

            foreach (var pairSymbolsBinary in GetSliced(str, 64))
            {
                if (pairSymbolsBinary.Length == 32)
                {
                    _sb.Append(pairSymbolsBinary);
                    break;
                }

                _sb.Append(ReplaceBits(pairSymbolsBinary, false));



            }
            string result = _sb.ToString();
            _sb.Clear();
            return result;
        }
        private string DecryptEpicurus(string str)
        {

            foreach (var oneBinaryChar in GetSliced(str, 32))
            {
                int oneBinaryInt = Convert.ToInt32(oneBinaryChar, 2);
                int binary = oneBinaryInt ^ _key;
                string binaryXorStr = Convert.ToString(binary, 2);
                binaryXorStr = EnlargeBinary(binaryXorStr, true);
                _sb.Append(binaryXorStr);


            }
            string result = _sb.ToString();
            _sb.Clear();
            return result;
        }
        private string DecryptSchopenhauer(string str)
        {
            var itemCollection = new List<string>();
            foreach (var item in GetSliced(str, str.Length / 8))
            {
                itemCollection.Add(item);
            }
            _sb.Append(itemCollection[2]);
            _sb.Append(itemCollection[5]);
            _sb.Append(itemCollection[0]);
            _sb.Append(itemCollection[3]);
            _sb.Append(itemCollection[7]);
            _sb.Append(itemCollection[6]);
            _sb.Append(itemCollection[4]);
            _sb.Append(itemCollection[1]);

            string result = _sb.ToString();
            _sb.Clear();

            return result;
        }
        private string DecryptBismark(string str)
        {

            foreach (var c in str)
            {
                int binaryNum = (int)c;
                string binaryChar = Convert.ToString(binaryNum, 2);
                binaryChar = EnlargeBinary(binaryChar, false);
                _sb.Append(binaryChar);
            }
            string result = _sb.ToString();
            _sb.Clear();

            return result;
        }
        private string DecryptFunc(string str)
        {
            if (str.Length == 0) return "";
            string result = str;
            result = DecryptComod(result);
            result = DecryptSeneca(result);
            result = DecryptMakiavelli(result);
            result = DecryptBismark(result);
            result = DecryptSchopenhauer(result);
            result = DecryptEpicurus(result);
            result = DecryptMaximus(result);
            result = DecryptEpictetus(result);
            result = DecryptCicero(result);
            result = DecryptAvreliy(result);
            result = DecryptComod(result);
            result = DecryptSeneca(result);
            result = DecryptMakiavelli(result);
            result = DecryptCeasar(result);


            return result;
        }
        public string Decrypt(string str, string key)
        {
            _isStrKey = true;
            _sb = new StringBuilder(Math.Abs(str.Length * 3));
            _key = TransformstionStrKeyToInt(key);
            return DecryptFunc(str);

        }

        public string Decrypt(string str, int key)
        {
            _isStrKey = false;
            _sb = new StringBuilder(Math.Abs(str.Length) * 3);
            _key = (key == 0) ? TransformstionStrKeyToInt(_keyForZero) : key;

            return DecryptFunc(str);
        }
    }
}
