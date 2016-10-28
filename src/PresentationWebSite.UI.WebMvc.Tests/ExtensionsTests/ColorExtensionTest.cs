using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using PresentationWebSite.UI.WebMvc.Helpers.Extensions;

namespace PresentationWebSite.UI.WebMvc.Tests.ExtensionsTests
{
    [TestFixture]
    public class ColorExtensionTest
    {
        private readonly List<Color> _colors = new List<Color>{ Color.Black, Color.White, Color.AliceBlue };

        [Test]
        public void Should_Return_Hex_String()
        {
            //Act
            foreach (var color in _colors)
                Assert.AreEqual(color.GetHexValue(), "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2"));
        }

        [Ignore("Replace algo for getting unused color")]
        public void Should_Return_New_Color()
        {
            foreach (var color in _colors)
            {
                var colors = new List<Color>() { color };
                for (int i = 0; i < 20; i++)
                {
                    Assert.IsFalse(colors.Contains(colors[i].GetNextColor()));
                    colors.Add(colors[i].GetNextColor());
                }
            }
        }
    }
}
