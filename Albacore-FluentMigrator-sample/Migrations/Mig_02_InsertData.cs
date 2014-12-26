
using FluentMigrator;

namespace Albacore_FluentMigrator_sample.Migrations
{
    [Migration(2)]
    public class Mig_02_InsertData : Migration
    {
        public override void Up()
        {
            Insert.IntoTable("user_table")
                .Row(new { user_id = 1, user_name = "hoge" })
                .Row(new { user_id = 2, user_name = "fuga" });
        }

        public override void Down()
        {
            Delete.FromTable("user_table").AllRows();
        }
    }
}
