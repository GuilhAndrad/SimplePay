using FluentMigrator;

namespace SimplePay.Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.Add_Users_Table, "Creating Users table")]
public class Version0001 : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("Users")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Active").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("FullName").AsString(250).NotNullable()
            .WithColumn("Document").AsString(20).NotNullable().Unique()
            .WithColumn("Email").AsString(250).NotNullable().Unique()
            .WithColumn("Password").AsString(255).NotNullable()
            .WithColumn("Type").AsString(50).NotNullable();
    }
}