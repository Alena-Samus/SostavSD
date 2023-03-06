namespace SostavSD.Shared
{
	enum DesignStage
	{
		ArchitecturalProject,
		BuildingProject,
		OneStageConstructionProject,
		PreProjectDocumentation,
		ProjectDocumentation,
		WorkingDocumentation
	}

	enum BuildingView
	{
		NewConstruction,
		Overhaul,
		Reconstruction,
		Modernization,
		TechnicalModernization
	}

	enum BuildingZone
	{
		City,
		CityAfter2021,
		Village,
		VillageAfter2021,
		Minsk,
		MinskAfter2019,
		MinskAfter2021
	}
	enum SourceOfFaunding
	{
		OwnFunds,
		BudgetResources
	}
}
