using Microsoft.EntityFrameworkCore;

namespace ReserseDownloadsData;

public partial class BookContext : DbContext
{
    public BookContext () { }

    public BookContext (DbContextOptions<BookContext> options)
        : base(options) { }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<System> Systems { get; set; }

    public virtual DbSet<Word> Words { get; set; }

    protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        =>
        optionsBuilder.UseSqlite(
            "Data Source=D:\\Projects\\EroMangaManager\\ReverseDownloadsData\\source\\bika_v1.4.2_windows_x64\\book.db"
        );

    protected override void OnModelCreating (ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("book");

            entity.Property(e => e.Id).HasColumnType("varchar").HasColumnName("id");
            entity.Property(e => e.Author).HasColumnType("varchar").HasColumnName("author");
            entity.Property(e => e.Categories).HasColumnType("varchar").HasColumnName("categories");
            entity
                .Property(e => e.ChineseTeam)
                .HasColumnType("varchar")
                .HasColumnName("chineseTeam");
            entity.Property(e => e.CreatedAt).HasColumnType("varchar").HasColumnName("created_at");
            entity.Property(e => e.Creator).HasColumnType("varchar").HasColumnName("creator");
            entity
                .Property(e => e.Description)
                .HasColumnType("varchar")
                .HasColumnName("description");
            entity.Property(e => e.EpsCount).HasColumnType("INT").HasColumnName("epsCount");
            entity.Property(e => e.FileServer).HasColumnType("varchar").HasColumnName("fileServer");
            entity.Property(e => e.Finished).HasColumnType("INT").HasColumnName("finished");
            entity.Property(e => e.LikesCount).HasColumnType("INT").HasColumnName("likesCount");
            entity.Property(e => e.Pages).HasColumnType("INT").HasColumnName("pages");
            entity.Property(e => e.Path).HasColumnType("varchar").HasColumnName("path");
            entity.Property(e => e.Tags).HasColumnType("varchar").HasColumnName("tags");
            entity.Property(e => e.Title).HasColumnType("varchar").HasColumnName("title");
            entity.Property(e => e.Title2).HasColumnType("varchar").HasColumnName("title2");
            entity.Property(e => e.TotalLikes).HasColumnType("INT").HasColumnName("totalLikes");
            entity.Property(e => e.TotalViews).HasColumnType("INT").HasColumnName("totalViews");
            entity.Property(e => e.UpdatedAt).HasColumnType("varchar").HasColumnName("updated_at");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasNoKey().ToTable("category");

            entity.Property(e => e.BookId).HasColumnType("VARCHAR").HasColumnName("bookId");
            entity.Property(e => e.Category1).HasColumnType("INT").HasColumnName("category");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasNoKey().ToTable("favorite");

            entity.HasIndex(e => new { e.Id , e.User } , "id_user").IsUnique();

            entity.Property(e => e.Id).HasColumnType("VARCHAR").HasColumnName("id");
            entity.Property(e => e.SortId).HasColumnType("BIGINT").HasColumnName("sortId");
            entity.Property(e => e.User).HasColumnType("VARCHAR").HasColumnName("user");
        });

        modelBuilder.Entity<System>(entity =>
        {
            entity.ToTable("system");

            entity.Property(e => e.Id).HasColumnType("VARCHAR").HasColumnName("id");
            entity.Property(e => e.Size).HasColumnType("INT").HasColumnName("size");
            entity.Property(e => e.SubVersion).HasColumnType("BIGINT").HasColumnName("sub_version");
            entity.Property(e => e.Time).HasColumnType("VARCHAR").HasColumnName("time");
        });

        modelBuilder.Entity<Word>(entity =>
        {
            entity.ToTable("words");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Word1).HasColumnType("VARCHAR").HasColumnName("word");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial (ModelBuilder modelBuilder);
}
