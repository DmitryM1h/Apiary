
using ApiaryEngine;


ApiarySimulationEngine engine = new();

try
{
    await engine.Run();
}
catch
{
    var t = 2;
}

