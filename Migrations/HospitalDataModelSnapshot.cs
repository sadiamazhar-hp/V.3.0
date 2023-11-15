﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using V._3._0.App_Data;

#nullable disable

namespace V._3._0.Migrations
{
    [DbContext(typeof(HospitalData))]
    partial class HospitalDataModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("V._3._0.Models.MedicalInfo", b =>
                {
                    b.Property<int>("MedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MedId"));

                    b.Property<byte[]>("ImageFile")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageFileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientsId")
                        .HasColumnType("int");

                    b.HasKey("MedId");

                    b.HasIndex("PatientsId");

                    b.ToTable("MedicalInfo");
                });

            modelBuilder.Entity("V._3._0.Models.PatientApp", b =>
                {
                    b.Property<int>("App")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("App"));

                    b.Property<DateTime>("AppDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("AppTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("PatientsId")
                        .HasColumnType("int");

                    b.Property<string>("Process")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("App");

                    b.HasIndex("PatientsId");

                    b.ToTable("PatientApp");
                });

            modelBuilder.Entity("V._3._0.Models.PatientPayment", b =>
                {
                    b.Property<int>("PayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PayId"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<int>("PatientsId")
                        .HasColumnType("int");

                    b.HasKey("PayId");

                    b.HasIndex("PatientsId");

                    b.ToTable("PatientPayments");
                });

            modelBuilder.Entity("V._3._0.Models.Patients", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfAdm")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfDis")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Roomno")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("V._3._0.Models.PaymentDetail", b =>
                {
                    b.Property<int>("Sno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Sno"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("PatientPaymentPayId")
                        .HasColumnType("int");

                    b.Property<int>("PaymentID")
                        .HasColumnType("int");

                    b.Property<string>("procedure")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Sno");

                    b.HasIndex("PatientPaymentPayId");

                    b.ToTable("PaymentDetails");
                });

            modelBuilder.Entity("V._3._0.Models.PersonalInfo", b =>
                {
                    b.Property<int>("PIId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PIId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("Contact")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FatherName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatId")
                        .HasColumnType("int");

                    b.HasKey("PIId");

                    b.HasIndex("PatId")
                        .IsUnique();

                    b.ToTable("PersonalInfo");
                });

            modelBuilder.Entity("V._3._0.Models.SignUp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HospitalName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HosUser");
                });

            modelBuilder.Entity("V._3._0.Models.MedicalInfo", b =>
                {
                    b.HasOne("V._3._0.Models.Patients", "Patients")
                        .WithMany("MedicalInfo")
                        .HasForeignKey("PatientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patients");
                });

            modelBuilder.Entity("V._3._0.Models.PatientApp", b =>
                {
                    b.HasOne("V._3._0.Models.Patients", "Patients")
                        .WithMany("PatientApp")
                        .HasForeignKey("PatientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patients");
                });

            modelBuilder.Entity("V._3._0.Models.PatientPayment", b =>
                {
                    b.HasOne("V._3._0.Models.Patients", "Patients")
                        .WithMany("PatientPayments")
                        .HasForeignKey("PatientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patients");
                });

            modelBuilder.Entity("V._3._0.Models.PaymentDetail", b =>
                {
                    b.HasOne("V._3._0.Models.PatientPayment", "PatientPayment")
                        .WithMany("PaymentDetails")
                        .HasForeignKey("PatientPaymentPayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PatientPayment");
                });

            modelBuilder.Entity("V._3._0.Models.PersonalInfo", b =>
                {
                    b.HasOne("V._3._0.Models.Patients", "Patients")
                        .WithOne("PersonalInfo")
                        .HasForeignKey("V._3._0.Models.PersonalInfo", "PatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patients");
                });

            modelBuilder.Entity("V._3._0.Models.PatientPayment", b =>
                {
                    b.Navigation("PaymentDetails");
                });

            modelBuilder.Entity("V._3._0.Models.Patients", b =>
                {
                    b.Navigation("MedicalInfo");

                    b.Navigation("PatientApp");

                    b.Navigation("PatientPayments");

                    b.Navigation("PersonalInfo")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
