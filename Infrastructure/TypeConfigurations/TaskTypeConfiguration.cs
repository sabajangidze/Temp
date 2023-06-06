using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.TypeConfigurations
{
    public class EventTypeConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Date).IsRequired();
            builder.Property(e => e.ReservedCount).IsRequired();
            builder.Property(e => e.PlaceCount).IsRequired();

            builder.HasOne(x => x.User)
                   .WithMany(x => x.Events)
                   .HasForeignKey(i => i.UserId).IsRequired();
        }
    }
}