namespace TarkovServerU19.BSGClasses
{
    public class Received
    {
        public Disordered messages = new Disordered();

        public Disordered bytes = new Disordered();

        public void Increment(int value)
        {
            messages.Increment();
            bytes.Increment(value);
        }

        public void Commit()
        {
            messages.Commit();
            bytes.Commit();
        }
    }
}