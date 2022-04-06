using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DvD_Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Actor",
                columns: table => new
                {
                    ActorNumber = table.Column<int>(type: "int", nullable: false),
                    ActorName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ActorLastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Actor_PK", x => x.ActorNumber);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DVDCategory",
                columns: table => new
                {
                    CategoryNumber = table.Column<int>(type: "int", nullable: false),
                    CategoryDescription = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    AgeRestricted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("DVDCategory_PK", x => x.CategoryNumber);
                });

            migrationBuilder.CreateTable(
                name: "LoanType",
                columns: table => new
                {
                    LoanTypeNumber = table.Column<int>(type: "int", nullable: false),
                    LoanType = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("LoanType_PK", x => x.LoanTypeNumber);
                });

            migrationBuilder.CreateTable(
                name: "MembershipCategory",
                columns: table => new
                {
                    McategoryNumber = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    TotalLoans = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("MembershipCategory_PK", x => x.McategoryNumber);
                });

            migrationBuilder.CreateTable(
                name: "Producer",
                columns: table => new
                {
                    ProducerNumber = table.Column<int>(type: "int", nullable: false),
                    ProducerName = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Producer_PK", x => x.ProducerNumber);
                });

            migrationBuilder.CreateTable(
                name: "Studio",
                columns: table => new
                {
                    StudioNumber = table.Column<int>(type: "int", nullable: false),
                    StudioName = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Studio_PK", x => x.StudioNumber);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    MemberNumber = table.Column<int>(type: "int", nullable: false),
                    CategoryNumber = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false),
                    FirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Member_PK", x => x.MemberNumber);
                    table.ForeignKey(
                        name: "MembershipCategory_FK",
                        column: x => x.CategoryNumber,
                        principalTable: "MembershipCategory",
                        principalColumn: "McategoryNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DVDTitle",
                columns: table => new
                {
                    DVDNumber = table.Column<int>(type: "int", nullable: false),
                    ProducerNumber = table.Column<int>(type: "int", nullable: false),
                    CategoryNumber = table.Column<int>(type: "int", nullable: false),
                    StudioNumber = table.Column<int>(type: "int", nullable: false),
                    DateReleased = table.Column<DateTime>(type: "date", nullable: false),
                    StandardCharge = table.Column<decimal>(type: "decimal(28,0)", nullable: false),
                    PenaltyCharge = table.Column<decimal>(type: "decimal(28,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("DVDTitle_PK", x => x.DVDNumber);
                    table.ForeignKey(
                        name: "DVDCategory_FK",
                        column: x => x.CategoryNumber,
                        principalTable: "DVDCategory",
                        principalColumn: "CategoryNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Producer_FK",
                        column: x => x.ProducerNumber,
                        principalTable: "Producer",
                        principalColumn: "ProducerNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Studio_FK",
                        column: x => x.StudioNumber,
                        principalTable: "Studio",
                        principalColumn: "StudioNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CastMember",
                columns: table => new
                {
                    DVDNumber = table.Column<int>(type: "int", nullable: false),
                    ActorNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CastMember_PK", x => new { x.DVDNumber, x.ActorNumber });
                    table.ForeignKey(
                        name: "Actor_FK",
                        column: x => x.ActorNumber,
                        principalTable: "Actor",
                        principalColumn: "ActorNumber");
                    table.ForeignKey(
                        name: "DVDTitle_FK",
                        column: x => x.DVDNumber,
                        principalTable: "DVDTitle",
                        principalColumn: "DVDNumber");
                });

            migrationBuilder.CreateTable(
                name: "DVDCopy",
                columns: table => new
                {
                    CopyNumber = table.Column<int>(type: "int", nullable: false),
                    DVDNumber = table.Column<int>(type: "int", nullable: false),
                    DatePurchased = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("DVDCopy_PK", x => x.CopyNumber);
                    table.ForeignKey(
                        name: "DVDTitle_FKv2",
                        column: x => x.DVDNumber,
                        principalTable: "DVDTitle",
                        principalColumn: "DVDNumber");
                });

            migrationBuilder.CreateTable(
                name: "Loan",
                columns: table => new
                {
                    LoanNumber = table.Column<int>(type: "int", nullable: false),
                    CopyNumber = table.Column<int>(type: "int", nullable: false),
                    MemberNumber = table.Column<int>(type: "int", nullable: false),
                    DateOut = table.Column<DateTime>(type: "date", nullable: true),
                    DateDue = table.Column<DateTime>(type: "date", nullable: true),
                    DateReturned = table.Column<DateTime>(type: "date", nullable: true),
                    TypeNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Loan_PK", x => x.LoanNumber);
                    table.ForeignKey(
                        name: "DVDCopy_FK",
                        column: x => x.CopyNumber,
                        principalTable: "DVDCopy",
                        principalColumn: "CopyNumber");
                    table.ForeignKey(
                        name: "LoanType_FK",
                        column: x => x.TypeNumber,
                        principalTable: "LoanType",
                        principalColumn: "LoanTypeNumber");
                    table.ForeignKey(
                        name: "Member_FK",
                        column: x => x.MemberNumber,
                        principalTable: "Member",
                        principalColumn: "MemberNumber");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CastMember_ActorNumber",
                table: "CastMember",
                column: "ActorNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DVDCopy_DVDNumber",
                table: "DVDCopy",
                column: "DVDNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DVDTitle_CategoryNumber",
                table: "DVDTitle",
                column: "CategoryNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DVDTitle_ProducerNumber",
                table: "DVDTitle",
                column: "ProducerNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DVDTitle_StudioNumber",
                table: "DVDTitle",
                column: "StudioNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_CopyNumber",
                table: "Loan",
                column: "CopyNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_MemberNumber",
                table: "Loan",
                column: "MemberNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Loan_TypeNumber",
                table: "Loan",
                column: "TypeNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Member_CategoryNumber",
                table: "Member",
                column: "CategoryNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CastMember");

            migrationBuilder.DropTable(
                name: "Loan");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Actor");

            migrationBuilder.DropTable(
                name: "DVDCopy");

            migrationBuilder.DropTable(
                name: "LoanType");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "DVDTitle");

            migrationBuilder.DropTable(
                name: "MembershipCategory");

            migrationBuilder.DropTable(
                name: "DVDCategory");

            migrationBuilder.DropTable(
                name: "Producer");

            migrationBuilder.DropTable(
                name: "Studio");
        }
    }
}
