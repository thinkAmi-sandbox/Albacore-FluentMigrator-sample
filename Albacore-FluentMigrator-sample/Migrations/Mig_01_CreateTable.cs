
using FluentMigrator;

namespace Albacore_FluentMigrator_sample.Migrations
{
    [Migration(1)]
    public class Mig_01_CreateTable : AutoReversingMigration
    {
        public override void Up()
        {
            Create.Table("user_table")
                .WithColumn("user_id").AsInt32().PrimaryKey()
                .WithColumn("user_name").AsString();
        }
    }
}
