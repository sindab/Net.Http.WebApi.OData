// -----------------------------------------------------------------------
// <copyright file="ConstantNodeParser.cs" company="Project Contributors">
// Copyright 2012 - 2017 Project Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// </copyright>
// -----------------------------------------------------------------------
namespace Net.Http.WebApi.OData.Query.Parsers
{
    using System;
    using System.Globalization;
    using Expressions;
    using Model;

    internal static class ConstantNodeParser
    {
        private const string ODataDateFormat = "yyyy-MM-dd";

        internal static ConstantNode ParseConstantNode(Token token)
        {
            switch (token.TokenType)
            {
                case TokenType.Date:
                    var dateTimeValue = DateTime.ParseExact(token.Value, ODataDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
                    return ConstantNode.Date(token.Value, dateTimeValue);

                case TokenType.DateTimeOffset:
                    var dateTimeOffsetValue = DateTimeOffset.Parse(token.Value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
                    return ConstantNode.DateTimeOffset(token.Value, dateTimeOffsetValue);

                case TokenType.Decimal:
                    var decimalText = token.Value.Substring(0, token.Value.Length - 1);
                    var decimalValue = decimal.Parse(decimalText, CultureInfo.InvariantCulture);
                    return ConstantNode.Decimal(token.Value, decimalValue);

                case TokenType.Double:
                    var doubleText = token.Value.EndsWith("d", StringComparison.OrdinalIgnoreCase)
                        ? token.Value.Substring(0, token.Value.Length - 1)
                        : token.Value;
                    var doubleValue = double.Parse(doubleText, CultureInfo.InvariantCulture);
                    return ConstantNode.Double(token.Value, doubleValue);

                case TokenType.Duration:
                    var durationText = token.Value.Substring(9, token.Value.Length - 10)
                        .Replace("P", string.Empty)
                        .Replace("DT", ".")
                        .Replace("H", ":")
                        .Replace("M", ":")
                        .Replace("S", string.Empty);
                    var durationTimeSpanValue = TimeSpan.Parse(durationText, CultureInfo.InvariantCulture);
                    return ConstantNode.Duration(token.Value, durationTimeSpanValue);

                case TokenType.Enum:
                    var firstQuote = token.Value.IndexOf('\'');
                    var edmEnumTypeName = token.Value.Substring(0, firstQuote);
                    var edmEnumType = (EdmEnumType)EdmType.GetEdmType(edmEnumTypeName);
                    var edmEnumMemberName = token.Value.Substring(firstQuote + 1, token.Value.Length - firstQuote - 2);
                    var enumValue = edmEnumType.GetClrValue(edmEnumMemberName);

                    return new ConstantNode(edmEnumType, token.Value, enumValue);

                case TokenType.False:
                    return ConstantNode.False;

                case TokenType.Guid:
                    var guidValue = Guid.ParseExact(token.Value, "D");
                    return ConstantNode.Guid(token.Value, guidValue);

                case TokenType.Int32:
                    var int32Text = token.Value;
                    if (int32Text == "0")
                    {
                        return ConstantNode.Int32Zero;
                    }

                    var int32Value = int.Parse(int32Text, CultureInfo.InvariantCulture);
                    return ConstantNode.Int32(token.Value, int32Value);

                case TokenType.Int64:
                    if (token.Value == "0l" || token.Value == "0L")
                    {
                        return ConstantNode.Int64Zero;
                    }

                    var int64Text = token.Value.Substring(0, token.Value.Length - 1);
                    var int64Value = long.Parse(int64Text, CultureInfo.InvariantCulture);
                    return ConstantNode.Int64(token.Value, int64Value);

                case TokenType.Null:
                    return ConstantNode.Null;

                case TokenType.Single:
                    var singleText = token.Value.Substring(0, token.Value.Length - 1);
                    var singleValue = float.Parse(singleText, CultureInfo.InvariantCulture);
                    return ConstantNode.Single(token.Value, singleValue);

                case TokenType.String:
                    var stringText = token.Value.Trim('\'').Replace("''", "'");
                    return ConstantNode.String(token.Value, stringText);

                case TokenType.TimeOfDay:
                    var timeSpanTimeOfDayValue = TimeSpan.Parse(token.Value, CultureInfo.InvariantCulture);
                    return ConstantNode.Time(token.Value, timeSpanTimeOfDayValue);

                case TokenType.True:
                    return ConstantNode.True;

                default:
                    throw new NotSupportedException(token.TokenType.ToString());
            }
        }
    }
}