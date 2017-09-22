
namespace ValidationMeshTest
{
	public class TraditionValidationModel : Model
	{
		public override string this[string columnName]
		{
			get
			{
				if (columnName == "Property1")
				{
					if (string.IsNullOrEmpty(Property1))
					{
						return "required";
					}
				}
				else if (columnName == "Property2")
				{
					if (string.IsNullOrEmpty(Property2))
					{
						return "required";
					}
					else if (Property2.Length > 5)
					{
						return "too long";
					}
				}
				else if (columnName == "Property3")
				{
					if (string.IsNullOrEmpty(Property3))
					{
						return "required";
					}
					else
					{
						int result;
						if (!int.TryParse(Property3, out result))
						{
							return "invalid numeric value";
						}
					}
				}
				return null;
			}
		}
	}
}
