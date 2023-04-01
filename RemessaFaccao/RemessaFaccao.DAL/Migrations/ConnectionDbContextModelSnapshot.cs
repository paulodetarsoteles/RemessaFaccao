﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RemessaFaccao.DAL.Setting;

#nullable disable

namespace RemessaFaccao.DAL.Migrations
{
    [DbContext(typeof(ConnectionDbContext))]
    partial class ConnectionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            //modelBuilder.Entity("RemessaFaccao.DAL.Models.Faccao", b =>
            //    {
            //        b.Property<int>("FaccaoId")
            //            .ValueGeneratedOnAdd()
            //            .HasColumnType("int");

            //        SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FaccaoId"));

            //        b.Property<bool>("Ativo")
            //            .HasColumnType("bit");

            //        b.Property<string>("Email")
            //            .HasColumnType("nvarchar(max)");

            //        b.Property<string>("Endereco")
            //            .HasColumnType("nvarchar(max)");

            //        b.Property<string>("FormaDePagamento")
            //            .HasColumnType("nvarchar(max)");

            //        b.Property<string>("Nome")
            //            .IsRequired()
            //            .HasColumnType("nvarchar(max)");

            //        b.Property<string>("Observacoes")
            //            .HasColumnType("nvarchar(max)");

            //        b.Property<int>("Qualificacao")
            //            .HasColumnType("int");

            //        b.Property<string>("Telefone1")
            //            .HasColumnType("nvarchar(max)");

            //        b.Property<string>("Telefone2")
            //            .HasColumnType("nvarchar(max)");

            //        b.HasKey("FaccaoId");

            //        b.ToTable("Faccao");
            //    });

            //modelBuilder.Entity("RemessaFaccao.DAL.Models.Perfil", b =>
            //    {
            //        b.Property<int>("PerfilId")
            //            .ValueGeneratedOnAdd()
            //            .HasColumnType("int");

            //        SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PerfilId"));

            //        b.Property<string>("Nome")
            //            .IsRequired()
            //            .HasColumnType("nvarchar(max)");

            //        b.HasKey("PerfilId");

            //        b.ToTable("Perfil");
            //    });

            //modelBuilder.Entity("RemessaFaccao.DAL.Models.Remessa", b =>
            //    {
            //        b.Property<int>("RemessaId")
            //            .ValueGeneratedOnAdd()
            //            .HasColumnType("int");

            //        SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RemessaId"));

            //        b.Property<DateTime?>("DataDeEntrega")
            //            .HasColumnType("datetime2");

            //        b.Property<DateTime?>("DataPrazo")
            //            .HasColumnType("datetime2");

            //        b.Property<DateTime?>("DataRecebimento")
            //            .HasColumnType("datetime2");

            //        b.Property<int?>("FaccaoId")
            //            .HasColumnType("int");

            //        b.Property<string>("Observacoes")
            //            .HasColumnType("nvarchar(max)");

            //        b.Property<int>("Quantidade")
            //            .HasColumnType("int");

            //        b.Property<string>("Referencia")
            //            .IsRequired()
            //            .HasColumnType("nvarchar(max)");

            //        b.Property<int>("StatusRemessa")
            //            .HasColumnType("int");

            //        b.Property<decimal>("ValorTotal")
            //            .HasColumnType("decimal(18,2)");

            //        b.Property<decimal>("ValorUnitario")
            //            .HasColumnType("decimal(18,2)");

            //        b.HasKey("RemessaId");

            //        b.ToTable("Remessa");
            //    });

            //modelBuilder.Entity("RemessaFaccao.DAL.Models.Usuario", b =>
            //    {
            //        b.Property<int>("UsuarioId")
            //            .ValueGeneratedOnAdd()
            //            .HasColumnType("int");

            //        SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsuarioId"));

            //        b.Property<string>("LoginUsuario")
            //            .IsRequired()
            //            .HasColumnType("nvarchar(max)");

            //        b.Property<string>("Nome")
            //            .IsRequired()
            //            .HasColumnType("nvarchar(max)");

            //        b.Property<string>("Observacoes")
            //            .HasColumnType("nvarchar(max)");

            //        b.Property<int>("PerfilId")
            //            .HasColumnType("int");

            //        b.Property<string>("Senha")
            //            .IsRequired()
            //            .HasColumnType("nvarchar(max)");

            //        b.HasKey("UsuarioId");

            //        b.ToTable("Usuario");
            //    });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
