using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Container.Models
{
  public static  class Utils
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public static string GetDescription(this Enum value)
        {
            try
            {
                if (value == null)
                {
                    return string.Empty;
                }

                FieldInfo field = value.GetType().GetField(value.ToString());

                if (field == null)
                {
                    return string.Empty;
                }
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))
                                as DescriptionAttribute;

                return attribute == null ? value.ToString() : attribute.Description;
            }
            catch (Exception exp)
            {
                Log.Error(exp, exp.Message);
            }

            return string.Empty;
        }

        public static string GetEnumMember(this Enum value)
        {
            try
            {
                if (value == null)
                {
                    return string.Empty;
                }

                FieldInfo field = value.GetType().GetField(value.ToString());

                if (field == null)
                {
                    return string.Empty;
                }
                var attribute = Attribute.GetCustomAttribute(field, typeof(EnumMemberAttribute))
                    as EnumMemberAttribute;

                return attribute == null ? value.ToString() : attribute.Value;
            }
            catch (Exception exp)
            {
                Log.Error(exp, exp.Message);
            }

            return string.Empty;
        }


        /// <summary>
        /// To string array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string ToStringArray<T>(this IEnumerable<T> array)
        {
            var result = new StringBuilder();
            Type typeParameterType = typeof(T);
            result.Append(string.Format("{0}: \n", typeParameterType.Name));

            foreach (var item in array)
            {
                result.Append(item).Append("\n");
            }

            return result.ToString();
        }



        /// <summary>
        /// Get Description attribute value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static T GetValueFromDescription<T>(this string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
        }
    }
}
