using System;
using System.IO;
using System.Text;

namespace Publish.Common
{
    class FileHelper
    {
        private static object objWrite = true;

        /// <summary>
        /// Tạo mơi file rỗng
        /// </summary>
        /// <param name="filePath"></param>
        public static void CreateEmptyFile(string filePath)
        {
            lock (objWrite)
            {
                if (!File.Exists(filePath))
                {
                    using (var strLocal = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                        strLocal.Close();
                }
                else File.WriteAllText(filePath, string.Empty);
            }
        }

        /// <summary>
        /// Tạo file lộ trình
        /// </summary>
        public static void CreateTrackingFile(string filePath)
        {
            lock (objWrite)
            {
                if (!File.Exists(filePath))
                {
                    using (var strLocal = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                        strLocal.Close();
                }
            }
        }

        /// <summary>
        /// ghi một dòng dữ liệu vào file
        /// </summary>
        /// <param name="pathWrite"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool WriteFile(string pathWrite, string content)
        {
            bool result = false;
            lock (objWrite)
            {
                if (!File.Exists(pathWrite))
                {
                    var strLocal = new FileStream(pathWrite, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                    strLocal.Close();
                }

                var streamw = new StreamWriter(pathWrite, true, Encoding.GetEncoding("utf-8"));
                try
                {
                    streamw.WriteLine(content);
                    result = true;
                }
                catch
                {
                    result = false;
                }
                finally
                {
                    streamw.Close();
                }

                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathWrite"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool WriteFile2(string pathWrite, string content)
        {
            if (string.IsNullOrEmpty(content)) return true;

            bool result = false;
            lock (objWrite)
            {
                var streamw = new StreamWriter(pathWrite, true, Encoding.GetEncoding("utf-8"));
                try
                {
                    streamw.Write(content);
                    result = true;
                }
                catch
                {
                    result = false;
                }
                finally
                {
                    streamw.Close();
                }

                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathWrite"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool WriteTrackingNew(string pathWrite, string content)
        {
            if (string.IsNullOrEmpty(content)) return true;

            bool result = false;

            lock (objWrite)
            {
                var streamw = new StreamWriter(pathWrite, true, Encoding.GetEncoding("utf-8"));
                try
                {
                    streamw.Write(content);
                    result = true;
                }
                catch
                {
                    result = false;
                }
                finally
                {
                    streamw.Close();
                }
                return result;
            }
        }

        /// <summary>
        /// Ghi Log
        /// </summary>
        /// <param name="log"></param>
        /// <param name="ex"></param>
        public static void WriteLog(string log, string prefix = "Logs")
        {
            DateTime time = DateTime.Now;
            var path = Path.Combine(string.Format(@"{1}\{0:dd-MM-yyyy}\", time, prefix));
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            var file = path + time.ToString("HH-mm") + ".txt";
            WriteFile(file, log);
        }

        /// <summary>
        /// Ghi Log
        /// </summary>
        /// <param name="log"></param>
        /// <param name="ex"></param>
        public static void WriteLog(string log, Exception ex)
        {
            DateTime time = DateTime.Now;
            var path = Path.Combine(string.Format(@"Error\{0:dd-MM-yyyy}\", time));
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            var file = path + time.ToString("HH-mm") + ".txt";
            WriteFile(file, log + ": " + ex.Message + Environment.NewLine + ex.StackTrace + "----------------------------" + Environment.NewLine);
        }
        public static void GeneratorFileByDay(string types = "", string content = "")
        {
            DateTime time = DateTime.Now;
            var path = AppDomain.CurrentDomain.BaseDirectory + string.Format(@"{0}\{1}\{2}\{3}\", time.Year, time.Month, time.Day, types);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            var fullPath = path + time.Day + ".txt";
            WriteFile(fullPath, content);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="log"></param>
        /// <param name="ex"></param>
        public static void WriteLog2(string log, Exception ex)
        {
            DateTime time = DateTime.Now;
            var path = Path.Combine(string.Format(@"Error\{0:dd-MM-yyyy}\" + log + @"\", time));
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            var file = path + time.ToString("HH-mm") + ".txt";
            WriteFile(file, ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + "----------------------------" + Environment.NewLine);
        }

        /// <summary>
        /// Ghi Log
        /// </summary>
        public static void WriteLogTrip(string log, string prefix = "Logs")
        {
            DateTime time = DateTime.Now;
            var path = Path.Combine(string.Format(@"{1}\{0:dd-MM-yyyy}\", time, prefix));
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            var file = path + time.ToString("yyyy-MM-dd") + ".txt";
            WriteFile(file, log);
        }

        /// <summary>
        /// Ghi Log 1 file (Gianglt)
        /// </summary>
        public static void WriteLogOneFile(string log, string prefix = "Logs")
        {
            DateTime time = DateTime.Now;
            var path = Path.Combine(string.Format(@"{1}\{0:dd-MM-yyyy}\", time, prefix));
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            var file = path + "LogReader2014.txt";
            WriteFile(file, log);
        }

        /// <summary>
        /// Loại bỏ tiếng việt
        /// </summary>
        public static string Uni2KD2(string strKD)
        {
            string retVal = strKD;

            string[] arKD = {"a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                                                "a", "a", "a", "a", "a", "a", "e", "e", "e", "e", "e", "e", "e", "e",
                                                "e", "e", "e", "d", "i", "i", "i", "i", "i", "o", "o", "o", "o", "o", "o",
                                                "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "o", "u", "u", "u",
                                                "u", "u", "u", "u", "u", "u", "u", "u", "y", "y", "y", "y", "y",
                                                "A", "A", "A", "A", "A", "A", "A", "A", "A", "A", "A",
                                                "A", "A", "A", "A", "A", "A", "E", "E", "E", "E", "E", "E", "E", "E",
                                                "E", "E", "E", "D", "I", "I", "I", "I ", "I ", "O", "O", "O", "O", "O", "O",
                                                "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "O", "U", "U", "U",
                                                "U", "U", "U", "U", "U", "U", "U", "U", "Y", "Y", "Y", "Y", "Y"};

            string[] arUni = {"à", "á", "ả", "ã", "ạ", "ầ", "ấ", "ẩ", "ẫ", "ậ", "â",
                                                 "ằ", "ắ", "ẳ", "ẵ", "ặ", "ă", "è", "é", "ẻ", "ẽ", "ẹ", "ề", "ế", "ể",
                                                 "ễ", "ệ", "ê", "đ", "ì", "í", "ỉ", "ĩ", "ị", "ò", "ó", "ỏ", "õ", "ọ", "ồ",
                                                 "ố", "ổ", "ỗ", "ộ", "ô", "ờ", "ớ", "ở", "ỡ", "ợ", "ơ", "ù", "ú", "ủ",
                                                 "ũ", "ụ", "ừ", "ứ", "ử", "ữ", "ự", "ư", "ỳ", "ý", "ỷ", "ỹ", "ỵ",
                                                 "À", "Á", "Ả", "Ã", "Ạ", "Ầ", "Ấ", "Ẩ", "Ẫ", "Ậ", "Â",
                                                 "Ằ", "Ắ", "Ẳ", "Ẵ", "Ặ", "Ă", "È", "É", "Ẻ", "Ẽ", "Ẹ", "Ề", "Ế", "Ể",
                                                 "Ễ", "Ệ", "Ê", "Đ", "Ì", "Í", "Ỉ", "Ĩ ", "Ị ", "Ò", "Ó", "Ỏ", "Õ", "Ọ", "Ồ",
                                                 "Ố", "Ổ", "Ỗ", "Ộ", "Ô", "Ờ", "Ớ", "Ở", "Ỡ", "Ợ", "Ơ", "Ù", "Ú", "Ủ",
                                                 "Ũ", "Ụ", "Ừ", "Ứ", "Ử", "Ữ", "Ự", "Ư", "Ỳ", "Ý", "Ỷ", "Ỹ", "Ỵ"};

            for (int i = 0; i < arUni.Length; i++)
                retVal = retVal.Replace(arUni[i], arKD[i]);
            while (retVal.Contains("  "))
                retVal = retVal.Replace("  ", " ");

            retVal = retVal.Replace("_", "");
            return retVal;
        }
    }
}

