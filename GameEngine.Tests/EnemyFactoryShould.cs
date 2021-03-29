using System;
using Xunit;

namespace GameEngine.Tests
{
    [Trait("Category", "Enemy")]
    public class EnemyFactoryShould
    {
        [Fact]
        public void CreateNormalEnemyByDefault()
        {
            EnemyFactory sut = new EnemyFactory();
            Enemy enemy = sut.Create("Zombi");

            Assert.IsType<NormalEnemy>(enemy);
        }

        [Fact]
        public void CreateNormalEnemyByDefault_NotTypeExample()
        {
            EnemyFactory sut = new EnemyFactory();
            Enemy enemy = sut.Create("Zombi");

            Assert.IsNotType<DateTime>(enemy);
            
        }

        [Fact]
        public void CreateBossEnemy()
        {
            EnemyFactory sut = new EnemyFactory();
            Enemy enemy = sut.Create("Zombi King", true);

            Assert.IsType<BossEnemy>(enemy);  
        }

        [Fact(Skip = "Don't need to run this")]
        public void CreateBossEnemy_castReturnedTypeExample()
        {
            EnemyFactory sut = new EnemyFactory();
            Enemy enemy = sut.Create("Zombi King", true);

            BossEnemy boss = Assert.IsType<BossEnemy>(enemy);

            Assert.Equal("Zombi King", boss.Name);  
        }

        [Fact]
        public void CreateBossEnemy_AssertAssignableTypes()
        {
            EnemyFactory sut = new EnemyFactory();
            Enemy enemy = sut.Create("Zombi King", true);

            //Assert.IsType<Enemy>(enemy);  
            Assert.IsAssignableFrom<Enemy>(enemy);
        }

        [Fact]
        public void CreateSeparateInstances()
        {
            EnemyFactory sut = new EnemyFactory();
            Enemy enemy1 = sut.Create("Zombi");
            Enemy enemy2 = sut.Create("Zombi");

            Assert.NotSame(enemy1, enemy2);
        }

        [Fact]
        public void NotAllowNullName()
        {
            EnemyFactory sut = new EnemyFactory();

            //Assert.Throws<ArgumentNullException>(() => sut.Create(null));
            Assert.Throws<ArgumentNullException>("name", () => sut.Create(null));
        }


        [Fact]
        public void OnlyAllowedKingOrQueenBossEnemies()
        {
            EnemyFactory sut = new EnemyFactory();

            EnemyCreationException ex = 
                Assert.Throws<EnemyCreationException>(() => sut.Create("Zombie", true));

           Assert.Equal("Zombie", ex.RequestedEnemyName);   
        }
    }
}
