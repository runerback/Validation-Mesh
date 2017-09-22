using System;
using System.Collections.Concurrent;

namespace ValidationExtention
{
	public class Validator : IValidator
	{
		public string Validate(string propertyName)
		{
			PropertyValidator validator;
			if (this.validatorMap.TryGetValue(propertyName, out validator))
				return validator.Validate();
			return null;
		}

		private ConcurrentDictionary<string, PropertyValidator> validatorMap =
			new ConcurrentDictionary<string, PropertyValidator>();

		public void AddRule(string propertyName, Func<bool> rule, string errorMessage = null)
		{
			PropertyValidator validator;
			if (!this.validatorMap.TryGetValue(propertyName, out validator))
			{
				validator = new PropertyValidator(propertyName);
				this.validatorMap.TryAdd(propertyName, validator);
			}
			validator.AddRule(rule, errorMessage);
		}

		public override string ToString()
		{
			return string.Format("{0} properties", validatorMap.Count);
		}
	}
}
