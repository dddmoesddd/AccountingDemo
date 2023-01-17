﻿// <auto-generated />
using System;
using AccountingVoucher.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AccountingVoucher.Infrastructure.Migrations
{
    [DbContext(typeof(VoucherContext))]
    partial class VoucherContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AccountingVoucher.Domain.Entities.Voucher", b =>
                {
                    b.Property<long>("VoucherNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsBalance")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("VoucherNumber");

                    b.ToTable("Voucher", (string)null);

                    b
                        .HasAnnotation("MySql:ValueGeneratedOnAdd", true)
                        .HasAnnotation("Sqlite:Autoincrement", true)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
                });

            modelBuilder.Entity("AccountingVoucher.Domain.Entities.Voucher", b =>
                {
                    b.OwnsOne("AccountingVoucher.Domain.ValueObjects.Description", "Description", b1 =>
                        {
                            b1.Property<long>("VoucherNumber")
                                .HasColumnType("bigint");

                            b1.Property<string>("Value")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Description");

                            b1.HasKey("VoucherNumber");

                            b1.ToTable("Voucher");

                            b1.WithOwner()
                                .HasForeignKey("VoucherNumber");
                        });

                    b.OwnsMany("AccountingVoucher.Domain.ValueObjects.VoucherItems", "VoucherItems", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<bool>("IsDeleted")
                                .HasColumnType("bit");

                            b1.Property<long>("VoucherNumber")
                                .HasColumnType("bigint");

                            b1.Property<int>("VoucherType")
                                .HasColumnType("int");

                            b1.HasKey("Id");

                            b1.HasIndex("VoucherNumber");

                            b1.ToTable("VoucherItems");

                            b1.WithOwner()
                                .HasForeignKey("VoucherNumber");

                            b1.OwnsOne("AccountingVoucher.Domain.ValueObjects.Money", "CreditorPrice", b2 =>
                                {
                                    b2.Property<int>("VoucherItemsId")
                                        .HasColumnType("int");

                                    b2.Property<decimal>("Value")
                                        .HasColumnType("decimal(18,2)")
                                        .HasColumnName("CreditorPrice");

                                    b2.HasKey("VoucherItemsId");

                                    b2.ToTable("VoucherItems");

                                    b2.WithOwner()
                                        .HasForeignKey("VoucherItemsId");
                                });

                            b1.OwnsOne("AccountingVoucher.Domain.ValueObjects.Money", "DebtorPrice", b2 =>
                                {
                                    b2.Property<int>("VoucherItemsId")
                                        .HasColumnType("int");

                                    b2.Property<decimal>("Value")
                                        .HasColumnType("decimal(18,2)")
                                        .HasColumnName("DebtorPrice");

                                    b2.HasKey("VoucherItemsId");

                                    b2.ToTable("VoucherItems");

                                    b2.WithOwner()
                                        .HasForeignKey("VoucherItemsId");
                                });

                            b1.OwnsOne("AccountingVoucher.Domain.ValueObjects.Description", "Description", b2 =>
                                {
                                    b2.Property<int>("VoucherItemsId")
                                        .HasColumnType("int");

                                    b2.Property<string>("Value")
                                        .HasColumnType("nvarchar(max)")
                                        .HasColumnName("Description");

                                    b2.HasKey("VoucherItemsId");

                                    b2.ToTable("VoucherItems");

                                    b2.WithOwner()
                                        .HasForeignKey("VoucherItemsId");
                                });

                            b1.OwnsOne("AccountingVoucher.Domain.ValueObjects.Money", "StreetPrice", b2 =>
                                {
                                    b2.Property<int>("VoucherItemsId")
                                        .HasColumnType("int");

                                    b2.Property<decimal>("Value")
                                        .HasColumnType("decimal(18,2)")
                                        .HasColumnName("StreetPrice");

                                    b2.HasKey("VoucherItemsId");

                                    b2.ToTable("VoucherItems");

                                    b2.WithOwner()
                                        .HasForeignKey("VoucherItemsId");
                                });

                            b1.Navigation("CreditorPrice");

                            b1.Navigation("DebtorPrice");

                            b1.Navigation("Description");

                            b1.Navigation("StreetPrice");
                        });

                    b.Navigation("Description");

                    b.Navigation("VoucherItems");
                });
#pragma warning restore 612, 618
        }
    }
}
