using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNewsEventsAlertsBenchmarks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssetId = table.Column<Guid>(type: "TEXT", nullable: true),
                    AlertType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Condition = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    LastTriggered = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alerts_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Alerts_UserProfiles_UserId",
                        column: x => x.UserId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Benchmarks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Symbol = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Benchmarks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarketEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EventType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 5000, nullable: false),
                    SourceUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    PublishedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AffectedAssetsJson = table.Column<string>(type: "TEXT", nullable: true),
                    ImpactAnalysis = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: true),
                    Severity = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    IsProcessed = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsAlertSent = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    Source = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    SourceUrl = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", maxLength: 10000, nullable: true),
                    AISummary = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Sentiment = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
                    RelevanceScore = table.Column<decimal>(type: "TEXT", precision: 5, scale: 2, nullable: true),
                    RelatedAssetsJson = table.Column<string>(type: "TEXT", nullable: true),
                    IsRead = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssetId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    OpenPrice = table.Column<decimal>(type: "TEXT", precision: 18, scale: 6, nullable: false),
                    HighPrice = table.Column<decimal>(type: "TEXT", precision: 18, scale: 6, nullable: false),
                    LowPrice = table.Column<decimal>(type: "TEXT", precision: 18, scale: 6, nullable: false),
                    ClosePrice = table.Column<decimal>(type: "TEXT", precision: 18, scale: 6, nullable: false),
                    AdjustedClose = table.Column<decimal>(type: "TEXT", precision: 18, scale: 6, nullable: false),
                    Volume = table.Column<long>(type: "INTEGER", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceHistories_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlertHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AlertId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Message = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    TriggeredAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsRead = table.Column<bool>(type: "INTEGER", nullable: false),
                    RelatedEntityType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    RelatedEntityId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlertHistories_Alerts_AlertId",
                        column: x => x.AlertId,
                        principalTable: "Alerts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BenchmarkValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    BenchmarkId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Value = table.Column<decimal>(type: "TEXT", precision: 18, scale: 6, nullable: false),
                    DailyReturn = table.Column<decimal>(type: "TEXT", precision: 8, scale: 4, nullable: true),
                    AccumulatedReturn = table.Column<decimal>(type: "TEXT", precision: 8, scale: 4, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenchmarkValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BenchmarkValues_Benchmarks_BenchmarkId",
                        column: x => x.BenchmarkId,
                        principalTable: "Benchmarks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlertHistories_AlertId_TriggeredAt",
                table: "AlertHistories",
                columns: new[] { "AlertId", "TriggeredAt" });

            migrationBuilder.CreateIndex(
                name: "IX_AlertHistories_IsRead",
                table: "AlertHistories",
                column: "IsRead");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_AssetId",
                table: "Alerts",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Alerts_UserId_IsActive",
                table: "Alerts",
                columns: new[] { "UserId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_Benchmarks_IsActive",
                table: "Benchmarks",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Benchmarks_Symbol",
                table: "Benchmarks",
                column: "Symbol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BenchmarkValues_BenchmarkId_Date",
                table: "BenchmarkValues",
                columns: new[] { "BenchmarkId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarketEvents_IsProcessed",
                table: "MarketEvents",
                column: "IsProcessed",
                filter: "IsProcessed = 0");

            migrationBuilder.CreateIndex(
                name: "IX_MarketEvents_PublishedAt",
                table: "MarketEvents",
                column: "PublishedAt");

            migrationBuilder.CreateIndex(
                name: "IX_NewsItems_IsRead",
                table: "NewsItems",
                column: "IsRead");

            migrationBuilder.CreateIndex(
                name: "IX_NewsItems_PublishedAt",
                table: "NewsItems",
                column: "PublishedAt");

            migrationBuilder.CreateIndex(
                name: "IX_PriceHistories_AssetId_Date",
                table: "PriceHistories",
                columns: new[] { "AssetId", "Date" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlertHistories");

            migrationBuilder.DropTable(
                name: "BenchmarkValues");

            migrationBuilder.DropTable(
                name: "MarketEvents");

            migrationBuilder.DropTable(
                name: "NewsItems");

            migrationBuilder.DropTable(
                name: "PriceHistories");

            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.DropTable(
                name: "Benchmarks");
        }
    }
}
