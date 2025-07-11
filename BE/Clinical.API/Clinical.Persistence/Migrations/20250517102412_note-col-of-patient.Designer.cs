﻿// <auto-generated />
using System;
using Clinical.Persistence.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Clinical.Persistence.Migrations
{
    [DbContext(typeof(ClinicalDbContext))]
    [Migration("20250517102412_note-col-of-patient")]
    partial class notecolofpatient
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Clinical.Domain.Entitites.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreateBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<int?>("ModifyBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Doctors", (string)null);
                });

            modelBuilder.Entity("Clinical.Domain.Entitites.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int?>("CreateBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("LowerLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MedicalTreatmentDepartment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ModifyBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TreatmentIndication")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.ToTable("Patients", (string)null);
                });

            modelBuilder.Entity("Clinical.Domain.Entitites.PatientPrescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreateBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<string>("MedicineName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ModifyBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("NumberOfPillsPerDose")
                        .HasColumnType("int");

                    b.Property<int?>("NumberOfTimesPerDay")
                        .HasColumnType("int");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("PatientPrescriptions", (string)null);
                });

            modelBuilder.Entity("Clinical.Domain.Entitites.Patient", b =>
                {
                    b.HasOne("Clinical.Domain.Entitites.Doctor", "Doctor")
                        .WithMany("Patients")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("Clinical.Domain.Entitites.PatientPrescription", b =>
                {
                    b.HasOne("Clinical.Domain.Entitites.Patient", "Patient")
                        .WithMany("PatientPrescriptions")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Clinical.Domain.Entitites.Doctor", b =>
                {
                    b.Navigation("Patients");
                });

            modelBuilder.Entity("Clinical.Domain.Entitites.Patient", b =>
                {
                    b.Navigation("PatientPrescriptions");
                });
#pragma warning restore 612, 618
        }
    }
}
