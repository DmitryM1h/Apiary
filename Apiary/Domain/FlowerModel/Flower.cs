using ApiaryEngine.Abstractions;
using ApiaryEngine.Domain.Bees;
using ApiaryEngine.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiaryEngine.Domain.FlowerModel
{
    public record class Flower(int Id, Point position) : IActor
    {
        private volatile int _nectarAmount = 100;
        private volatile bool _isRefreshing = false;
        private Lock _lock = new();
        private CancellationTokenSource cts = new();

        public Point Position { get; set; } = position;
        public int NectarAmount => _nectarAmount;

        public int GetHoney(int honeyAmount)
        {
            lock (this)
            {
                if (honeyAmount > _nectarAmount)
                    throw new ArgumentException("Not enough honey");

                _nectarAmount -= honeyAmount;
            }

            RefreshIfOutOfHoney();

            StopRefreshingIfBeingCollected();

            return honeyAmount;
        }

        public void RefreshIfOutOfHoney()
        {
            if (NectarAmount == 0 && _isRefreshing == false)
            {
                using var lockScope = _lock.EnterScope();

                if (NectarAmount == 0 && _isRefreshing == false)
                    Refresh();

            }
        }

        public void StopRefreshingIfBeingCollected()
        {

            if (NectarAmount > 0 && _isRefreshing == true)
            {
                using var lockScope = _lock.EnterScope();

                if (NectarAmount > 0 && _isRefreshing == true)
                {
                    cts.Cancel();
                    cts.Dispose();
                    cts = new();
                }
            }
        }
        private void Refresh()
        {
            _isRefreshing = true;
            cts.CancelAfter(TimeSpan.FromSeconds(15));
            Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(5000, cts.Token); // начнем регенерацию через 5 секунд
                    while (!cts.Token.IsCancellationRequested && _nectarAmount < 100)
                    {
                        await Task.Delay(1000);
                        _nectarAmount += 10;
                    }
                    Console.WriteLine($"Flower has been refreshed (id = {Id})");
                }
                catch (OperationCanceledException) { }
                catch (Exception ex)
                {
                    Console.WriteLine("Critical Exception while refreshing flower");
                }
                finally
                {
                    _isRefreshing = false;
                    cts?.Dispose();
                    cts = new();
                }
            });
        }

        public void Tick()
        {
            throw new NotImplementedException();
        }

        public IActorState GetState()
        {
            return new FlowerState() { FlowerId = Id, Position = Position, NectarAmount = _nectarAmount };
        }
    }
}
