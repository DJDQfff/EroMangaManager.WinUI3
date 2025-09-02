using System;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace TonquerPicacgDatabaseReverse.Download;

public partial class DownloadContext : DbContext
{
    public DownloadContext()
    {
    }

    public DownloadContext(DbContextOptions<DownloadContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Download> Downloads { get; set; }

    public virtual DbSet<DownloadEp> DownloadEps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=D:/exe/bika/data/download.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Download>(entity =>
        {
            entity.HasKey(e => e.BookId);

            entity.ToTable("download");

            entity.Property(e => e.BookId)
                .HasColumnType("varchar")
                .HasColumnName("bookId");
            entity.Property(e => e.ConvertPath)
                .HasColumnType("varchar")
                .HasColumnName("convertPath");
            entity.Property(e => e.ConvertStatus)
                .HasColumnType("varchar")
                .HasColumnName("convertStatus");
            entity.Property(e => e.CurConvertEpsId)
                .HasColumnType("INT")
                .HasColumnName("curConvertEpsId");
            entity.Property(e => e.CurDownloadEpsId)
                .HasColumnType("INT")
                .HasColumnName("curDownloadEpsId");
            entity.Property(e => e.DownloadEpsIds)
                .HasColumnType("varchar")
                .HasColumnName("downloadEpsIds");
            entity.Property(e => e.SavePath)
                .HasColumnType("varchar")
                .HasColumnName("savePath");
            entity.Property(e => e.Status)
                .HasColumnType("varchar")
                .HasColumnName("status");
            entity.Property(e => e.Tick)
                .HasColumnType("INT")
                .HasColumnName("tick");
            entity.Property(e => e.Title)
                .HasColumnType("varchar")
                .HasColumnName("title");
        });

        modelBuilder.Entity<DownloadEp>(entity =>
        {
            entity.HasKey(e => new { e.BookId, e.EpsId });

            entity.ToTable("download_eps");

            entity.Property(e => e.BookId)
                .HasColumnType("varchar")
                .HasColumnName("bookId");
            entity.Property(e => e.EpsId)
                .HasColumnType("INT")
                .HasColumnName("epsId");
            entity.Property(e => e.CurPreConvertId)
                .HasColumnType("INT")
                .HasColumnName("curPreConvertId");
            entity.Property(e => e.CurPreDownloadIndex)
                .HasColumnType("INT")
                .HasColumnName("curPreDownloadIndex");
            entity.Property(e => e.EpsTitle)
                .HasColumnType("varchar")
                .HasColumnName("epsTitle");
            entity.Property(e => e.PicCnt)
                .HasColumnType("INT")
                .HasColumnName("picCnt");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}