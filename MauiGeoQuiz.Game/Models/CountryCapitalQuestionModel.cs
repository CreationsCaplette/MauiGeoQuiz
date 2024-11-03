namespace MauiGeoQuiz.Game.Models;
public record CountryCapitalQuestionModel(
    int QuestionIndex,
    string Question,
    IEnumerable<string> Answers,
    int AnswerIndex);
