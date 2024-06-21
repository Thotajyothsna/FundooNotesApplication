using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entities;

namespace RepositoryLayer.Context
{
    public class FundooNotesContext : DbContext
    {
        public FundooNotesContext(DbContextOptions options ):base(options)
        {
        
        }
         public DbSet<UserEntity> Users { get; set; }
         public DbSet<NotesEntity> Notes { get; set; }
         public DbSet<ProductsEntity> Products { get; set; }
         public DbSet<LabelsEntity>Labels { get; set; }
         public DbSet<CollaboratorsEntity> Collaborators { get; set; }
         public DbSet<InventoryEntity> InventoryTable { get; set; }
    }
}
