using System;
using System.Linq.Expressions;

namespace ValidationExtention
{
	public static class ValidatableProperty
	{
		public static IValidatableProperty<TSource, TElement> Select<TSource, TElement>(this IValidatable<TSource> validatable, Expression<Func<TSource, TElement>> keySelector)
		{
			if (validatable == null)
				throw new ArgumentNullException("validatable");
			if (keySelector == null)
				throw new ArgumentNullException("keySelector");

			return new ValidatablePropertyAdapter<TSource, TElement>(validatable, keySelector);
		}

		public static IValidatableProperty<TSource, TElement> AddRule<TSource, TElement>(this IValidatableProperty<TSource, TElement> validatable, Func<TElement, bool> rule, string errorMessage = null)
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

		public static IValidatableProperty<TSource, TElement> Required<TSource, TElement>(this IValidatableProperty<TSource, TElement> validatable, string errorMessage = "required")
		{
			return AddRule(
				validatable,
				ValidationRules.Required<TElement>(),
				errorMessage);
		}

		public static IValidatableProperty<TSource, TElement> StringLength<TSource, TElement>(this IValidatableProperty<TSource, TElement> validatable, int maxLength, string errorMessage = "too long")
		{
			return AddRule(
				validatable,
				ValidationRules.StringLength<TElement>(maxLength),
				errorMessage);
		}

		public static IValidatableProperty<TSource, TElement> StringLength<TSource, TElement>(this IValidatableProperty<TSource, TElement> validatable, int maxLength, int minLength, string errorMessage = "invalid string length")
		{
			return AddRule(
				validatable,
				ValidationRules.StringLength<TElement>(maxLength, minLength),
				errorMessage);
		}

		public static IValidatableProperty<TSource, TElement> IsInt32<TSource, TElement>(this IValidatableProperty<TSource, TElement> validatable, string errorMessage = "invalid numeric value")
		{
			return AddRule(
				validatable,
				ValidationRules.IsInt32<TElement>(),
				errorMessage);
		}

		public static IValidatableProperty<TSource, TElement> IsDouble<TSource, TElement>(this IValidatableProperty<TSource, TElement> validatable, string errorMessage = "invalid numeric value")
		{
			return AddRule(
				validatable,
				ValidationRules.IsDouble<TElement>(),
				errorMessage);
		}
	}
}
