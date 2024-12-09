using System;

abstract class ReportGenerator
{
    // щаблоны метат
    public void GenerateReport()
    {
        CollectData();
        ProcessData();
        FormatReport();
        ExportReport();
    }

    // апстрактныи митады тля риализацыи ф паткласах
    protected abstract void CollectData();
    protected abstract void ProcessData();
    protected abstract void FormatReport();
    protected abstract void ExportReport();
}
class StudentReportGenerator : ReportGenerator
{
    protected override void CollectData()
    {
        Console.WriteLine("Собираются данные о студентах...");
    }

    protected override void ProcessData()
    {
        Console.WriteLine("Данные о студентах обрабатываются...");
    }

    protected override void FormatReport()
    {
        Console.WriteLine("Отчет по студентам форматируется...");
    }

    protected override void ExportReport()
    {
        Console.WriteLine("Отчет по студентам экспортирован.");
    }
}
class CourseReportGenerator : ReportGenerator
{
    protected override void CollectData()
    {
        Console.WriteLine("Собираются данные о курсах...");
    }

    protected override void ProcessData()
    {
        Console.WriteLine("Данные о курсах обрабатываются...");
    }

    protected override void FormatReport()
    {
        Console.WriteLine("Отчет по курсам форматируется...");
    }

    protected override void ExportReport()
    {
        Console.WriteLine("Отчет по курсам экспортирован.");
    }
}
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Генерация отчета по студентам:");
        ReportGenerator studentReport = new StudentReportGenerator();
        studentReport.GenerateReport();

        Console.WriteLine();

        Console.WriteLine("Генерация отчета по курсам:");
        ReportGenerator courseReport = new CourseReportGenerator();
        courseReport.GenerateReport();
    }
}
