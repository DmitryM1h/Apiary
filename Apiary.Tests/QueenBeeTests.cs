using ApiaryEngine.abstractions;
using ApiaryEngine.Domain.Bees.QueenBee;
using ApiaryEngine.Domain.Bees.QueenBee.States;

namespace Apiary.Tests
{
    public class QueenBeeTests
    {
        [Fact]
        public async Task WaitingStateCompleted()
        {
            var bee = new QueenBee(hiveId: 2);

            Assert.Equal(typeof(WaitingState), bee.State.GetType());

            await Task.Delay(TimeSpan.FromSeconds(QueenBee._secondsToTryProduce));

            await bee.Tick();

            Assert.Equal(typeof(CollectingHoneyState), bee.State.GetType());

        }
    }
}
