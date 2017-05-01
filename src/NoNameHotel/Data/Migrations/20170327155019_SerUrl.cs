using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoNameHotel.Data.Migrations
{
    public partial class SerUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureUrl2",
                table: "Service",
                maxLength: 200,
                nullable: false,
                defaultValue: "");



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl2",
                table: "Service");
        }
    }
}
