using ValidationExtention;

namespace ValidationMeshTest
{
	public class NewValidationModel : Model
	{
		public NewValidationModel()
		{
			this.validatable = new NewValidationModelValdiatable(this);
		}

		private IValidatable<NewValidationModel> validatable;
		public IValidatable<Model> Validatable
		{
			get { return validatable; }
		}

		public override string this[string columnName]
		{
			get { return Validatable.Validator.Validate(columnName); }
		}
	}
}
