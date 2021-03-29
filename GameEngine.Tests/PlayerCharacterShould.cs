using System;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    public class PlayerCharacterShould : IDisposable
    {
        private readonly PlayerCharacter _sut;
        private readonly ITestOutputHelper _output;

        public PlayerCharacterShould(ITestOutputHelper output)
        {
            _sut = new PlayerCharacter();
            _output = output;

            _output.WriteLine("Creating new PlayerCharacter");
        }

        public void Dispose()
        {
            //cleanup code
            _output.WriteLine($"Disposing PlayerCharacter {_sut.FullName}");
        }

        [Fact]
        public void BeInexperienceWhenNew()
        {
            Assert.True(_sut.IsNoob);
        }

        [Fact]
        public void CalculateFullName()
        {
            _sut.FirstName = "Sarah";
            _sut.LastName = "Smith";

            Assert.Equal("Sarah Smith", _sut.FullName);
            Assert.StartsWith("Sarah", _sut.FullName);
            Assert.EndsWith("Smith", _sut.FullName);
            Assert.Equal("SARAH Smith", _sut.FullName, ignoreCase: true);
            Assert.Contains("ah Sm", _sut.FullName);

            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+",_sut.FullName);
        }

        [Fact]
        public void StartWithDefaultHealth()
        {
            Assert.Equal(100, _sut.Health);
            Assert.NotEqual(0, _sut.Health);
        }

        [Fact]
        public void IncreaseHealthAfterSleeping()
        {
            _sut.Sleep();

            //Assert.True(_sut.Health >= 101 && _sut.Health <=200);
            Assert.InRange(_sut.Health, 101, 200);
        }

        [Fact]
        public void NotHaveNickNameByDefault()
        {
            Assert.Null(_sut.Nickname);
        }

        [Fact]
        public void HaveALongBow()
        {
            Assert.Contains("Long Bow", _sut.Weapons);
            Assert.DoesNotContain("Longss Bow", _sut.Weapons);
        }

        [Fact]
        public void HaveAtLeastOneKindOfSword()
        {            
            Assert.Contains(_sut.Weapons, weapon => weapon.Contains("Sword"));
        }

        [Fact]
        public void HAVENOEMPTYDEFAULTWEAPONS()
        {
            Assert.All(_sut.Weapons, weapon => Assert.False(string.IsNullOrWhiteSpace(weapon)));
        }

        [Fact]
        public void HaveAllExpectedWeapons()
        {
            var expectedWeapons = new []{"Long Bow", "Short Bow", "Short Sword"};
           
            Assert.Equal(expectedWeapons, _sut.Weapons);

        }

        [Fact]
        public void RaiseSleptEvent()
        {
            Assert.Raises<EventArgs>(
                handler => _sut.PlayerSlept += handler,
                handler => _sut.PlayerSlept -= handler,
                () => _sut.Sleep()
            );
        }

        [Fact]
        public void RaisePropertyChangedEvent()
        {
            Assert.PropertyChanged(
                _sut,
                "Health",
                () => _sut.TakeDamage(10)
            );
        }

        [Theory]
        // [InlineData(0, 100)]
        // [InlineData(1, 99)]
        // [InlineData(50, 50)]
        // [InlineData(101, 1)]
        // [MemberData(nameof(InternalHealthDamageTestData.TestData),
        //        MemberType = typeof(InternalHealthDamageTestData))]
        // [MemberData(nameof(ExternalHealthDamageTestData.TestData),
        //        MemberType = typeof(ExternalHealthDamageTestData))]
        [HealthDamageData]
        public void TakeDamage(int damage, int expectedHealth)
        {
            _sut.TakeDamage(damage);

            Assert.Equal(expectedHealth, _sut.Health);
        }
    }
}
