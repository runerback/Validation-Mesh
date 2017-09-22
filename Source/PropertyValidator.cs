using System;
using System.Collections.Generic;

namespace ValidationExtention
{
	internal class PropertyValidator : IPropertyValidator
	{
		public PropertyValidator(string propertyName)
		{
			if (string.IsNullOrEmpty(propertyName))
				throw new ArgumentNullException("propertyName");
			this.propertyName = propertyName;
		}

		private string propertyName;
		public string PropertyName
		{
			get { return this.propertyName; }
		}

		private List<ValidationRule> validationRules = new List<ValidationRule>();

		public void AddRule(Func<bool> rule, string errorMessage = null)
		{
			this.validationRules.Add(
				new ValidationRule(rule, errorMessage));
		}

		public string Validate()
		{
			foreach (var rule in this.validationRules)
			{
				if (!rule.Passed)
					return rule.ErrorMessage;
			}
			return null;
		}

		public override string ToString()
		{
			return string.Format("{0} - {1} rules", propertyName, validationRules.Count);
		}
	}
}
