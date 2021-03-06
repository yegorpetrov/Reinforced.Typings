﻿using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Reinforced.Typings.Attributes;

namespace Reinforced.Typings.Generators
{
    /// <summary>
    ///     Various extensions for settings
    /// </summary>
    public static class ContextExtensions
    {

        /// <summary>
        ///     Conditionally (based on settings) turns method name to camelCase
        /// </summary>
        /// <param name="context">Settings object</param>
        /// <param name="regularName">Regular method name</param>
        /// <returns>Method name in camelCase if camelCasing enabled, initial string otherwise</returns>
        public static string ConditionallyConvertMethodNameToCamelCase(this ExportContext context, string regularName)
        {
            if (!context.Global.CamelCaseForMethods) return regularName;
            return ConvertToCamelCase(regularName);
        }

        /// <summary>
        ///     Conditionally (based on settings) turns method name to PascalCase
        /// </summary>
        /// <param name="context">Settings object</param>
        /// <param name="regularName">Regular method name</param>
        /// <returns>Method name in camelCase if camelCasing enabled, initial string otherwise</returns>
        public static string ConditionallyConvertMethodNameToPascalCase(this ExportContext context, string regularName)
        {
            if (!context.Global.CamelCaseForMethods) return regularName;
            return ConvertToCamelCase(regularName);
        }

        /// <summary>
        ///     Conditionally (based on settings) turns property name to camelCase
        /// </summary>
        /// <param name="context">Settings object</param>
        /// <param name="regularName">Regular property name</param>
        /// <returns>Property name in camelCase if camelCasing enabled, initial string otherwise</returns>
        public static string ConditionallyConvertPropertyNameToCamelCase(this ExportContext context,
            string regularName)
        {
            if (!context.Global.CamelCaseForProperties) return regularName;
            return ConvertToCamelCase(regularName);
        }

        /// <summary>
        ///     Conditionally (based on attribute setting) turns member name to camelCase
        /// </summary>
        /// <param name="member">Member</param>
        /// <param name="regularName">Regular property name</param>
        /// <returns>Property name in camelCase if camelCasing enabled, initial string otherwise</returns>
        public static string CamelCaseFromAttribute(this MemberInfo member, string regularName)
        {
            var attr = ConfigurationRepository.Instance.ForMember<TsTypedMemberAttributeBase>(member);
            if (attr == null) return regularName;
            if (attr.ShouldBeCamelCased) return ConvertToCamelCase(regularName);
            return regularName;
        }

        /// <summary>
        ///     Conditionally (based on attribute setting) turns member name to PascalCase
        /// </summary>
        /// <param name="member">Member</param>
        /// <param name="regularName">Regular property name</param>
        /// <returns>Property name in PascalCase if pascalCasing enabled, initial string otherwise</returns>
        public static string PascalCaseFromAttribute(this MemberInfo member, string regularName)
        {
            var attr = ConfigurationRepository.Instance.ForMember<TsTypedMemberAttributeBase>(member);
            if (attr == null) return regularName;
            if (attr.ShouldBePascalCased) return ConvertToPascalCase(regularName);
            return regularName;
        }

        private static string ConvertToCamelCase(string s)
        {
            if (!char.IsLetter(s[0])) return s;
            StringBuilder result = new StringBuilder();
            int i;
            for (i = 0; i < s.Length; i++)
            {
                if (i < s.Length - 1 && char.IsLower(s[i + 1])) break;
                if (char.IsUpper(s[i])) result.Append(char.ToLowerInvariant(s[i]));
            }

            if (i < s.Length - 1)
            {
                if (i == 0)
                {
                    result.Append(char.ToLowerInvariant(s[0]));
                    result.Append(s.Substring(1));
                }
                else
                {
                    result.Append(s.Substring(i));
                }
            }
            return result.ToString();
        }

        private static string ConvertToPascalCase(string s)
        {
            if (!char.IsLetter(s[0])) return s;
            if (char.IsLower(s[0]))
            {
                return char.ToUpper(s[0]) + s.Substring(1);
            }
            return s;
        }
    }
}