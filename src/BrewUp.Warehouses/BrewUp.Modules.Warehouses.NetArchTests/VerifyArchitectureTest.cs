using BrewUp.Modules.Warehouses.Domain;
using BrewUp.Modules.Warehouses.Domain.CommandHandlers;
using NetArchTest.Rules;
using Xunit;

namespace BrewUp.Modules.Warehouses.NetArchTests;

public class VerifyArchitectureTest
{
	[Fact]
	public void Should_Architecture_BeCompliant()
	{
		var types = Types.InAssembly(typeof(IWarehousesFacade).Assembly)
			.That()
			.ResideInNamespace("BrewUp.Modules.Warehouses");
		
		var result = types
			.ShouldNot()
			.HaveDependencyOn("BrewUp.Modules.Purchases")
			.GetResult()
			.IsSuccessful;

		Assert.True(result);
	}

	[Fact]
	public void ShouldNot_Domain_HavingReferenceToReadModel()
	{
		var types = Types.InAssembly(typeof(WarehousesDomainHelper).Assembly)
			.That()
			.ResideInNamespace("BrewUp.Modules.Warehouses.Domain");
		
		var result = types
			.ShouldNot()
			.HaveDependencyOn("BrewUp.Modules.Warehouses.ReadModel")
			.GetResult()
			.IsSuccessful;

		Assert.True(result);
	}

	[Fact]
	public void CommandHandler_ShouldBeSealed()
	{
		var types = Types.InAssembly(typeof(CreateBeerCommandHandler).Assembly)
			.That()
			.ResideInNamespace("BrewUp.Modules.Warehouses.Domain.CommandHandlers");

		var result = types
			.Should()
			.BeSealed()
			.GetResult()
			.IsSuccessful;

		Assert.True(result);
	}
}