using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Allsworth.TCMS.Client
{
	public abstract class ValidationBase<TSource> : IValidatable<TSource>
	{
		public ValidationBase(TSource source)
		{
			this.source = source;
		}

		private TSource source;
		public TSource Source
		{
			get { return source; }
		}

		public abstract IValidator Validator { get; }

		public string Error
		{
			get { return null; }
		}

		public string this[string columnName]
		{
			get
			{
				return Validator.Validate(columnName);
			}
		}
	}
}
