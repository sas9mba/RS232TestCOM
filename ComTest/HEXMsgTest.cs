using RomanovTestCom;
using System;
using Xunit;

namespace ComTest
{
    public class ASCIIMsgTest
    {
        [Theory]
        [InlineData("FF1105", "46-46-31-31-30-35")]
        [InlineData("fF1105", "66-46-31-31-30-35")]
        [InlineData("A2", "41-32")]
        [InlineData("", "")]
        [InlineData("OOO", "4F-4F-4F")]
        public void TestConvertToHEX(String inText, String outText)
        {
            ASCIIMsg msg = new ASCIIMsg();
            msg.SetMessage(inText);
            Assert.Equal(outText, msg.GetHex());
        }

        [Theory]
        [InlineData("AsD", new byte[] { 65, 115, 68 })]
        [InlineData("0Ac", new byte[] { 48, 65, 99 })]
        public void TestConvertToByte(String inText, Byte[] outByte)
        {
            ASCIIMsg msg = new ASCIIMsg();
            msg.SetMessage(inText);
            Assert.Equal(outByte, msg.Bytes);
        }
    }
}
