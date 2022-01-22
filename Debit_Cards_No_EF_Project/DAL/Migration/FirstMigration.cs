using FluentMigrator;

namespace Debit_Cards_No_EF_Project.DAL.Migration
{
    [Migration(1)]
    public sealed class FirstMigration : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("Debit_Cards")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("NumberCard").AsInt64()
                .WithColumn("CurrencyName").AsString()
                .WithColumn("Holder").AsString()
                .WithColumn("Month").AsInt32()
                .WithColumn("Year").AsInt32();
        }

        public override void Down()
            => Delete.Table("Debit_Cards");
    }
}
