using System;

namespace ValidationExtention
{
	public interface IValidatable<out TSource>
	{
		TSource Source { get; }
		IValidator Validator { get; }
	}

	public interface IValidatableProperty<TSource, TElement>
	{
		IValidatable<TSource> SourceValidatable { get; }
		string PropertyName { get; }
		Func<TElement> PropertySelector { get; }
	}
}
