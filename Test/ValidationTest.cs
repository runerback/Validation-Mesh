using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ValidationMeshTest
{
	[TestClass]
	public class ValidationTest
	{
		private static Model traditionDataErrorModel;
		private static Model newDataErrorModel;

		private static readonly string p1 = "Property1", p2 = "Property2", p3 = "Property3";

		static ValidationTest()
		{
			string v1 = "", v2 = "this is a string which length is greater than 5!", v3 = "0";
			traditionDataErrorModel = new TraditionValidationModel()
			{
				Property1 = v1,
				Property2 = v2,
				Property3 = v3
			};
			newDataErrorModel = new NewValidationModel()
			{
				Property1 = v1,
				Property2 = v2,
				Property3 = v3
			};
		}

		private void traditionMethod(bool print)
		{
			string result1 = traditionDataErrorModel[p1];
			string result2 = traditionDataErrorModel[p2];
			string result3 = traditionDataErrorModel[p3];
			if (print)
			{
				Console.WriteLine("{0}: {1}", p1, result1);
				Console.WriteLine("{0}: {1}", p2, result2);
				Console.WriteLine("{0}: {1}", p3, result3);
			}
		}

		[TestMethod]
		public void Tradition()
		{
			traditionMethod(true);
		}

		[TestMethod]
		public void Tradition1000()
		{
			for (int i = 0; i < 1000; i++)
			{
				traditionMethod(false);
			}
		}

		private void newMethod(bool print)
		{
			string result1 = newDataErrorModel[p1];
			string result2 = newDataErrorModel[p2];
			string result3 = newDataErrorModel[p3];
			if (print)
			{
				Console.WriteLine("{0}: {1}", p1, result1);
				Console.WriteLine("{0}: {1}", p2, result2);
				Console.WriteLine("{0}: {1}", p3, result3);
			}
		}

		[TestMethod]
		public void New()
		{
			newMethod(true);
		}

		[TestMethod]
		public void New1000()
		{
			for (int i = 0; i < 1000; i++)
			{
				newMethod(false);
			}
		}

		[TestMethod]
		public void Compare()
		{
			Assert.AreEqual<string>(traditionDataErrorModel[p1], newDataErrorModel[p1]);
			Assert.AreEqual<string>(traditionDataErrorModel[p2], newDataErrorModel[p2]);
			Assert.AreEqual<string>(traditionDataErrorModel[p3], newDataErrorModel[p3]);
		}
	}
}
