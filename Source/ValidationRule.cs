using System;

namespace ValidationExtention
{
	internal class ValidationRule
	{
		public ValidationRule(Func<bool> rule, string errorMessage = null)
		{
			if (rule == null)
				this.rule = () => true;
			else
				this.rule = rule;

			if (string.IsNullOrEmpty(errorMessage))
				this.errorMessage = default_error_message;
			else
				this.errorMessage = errorMessage;
		}

		private Func<bool> rule;
		private static readonly string default_error_message = "invalid";

		private string errorMessage;
		public string ErrorMessage
		{
			get { return this.errorMessage; }
		}

		public bool Passed
		{
			get { return rule(); }
		}

		public override string ToString()
		{
			return Passed ? "Passed" : string.Format("Not Pass: {0}", ErrorMessage);
		}
	}
}
