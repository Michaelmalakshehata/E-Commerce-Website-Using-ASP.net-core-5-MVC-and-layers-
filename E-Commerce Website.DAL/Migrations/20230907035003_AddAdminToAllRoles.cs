﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace E_Commerce_Website.DAL.Migrations
{
    public partial class AddAdminToAllRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into dbo.AspNetUserRoles (UserId,RoleId) select '229cea89-85ba-4bae-846d-4933f5096b35', Id from dbo.AspNetRoles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM dbo.AspNetUserRoles WHERE UserId='229cea89-85ba-4bae-846d-4933f5096b35'");
        }
    }
}
