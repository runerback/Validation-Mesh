using System;

namespace ValidationExtention
{
	public interface IValidatableProperty<TSource, TKey>
	{
		IValidatable<TSource> SourceValidatable { get; }
		string PropertyName { get; }
		Func<TKey> PropertySelector { get; }
	}
}
