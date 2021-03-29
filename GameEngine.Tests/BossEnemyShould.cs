using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    public class BossEnemyShould
    {
        private readonly ITestOutputHelper _outputHelper;

        public BossEnemyShould(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }
   
        [Fact]
        [Trait("Category", "Boss")]
        public void HaveCorrectPower()
        {
            _outputHelper.WriteLine("Creating Boss Enemy");
            BossEnemy sut = new BossEnemy();
            Assert.Equal(166.667, sut.TotalSpecialAttackPower, 3);
        }
    }
}