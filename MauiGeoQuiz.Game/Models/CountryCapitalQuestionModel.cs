namespace MauiGeoQuiz.Game.Models;
public record CountryCapitalQuestionModel(string Question, IEnumerable<string> Answers, int AnswerIndex);
