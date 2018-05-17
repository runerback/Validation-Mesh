using System;

namespace ValidationExtention
{
	public interface IValidatable<out TSource>
	{
		TSource Source { get; }
		IValidator Validator { get; }
	}
}
