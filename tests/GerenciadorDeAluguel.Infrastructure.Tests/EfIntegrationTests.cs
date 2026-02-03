using GerenciadorDeAluguel.Infrastructure;
using GerenciadorDeAluguel.TestUtilities.Builders;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GerenciadorDeAluguel.Infrastructure.Tests;

public class EfIntegrationTests
{
    [Fact]
    public async Task ShouldPersistEntitiesAndValueObjects()
    {
        var conn = new SqliteConnection("Data Source=:memory:");
        conn.Open();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlite(conn).Options;

        using var ctx = new ApplicationDbContext(options);
        ctx.Database.EnsureCreated();

        var owner = TestDataBuilder.CreateValidOwner();
        await ctx.Clients.AddAsync(owner);

        var address = TestDataBuilder.CreateValidAddress();
        var property = TestDataBuilder.CreateValidProperty(owner, address, TestDataBuilder.CreateValidMoney());
        await ctx.Properties.AddAsync(property);

        var reservation = TestDataBuilder.CreateValidReservation();
        reservation = new Reservation(reservation.Tenant, property, reservation.Period, property.MonthlyRent);
        await ctx.Reservations.AddAsync(reservation);

        await ctx.SaveChangesAsync();

        var savedProperty = await ctx.Properties.FirstAsync(p => p.Id == property.Id);
        Assert.Equal(property.MonthlyRent.Value, savedProperty.MonthlyRent.Value);

        var savedClient = await ctx.Clients.FirstAsync(c => c.Id == owner.Id);
        Assert.Equal(owner.DocumentNumber.Value, savedClient.DocumentNumber.Value);

        var savedReservation = await ctx.Reservations.FirstAsync(r => r.Id == reservation.Id);
        Assert.Equal(reservation.Period.Start, savedReservation.Period.Start);

        conn.Close();
    }
}
