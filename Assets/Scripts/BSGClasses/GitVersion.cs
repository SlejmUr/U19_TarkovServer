using System;
using UnityEngine.Networking;

namespace TarkovServerU19.BSGClasses
{
    public class GitVersion
    {
        public override string ToString()
        {
            return string.Format("CommitHash: '{0}' CommitDate: '{1}' CommitBranch: '{2}' CommitSubject: '{3}'", new object[]
            {
            this.CommitHash,
            this.CommitDate,
            this.CommitBranch,
            this.CommitSubject
            });
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(this.CommitHash);
            writer.Write(this.CommitDate.ToOADate());
            writer.Write(this.CommitSubject);
            writer.Write(this.CommitBranch);
        }

        public static GitVersion Deserialize(NetworkReader reader)
        {
            return new GitVersion
            {
                CommitHash = reader.ReadString(),
                CommitDate = DateTime.FromOADate(reader.ReadDouble()),
                CommitSubject = reader.ReadString(),
                CommitBranch = reader.ReadString()
            };
        }

        public string CommitHash = "d7958f33e84ee1ab5ced92cea3eb11280db0f96a";
        public DateTime CommitDate = DateTime.FromOADate(45205.6881134259);
        public string CommitSubject = "Merge branch 'bugfixes/EFT-31483_crit_art_fixes' into 'release/0.13.5.3'";
        public string CommitBranch = "release/0.13.5.3";
    }
}
