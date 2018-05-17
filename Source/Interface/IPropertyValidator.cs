using System;

namespace ValidationExtention
{
	internal interface IPropertyValidator
	{
		string PropertyName { get; }
		string Validate();
		void AddRule(Func<bool> rule, string errorMessage);
	}
}
