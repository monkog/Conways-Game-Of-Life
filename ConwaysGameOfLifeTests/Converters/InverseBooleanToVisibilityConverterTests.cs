using System.Globalization;
using System.Windows;
using ConwaysGameOfLife.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConwaysGameOfLifeTests.Converters
{
	[TestClass]
	public class InverseBooleanToVisibilityConverterTests
	{
		private InverseBooleanToVisibilityConverter _unitUnderTest;

		[TestInitialize]
		public void Initialize()
		{
			_unitUnderTest = new InverseBooleanToVisibilityConverter();
		}

		[TestMethod]
		[DataRow(true, Visibility.Collapsed)]
		[DataRow(false, Visibility.Visible)]
		[DataRow(null, Visibility.Collapsed)]
		public void Convert_Value_ExpectedResult(object value, Visibility expected)
		{
			var result = _unitUnderTest.Convert(value, typeof(Visibility), null, CultureInfo.CurrentCulture);

			Assert.AreEqual(expected, result);
		}
	}
}
