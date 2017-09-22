using System;
using ValidationExtention;

namespace ValidationMeshTest
{
	public class NewValidationModelValdiatable : IValidatable<NewValidationModel>
	{
		public NewValidationModelValdiatable(NewValidationModel source)
		{
			if (source == null)
				throw new ArgumentNullException("source");
			this.source = source;

			var property1Validatable = this.Select(item => item.Property1);
			property1Validatable.Required();

			var property2Validatable = this.Select(item => item.Property2);
			property2Validatable.Required().StringLength(5);

			var property3Validatable = this.Select(item => item.Property3);
			property3Validatable.Required().IsInt32();
		}

		private NewValidationModel source;
		public NewValidationModel Source
		{
			get { return source; }
		}

		private IValidator validator = new Validator();
		public IValidator Validator
		{
			get { return validator; }
		}
	}
}
