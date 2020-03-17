using RomanovTestCom;
using RomanovTestCom.Common;
using System;
using System.Windows.Input;
using Xunit;

namespace ComTest
{
    public class CommonChecksTest
    {
        [Theory]
        [InlineData(Key.A, false)]
        [InlineData(Key.D0, true)]
        [InlineData(Key.D9, true)]
        [InlineData(Key.B, false)]
        [InlineData(Key.LeftCtrl, false)]
        public void TestKeyIsNumber(Key inKey, Boolean answer)
        {
            Assert.Equal(Checks.KeyIsNumber(inKey), answer);
        }
    }
}
