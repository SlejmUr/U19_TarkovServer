namespace TarkovServerU19.BSGEnums
{
    public struct RadioTransmitterData
    {

        public string PlayerProfileIDForSend;

        public int PlayerID;

        public bool IsEncoded;

        public RadioTransmitterStatus Status;

        public bool IsAgressor;
    }
    public enum RadioTransmitterStatus
    {

        NotInitialized,

        NoRadioTransmitter,

        Red,

        Green,

        Yellow
    }
}