using SQLite;

namespace MauiGeoQuiz.Game.Models;

[Table("countries")]
public class CountryLocalDto
{
    [PrimaryKey, AutoIncrement, Column("Id")]
    public int Id { get; set; }

    [Column("Name")]
    public string Name { get; set; }

    [Column("Capital")]
    public string Capital {  get; set; }
}