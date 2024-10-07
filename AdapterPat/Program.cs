using System;
using AdapterPat;
public class Program
{
    public static void Main(string[] args)
    {
        
        ComputerGame game = new ComputerGame(
            "Ghost of Tsushima",
            PegiAgeRating.P18,
            60, 
            4096,
            75, 
            8, 
            4, 
            3.9 
        );

        
        PCGame gameAdapter = new ComputerGameAdapter(game);

        
        Console.WriteLine("Название игры: " + gameAdapter.getTitle());
        Console.WriteLine("Минимальный возраст: " + gameAdapter.getPegiAllowedAge());
        Console.WriteLine("Triple A: " + (gameAdapter.isTripleAGame() ? "Да" : "Нет"));

       
        GameRequirements requirements = gameAdapter.getRequirements();
        Console.WriteLine("Минимальная видеопамять (ГБ): " + requirements.getGpuGb());
        Console.WriteLine("Необходимое место на диске (Гб): " + requirements.getHDDGb());
        Console.WriteLine("Необходимая ОП (ГБ): " + requirements.getRAMGb());
        Console.WriteLine("Скорость процессора (ГГц): " + requirements.getCpuGhz());
        Console.WriteLine("Необходимое количество ядер: " + requirements.getCoresNum());
        
    }
}