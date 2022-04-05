using Bunit;
using Shopper.BlazorServerSide.Pages;
using Xunit;

namespace Shopper.Tests
{
    public class CounterTests : TestContext
    {
        [Fact]
        public void Counter_WhenClicked_ShouldIncrement()
        {
            // Arrange
            IRenderedFragment cut = RenderComponent<Counter>();

            // Act
            var button = cut.Find("button");
            button.Click();

            // Assert
            var paragraph = cut.Find("p");
            paragraph.MarkupMatches(@"<p role=""status"">Current count: 1</p>");
        }
    }
}
