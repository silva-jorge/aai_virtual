using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AAI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Ticker = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    AssetClass = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Exchange = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Sector = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Currency = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false, defaultValue: "BRL"),
                    CurrentPrice = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: true),
                    CurrentPriceCurrency = table.Column<string>(type: "TEXT", maxLength: 3, nullable: true, defaultValue: "BRL"),
                    LastPriceUpdate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    IsManualEntry = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RiskProfile = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    InvestmentGoal = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    VolatilityTolerance = table.Column<decimal>(type: "TEXT", precision: 5, scale: 2, nullable: false),
                    TimeHorizonMonths = table.Column<int>(type: "INTEGER", nullable: false),
                    RebalanceThresholdPercent = table.Column<decimal>(type: "TEXT", precision: 5, scale: 2, nullable: false),
                    TargetAllocationJson = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    PasswordSalt = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Portfolios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Currency = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false, defaultValue: "BRL"),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Portfolios_UserProfiles_UserId",
                        column: x => x.UserId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PortfolioId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AssetId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: false),
                    AverageCost = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: false),
                    AverageCostCurrency = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false, defaultValue: "BRL"),
                    TotalInvested = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: false),
                    TotalInvestedCurrency = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false, defaultValue: "BRL"),
                    CurrentValue = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: false),
                    CurrentValueCurrency = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false, defaultValue: "BRL"),
                    AllocationPercent = table.Column<decimal>(type: "TEXT", precision: 5, scale: 2, nullable: false),
                    UnrealizedGainLoss = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: false),
                    UnrealizedGainLossCurrency = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false, defaultValue: "BRL"),
                    UnrealizedGainLossPercent = table.Column<decimal>(type: "TEXT", precision: 8, scale: 4, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Positions_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PositionId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TransactionType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Quantity = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: false),
                    UnitPrice = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: false),
                    UnitPriceCurrency = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false, defaultValue: "BRL"),
                    TotalValue = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: false),
                    TotalValueCurrency = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false, defaultValue: "BRL"),
                    Fees = table.Column<decimal>(type: "TEXT", precision: 18, scale: 8, nullable: false),
                    FeesCurrency = table.Column<string>(type: "TEXT", maxLength: 3, nullable: false, defaultValue: "BRL"),
                    TransactionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Broker = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AssetClass",
                table: "Assets",
                column: "AssetClass");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_IsActive",
                table: "Assets",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_IsDeleted",
                table: "Assets",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_Ticker",
                table: "Assets",
                column: "Ticker",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_IsDeleted",
                table: "Portfolios",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_UserId",
                table: "Portfolios",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Positions_AssetId",
                table: "Positions",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_IsDeleted",
                table: "Positions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_PortfolioId",
                table: "Positions",
                column: "PortfolioId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_PortfolioId_AssetId",
                table: "Positions",
                columns: new[] { "PortfolioId", "AssetId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_IsDeleted",
                table: "Transactions",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PositionId",
                table: "Transactions",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionDate",
                table: "Transactions",
                column: "TransactionDate");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionType",
                table: "Transactions",
                column: "TransactionType");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_IsDeleted",
                table: "UserProfiles",
                column: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Portfolios");

            migrationBuilder.DropTable(
                name: "UserProfiles");
        }
    }
}
