using System;

namespace ValidationExtention
{
	public interface IValidator
	{
		string Validate(string propertyName);
		void AddRule(string propertyName, Func<bool> rule, string errorMessage);
	}

	internal interface IPropertyValidator
	{
		string PropertyName { get; }
		string Validate();
		void AddRule(Func<bool> rule, string errorMessage);
	}
}
