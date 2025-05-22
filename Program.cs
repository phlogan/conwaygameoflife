var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
var fileName = "input.life";
var filePath = Path.Combine(baseDirectory, fileName);
var aliveCellsHash = new HashSet<Cell>();

if (File.Exists(filePath))
{
    foreach (var line in File.ReadLines(filePath))
    {
        if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
            continue;

        var axis = line.Split(' ');
        if (axis.Length == 2 && long.TryParse(axis[0], out var x) && long.TryParse(axis[1], out var y))
        {
            aliveCellsHash.Add(new Cell(x, y));
        }
    }

    Console.WriteLine($"Running Game of Life for the following input:");
}
else
{
    Console.WriteLine($"File not found. Make sure that there is a \"{fileName}\" file in the following directory: {AppDomain.CurrentDomain.BaseDirectory}");
    aliveCellsHash = new HashSet<Cell>
    {
        new Cell(0, 1),
        new Cell(1, 2),
        new Cell(2, 0),
        new Cell(2, 1),
        new Cell(2, 2),
        new Cell(-2000000000000, -2000000000000),
        new Cell(-2000000000001, -2000000000001),
        new Cell(-2000000000000, -2000000000001)
    };

    Console.WriteLine($"Running Game of Life for the DEFAULT input:");
}

Console.WriteLine(string.Join("\r\n", aliveCellsHash.Select(e => e.X + "," + e.Y)));

var ticksLimit = 10;
for (int tick = 0; tick < ticksLimit; tick++)
{
    //Cell / Alive surroundings cells count
    var contenderToAliveCell = new Dictionary<Cell, long>();
    var dyingCells = new List<Cell>();
    foreach (var cell in aliveCellsHash)
    {
       var surroundings = CellsHelper.GetCellSurroundings(aliveCellsHash, cell);
        if (surroundings.Alive.Count < 2 || surroundings.Alive.Count > 3)
        {
            dyingCells.Add(cell);
        }
        foreach (var surr in surroundings.Dead)
        {
            if (!contenderToAliveCell.ContainsKey(surr))
            {
                contenderToAliveCell.Add(surr, 0);
            }

            contenderToAliveCell[surr] += 1;
        } 
    }

    foreach (var contender in contenderToAliveCell)
    {
        if (contender.Value == 3)
        {
            aliveCellsHash.Add(contender.Key);
        }
    }
    foreach (var dyingCell in dyingCells)
    {
        aliveCellsHash.Remove(dyingCell);
    }

    Console.WriteLine($"Tick {tick+1}:");
    Console.WriteLine(string.Join("\r\n", aliveCellsHash.Select(e => e.X + "," + e.Y)));
}

public record struct Cell(long X, long Y);
public record struct CellSurroundings(HashSet<Cell> Alive, HashSet<Cell> Dead);
public static class CellsHelper
{
    public static CellSurroundings GetCellSurroundings(HashSet<Cell> aliveCells, Cell cell)
    {
        var result = new CellSurroundings(new HashSet<Cell>(), new HashSet<Cell>());
        var positions = new List<Cell>
        {
            new Cell(cell.X-1, cell.Y-1),
            new Cell(cell.X-1, cell.Y),
            new Cell(cell.X-1,cell.Y+1),
            new Cell(cell.X+1, cell.Y-1),
            new Cell(cell.X+1, cell.Y),
            new Cell(cell.X+1,cell.Y+1),
            new Cell(cell.X, cell.Y-1),
            new Cell(cell.X, cell.Y+1),
        };

        foreach (var position in positions)
        {
            if (aliveCells.TryGetValue(position, out Cell outCell))
            {
                result.Alive.Add(outCell);
            }
            else
            {
                result.Dead.Add(new Cell(position.X, position.Y));
            }
        }

        return result;
    }
}