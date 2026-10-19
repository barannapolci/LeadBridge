using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeadBridge.Core.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Avatars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AvatarLink = table.Column<string>(type: "character varying(2200)", maxLength: 2200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avatars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    LastName = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Country = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Region = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    City = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessTypePersonalInfo",
                columns: table => new
                {
                    BusinessTypesId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonalInfosId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessTypePersonalInfo", x => new { x.BusinessTypesId, x.PersonalInfosId });
                    table.ForeignKey(
                        name: "FK_BusinessTypePersonalInfo_BusinessTypes_BusinessTypesId",
                        column: x => x.BusinessTypesId,
                        principalTable: "BusinessTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessTypePersonalInfo_PersonalInfos_PersonalInfosId",
                        column: x => x.PersonalInfosId,
                        principalTable: "PersonalInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    IsEmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PersonalInfoId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserRole = table.Column<int>(type: "integer", nullable: false),
                    RegisterDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AvgReviews = table.Column<double>(type: "double precision", nullable: false),
                    AvatarId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Avatars_AvatarId",
                        column: x => x.AvatarId,
                        principalTable: "Avatars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_PersonalInfos_PersonalInfoId",
                        column: x => x.PersonalInfoId,
                        principalTable: "PersonalInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailConfirmation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ConfirmationToken = table.Column<string>(type: "text", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailConfirmation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailConfirmation_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BusinessTypes",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "Авторизовані сервісні центри та великі мережі технічного обслуговування.", "Великі СТО (мережеві)" },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "Локальні точки швидкого ремонту та обслуговування.", "Придорожні майстерні та шиномонтажі" },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "СТО, що спеціалізуються виключно на TIR та комерційному транспорті (TIR-сервіси).", "Спеціалізовані вантажні сервіси" },
                    { new Guid("20000000-0000-0000-0000-000000000001"), "Компанії з власним парком фур та комерційних авто.", "Логістичні компанії (Вантажоперевезення)" },
                    { new Guid("20000000-0000-0000-0000-000000000002"), "Компанії з великим парком легкових автомобілів (міська логістика).", "Служби таксі та доставки" },
                    { new Guid("20000000-0000-0000-0000-000000000003"), "Міжміські та міські пасажирські перевізники.", "Автобусні парки (Пасажирські перевезення)" },
                    { new Guid("30000000-0000-0000-0000-000000000001"), "Великі підприємства з масивним парком важкої сільгосптехніки (трактори, комбайни).", "Агрохолдинги" },
                    { new Guid("30000000-0000-0000-0000-000000000002"), "Локальні фермери з невеликим парком техніки.", "Приватні фермерські господарства" },
                    { new Guid("40000000-0000-0000-0000-000000000001"), "Компанії з важкою будівельною технікою (крани, бульдозери, самоскиди).", "Будівельні підрядники" },
                    { new Guid("40000000-0000-0000-0000-000000000002"), "Компанії, що здають техніку в оренду та самостійно її обслуговують.", "Прокат та оренда спецтехніки" },
                    { new Guid("50000000-0000-0000-0000-000000000001"), "Підприємства, що потребують індустріальних мастил для станків та конвеєрів.", "Заводи (Важка промисловість)" },
                    { new Guid("50000000-0000-0000-0000-000000000002"), "Підприємства з надважкою видобувною технікою.", "Видобувна промисловість (Кар'єри, Шахти)" },
                    { new Guid("60000000-0000-0000-0000-000000000001"), "Оптові та дрібнооптові закупівлі для подальшого роздрібного перепродажу.", "Автомагазини та ринки" },
                    { new Guid("60000000-0000-0000-0000-000000000002"), "Приватні автозаправні станції, що реалізують мастила.", "Локальні АЗС" },
                    { new Guid("70000000-0000-0000-0000-000000000001"), "Автодори, водоканали, прибирання сміття.", "Державний сектор та Комунальні підприємства" },
                    { new Guid("70000000-0000-0000-0000-000000000002"), "Військові частини та обслуговування оборонної техніки.", "Оборонний сектор та ЗСУ" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessTypePersonalInfo_PersonalInfosId",
                table: "BusinessTypePersonalInfo",
                column: "PersonalInfosId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailConfirmation_UserId",
                table: "EmailConfirmation",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RecipientId",
                table: "Reviews",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_SenderId_RecipientId",
                table: "Reviews",
                columns: new[] { "SenderId", "RecipientId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AvatarId",
                table: "Users",
                column: "AvatarId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonalInfoId",
                table: "Users",
                column: "PersonalInfoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessTypePersonalInfo");

            migrationBuilder.DropTable(
                name: "EmailConfirmation");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "BusinessTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Avatars");

            migrationBuilder.DropTable(
                name: "PersonalInfos");
        }
    }
}
