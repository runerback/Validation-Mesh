using System;
using System.ComponentModel;

namespace ValidationMeshTest
{
	public abstract class Model : IDataErrorInfo
	{
		private string property1;
		public string Property1
		{
			get { return this.property1; }
			set { this.property1 = value; }
		}
		public string Property2 { get; set; }
		public string Property3 { get; set; }

		public string Error
		{
			get { throw new NotImplementedException(); }
		}

		public abstract string this[string columnName] { get; }
	}
}
