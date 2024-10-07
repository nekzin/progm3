using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdapterPat
{
    public class ComputerGame
    {
        private string name;
        private PegiAgeRating pegiAgeRating;
        private double budgetInMillionsOfDollars;
        private int minimumGpuMemoryInMegabytes;
        private int diskSpaceNeededInGB;
        private int ramNeededInGb;
        private int coresNeeded;
        private double coreSpeedInGhz;

        public ComputerGame(string name,
                            PegiAgeRating pegiAgeRating,
                            double budgetInMillionsOfDollars,
                            int minimumGpuMemoryInMegabytes,
                            int diskSpaceNeededInGB,
                            int ramNeededInGb,
                            int coresNeeded,
                            double coreSpeedInGhz)
        {
            this.name = name;
            this.pegiAgeRating = pegiAgeRating;
            this.budgetInMillionsOfDollars = budgetInMillionsOfDollars;
            this.minimumGpuMemoryInMegabytes = minimumGpuMemoryInMegabytes;
            this.diskSpaceNeededInGB = diskSpaceNeededInGB;
            this.ramNeededInGb = ramNeededInGb;
            this.coresNeeded = coresNeeded;
            this.coreSpeedInGhz = coreSpeedInGhz;
        }

        public string getName() => name;
        public PegiAgeRating getPegiAgeRating() => pegiAgeRating;
        public double getBudgetInMillionsOfDollars() => budgetInMillionsOfDollars;
        public int getMinimumGpuMemoryInMegabytes() => minimumGpuMemoryInMegabytes;
        public int getDiskSpaceNeededInGB() => diskSpaceNeededInGB;
        public int getRamNeededInGb() => ramNeededInGb;
        public int getCoresNeeded() => coresNeeded;
        public double getCoreSpeedInGhz() => coreSpeedInGhz;
    }

    public enum PegiAgeRating
    {
        P3, P7, P12, P16, P18
    }

    public class GameRequirements
    {
        private int gpuGb;
        private int HDDGb;
        private int RAMGb;
        private double cpuGhz;
        private int coresNum;

        public GameRequirements(int gpuGb,
                                int HDDGb,
                                int RAMGb,
                                double cpuGhz,
                                int coresNum)
        {
            this.gpuGb = gpuGb;
            this.HDDGb = HDDGb;
            this.RAMGb = RAMGb;
            this.cpuGhz = cpuGhz;
            this.coresNum = coresNum;
        }

        public int getGpuGb() => gpuGb;
        public int getHDDGb() => HDDGb;
        public int getRAMGb() => RAMGb;
        public double getCpuGhz() => cpuGhz;
        public int getCoresNum() => coresNum;
    }

    public interface PCGame
    {
        string getTitle();
        int getPegiAllowedAge();
        bool isTripleAGame();
        GameRequirements getRequirements();
    }

    public class ComputerGameAdapter : PCGame
    {
        private ComputerGame computerGame;

        public ComputerGameAdapter(ComputerGame computerGame)
        {
            this.computerGame = computerGame;
        }

        public string getTitle() => computerGame.getName();

        public int getPegiAllowedAge()
        {
            switch (computerGame.getPegiAgeRating())
            {
                case PegiAgeRating.P3: return 3;
                case PegiAgeRating.P7: return 7;
                case PegiAgeRating.P12: return 12;
                case PegiAgeRating.P16: return 16;
                case PegiAgeRating.P18: return 18;
                default: return 0;
            }
        }

        public bool isTripleAGame() => computerGame.getBudgetInMillionsOfDollars() > 50;

        public GameRequirements getRequirements()
        {
            return new GameRequirements(
                computerGame.getMinimumGpuMemoryInMegabytes() / 1024,
                computerGame.getDiskSpaceNeededInGB() * 8,
                computerGame.getRamNeededInGb(),
                computerGame.getCoreSpeedInGhz(),
                computerGame.getCoresNeeded()
            );
        }
    }
}
