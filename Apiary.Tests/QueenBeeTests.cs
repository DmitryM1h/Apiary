using ApiaryEngine.abstractions;
using ApiaryEngine.Domain;
using ApiaryEngine.Domain.Bees.QueenBee;
using ApiaryEngine.Domain.Bees.QueenBee.States;

namespace Apiary.Tests
{
    public class QueenBeeTests
    {
        [Fact]
        public async Task WaitingStateCompleted()
        {
            var hive = new Hive(hiveId: 2);

            var bee = new QueenBee(hive);

            Assert.Equal(typeof(WaitingState), bee.GetType());

            await Task.Delay(TimeSpan.FromSeconds(QueenBee._secondsToTryProduce));

            bee.Tick();

            Assert.Equal(typeof(CollectingHoneyState), bee.State.GetType());

        }
    }
}
