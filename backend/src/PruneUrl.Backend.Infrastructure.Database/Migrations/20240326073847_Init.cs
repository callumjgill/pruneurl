using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PruneUrl.Backend.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Init : Migration
{
  /// <inheritdoc />
  protected override void Up(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.CreateTable(
      name: "ShortUrls",
      columns: table => new
      {
        Id = table
          .Column<int>(type: "integer", nullable: false)
          .Annotation(
            "Npgsql:ValueGenerationStrategy",
            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
          ),
        LongUrl = table.Column<string>(type: "text", nullable: false),
        Url = table.Column<string>(type: "text", nullable: false)
      },
      constraints: table =>
      {
        table.PrimaryKey("PK_ShortUrls", x => x.Id);
      }
    );

    migrationBuilder.CreateIndex(name: "IX_ShortUrls_Url", table: "ShortUrls", column: "Url");
  }

  /// <inheritdoc />
  protected override void Down(MigrationBuilder migrationBuilder)
  {
    migrationBuilder.DropTable(name: "ShortUrls");
  }
}
