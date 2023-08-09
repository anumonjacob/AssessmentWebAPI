using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using pasAPI.Models;

namespace pasAPI.Data;

public partial class PasDbContext : DbContext
{
    public PasDbContext()
    {
    }

    public PasDbContext(DbContextOptions<PasDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aocolumn> Aocolumns { get; set; }

    public virtual DbSet<Field> Fields { get; set; }

    public virtual DbSet<Form> Forms { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aocolumn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_AOColumn_Id");

            entity.ToTable("AOColumn", tb => tb.HasTrigger("tr_AOColumn_Delete"));

            entity.HasIndex(e => e.Name, "ix_AOColumn_Name");

            entity.HasIndex(e => e.TableId, "ix_AOColumn_TableId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Comment).HasMaxLength(2048);
            entity.Property(e => e.DataType).HasMaxLength(128);
            entity.Property(e => e.Description).HasMaxLength(128);
            entity.Property(e => e.Distortion).HasMaxLength(64);
            entity.Property(e => e.Name).HasMaxLength(128);
            entity.Property(e => e.Type).HasMaxLength(128);
        });

        modelBuilder.Entity<Field>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Field_Id");

            entity.ToTable("Field");

            entity.HasIndex(e => e.ColumnId, "ix_Field_ColumnId");

            entity.HasIndex(e => e.FormId, "ix_Field_FormId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AddChangeDeleteFlag)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.AmendablePostIssuance).HasDefaultValueSql("((1))");
            entity.Property(e => e.AmendablePreRenewal).HasDefaultValueSql("((1))");
            entity.Property(e => e.AuditCondition).HasColumnType("ntext");
            entity.Property(e => e.AuditViewOnly).HasDefaultValueSql("((0))");
            entity.Property(e => e.Auditable).HasDefaultValueSql("((0))");
            entity.Property(e => e.Comment).HasMaxLength(2048);
            entity.Property(e => e.Condition).HasColumnType("ntext");
            entity.Property(e => e.Default).HasMaxLength(2048);
            entity.Property(e => e.DialogFileName).HasMaxLength(128);
            entity.Property(e => e.DialogFileType).HasMaxLength(3);
            entity.Property(e => e.DisplayColumns).HasMaxLength(128);
            entity.Property(e => e.DisplayController).HasDefaultValueSql("((0))");
            entity.Property(e => e.Help).HasMaxLength(128);
            entity.Property(e => e.HelpText).HasColumnType("ntext");
            entity.Property(e => e.Label).HasMaxLength(256);
            entity.Property(e => e.LinkText).HasMaxLength(128);
            entity.Property(e => e.Mask).HasMaxLength(128);
            entity.Property(e => e.Maximum).HasMaxLength(128);
            entity.Property(e => e.Minimum).HasMaxLength(128);
            entity.Property(e => e.PolicyDisabled).HasDefaultValueSql("((0))");
            entity.Property(e => e.PolicyDisplay).HasDefaultValueSql("((1))");
            entity.Property(e => e.PolicyReadOnly).HasDefaultValueSql("((0))");
            entity.Property(e => e.PolicyRequired).HasDefaultValueSql("((0))");
            entity.Property(e => e.QuoteDisabled).HasDefaultValueSql("((0))");
            entity.Property(e => e.QuoteDisplay).HasDefaultValueSql("((1))");
            entity.Property(e => e.QuoteReadOnly).HasDefaultValueSql("((0))");
            entity.Property(e => e.QuoteRequired).HasDefaultValueSql("((0))");
            entity.Property(e => e.RequiredCondition).HasColumnType("ntext");
            entity.Property(e => e.TextAreaCols).HasDefaultValueSql("((0))");
            entity.Property(e => e.TextAreaRows).HasDefaultValueSql("((0))");
            entity.Property(e => e.Type).HasMaxLength(128);
            entity.Property(e => e.XslValue).HasMaxLength(2048);

            entity.HasOne(d => d.Column).WithMany(p => p.Fields)
                .HasForeignKey(d => d.ColumnId)
                .HasConstraintName("fk_Field_Column");

            entity.HasOne(d => d.Form).WithMany(p => p.Fields)
                .HasForeignKey(d => d.FormId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_Field_Form");
        });

        modelBuilder.Entity<Form>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_Form_Id");

            entity.ToTable("Form");

            entity.HasIndex(e => e.RatebookId, "ix_Form_RatebookId");

            entity.HasIndex(e => e.TableId, "ix_Form_TableId");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AddChangeDeleteFlag)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.BtnCndAdd).HasMaxLength(128);
            entity.Property(e => e.BtnCndCopy).HasMaxLength(128);
            entity.Property(e => e.BtnCndDelete).HasMaxLength(128);
            entity.Property(e => e.BtnCndModify).HasMaxLength(128);
            entity.Property(e => e.BtnCndRenumber).HasMaxLength(128);
            entity.Property(e => e.BtnCndView).HasMaxLength(128);
            entity.Property(e => e.BtnCndViewDetail).HasMaxLength(128);
            entity.Property(e => e.BtnLblAdd).HasMaxLength(128);
            entity.Property(e => e.BtnLblCopy).HasMaxLength(128);
            entity.Property(e => e.BtnLblDelete).HasMaxLength(128);
            entity.Property(e => e.BtnLblModify).HasMaxLength(128);
            entity.Property(e => e.BtnLblRenumber).HasMaxLength(128);
            entity.Property(e => e.BtnLblView).HasMaxLength(128);
            entity.Property(e => e.BtnLblViewDetail).HasMaxLength(128);
            entity.Property(e => e.BtnResAdd).HasMaxLength(128);
            entity.Property(e => e.BtnResCopy).HasMaxLength(128);
            entity.Property(e => e.BtnResDelete).HasMaxLength(128);
            entity.Property(e => e.BtnResModify).HasMaxLength(128);
            entity.Property(e => e.BtnResRenumber).HasMaxLength(128);
            entity.Property(e => e.BtnResView).HasMaxLength(128);
            entity.Property(e => e.BtnResViewDetail).HasMaxLength(128);
            entity.Property(e => e.Comment).HasMaxLength(2048);
            entity.Property(e => e.Condition).HasColumnType("ntext");
            entity.Property(e => e.HelpText).HasColumnType("ntext");
            entity.Property(e => e.Hidden).HasDefaultValueSql("((0))");
            entity.Property(e => e.HidePremium).HasDefaultValueSql("((0))");
            entity.Property(e => e.MaxOccurs).HasDefaultValueSql("((99))");
            entity.Property(e => e.MinOccurs).HasDefaultValueSql("((0))");
            entity.Property(e => e.Name).HasMaxLength(128);
            entity.Property(e => e.Number).HasMaxLength(128);
            entity.Property(e => e.SubSequence).HasDefaultValueSql("((1))");
            entity.Property(e => e.TabCondition).HasColumnType("ntext");
            entity.Property(e => e.TabResourceName).HasMaxLength(128);
            entity.Property(e => e.TemplateFile).HasMaxLength(128);
            entity.Property(e => e.Type).HasMaxLength(128);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
