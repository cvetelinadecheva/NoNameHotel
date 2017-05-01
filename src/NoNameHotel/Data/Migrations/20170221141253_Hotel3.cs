using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoNameHotel.Data.Migrations
{
    public partial class Hotel3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Service",
                maxLength: 500,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "SmallPictureUrl",
                table: "Service",
                maxLength: 200,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "Service",
                maxLength: 500,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "PictureUrl",
                table: "Service",
                maxLength: 200,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "FullDescription",
                table: "Service",
                maxLength: 2000,
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Service",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SmallPictureUrl",
                table: "Service",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShortDescription",
                table: "Service",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PictureUrl",
                table: "Service",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullDescription",
                table: "Service",
                maxLength: 2000,
                nullable: true);
        }
    }
}
