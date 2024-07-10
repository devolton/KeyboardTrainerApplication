using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Encrypter
{
    public sealed partial class DevoltonEncrypter
    {
        //метод генерации делителя в зависимости от ключа 
        private int GetDeliveder()
        {
            var sb = new StringBuilder(32);
            string keyStr = Convert.ToString(_Key, 2);
            string resultStr;
            int result;
            keyStr = EnlargeBinary(keyStr, true);
            sb.Append(keyStr.Substring(25, 5));
            sb.Append(keyStr[keyStr.Length - 1]);
            resultStr=EnlargeBinary(sb.ToString(),true);
            result = Convert.ToInt32(resultStr, 2);
            return result;
        }


        //метод трансформации String ключа в Int
        private int TransformstionStrKeyToInt(string keyStr)
        {
            if (keyStr == "") return 2;
            int booferNumber = 0;
            for (int i = 0; i < keyStr.Length - 1; i += 2)
            {
                if (i == keyStr.Length - 1) break;
                var firstBitStr = Convert.ToString((int)keyStr[i], 2);
                var secondBitStr = Convert.ToString((int)keyStr[i + 1], 2);
                firstBitStr = EnlargeBinary(firstBitStr, false);
                secondBitStr = EnlargeBinary(secondBitStr, false);
                string concatStr = firstBitStr + secondBitStr;
                concatStr = EnlargeBinary(concatStr, true);
                var currentNum = Convert.ToInt32(concatStr, 2);
                if (booferNumber != 0)
                    booferNumber ^= currentNum;
                else
                    booferNumber = currentNum;


            }
            return booferNumber;

        }
        private BitArray XorBitArrayInit(bool isSecondBlock)
        {

            return (isSecondBlock) ? new BitArray(new bool[] { true, true, true, true, true, true, true, true }) :
                new BitArray(new bool[] { true, false, true, false, true, false, true, false });
        }

        private Dictionary<int, int> BitPermutationIndexDictInit()
        {
            Dictionary<int, int> dict = new Dictionary<int, int>(30)
            {
                //0 and 31 NO 
                //NewIndex,OldIndex
                {1,24},{2,21},{3,18},{4,17},{5,8},{6,28},{7,20},{8,5},{9,10},{10,9},{11,30},{12,15},{13,25},{14,29},{15,12},{16,22},{17,4},{18,3},{ 19,23},{20,7}
                ,{21,2},{22,16},{23,19},{24,1},{25,13},{26,27},{27,26},{28,6},{29,14},{30,11}


            };
            return dict;
        }

        private Dictionary<int, int> TwoCharsBitPermutationIndexDictInit()
        {
            Dictionary<int, int> dict = new Dictionary<int, int>(30)
            {
                //написать таблицу смены битов
                {25,57},{57,25},{34,10},{10,34},{14,37},{37,14},{30,50},{50,30},{60,8},{8,60},{4,29},{29,4},{21,22 },{22,21},
                {44,17},{17,44},{32,2},{2,32}, {15,27},{27,15},{52,6},{6,52},{61,5},{5,61},{41,33},{33,41},{48,12},{12,48},
                {55,19 },{19,55}
            };
            return dict;
        }

        //метод перестановки битов
        private string ReplaceBits(string str, bool isEncrypt)
        {
            var sb = new StringBuilder(str.Length);
            Dictionary<int, int> bitPermutationIndexDict = (str.Length == 32) ? _oneBytePermutationIndexDict : _twoBytePermutationIndexDict;
            if (str.Length == 0) return str;
            for (int i = 0; i < str.Length; i++)
            {
                if (isEncrypt)
                {

                    if (bitPermutationIndexDict.TryGetValue(i, out int index))
                    {
                        sb.Append(str[index]);
                    }
                    else
                        sb.Append(str[i]);
                }
                else
                {
                    if (bitPermutationIndexDict.Any(item => item.Value.Equals(i)))
                    {
                        var pair = bitPermutationIndexDict.First(item => item.Value.Equals(i));
                        sb.Append(str[pair.Key]);
                    }
                    else
                        sb.Append(str[i]);

                }
            }


            return sb.ToString();
        }

        //метод конвертации строки в BitArray
        private BitArray FromBinaryStringToBitArray(string str)
        {
            if (string.IsNullOrEmpty(str)) throw new ArgumentException("Incorrect operation!", nameof(str));


            char[] charArr = str.ToCharArray();
            BitArray bitArray = new BitArray(charArr.Length);

            for (int i = 0; i < charArr.Length; i++)
            {
                if (charArr[i] == '1')
                    bitArray[i] = true;

                else if (charArr[i] == '0')
                    bitArray[i] = false;
                else
                    throw new ArgumentException("Incorrect char value!", nameof(str));

            }

            return bitArray;
        }

        //метод конвертации BitArray в строку
        private string FromBitArrayToBinaryString(BitArray bitArray)
        {
            if (bitArray == null) throw new ArgumentException("Incorrect operation", nameof(bitArray));
            StringBuilder sb = new StringBuilder(bitArray.Length);
            for (int i = 0; i < bitArray.Length; i++)
            {
                sb.Append((bitArray[i]) ? '1' : '0');
            }
            return sb.ToString();
        }


        //метод разбивания символа в байты
        private string[] GetBrokenIntPerByte(string str, bool isBytePair)
        {
            int bytesLength = (isBytePair) ? 8 : 4;
            string[] bytes = new string[bytesLength];
            int bytesIndex = 0;
            for (int i = 0; i < str.Length; i += 8)
            {
                bytes[bytesIndex] = str.Substring(i, 8);
                bytesIndex++;
            }
            return bytes;
        }
        //метод удленнения бинарной строки до нужной длинны
        private string EnlargeBinary(string binary, bool isEnlargeToByte)
        {
            int length = (isEnlargeToByte) ? 32 : 8;
            if (binary.Length > length) throw new ArgumentException("Incorrect param!", nameof(length));
            foreach (char ch in binary)
            {
                if (ch != '0' && ch != '1') throw new ArgumentException("Incorrect param!", nameof(binary));
            }
            StringBuilder sb = new StringBuilder(binary);
            for (int i = 0; i < length - binary.Length; i++)
            {
                sb.Insert(0, '0');
            }
            return sb.ToString();

        }

        //метод разбивания строки на части
        private IEnumerable<string> GetSliced(string str, int size)
        {
            if (str.Length < size || size == 0)
            {
                yield return str;
                yield break;
            }
            if (size < 0) size = Math.Abs(size) / 2;
            int slicesCount = str.Length / size;
            int lastSliceStartIndex = slicesCount * size;
            for (int i = 0; i < size * slicesCount; i += size)
            {
                yield return str.Substring(i, size);
            }
            if (lastSliceStartIndex < str.Length)
            {
                yield return str.Substring(lastSliceStartIndex, str.Length - lastSliceStartIndex);
            }
        }

        //метод конвертации char в Int
        private IEnumerable<int> GetIntCharValue(string str)
        {
            foreach (char ch in str)
            {
                yield return (int)ch;
            }
        }




        //метод поиска наиболее чаще всречаемого символа
        private char GetTheMostFrequentlyOccurringCharacter(string str)
        {
            var charCount = str.Where(symbol => symbol != ' ').GroupBy(symbol => symbol).ToDictionary(symbol => symbol.Key, symbol => symbol.Count());
            return charCount.OrderByDescending(symbol => symbol.Value).First().Key;


        }
        // метоод поиска наименне чаще встречаемого символа 
        private char GetLeastFrequentlyOccurringCharacter(string str)
        {
            var charCount = str.Where(symbol => symbol != ' ').GroupBy(symbol => symbol).ToDictionary(symbol => symbol.Key, symbol => symbol.Count());
            return charCount.OrderByDescending(symbol => symbol.Value).Last().Key;

        }
    }
}
