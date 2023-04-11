using NLog;
using System;
using System.ComponentModel;
using System.Data.SqlClient;

namespace DB
{
    public static partial class MSDB
    {
        /// <summary>
        /// The connection string
        /// </summary>
        private static string _ConnectionString;
        // ReSharper disable InconsistentNaming
        /// <summary>
        /// The log
        /// </summary>
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes the specified host name.
        /// </summary>
        /// <param name="hostName">Name of the host.</param>
        /// <param name="port">The port.</param>
        /// <param name="user">The user.</param>
        /// <param name="pass">The pass.</param>
        /// <param name="dbase">The dbase.</param>
        /// <param name="perfCounter">if set to <c>true</c> [perf counter].</param>
        public static void Init(string hostName,
                                string dbase,
                                bool perfCounter = false)
        {
            if (_ConnectionString == null)
            {

#if DEBUG
                _ConnectionString = $"Data Source={hostName};Initial Catalog={dbase};Integrated Security=True;"
#else
                _ConnectionString = $"Server={hostName};Database={dbase};User Id=;Password=;"
#endif
                ;

            }

            Log.Debug(_ConnectionString);
        }





        /// <summary>
        /// Checks the connection.
        /// </summary>
        /// <returns></returns>
        public static bool CheckConnection()
        {
            SqlConnection connection = null;
            try
            {
                connection = GetConnection();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT TOP 1 1 TradeMarks";
                    cmd.ExecuteScalar();
                }

                return true;
            }
            catch (Exception e)
            {
                Log.Error(e, e.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">Connection not initialized!</exception>
        private static SqlConnection GetConnection()
        {
            if (string.IsNullOrEmpty(_ConnectionString))
            {
                throw new Exception("Connection not initialized!");
            }

            var connection = new SqlConnection(_ConnectionString);
            connection.Open();

            return connection;
        }

        /// <summary>
        /// Gets the int.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static int GetInt(object value)
        {
            return value != DBNull.Value ? Convert.ToInt32(value) : 0;
        }

        /// <summary>
        /// Gets the long.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static long GetLong(object value)
        {
            return value != DBNull.Value ? Convert.ToInt64(value) : 0;
        }

        /// <summary>
        /// Gets the decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static decimal GetDecimal(object value)
        {
            return value != DBNull.Value ? Convert.ToDecimal(value) : 0M;
        }

        /// <summary>
        /// Gets the bool.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static bool GetBool(object value)
        {
            return value != DBNull.Value && Convert.ToInt32(value) != 0;
        }

        /// <summary>
        /// Gets the BLOB.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static byte[] GetBlob(object value)
        {
            return value != DBNull.Value ? (byte[])value : null;
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <returns></returns>
        private static string GetString(object order)
        {
            return order != DBNull.Value ? Convert.ToString(order) : null;
        }

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <returns></returns>
        private static DateTime? GetDateTime(object order)
        {
            return order != DBNull.Value ? (DateTime?)Convert.ToDateTime(order) : null;
        }

        /// <summary>
        /// Gets the clob.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private static string GetClob(object value)
        {
            return value != DBNull.Value ? value.ToString() : string.Empty;
        }

        /// <summary>
        /// Descriptions the attribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        private static string DescriptionAttr<T>(this T source)
        {
            var fi = source.GetType().GetField(source.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;

            return source.ToString();
        }

        /// <summary>
        /// Gets the date time.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        private static DateTime GetDateTime<T>(this T source) where T : class
        {
            return source != DBNull.Value ? Convert.ToDateTime(source) : DateTime.MinValue;
        }

        private static decimal GetDecimal<T>(this T source) where T : class
        {
            return source != DBNull.Value ? Convert.ToDecimal(source) : 0M;
        }

        /// <summary>
        /// Gets the int.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        private static int GetInt<T>(this T source) where T : class
        {
            return source != DBNull.Value ? Convert.ToInt32(source) : 0;
        }

        /// <summary>
        /// Gets the long.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        private static long GetLong<T>(this T source) where T : class
        {
            return source != DBNull.Value ? Convert.ToInt64(source) : 0L;
        }

        /// <summary>
        /// Gets the string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        private static string GetString<T>(this T source) where T : class
        {
            return source != DBNull.Value ? Convert.ToString(source) : string.Empty;
        }

        /// <summary>
        /// Gets the bool.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        private static bool GetBool<T>(this T source) where T : class
        {
            return source != DBNull.Value && Convert.ToInt32(source) != 0;
        }
    }
}
