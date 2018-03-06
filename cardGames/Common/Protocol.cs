using ProtoBuf;

namespace Common
{
    [ProtoContract]
    public class Protocol
    {
        public enum State {Wait, Play, Gamble, Disconnect, PrintHand, Card};
        [ProtoMember(1)]
        public State state;
        [ProtoMember(2)]
        public string Msg;
    }
}
