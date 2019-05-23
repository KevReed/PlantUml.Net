using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlantUml.Net;

namespace test
{
    [TestClass]
    public class Test_EncodingSpike
    {
        [TestMethod]
        public void NonUtf8Encoding_Should_Not_Be_Convertable_To_Utf8_OldWay()
        {
            string ruText = "Привет";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string utf8RuText = OldConvertToUtf8(ruText, Encoding.GetEncoding("Windows-1251"));
            ruText.Should().NotBe(utf8RuText);
        }
        
        [TestMethod]
        public void NonUtf8Encoding_Should_Be_Convertable_To_Utf8()
        {
            string ruText = "Привет";
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            string utf8RuText = PlantUmlTextEncoding.ConvertToUtf8(ruText, Encoding.GetEncoding("Windows-1251"));
            ruText.Should().Be(utf8RuText);
        }
        
        string OldConvertToUtf8(string text, Encoding srcEncoding)
        {
            return Encoding.UTF8.GetString(srcEncoding.GetBytes(text));
        }

    }
}