using System;
using System.Collections.Generic;

public interface IDocumentComponent
{
    void Add(IDocumentComponent component);
    void Remove(IDocumentComponent component);
    void Display(int indent);
}

public class Paragraph : IDocumentComponent
{
    private string _text;

    public Paragraph(string text)
    {
        _text = text;
    }

    public void Add(IDocumentComponent component)
    {
        throw new NotSupportedException("нельзя добавить в параграф");
    }

    public void Remove(IDocumentComponent component)
    {
        throw new NotSupportedException("нельзя удалить из параграфа");
    }

    public void Display(int indent)
    {
        Console.WriteLine(new string(' ', indent) + _text);
    }
}

public class Section : IDocumentComponent
{
    private string _title;
    private List<IDocumentComponent> _components;

    public Section(string title)
    {
        _title = title;
        _components = new List<IDocumentComponent>();
    }

    public void Add(IDocumentComponent component)
    {
        // проверка
        if (component is Paragraph || component is Section)
        {
            _components.Add(component);
        }
        else
        {
            throw new NotSupportedException("Можно добавлять только параграфы и разделы");
        }
    }

    public void Remove(IDocumentComponent component)
    {
        _components.Remove(component);
    }

    public void Display(int indent)
    {
        Console.WriteLine(new string(' ', indent) + _title);
        foreach (var component in _components)
        {
            component.Display(indent + 2); // увеличиваем отступ для дочерних компонентов
        }
    }
}

public class Document
{
    private List<Section> _sections; // Изменяем список на конкретные секции

    public Document()
    {
        _sections = new List<Section>();
    }

    public void Add(Section section)
    {
        _sections.Add(section); // Добавляем только разделы
    }

    public void Remove(Section section)
    {
        _sections.Remove(section);
    }

    public void Display(int indent)
    {
        Console.WriteLine("Document:");
        foreach (var section in _sections)
        {
            section.Display(indent + 2); // Увеличиваем отступ для разделов
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Document document = new Document();

        Section section1 = new Section("Введение");
        section1.Add(new Paragraph("Это параграф введения"));

        Section section2 = new Section("Основной контент");
        section2.Add(new Paragraph("Первый параграф основного контента"));
        section2.Add(new Paragraph("Второй параграф основного контента"));

        Section subSection = new Section("Секция");
        subSection.Add(new Paragraph("Это параграф секции"));
        section2.Add(subSection);

        document.Add(section1);
        document.Add(section2);

        document.Display(0);
    }
}
