using NUnit.Framework;


namespace csharp_example

{
    [TestFixture]
    public class CartPageObjectTest : BasicTest
    {
        [Test]
        public void AddProduct()
        {
            int prodsToAddCount = 3;

            App.AddToCartAndCount(prodsToAddCount);
            App.RemoveAllCartItems();

        }

        [TearDown]
        public void stop()
        {
            App.Quit();
        }
    }
}