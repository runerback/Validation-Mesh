using System;
using System.Linq.Expressions;

namespace ValidationExtention
{
	internal class ValidatablePropertyAdapter<TSource, TElement> : IValidatableProperty<TSource, TElement>
	{
		public ValidatablePropertyAdapter(IValidatable<TSource> sourceValidatable, Expression<Func<TSource, TElement>> keySelectorExpression)
		{
			if (sourceValidatable == null)
				throw new ArgumentNullException("sourceValidatable");
			if (sourceValidatable.Source == null)
				throw new ArgumentNullException("source");
			if (sourceValidatable.Validator == null)
				throw new ArgumentNullException("validator");
			if (keySelectorExpression == null)
				throw new ArgumentNullException("keySelectorExpression");

			MemberExpression keySelectorExpMember = keySelectorExpression.Body as MemberExpression;
			if (keySelectorExpMember.Member.MemberType != System.Reflection.MemberTypes.Property)
				throw new NotSupportedException("only support property");
			string propertyName = keySelectorExpMember.Member.Name;

			this.propertyName = propertyName;
			var keySelector = keySelectorExpression.Compile();
			this.propertySelector = () => keySelector(sourceValidatable.Source);
			this.sourceValidatable = sourceValidatable;
		}

		private IValidatable<TSource> sourceValidatable;
		public IValidatable<TSource> SourceValidatable
		{
			get { return sourceValidatable; }
		}

		private string propertyName;
		public string PropertyName
		{
			get { return propertyName; }
		}

		private Func<TElement> propertySelector;
		public Func<TElement> PropertySelector
		{
			get { return propertySelector; }
		}
	}
}
