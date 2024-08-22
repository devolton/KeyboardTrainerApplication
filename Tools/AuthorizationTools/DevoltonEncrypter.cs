using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Encrypter
{
    /// <summary>
    /// This is Devolton custom Dectypter
    /// </summary>
    public class DevoltonEncrypter:BaseDevoltonEncrypter
    {
        
        private DevoltonEncrypter():base()
        {
            

        }
        private static DevoltonEncrypter _instance { get; set; }
        public static DevoltonEncrypter Instance()
        {
            _instance ??= new DevoltonEncrypter();
            return _instance;
        }




        //CeaserBlock - разбивает строку на части в зависимости от ключа и в каждом втором блоке смещает значение символа по ASCII таблице
        private string EncryptCeasar(string str)
        {
            List<string> strItems = new List<string>(100);
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
                        _sb.Append((char)(oneStr[i] + _key));

                    else
                        _sb.Append((char)(oneStr[i] - _key));
                }

            }
            string result = _sb.ToString();
            _sb.Clear();
            return result;
        }



        //EncryptMakiavelli - перестановка символов по индексу в зависимости от ключа
        private string EncryptMakiavelli(string str)
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

      

        //EncryptSeneca - разрезаем строку на части в зависимости от суммы количества символов который встречаются чаще и реже всех,
        // каждую из частей режем еще на две части и меняем местами
        private string EncryptSeneca(string str)
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

       




        //EncryptComod - разбиваем на части в зависимости от ключа и каждую врорую часть меняем задом-наперед

        private string EncryptComod(string str)
        {
            var sliceSize = (_isStrKey) ? GetDeliveder() : GetDeliveder()+1;
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

       


        //EncryptAvreliy - переводим каждый символ в 32битовое представление и разбиваем на байты. Второй и третий байт XOR 

        private string EncryptAvreliy(string str)
        {


            string[] byteArr;
            foreach (var c in GetIntCharValue(str))
            {
                byteArr = GetBrokenIntPerByte(EnlargeBinary(Convert.ToString(c, 2), true), false);
                BitArray secondBlock = FromBinaryStringToBitArray(byteArr[1]);
                BitArray thirdBlock = FromBinaryStringToBitArray(byteArr[2]);
                var xorSecondResult = secondBlock.Xor(_xorBitArrayForSecondBlock);
                var xorThirdResult = thirdBlock.Xor(_xorBitArrayForThirdBlock);
                _sb.Append(byteArr[0]);
                _sb.Append(FromBitArrayToBinaryString(xorSecondResult));
                _sb.Append(FromBitArrayToBinaryString(xorThirdResult));
                _sb.Append(byteArr[3]);

            }

            string result = _sb.ToString();
            _sb.Clear();

            return result;
        }

    

        //EncryptCicero- переставляем биты в одном символе 

        private string EncryptCicero(string str)
        {


            foreach (var oneChar in GetSliced(str, 32))
            {
                _sb.Append(ReplaceBits(oneChar, true));

            }
            string result = _sb.ToString();
            _sb.Clear();
            return result;
        }


  


        //EncryptEpicutetus - в певром байте каждого символа инвертируем первые 4 бита и меняем местами с второй частью,
        //в третем байте инвертируем последние 4 бита и так же меняем местами с первой частью
        private string EncryptEpictetus(string str)
        {

            foreach (var oneCharBytes in GetSliced(str, 32))
            {
                var byteArr = GetBrokenIntPerByte(oneCharBytes, false); //4 блока по 1 байту
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




        //EncryptMaximus - меняем местами биты в двух рядом стоящих символах
        private string EncryptMaximus(string str)
        {

            foreach (var pairSymbolsBinary in GetSliced(str, 64))
            {
                if (pairSymbolsBinary.Length == 32)
                {

                    _sb.Append(pairSymbolsBinary);
                    break;
                }
                _sb.Append(ReplaceBits(pairSymbolsBinary, true));
            }
            string result = _sb.ToString();
            _sb.Clear();
            return result;

        }

       


        //EncryptEpicurus - XOR каждый символ в ключем
        private string EncryptEpicurus(string str)
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


     

        //EncryptSchoprnhauer- разбиваем строку битов на 8 частей и перемешиваем 
        private string EncryptSchopenhauer(string str)
        {

            var itemCollection = new List<string>();
            foreach (var item in GetSliced(str, str.Length / 8))
            {
                itemCollection.Add(item);
            }
            _sb.Append(itemCollection[2]);
            _sb.Append(itemCollection[7]);
            _sb.Append(itemCollection[0]);
            _sb.Append(itemCollection[3]);
            _sb.Append(itemCollection[6]);
            _sb.Append(itemCollection[1]);
            _sb.Append(itemCollection[5]);
            _sb.Append(itemCollection[4]);
            string result = _sb.ToString();
            _sb.Clear();
            return result;
        }



  

        //EncryptBimark - каждый из 4 байтов одного символа возвращаем в Char
        private string EncryptBismark(string str)
        {

            foreach (var oneByte in GetSliced(str, 8))
            {
                var charElement = (char)(Convert.ToInt32(oneByte, 2));
                _sb.Append(charElement);
            }


            string result = _sb.ToString();
            _sb.Clear();

            return result;

        }

   


        private string EncryptFunc(string str)
        {
            if (str.Length == 0) return "";
            string result = str;

            result = EncryptCeasar(result);
            result = EncryptMakiavelli(result);
            result = EncryptSeneca(result);
            result = EncryptComod(result);
            result = EncryptAvreliy(result);
            result = EncryptCicero(result);
            result = EncryptEpictetus(result);
            result = EncryptMaximus(result);
            result = EncryptEpicurus(result);
            result = EncryptSchopenhauer(result);
            result = EncryptBismark(result);
            result = EncryptMakiavelli(result);
            result = EncryptSeneca(result);
            result = EncryptComod(result);

            return result;
        }

        




        public string Encrypt(string str, int key)
        {
            _isStrKey = false;
            _sb = new StringBuilder(Math.Abs(str.Length) * 3);
            _key = (key == 0) ? TransformstionStrKeyToInt(_keyForZero) : key;

            return EncryptFunc(str);
        }

        public string Encrypt(string str, string key)
        {
            _isStrKey = true;
            _sb = new StringBuilder(Math.Abs(str.Length) * 3);
            _key = TransformstionStrKeyToInt(key);
            return EncryptFunc(str);

        }
       














    }

}
