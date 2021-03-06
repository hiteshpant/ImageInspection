// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Reader.Infrastructure;

namespace Reader.Infrastructure.Migrations
{
    [DbContext(typeof(CadParsingContext))]
    [Migration("20210908121139_migration2")]
    partial class migration2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Reader.Domain.CADModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsInspected")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("CadModel");
                });

            modelBuilder.Entity("Reader.Domain.Geometry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CADModelId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CADModelId");

                    b.ToTable("Geometry");
                });

            modelBuilder.Entity("Reader.Domain.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GeometryId")
                        .HasColumnType("int");

                    b.Property<double>("X")
                        .HasColumnType("float");

                    b.Property<double>("Y")
                        .HasColumnType("float");

                    b.Property<double>("Z")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("GeometryId");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("Reader.Domain.Geometry", b =>
                {
                    b.HasOne("Reader.Domain.CADModel", null)
                        .WithMany("Geometry")
                        .HasForeignKey("CADModelId");
                });

            modelBuilder.Entity("Reader.Domain.Position", b =>
                {
                    b.HasOne("Reader.Domain.Geometry", null)
                        .WithMany("Positions")
                        .HasForeignKey("GeometryId");
                });

            modelBuilder.Entity("Reader.Domain.CADModel", b =>
                {
                    b.Navigation("Geometry");
                });

            modelBuilder.Entity("Reader.Domain.Geometry", b =>
                {
                    b.Navigation("Positions");
                });
#pragma warning restore 612, 618
        }
    }
}
