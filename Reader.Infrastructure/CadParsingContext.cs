using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Reader.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Reader.Infrastructure
{
    public class CadParsingContext:DbContext,IUnitOfWork
    {
        private readonly IMediator _mediator;


        public DbSet<CADModel> CadModel{ get; set; }
        public DbSet<Geometry> Geometry { get; set; }
        public DbSet<Position> Position { get; set; }


        public CadParsingContext(DbContextOptions<CadParsingContext> options) : base(options) { }       

        //public CadParsingContext(DbContextOptions<CadParsingContext> options, IMediator mediator) : base(options)
        //{
        //    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        //    System.Diagnostics.Debug.WriteLine("CadParsingContext::ctor ->" + this.GetHashCode());
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            //await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

    public class CadParsingContextDesignFactory : IDesignTimeDbContextFactory<CadParsingContext>
    {
        public CadParsingContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CadParsingContext>()
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Inspection;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            return new CadParsingContext(optionsBuilder.Options);
        }
    }
}
