using FluentMigrator;

namespace SimplePay.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.Add_Wallets_Table, "Creating Wallets table")]
public class Version0002 : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("Wallets")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Active").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("UserId").AsInt32().NotNullable().Unique().ForeignKey("Users", "Id")
            .WithColumn("Balance").AsDecimal(18, 2).NotNullable().WithDefaultValue(0)
            .WithColumn("Version").AsInt32().NotNullable().WithDefaultValue(0);
    }
}
