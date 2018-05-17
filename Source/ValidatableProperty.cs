using System;
using System.Linq.Expressions;

namespace ValidationExtention
{
	public static class ValidatableProperty
	{
		public static IValidatableProperty<TSource, TKey> Select<TSource, TKey>(this IValidatable<TSource> validatable, Expression<Func<TSource, TKey>> keySelector)
		{
			if (validatable == null)
				throw new ArgumentNullException("validatable");
			if (keySelector == null)
				throw new ArgumentNullException("keySelector");

			return new ValidatablePropertyAdapter<TSource, TKey>(validatable, keySelector);
		}

		#region AddRule

		public static IValidatableProperty<TSource, TKey> AddRule<TSource, TKey>(this IValidatableProperty<TSource, TKey> validatable, Func<TKey, bool> rule, string errorMessage = null)
		{
			if (validatable == null)
				throw new ArgumentNullException("validatable");
			var validator = validatable.SourceValidatable.Validator;
			if (validator == null)
				throw new ArgumentNullException("validator");
			validator.AddRule(
				validatable.PropertyName,
				() => rule(validatable.PropertySelector()),
				errorMessage);
			return validatable;
		}

		public static IValidatableProperty<TSource, TKey> Required<TSource, TKey>(this IValidatableProperty<TSource, TKey> validatable, string errorMessage = "required")
		{
			return AddRule(
				validatable,
				ValidationRules.Required<TKey>(),
				errorMessage);
		}

		public static IValidatableProperty<TSource, TKey> StringLength<TSource, TKey>(this IValidatableProperty<TSource, TKey> validatable, int maxLength, string errorMessage = "too long")
		{
			return AddRule(
				validatable,
				ValidationRules.StringLength<TKey>(maxLength),
				errorMessage);
		}

		public static IValidatableProperty<TSource, TKey> StringLength<TSource, TKey>(this IValidatableProperty<TSource, TKey> validatable, int maxLength, int minLength, string errorMessage = "invalid string length")
		{
			return AddRule(
				validatable,
				ValidationRules.StringLength<TKey>(maxLength, minLength),
				errorMessage);
		}

		public static IValidatableProperty<TSource, TKey> IsInt32<TSource, TKey>(this IValidatableProperty<TSource, TKey> validatable, string errorMessage = "invalid numeric value")
		{
			return AddRule(
				validatable,
				ValidationRules.IsInt32<TKey>(),
				errorMessage);
		}

		public static IValidatableProperty<TSource, TKey> IsDouble<TSource, TKey>(this IValidatableProperty<TSource, TKey> validatable, string errorMessage = "invalid numeric value")
		{
			return AddRule(
				validatable,
				ValidationRules.IsDouble<TKey>(),
				errorMessage);
		}

		#endregion AddRule
	}
}
