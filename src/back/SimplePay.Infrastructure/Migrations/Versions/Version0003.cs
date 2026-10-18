using FluentMigrator;

namespace SimplePay.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.Add_Transfers_Table, "Creating Transfers table")]
public class Version0003 : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("Transfers")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Active").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("PayerId").AsInt32().NotNullable().ForeignKey("Users", "Id")
            .WithColumn("PayeeId").AsInt32().NotNullable().ForeignKey("Users", "Id")
            .WithColumn("Amount").AsDecimal(18, 2).NotNullable()
            .WithColumn("CreatedAt").AsDateTime().NotNullable();
    }
}
