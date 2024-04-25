using System;
using Nest;

public class YourDataModel
{
    [Text(Name = "author")]
    public string Author { get; set; }

    [Text(Name = "keywords")]
    public string Keywords { get; set; }

    [Text(Name = "publisher")]
    public string Publisher { get; set; }

    [Text(Name = "title")]
    public string Title { get; set; }  // Дополнительное поле для названия документа

    [Keyword(Index = false)]
    public string FilePath { get; set; }  // Путь к файлу, не индексируем

    [Date]
    public DateTime PublishedDate { get; set; }  // Дата публикации, если доступна
}
