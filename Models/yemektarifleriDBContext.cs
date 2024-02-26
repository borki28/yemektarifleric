using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace yemektarifleric.Models
{
    public partial class yemektarifleriDBContext : DbContext
    {
        public yemektarifleriDBContext()
        {
        }

        public yemektarifleriDBContext(DbContextOptions<yemektarifleriDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Kategoriler> Kategorilers { get; set; }
        public virtual DbSet<Kullanicilar> Kullanicilars { get; set; }
        public virtual DbSet<Menuler> Menulers { get; set; }
        public virtual DbSet<Sayfalar> Sayfalars { get; set; }
        public virtual DbSet<YemekTarifleri> YemekTarifleris { get; set; }
        public virtual DbSet<Yorumlar> Yorumlars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer("Server=KARAKUS;Database=yemektarifleriDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Turkish_CI_AS");

            modelBuilder.Entity<Kategoriler>(entity =>
            {
                entity.HasKey(e => e.KategoriId)
                    .HasName("PK__Kategori__1D9181BD4D8ABF2F");

                entity.ToTable("Kategoriler");

                entity.Property(e => e.KategoriId).HasColumnName("kategoriID");

                entity.Property(e => e.Aktif).HasColumnName("aktif");

                entity.Property(e => e.Kategoriadi)
                    .HasMaxLength(100)
                    .HasColumnName("kategoriadi");

                entity.Property(e => e.Silindi).HasColumnName("silindi");
            });

            modelBuilder.Entity<Kullanicilar>(entity =>
            {
                entity.HasKey(e => e.KullaniciId)
                    .HasName("PK__Kullanic__848DC54B37B5EF2C");

                entity.ToTable("Kullanicilar");

                entity.Property(e => e.KullaniciId).HasColumnName("kullaniciID");

                entity.Property(e => e.Adi)
                    .HasMaxLength(100)
                    .HasColumnName("adi");

                entity.Property(e => e.Aktif).HasColumnName("aktif");

                entity.Property(e => e.Eposta)
                    .HasMaxLength(100)
                    .HasColumnName("eposta");

                entity.Property(e => e.Parola)
                    .HasMaxLength(35)
                    .HasColumnName("parola");

                entity.Property(e => e.Silindi).HasColumnName("silindi");

                entity.Property(e => e.Soyadi)
                    .HasMaxLength(100)
                    .HasColumnName("soyadi");

                entity.Property(e => e.Telefon)
                    .HasMaxLength(15)
                    .HasColumnName("telefon");

                entity.Property(e => e.Yetki).HasColumnName("yetki");
            });

            modelBuilder.Entity<Menuler>(entity =>
            {
                entity.HasKey(e => e.MenuId)
                    .HasName("PK__Menuler__3B407174D08C333D");

                entity.ToTable("Menuler");

                entity.Property(e => e.MenuId).HasColumnName("menuId");

                entity.Property(e => e.Aktif).HasColumnName("aktif");

                entity.Property(e => e.Baslik)
                    .HasMaxLength(250)
                    .HasColumnName("baslik");

                entity.Property(e => e.Silindi).HasColumnName("silindi");

                entity.Property(e => e.Sira).HasColumnName("sira");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .HasColumnName("url");

                entity.Property(e => e.UstId).HasColumnName("ustID");
            });

            modelBuilder.Entity<Sayfalar>(entity =>
            {
                entity.HasKey(e => e.SayfaId)
                    .HasName("PK__Sayfalar__F81FC2B2F6A1FDBB");

                entity.ToTable("Sayfalar");

                entity.Property(e => e.SayfaId).HasColumnName("sayfaID");

                entity.Property(e => e.Aktif).HasColumnName("aktif");

                entity.Property(e => e.Baslik)
                    .HasMaxLength(250)
                    .HasColumnName("baslik");

                entity.Property(e => e.Icerik)
                    .HasColumnType("ntext")
                    .HasColumnName("icerik");

                entity.Property(e => e.Silindi).HasColumnName("silindi");
            });

            modelBuilder.Entity<YemekTarifleri>(entity =>
            {
                entity.HasKey(e => e.TarifId)
                    .HasName("PK__YemekTar__87FD2128F88EE808");

                entity.ToTable("YemekTarifleri");

                entity.Property(e => e.TarifId).HasColumnName("tarifID");

                entity.Property(e => e.Aktif).HasColumnName("aktif");

                entity.Property(e => e.Eklemetarihi)
                    .HasColumnType("datetime")
                    .HasColumnName("eklemetarihi");

                entity.Property(e => e.KategoriId).HasColumnName("kategoriID");

                entity.Property(e => e.Silindi).HasColumnName("silindi");

                entity.Property(e => e.Sira).HasColumnName("sira");

                entity.Property(e => e.Tarif)
                    .HasColumnType("ntext")
                    .HasColumnName("tarif");

                entity.Property(e => e.Yemekadi)
                    .HasMaxLength(250)
                    .HasColumnName("yemekadi");

                entity.HasOne(d => d.Kategori)
                    .WithMany(p => p.YemekTarifleris)
                    .HasForeignKey(d => d.KategoriId)
                    .HasConstraintName("FK__YemekTari__kateg__3D5E1FD2");
            });

            modelBuilder.Entity<Yorumlar>(entity =>
            {
                entity.HasKey(e => e.YorumId)
                    .HasName("PK__Yorumlar__222191BFE15CB3BF");

                entity.ToTable("Yorumlar");

                entity.Property(e => e.YorumId).HasColumnName("yorumID");

                entity.Property(e => e.Aktif).HasColumnName("aktif");

                entity.Property(e => e.Eklemetarihi)
                    .HasColumnType("datetime")
                    .HasColumnName("eklemetarihi");

                entity.Property(e => e.Silindi).HasColumnName("silindi");

               

                entity.Property(e => e.TarifId).HasColumnName("tarifID");

                entity.Property(e => e.UyeId).HasColumnName("uyeID");

                entity.Property(e => e.Yorum)
                    .HasMaxLength(500)
                    .HasColumnName("yorum");

                entity.HasOne(d => d.TarifNavigation)
                    .WithMany(p => p.Yorumlars)
                    .HasForeignKey(d => d.TarifId)
                    .HasConstraintName("FK__Yorumlar__tarifI__4222D4EF");

                entity.HasOne(d => d.Uye)
                    .WithMany(p => p.Yorumlars)
                    .HasForeignKey(d => d.UyeId)
                    .HasConstraintName("FK__Yorumlar__uyeID__4316F928");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
