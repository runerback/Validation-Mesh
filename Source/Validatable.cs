using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Allsworth.TCMS.Client
{
	public static class Validatable
	{
		public static IValidatable<TSource> AsValidatable<TSource>(this IValidatable<TSource> validatable)
		{
			if (validatable == null)
				throw new ArgumentNullException("validatable");
			return validatable;
		}

		public static IValidatable<TSource> AddRule<TSource, TElement>(this IValidatable<TSource> validatable, Expression<Func<TSource, TElement>> keySelector, Func<TElement, bool> rule, string errorMessage = null)
		{
			if (validatable == null)
				throw new ArgumentNullException("validatable");
			if (validatable.Validator == null)
				throw new ArgumentNullException("validatable.Validator");
			if (rule == null)
				throw new ArgumentNullException("rule");

			MemberExpression keySelectorExp = keySelector.Body as MemberExpression;
			if (keySelectorExp.Member.MemberType != System.Reflection.MemberTypes.Property)
				throw new NotSupportedException("only support property");
			string propertyName = keySelectorExp.Member.Name;

			var selector = keySelector.Compile();

			validatable.Validator.AddRule(
				propertyName,
				() => rule(selector(validatable.Source)),
				errorMessage);

			return validatable;
		}

		public static IValidatable<TSource> Required<TSource, TElement>(this IValidatable<TSource> validatable, Expression<Func<TSource, TElement>> keySelector, string errorMessage = "required")
		{
			return AddRule(
				validatable,
				keySelector,
				ValidationRules.Required<TElement>(),
				errorMessage);
		}

		public static IValidatable<TSource> StringLength<TSource, TElement>(this IValidatable<TSource> validatable, Expression<Func<TSource, TElement>> keySelector, int maxLength, string errorMessage = "too long")
		{
			return AddRule(
				validatable,
				keySelector,
				ValidationRules.StringLength<TElement>(maxLength),
				errorMessage);
		}

		public static IValidatable<TSource> StringLength<TSource, TElement>(this IValidatable<TSource> validatable, Expression<Func<TSource, TElement>> keySelector, int maxLength, int minLength, string errorMessage = "invalid string length")
		{
			return AddRule(
				validatable,
				keySelector,
				ValidationRules.StringLength<TElement>(maxLength, minLength),
				errorMessage);
		}

		#region Is

		public static IValidatable<TSource> IsInt32<TSource, TElement>(this IValidatable<TSource> validatable, Expression<Func<TSource, TElement>> keySelector, string errorMessage = "invalid numeric value")
		{
			return AddRule(
				validatable,
				keySelector,
				ValidationRules.IsInt32<TElement>(),
				errorMessage);
		}

		public static IValidatable<TSource> IsDouble<TSource, TElement>(this IValidatable<TSource> validatable, Expression<Func<TSource, TElement>> keySelector, string errorMessage = "invalid numeric value")
		{
			return AddRule(
				validatable,
				keySelector,
				ValidationRules.IsDouble<TElement>(),
				errorMessage);
		}

		#endregion Is

		
	}
}
