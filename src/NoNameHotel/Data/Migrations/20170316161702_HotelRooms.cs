using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NoNameHotel.Data.Migrations
{
    public partial class HotelRooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Picture2",
                table: "Category",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Picture3",
                table: "Category",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Picture4",
                table: "Category",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture2",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "Picture3",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "Picture4",
                table: "Category");
        }
    }
}
