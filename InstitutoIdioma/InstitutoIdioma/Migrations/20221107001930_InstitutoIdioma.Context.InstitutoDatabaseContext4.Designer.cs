﻿// <auto-generated />
using System;
using InstitutoIdioma.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InstitutoIdioma.Migrations
{
    [DbContext(typeof(InstitutoDatabaseContext))]
    [Migration("20221107001930_InstitutoIdioma.Context.InstitutoDatabaseContext4")]
    partial class InstitutoIdiomaContextInstitutoDatabaseContext4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.30")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("InstitutoIdioma.Models.Examen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("EstaAprobado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Examenes");
                });

            modelBuilder.Entity("InstitutoIdioma.Models.Opcion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("EsCorrecta")
                        .HasColumnType("bit");

                    b.Property<int?>("PreguntaId")
                        .HasColumnType("int");

                    b.Property<string>("Texto")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PreguntaId");

                    b.ToTable("Opciones");
                });

            modelBuilder.Entity("InstitutoIdioma.Models.Pregunta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Enunciado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ExamenId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ExamenId");

                    b.ToTable("preguntas");
                });

            modelBuilder.Entity("InstitutoIdioma.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contrasenia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dni")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<int>("Nivel")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreUsuario")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoPerfil")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("InstitutoIdioma.Models.Examen", b =>
                {
                    b.HasOne("InstitutoIdioma.Models.Usuario", null)
                        .WithMany("Examenes")
                        .HasForeignKey("UsuarioId");
                });

            modelBuilder.Entity("InstitutoIdioma.Models.Opcion", b =>
                {
                    b.HasOne("InstitutoIdioma.Models.Pregunta", "Pregunta")
                        .WithMany("Opciones")
                        .HasForeignKey("PreguntaId")
                        .HasConstraintName("ForeignKey_Opciones_Pregunta")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InstitutoIdioma.Models.Pregunta", b =>
                {
                    b.HasOne("InstitutoIdioma.Models.Examen", "Examen")
                        .WithMany("Preguntas")
                        .HasForeignKey("ExamenId")
                        .HasConstraintName("ForeignKey_Preguntas_Examen")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
