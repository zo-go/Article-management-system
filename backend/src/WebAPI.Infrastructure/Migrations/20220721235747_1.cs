using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Infrastructure.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "app_article_class",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    app_article_classname = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    display_order = table.Column<int>(type: "integer", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_article_class", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_audit_log",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    parameters = table.Column<string>(type: "text", nullable: true),
                    browser_info = table.Column<string>(type: "text", nullable: true),
                    client_name = table.Column<string>(type: "text", nullable: true),
                    client_ip_address = table.Column<string>(type: "text", nullable: true),
                    execution_duration = table.Column<int>(type: "integer", nullable: false),
                    execution_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    return_value = table.Column<string>(type: "text", nullable: true),
                    exception = table.Column<string>(type: "text", nullable: true),
                    service_name = table.Column<string>(type: "text", nullable: true),
                    user_info = table.Column<string>(type: "text", nullable: true),
                    custom_data = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    display_order = table.Column<int>(type: "integer", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true),
                    MethodName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_audit_log", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_exception_log",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    short_message = table.Column<string>(type: "text", nullable: false),
                    full_message = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    display_order = table.Column<int>(type: "integer", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_exception_log", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_file_information",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    origin_file_name = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    current_file_name = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    relative_path = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    file_size = table.Column<long>(type: "bigint", nullable: false),
                    file_type = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    display_order = table.Column<int>(type: "integer", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_file_information", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_identity_user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", maxLength: 2000, nullable: false),
                    user_name = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    refresh_token = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    refresh_token_expiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    display_order = table.Column<int>(type: "integer", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_identity_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_name = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    display_order = table.Column<int>(type: "integer", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    user_sex = table.Column<string>(type: "text", nullable: true),
                    user_birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    email = table.Column<string>(type: "text", nullable: true),
                    user_address = table.Column<string>(type: "text", nullable: true),
                    avatar = table.Column<string>(type: "text", nullable: true),
                    nickname = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    display_order = table.Column<int>(type: "integer", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "app_article",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    article_title = table.Column<string>(type: "text", nullable: false),
                    article_second_title = table.Column<string>(type: "text", nullable: true),
                    article_content = table.Column<string>(type: "text", nullable: true),
                    document = table.Column<string>(type: "text", nullable: true),
                    app_article_class_id = table.Column<Guid>(type: "uuid", nullable: false),
                    app_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    article_praise = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    display_order = table.Column<int>(type: "integer", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true),
                    viewstsNumber = table.Column<long>(type: "bigint", nullable: false),
                    CommentsNumber = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_article", x => x.id);
                    table.ForeignKey(
                        name: "FK_app_article_app_article_class_app_article_class_id",
                        column: x => x.app_article_class_id,
                        principalTable: "app_article_class",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_app_article_app_user_app_user_id",
                        column: x => x.app_user_id,
                        principalTable: "app_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "app_comment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_comment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    comment_content = table.Column<string>(type: "text", nullable: true),
                    app_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    app_Article_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    display_order = table.Column<int>(type: "integer", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_comment", x => x.id);
                    table.ForeignKey(
                        name: "FK_app_comment_app_user_app_user_id",
                        column: x => x.app_user_id,
                        principalTable: "app_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "app_user_role",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    app_user_id = table.Column<Guid>(type: "uuid", maxLength: 2000, nullable: false),
                    app_role_id = table.Column<Guid>(type: "uuid", maxLength: 2000, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: true),
                    updated_by = table.Column<Guid>(type: "uuid", nullable: true),
                    is_actived = table.Column<bool>(type: "boolean", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    display_order = table.Column<int>(type: "integer", nullable: false),
                    remarks = table.Column<string>(type: "character varying(800)", maxLength: 800, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_app_user_role", x => x.id);
                    table.ForeignKey(
                        name: "FK_app_user_role_app_role_app_role_id",
                        column: x => x.app_role_id,
                        principalTable: "app_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_app_user_role_app_user_app_user_id",
                        column: x => x.app_user_id,
                        principalTable: "app_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_app_article_app_article_class_id",
                table: "app_article",
                column: "app_article_class_id");

            migrationBuilder.CreateIndex(
                name: "IX_app_article_app_user_id",
                table: "app_article",
                column: "app_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_app_comment_app_user_id",
                table: "app_comment",
                column: "app_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_app_user_role_app_role_id",
                table: "app_user_role",
                column: "app_role_id");

            migrationBuilder.CreateIndex(
                name: "IX_app_user_role_app_user_id",
                table: "app_user_role",
                column: "app_user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "app_article");

            migrationBuilder.DropTable(
                name: "app_audit_log");

            migrationBuilder.DropTable(
                name: "app_comment");

            migrationBuilder.DropTable(
                name: "app_exception_log");

            migrationBuilder.DropTable(
                name: "app_file_information");

            migrationBuilder.DropTable(
                name: "app_identity_user");

            migrationBuilder.DropTable(
                name: "app_user_role");

            migrationBuilder.DropTable(
                name: "app_article_class");

            migrationBuilder.DropTable(
                name: "app_role");

            migrationBuilder.DropTable(
                name: "app_user");
        }
    }
}
