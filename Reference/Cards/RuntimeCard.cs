using OfficeWar.Data;

namespace OfficeWar.Cards
{
    public sealed class RuntimeCard
    {
        static int _nextId = 1;

        public OfficeCardData Data { get; }
        public int InstanceId { get; }

        public RuntimeCard(OfficeCardData data)
        {
            Data = data;
            InstanceId = _nextId++;
        }

        public override string ToString() => $"{Data.displayName} (#{InstanceId})";
    }
}
