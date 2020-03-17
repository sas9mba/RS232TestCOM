using RomanovTestCom;
using System;
using Xunit;

namespace ComTest
{
    public class HEXMsgTest
    {
        [Theory]
        [InlineData("FF1105", "FF-11-05")]
        [InlineData("fF1105", "FF-11-05")]
        [InlineData("F1105", "0F-11-05")]
        [InlineData("", "")]
        [InlineData("0000", "00-00")]
        public void TestConvertToHEX(String inText, String outText)
        {
            HEXMsg msg = new HEXMsg();
            msg.SetMessage(inText);
            Assert.Equal(outText, msg.GetHex());
        }

        [Theory]
        [InlineData("asdf")]
        [InlineData("F110x")]
        [InlineData("FOO")]
        [InlineData("‘¿¿")]
        public void TestConvertToHEXWithExeption(String inText)
        {
            HEXMsg msg = new HEXMsg();
            Assert.Throws<FormatException>(() => msg.SetMessage(inText));
        }

        [Theory]
        [InlineData("FF1105", new byte[] { 255, 17, 5 })]
        [InlineData("0F1105", new byte[] { 15, 17, 5 })]
        public void TestConvertToByte(String inText, Byte[] outByte)
        {
            HEXMsg msg = new HEXMsg();
            msg.SetMessage(inText);
            Assert.Equal(outByte, msg.Bytes);
        }

        [Theory]
        [InlineData('0', true)]
        [InlineData('d', true)]
        [InlineData('F', true)]
        [InlineData('9', true)]
        [InlineData('s', false)]
        [InlineData('q', false)]
        public void TestIsCorrectChar(Char simbol, Boolean answer)
        {
            HEXMsg msg = new HEXMsg();
            Assert.Equal(msg.IsCorrect(simbol), answer);
        }
    }
}
