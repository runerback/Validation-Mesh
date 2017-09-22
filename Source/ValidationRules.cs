using System;
using System.Collections.Generic;

namespace ValidationExtention
{
	internal static class ValidationRules
	{
		public static Func<T, bool> Required<T>()
		{
			if (typeof(T) == typeof(string))
				return value => !string.IsNullOrEmpty(value as string);
			return value => !EqualityComparer<T>.Default.Equals(value, default(T));
		}

		public static Func<T, bool> StringLength<T>(int maxLength)
		{
			if (typeof(T) != typeof(string))
				throw new ArgumentException("T must be string type");
			return value =>
			{
				string strValue = value as string;
				if (string.IsNullOrEmpty(strValue))
					return true;
				return strValue.Length <= maxLength;
			};
		}

		public static Func<T, bool> StringLength<T>(int maxLength, int minLength)
		{
			if (typeof(T) != typeof(string))
				throw new ArgumentException("T must be string type");
			if (minLength < 0)
				throw new ArgumentException("minLength. cannot less than 0.");
			return value =>
			{
				string strValue = value as string;
				if (string.IsNullOrEmpty(strValue))
					return true;
				return strValue.Length >= minLength && 
					strValue.Length <= maxLength;
			};
		}

		public static Func<T, bool> IsInt32<T>()
		{
			if (typeof(T) != typeof(string))
				throw new ArgumentException("T must be string type");
			return value =>
			{
				int result;
				return int.TryParse(value as string, out result);
			};
		}

		public static Func<T, bool> IsDouble<T>()
		{
			if (typeof(T) != typeof(string))
				throw new ArgumentException("T must be string type");
			return value =>
			{
				double result;
				return double.TryParse(value as string, out result);
			};
		}
	}
}
