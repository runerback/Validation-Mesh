﻿using System;

namespace ValidationExtention
{
	public interface IValidator
	{
		string Validate(string propertyName);
		void AddRule(string propertyName, Func<bool> rule, string errorMessage);
	}
}
