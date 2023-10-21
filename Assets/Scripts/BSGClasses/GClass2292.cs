namespace TarkovServerU19.BSGClasses
{
    public abstract class GClass2292
    {
        // Token: 0x0600BD84 RID: 48516 RVA: 0x00334040 File Offset: 0x00332240
        protected GClass2292(Connection connection)
        {
            this.Connection = connection;
        }

        // Token: 0x0600BD85 RID: 48517 RVA: 0x00014C31 File Offset: 0x00012E31
        public virtual void Enter()
        {
        }

        // Token: 0x0600BD86 RID: 48518 RVA: 0x00014C31 File Offset: 0x00012E31
        public virtual void Exit()
        {
        }

        // Token: 0x0600BD87 RID: 48519 RVA: 0x00014C31 File Offset: 0x00012E31
        public virtual void HandelReceive(MessageSegment message)
        {
        }

        // Token: 0x0600BD88 RID: 48520 RVA: 0x00014C31 File Offset: 0x00012E31
        public virtual void Update()
        {
        }

        // Token: 0x0600BD89 RID: 48521 RVA: 0x00014C31 File Offset: 0x00012E31
        public virtual void Connect()
        {
        }

        // Token: 0x0600BD8A RID: 48522 RVA: 0x00014C31 File Offset: 0x00012E31
        public virtual void Disconnect()
        {
        }

        // Token: 0x0600BD8B RID: 48523 RVA: 0x00014C31 File Offset: 0x00012E31
        public virtual void Send(MessageSegment message)
        {
        }

        // Token: 0x0400A96A RID: 43370
        public Connection Connection;
    }

}
