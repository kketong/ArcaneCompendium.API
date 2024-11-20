using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
var url = $"http://0.0.0.0:{port}";
var target = Environment.GetEnvironmentVariable("TARGET") ?? "World";

var app = builder.Build();

// MongoDB
var connectionString = builder.Configuration.GetConnectionString("MongoDbConnectionString");
var mongoDBSettings = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseMongoDB(mongoDBSettings.AtlasURI, mongoDBSettings.DatabaseName));


app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/characterbuilder", async (
        HttpContext httpContext, 
        [FromBody] Species species, CharacterClass characterClass, Gender gender) =>
    {
        var 
        
        var prompt = $"A {gender} {species} {characterClass} from Dungeons & Dragons. The subject should have a mysterious and dramatic appearance while maintaining appropriate fantasy RPG aesthetics.";
        
        
        
        var response = new
        {
            Success = true,
            ImageUrl = "placeholder_url", // Would be replaced with actual generated image URL
            Prompt = prompt,
            Species = species
        };

        return response;
    })
    .WithName("GenerateCharacterImage")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public enum CharacterClass
{
    None,
    Artificer,
    Barbarian,
    Bard,
    Cleric,
    Druid,
    Fighter,
    Monk,
    Paladin,
    Ranger,
    Rogue ,
    Sorcerer,
    Warlock,
    Wizard,
}

public enum Gender 
{ 
    Feminine,
    Masculine,
    NonBinary,
}

public enum Species
{
    Dragonkin,    // Dragonborn equivalent
    Dwarf,
    Elf,
    Gnome,
    HalfElf,
    Halfling,
    HalfOrc,
    Human,
    Planetouched, // Tiefling/Aasimar equivalent
    Avian,        // Aarakocra equivalent
    Giant,        // Firbolg/Goliath equivalent
    Elemental,    // Genasi equivalent
    Beastkin,     // Tabaxi/Kenku equivalent
    Reptilian,    // Tortle equivalent
    Aquatic       // Triton equivalent
}

public enum SubSpecies
{
    // Dragonkin variants
    ChromaticDragonkin,    // Evil-aligned dragons
    MetallicDragonkin,     // Good-aligned dragons
    GemDragonkin,          // Psionic dragons
    
    // Dwarf variants
    HillDwarf,
    MountainDwarf,
    DuergarDwarf,         // Deep dwarf
    
    // Elf variants
    HighElf,
    WoodElf,
    DarkElf,              // Drow
    SeaElf,
    
    // Gnome variants
    ForestGnome,
    RockGnome,
    DeepGnome,           // Svirfneblin
    
    // Halfling variants
    Lightfoot,
    Stout,
    
    // HalfElf/HalfOrc - no variants typically
    StandardHalfElf,
    StandardHalfOrc,
    
    // Human variants
    StandardHuman,
    VariantHuman,
    
    // Planetouched variants
    Tiefling,
    Aasimar,
    
    // Avian variants
    Aarakocra,
    Owlin,
    
    // Giant variants
    Firbolg,
    Goliath,
    
    // Elemental variants
    AirGenasi,
    EarthGenasi,
    FireGenasi,
    WaterGenasi,
    
    // Beastkin variants
    Tabaxi,              // Cat-like
    Kenku,               // Raven-like
    Leonin,              // Lion-like
    
    // Reptilian variants
    Tortle,
    Lizardfolk,
    KoboldReptilian,
    
    // Aquatic variants
    Triton,
    Locathah,
    SeaElven            // Variant of sea elf
}