using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models
{
    public partial class LibraryContext : DbContext
    {
        public LibraryContext()
        {
        }

        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;database=mvc_library", x => x.ServerVersion("10.4.14-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                        .HasCharSet("utf8mb4")
                        .HasCollation("utf8mb4_general_ci");

                entity.HasData(
                new Category()
                {
                    ID = -1,
                    NameToken = "Dr. Seuss"
                    Type="WHite"
                },
                new Category()
                {
                    ID = -2,
                    NameToken = "Dr. Seuss"
                    Type="WHite"
                },
                new Category()
                {
                    ID = -3,
                    NameToken = "Dr. Seuss"
                    Type="WHite"
                },
                new Category()
                {
                    ID = -4,
                    NameToken = "Dr. Seuss"
                    Type="WHite"
                }
                );
            });

            modelBuilder.Entity<Book>(entity =>
            {

                // Any "string" types should have collation defined.
                // Numeric types such as ints and dates do not.
                entity.Property(e => e.Title)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.HasIndex(e => e.ID)
                    .HasName("FK_" + nameof(Book) + "_" + nameof(Category));

                // Always in the one with the foreign key.
                entity.HasOne(child => child.Category)
                    .WithMany(parent => parent.Books)
                    .HasForeignKey(child => child.ID)
                    // When we delete a record, we can modify the behaviour of the case where there are child records.
                    // Restrict: Throw an exception if we try to orphan a child record.
                   
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_" + nameof(Book) + "_" + nameof(Author));

                /*
                All three books must have a “CheckoutDate” equal to December 25, 2019.
                One book must be returned on-time with no extension.
                One book must be returned on-time with one extension.
                One book must not have been returned at all!
                */
                entity.HasData(
                    new Book()
                    {
                        ID = -1,
                        Title = "Green Eggs and Ham",
                        Category = -1,
                         Author = "Dr Green",
                        CheckedOutDate = new DateTime(2019, 12, 25),
                        PublicationDate = new DateTime(1960, 08, 12),
                        DueDate = new DateTime(2019, 12, 25).AddDays(14),
                        ReturnedDate = new DateTime(2020, 01, 02),
                        ExtensionCount = 0
                    },
                    new Book()
                    {
                        ID = -2,
                        Title = "Green Eggs and Ham",
                        Category = -1,
                        Author = "Dr Green",
                        CheckedOutDate = new DateTime(2019, 12, 25),
                        PublicationDate = new DateTime(1957, 03, 12),
                        DueDate = new DateTime(2019, 12, 25).AddDays(14).AddDays(7),
                        ReturnedDate = new DateTime(2019, 12, 25).AddDays(14).AddDays(4),
                        ExtensionCount = 0
                    },
                    new Book()
                    {
                        ID = -3,
                        Title = "How the Grinch Stole Christmas!",
                        Category = -1,
                        Author = "Dr Green",
                        CheckedOutDate = new DateTime(2019, 12, 25),
                        PublicationDate = new DateTime(1957, 10, 12),
                        DueDate = new DateTime(2019, 12, 25).AddDays(14),
                        ReturnedDate = null,
                        ExtensionCount = 0
                    },
                    new Book()
                    {
                        ID = -4,
                        Title = "Nineteen Eighty-Four",
                        Category = -1,
                        Author = "Dr Green",
                        CheckedOutDate = new DateTime(2018, 11, 17),
                        PublicationDate = new DateTime(1949, 06, 08),
                        DueDate = new DateTime(2018, 11, 17).AddDays(14),
                        ReturnedDate = new DateTime(2018, 11, 17).AddDays(2),
                        ExtensionCount = 0
                    },
                    new Book()
                    {
                        ID = -5,
                        Title = "The Call of Cthulhu",
                        Category = -1,
                        Author = "Dr Green",
                        CheckedOutDate = new DateTime(2020, 04, 22),
                        PublicationDate = new DateTime(1928, 02, 01),
                        DueDate = new DateTime(2020, 04, 22).AddDays(14),
                        ReturnedDate = new DateTime(2020, 04, 22).AddDays(12),
                        ExtensionCount = 0
                    },
                    new Book()
                    {
                        ID = -6,
                        Title = "Animal Farm",
                        Category = -1,
                        Author = "Dr Green",
                        CheckedOutDate = new DateTime(2020, 07, 02),
                        PublicationDate = new DateTime(1945, 08, 17),
                        DueDate = new DateTime(2020, 07, 02).AddDays(14),
                        ReturnedDate = new DateTime(2020, 07, 02).AddDays(9),
                        ExtensionCount = 0
                    },
                    new Book()
                    {
                        ID = -7,
                        Title = "Hamlet",
                       Category = -1,
                        Author = "Dr Green",
                        CheckedOutDate = new DateTime(2020, 09, 23),
                        PublicationDate = new DateTime(1600, 01, 01),
                        DueDate = new DateTime(2020, 09, 23).AddDays(14),
                        ReturnedDate = null,
                        ExtensionCount = 0
                    }
                    ); ;
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
