using ApiaryEngine.abstractions;
using ApiaryEngine.Domain;
using ApiaryEngine.Domain.Bees;
using ApiaryEngine.Domain.States.QueenBeeStates;

namespace Apiary.Tests
{
    public class QueenBeeTests
    {
        [Fact]
        public async Task WaitingStateCompleted()
        {
            var hive = new Hive(hiveId: 2);

            var bee = new QueenBee(hive);

            Assert.Equal(typeof(WaitingState), bee.State.GetType());

            await Task.Delay(TimeSpan.FromSeconds(QueenBee._secondsToTryProduce));

            await bee.Tick();

            Assert.Equal(typeof(CollectingHoneyState), bee.State.GetType());

        }
    }
}
